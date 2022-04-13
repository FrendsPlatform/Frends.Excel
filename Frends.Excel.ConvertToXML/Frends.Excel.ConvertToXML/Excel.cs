﻿using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml;
using ExcelDataReader;
using Frends.Excel.ConvertToXML.Definitions;

namespace Frends.Excel.ConvertToXML;

/// <summary>
/// Excel tasks.
/// </summary>
public static class Excel
{
    /// <summary>
    /// Converts Excel file to XML. [Documentation](https://github.com/FrendsPlatform/Frends.Excel/tree/main/Frends.Excel.ConvertToXML)
    /// </summary>
    /// <param name="input">Input configuration</param>
    /// <param name="options">Input options</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result containing the converted XML string.</returns>
    /// <exception cref="Exception"></exception>
    public static Result ConvertToXML(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = new FileStream(input.Path, FileMode.Open);
            using var excelReader = ExcelReaderFactory.CreateReader(stream);
            var result = excelReader.AsDataSet();
            var xml = ConvertDataSetToXml(result, options, Path.GetFileName(input.Path), cancellationToken);
            return new Result(true, xml, null);
        }
        catch (Exception ex)
        {
            if (options.ThrowErrorOnFailure)
            {
                throw new InvalidOperationException("Error while converting Excel file to XML", ex);
            }

            return new Result(false, null, $"Error while converting Excel file to XML: {ex}");
        }
    }

    private static string ConvertDataSetToXml(DataSet result, Options options, string fileName,
        CancellationToken cancellationToken)
    {
        var settings = new XmlWriterSettings { OmitXmlDeclaration = true };

        var builder = new StringBuilder();
        using var sw = new StringWriter(builder);
        using var xw = XmlWriter.Create(sw, settings);

        // Write workbook element. Workbook is also known as sheet.
        xw.WriteStartDocument();
        xw.WriteStartElement("workbook");
        xw.WriteAttributeString("workbook_name", fileName);

        foreach (DataTable table in result.Tables)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // Read only wanted worksheets. If none is specified read all.
            if (options.ShouldReadWorkSheet(table.TableName))
            {
                // Write worksheet element.
                xw.WriteStartElement("worksheet");
                xw.WriteAttributeString("worksheet_name", table.TableName);

                for (var i = 0; i < table.Rows.Count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var rowElementIsWritten = false;
                    for (var j = 0; j < table.Columns.Count; j++)
                    {
                        // Write column only if it has some content.
                        var content = table.Rows[i].ItemArray[j];
                        if (content != null && !string.IsNullOrWhiteSpace(content.ToString()))
                        {

                            if (!rowElementIsWritten)
                            {
                                xw.WriteStartElement("row");
                                xw.WriteAttributeString("row_header", (i + 1).ToString());
                                rowElementIsWritten = true;
                            }

                            xw.WriteStartElement("column");
                            if (options.UseNumbersAsColumnHeaders)
                            {
                                xw.WriteAttributeString("column_header", (j + 1).ToString());
                            }
                            else
                            {
                                xw.WriteAttributeString("column_header", ColumnIndexToColumnLetter(j + 1));
                            }

                            if (content.GetType().Name == "DateTime")
                            {
                                content = ConvertDateTimes((DateTime)content, options);
                            }

                            xw.WriteString(content.ToString());
                            xw.WriteEndElement();
                        }
                    }

                    if (rowElementIsWritten)
                    {
                        xw.WriteEndElement();
                    }
                }

                xw.WriteEndElement();
            }
        }

        xw.WriteEndDocument();
        xw.Close();
        return builder.ToString();
    }

    private static string ColumnIndexToColumnLetter(int colIndex)
    {
        var div = colIndex;
        var colLetter = string.Empty;
        int mod;
        while (div > 0)
        {
            mod = (div - 1) % 26;
            colLetter = (char)(65 + mod) + colLetter;
            div = ((div - mod) / 26);
        }
        return colLetter;
    }

    private static string ConvertDateTimes(DateTime date, Options options)
    {
        // Modify the date using date format var in options.

        if (options.ShortDatePattern)
        {
            return options.DateFormat switch
            {
                DateFormats.DDMMYYYY => date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                DateFormats.MMDDYYYY => date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                DateFormats.YYYYMMDD => date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                _ => date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern),
            };
        }
        else
        {
            return options.DateFormat switch
            {
                DateFormats.DDMMYYYY => date.ToString("dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture),
                DateFormats.MMDDYYYY => date.ToString("MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                DateFormats.YYYYMMDD => date.ToString("yyyy/MM/dd H:mm:ss", CultureInfo.InvariantCulture),
                _ => date.ToString(CultureInfo.CurrentCulture.DateTimeFormat),
            };
        }
    }
}
