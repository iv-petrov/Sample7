using Sample7.Enums;

namespace Sample7.Detectors
{
    public static partial class DetectTo
    {
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
