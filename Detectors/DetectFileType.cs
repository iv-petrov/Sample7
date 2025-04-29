using Sample7.Enums;

namespace Sample7.Detectors
{
    public static partial class DetectTo
    {
        /// <summary>
        /// Распознавание типа преобразования файла по его расширению
        /// </summary>
        /// <param name="inputFileName">Имя файла с расширением</param>
        /// <returns>Тип допустимой конвертации</returns>
        public static PrintServiceSupportedFileType DetectFileType(string inputFileName)
        {
            string inputFileExtention = Path.GetExtension(inputFileName.Trim()).Substring(1).ToLower();

            if (inputFileExtention == "pdf")
                return PrintServiceSupportedFileType.PdfFile;
            else if (inputFileExtention == "jpg")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "jpeg")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "png")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "bmp")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "tiff")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "tif")
                return PrintServiceSupportedFileType.ImageFile;
            else if (inputFileExtention == "xlsx")
                return PrintServiceSupportedFileType.CalcFile;
            else if (inputFileExtention == "xls")
                return PrintServiceSupportedFileType.CalcFile;
            else if (inputFileExtention == "xlsm")
                return PrintServiceSupportedFileType.CalcFile;
            else if (inputFileExtention == "docx")
                return PrintServiceSupportedFileType.DocumFile;
            else if (inputFileExtention == "doc")
                return PrintServiceSupportedFileType.DocumFile;
            else if (inputFileExtention == "txt")
                return PrintServiceSupportedFileType.TextFile;
            else if (inputFileExtention == "sql") // добавим sql-файл
                return PrintServiceSupportedFileType.TextFile;
            else if (inputFileExtention == "rtf")
                return PrintServiceSupportedFileType.DocumFile;
            else if (inputFileExtention == "html")
                return PrintServiceSupportedFileType.HtmlFile;
            else if (inputFileExtention == "htm")
                return PrintServiceSupportedFileType.HtmlFile;
            return PrintServiceSupportedFileType.Unknown;
        }
    }
}
