using Sample7.Enums;

namespace Sample7.Detectors
{
    public static class Detector
    {
        /// <summary>
        /// Распознавание типа преобразования файла по его расширению
        /// </summary>
        /// <param name="inputFileName">Имя файла с расширением</param>
        /// <returns>Тип допустимой конвертации</returns>
        public static SupportedFileType DetectFileType(string inputFileName)
        {
            string inputFileExtention = Path.GetExtension(inputFileName.Trim()).Substring(1).ToLower();

            if (inputFileExtention == "pdf")
                return SupportedFileType.PdfFile;
            else if (inputFileExtention == "jpg")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "jpeg")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "png")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "bmp")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "tiff")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "tif")
                return SupportedFileType.ImageFile;
            else if (inputFileExtention == "xlsx")
                return SupportedFileType.CalcFile;
            else if (inputFileExtention == "xls")
                return SupportedFileType.CalcFile;
            else if (inputFileExtention == "xlsm")
                return SupportedFileType.CalcFile;
            else if (inputFileExtention == "docx")
                return SupportedFileType.DocumFile;
            else if (inputFileExtention == "doc")
                return SupportedFileType.DocumFile;
            else if (inputFileExtention == "txt")
                return SupportedFileType.TextFile;
            else if (inputFileExtention == "sql") // добавим sql-файл
                return SupportedFileType.TextFile;
            else if (inputFileExtention == "rtf")
                return SupportedFileType.DocumFile;
            else if (inputFileExtention == "html")
                return SupportedFileType.HtmlFile;
            else if (inputFileExtention == "htm")
                return SupportedFileType.HtmlFile;
            return SupportedFileType.Unknown;
        }
        /// <summary>
        /// Тип преобразования для файлов изображений
        /// </summary>
        /// <param name="inputFileName">Имя файла изображения</param>
        /// <returns>Тип преобразования</returns>
        public static ImageFormat? DetectImageFormat(string inputFileName)
        {
            string inputFileExtention = Path.GetExtension(inputFileName.Trim()).Substring(1).ToLower();

            if (inputFileExtention == "jpg")
                return ImageFormat.Jpeg;
            else if (inputFileExtention == "jpeg")
                return ImageFormat.Jpeg;
            else if (inputFileExtention == "png")
                return ImageFormat.Png;
            else if (inputFileExtention == "tif")
                return ImageFormat.Tiff;
            else if (inputFileExtention == "tiff")
                return ImageFormat.Tiff;
            else if (inputFileExtention == "bmp")
                return ImageFormat.Bmp;
            return null;
        }

    }
}
