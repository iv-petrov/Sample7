using Sample7.Detectors;
using Sample7.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public static partial class ConvertTo
    {
        /// <summary>
        /// Конвертация входного файла в соответствии с его именем
        /// </summary>
        /// <param name="inputFileBytes">Содержимое исходного файла</param>
        /// <param name="inputFileName">Имя файла</param>
        /// <returns>Сконвертироывнное содержимое</returns>
        public static byte[] ConvertInputFileToPdf(byte[] inputFileBytes, string inputFileName)
        {
            byte[] pdfFileBytes;
            switch (DetectTo.DetectFileType(inputFileName))
            {
                case PrintServiceSupportedFileType.PdfFile:
                    // PDF конвертировать не нужно - его напрямую в обработку отдаем
                    pdfFileBytes = inputFileBytes;
                    break;
                case PrintServiceSupportedFileType.ImageFile:
                    pdfFileBytes = ConvertTo.ConvertSupportedImageToPdf(inputFileBytes, DetectTo.DetectImageFormat(inputFileName));
                    break;
                case PrintServiceSupportedFileType.TextFile:
                    pdfFileBytes = ConvertTo.ConvertTxtToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.CalcFile:
                    pdfFileBytes = ConvertTo.ConvertXlsxToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.DocumFile:
                    pdfFileBytes = ConvertTo.ConvertDocxToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.HtmlFile:
                    pdfFileBytes = ConvertTo.ConvertHtmlToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.Unknown:
                default:
                    pdfFileBytes = ConvertTo.ConvertUnknownToPdf(inputFileBytes);
                    break;
            }
            return pdfFileBytes;
        }
        /// <summary>
        /// Выделение расширения файла из его имени
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Расширение в нижнем регистре без начальной точки</returns>
        public static string GetExtensionFromFileName(string fileName)
        {
            return Path.GetExtension(fileName.Trim()).Substring(1).ToLower();
        }
    }
}
