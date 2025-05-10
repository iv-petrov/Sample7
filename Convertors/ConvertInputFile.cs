using Sample7.Detectors;
using Sample7.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public static class ConvertInputFile
    {
        /// <summary>
        /// Конвертация входного файла в соответствии с его именем
        /// </summary>
        /// <param name="inputFileBytes">Содержимое исходного файла</param>
        /// <param name="inputFileName">Имя файла</param>
        /// <returns>Сконвертироывнное содержимое</returns>
        public static IConvertToPdf Converter(byte[] inputFileBytes, string inputFileName)
        {
            IConvertToPdf pdfConverter;
            switch (Detector.DetectFileType(inputFileName))
            {
                case SupportedFileType.PdfFile:
                    // PDF конвертировать не нужно - его напрямую в обработку отдаем
                    pdfConverter = new ConvertPdf();
                    break;
                case SupportedFileType.ImageFile:
                    pdfConverter = new ConvertImage();
                    break;
                case SupportedFileType.TextFile:
                    pdfConverter = new ConvertTxt();
                    break;
                case SupportedFileType.CalcFile:
                    pdfConverter = new ConvertXlsx();
                    break;
                case SupportedFileType.DocumFile:
                    pdfConverter = new ConvertDocx();
                    break;
                case SupportedFileType.HtmlFile:
                    pdfConverter = new ConvertDocx();
                    break;
                case SupportedFileType.Unknown:
                default:
                    pdfConverter = new ConvertUnknown();
                    break;
            }
            return pdfConverter;
        }
    }
}
