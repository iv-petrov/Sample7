using System.IO.Compression;
using System.Text;

namespace Sample7 { 
    public enum PrintServiceSupportedFileType
    {
        Unknown = 0,
        ImageFile,
        TextFile,
        CalcFile,
        DocumFile,
        HtmlFile,
        PdfFile

    }
    public enum ImageFormat
    {
        Jpeg,
        Png,
        Tiff,
        Bmp
    }

    public static class PrintService
    {
        /// <summary>
        /// Конвертация входного файла в соответствии с его именем
        /// </summary>
        /// <param name="inputFileBytes">Содержимое исходного файла</param>
        /// <param name="inputFileName">Имя файла</param>
        /// <returns>Сконвертироывнное содержимое</returns>
        static byte[] ConvertInputFileToPdf(byte[] inputFileBytes, string inputFileName)
        {
            byte[] pdfFileBytes;
            PrintServiceSupportedFileType inputFileType = DetectFileType(inputFileName);

            switch (inputFileType)
            {
                case PrintServiceSupportedFileType.PdfFile:
                    // PDF конвертировать не нужно - его напрямую в обработку отдаем
                    pdfFileBytes = inputFileBytes;
                    break;
                case PrintServiceSupportedFileType.ImageFile:
                    pdfFileBytes = ConvertSupportedImageToPdf(inputFileBytes, inputFileName);
                    break;
                case PrintServiceSupportedFileType.TextFile:
                    pdfFileBytes = ConvertTxtToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.CalcFile:
                    pdfFileBytes = ConvertXlsxToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.DocumFile:
                    pdfFileBytes = ConvertDocxToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.HtmlFile:
                    pdfFileBytes = ConvertHtmlToPdf(inputFileBytes);
                    break;
                case PrintServiceSupportedFileType.Unknown:
                default:
                    pdfFileBytes = ConvertUnknownToPdf(inputFileBytes);
                    break;
            }

            return pdfFileBytes;
        }

        static byte[] ConvertXlsxToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации xlsx в pdf
            Console.WriteLine(inputFileBytes);
            throw new NotImplementedException();
        }

        static byte[] ConvertDocxToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации docx в pdf
            Console.WriteLine(inputFileBytes);
            throw new NotImplementedException();
        }

        static byte[] ConvertUnknownToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации неизвестного формата в pdf
            Console.WriteLine("{0}", inputFileBytes);
            throw new NotImplementedException();
        }

        static byte[] ConvertTxtToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации txt в pdf
            Console.WriteLine(inputFileBytes);
            throw new NotImplementedException();
        }

        static byte[] ConvertHtmlToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации html в pdf
            Console.WriteLine(inputFileBytes);
            throw new NotImplementedException();
        }
        /// <summary>
        /// Тип преобразования для файлов изображений
        /// </summary>
        /// <param name="inputFileName">Имя файла изображения</param>
        /// <returns>Тип преобразования</returns>
        static ImageFormat? DetectImageFormat(string inputFileName)
        {
            string inputFileExtention = GetExtensionFromFileName(inputFileName);

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

        static byte[] ConvertSupportedImageToPdf(byte[] inputFileBytes, string inputFileName)
        {
            // Здесь придется переделать на другой enum
            ImageFormat? supportedImageFileType = DetectImageFormat(inputFileName);
            //специфика конвертации изображения в pdf
            Console.WriteLine("{0}{1}", inputFileBytes, supportedImageFileType);
            throw new NotImplementedException();
        }
        /// <summary>
        /// Распознавание типа преобразования файла по его расширению
        /// </summary>
        /// <param name="inputFileName">Имя файла с расширением</param>
        /// <returns>Тип допустимой конвертации</returns>
        static PrintServiceSupportedFileType DetectFileType(string inputFileName)
        {
            string inputFileExtention = GetExtensionFromFileName(inputFileName);

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
            else if (inputFileExtention == "rtf")
                return PrintServiceSupportedFileType.DocumFile;
            else if (inputFileExtention == "html")
                return PrintServiceSupportedFileType.HtmlFile;
            else if (inputFileExtention == "htm")
                return PrintServiceSupportedFileType.HtmlFile;
            return PrintServiceSupportedFileType.Unknown;
        }
        /// <summary>
        /// Выделение расширения файла из его имени
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Расширение в нижнем регистре без начальной точки</returns>
        static string GetExtensionFromFileName(string fileName)
        {
            return Path.GetExtension(fileName).TrimStart('.').ToLower().Trim();
        }
        /// <summary>
        /// Добавление водяных знаков в файл pdf
        /// </summary>
        /// <param name="pdfBytes">Содержимое исодного файла</param>
        /// <param name="additionalText">Текст водяного знака</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Пока не реализовано</exception>
        static byte[] AddWatermarkToPdf(byte[] pdfBytes, string additionalText)
        {
            //модификация уже сконвертиованного PDF, на каждую страницу в колонтитул добавляется информация об электронных подписях документа
            Console.WriteLine("{0}{1}", pdfBytes, additionalText);
            throw new NotImplementedException();
        }
        /// <summary>
        /// Основная процедура преобразования
        /// </summary>
        /// <param name="inputFileBytes">Содержимое исходного файла или zip-архива</param>
        /// <param name="fileName">Наименование файла или архива</param>
        /// <param name="documentIndefNumber">Регистрационный номер документа</param>
        /// <param name="documentRegisteredDateTime">Дата регистрации документа</param>
        /// <param name="throwErrorOnUnkwonType">Выбрасывать исключение при ошибке</param>
        /// <param name="certificate">Параметры организации для штампа</param>
        /// <param name="proxyData">Параметры доверенности для штампа</param>
        /// <returns>Преобразованный файл или архив преоразованных файлов</returns>
        /// <exception cref="Exception"></exception>
        public static byte[] ProcessInputFile(
            byte[] inputFileBytes,
            string fileName,
            string documentIndefNumber,
            DateTime? documentRegisteredDateTime,
            bool throwErrorOnUnkwonType,
            Dictionary<string, string> certificate = null,
            Dictionary<string, string> proxyData = null)
        {
            // Определяем тип исходного файла
            string fileExtention = GetExtensionFromFileName(fileName);

            // Для архивов отдельный обработчик
            if (fileExtention == "zip")
                return ProcessInputZipFile(inputFileBytes, fileName, documentIndefNumber, documentRegisteredDateTime, throwErrorOnUnkwonType, certificate, proxyData);

            // Определяем тип файла
            var detectedFileType = DetectFileType(fileExtention);

            // тип не определился
            if (detectedFileType == PrintServiceSupportedFileType.Unknown && throwErrorOnUnkwonType)
               throw new Exception($"Ошибка конвертации документа ({fileName}) при формировании печатной формы документа в PDF.");

            // Тип определился - превращаем его в PDF
            byte[] pdf = ConvertInputFileToPdf(inputFileBytes, fileName);

            // Что-то пошло не так
            if (pdf == null)
                throw new Exception($"Ошибка при формировании печатной формы документа.");

            // Добавляем штамп и на выход
            return AddStampToPdf(pdf, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
        }
        /// <summary>
        /// Добавление штампа в документ
        /// </summary>
        /// <param name="pdf">Содержимое документа</param>
        /// <param name="documentIndefNumber">Регистрационный номер</param>
        /// <param name="documentRegisteredDateTime">Дата регистрации документа</param>
        /// <param name="certificate">Параметры организации для штампа</param>
        /// <param name="proxyData">Параметры доверенности для штампа</param>
        /// <returns></returns>
        public static byte[] AddStampToPdf(byte[] pdf, string documentIndefNumber, DateTime? documentRegisteredDateTime, Dictionary<string, string> certificate = null, Dictionary<string, string> proxyData = null)
        {
            var documentRegisteredDateTimeString = documentRegisteredDateTime.HasValue
                ? TimeZoneInfo.ConvertTime(documentRegisteredDateTime.Value, TimeZoneInfo.Utc, TimeZoneInfo.Local)
                    .ToString("dd.MM.yyyy HH:mm:ss")
                : "-";

            string organization = null;
            string surName = null;
            string givenName = null;
            string ogrn = null;
            string startDataSertificate = null;
            string endDataSertificate = null;
            string certificateSerialNumber = null;

            certificate?.TryGetValue("Organization", out organization);
            certificate?.TryGetValue("SurName", out surName);
            certificate?.TryGetValue("GivenName", out givenName);
            certificate?.TryGetValue("OGRN", out ogrn);
            certificate?.TryGetValue("NotBefore", out startDataSertificate);
            certificate?.TryGetValue("NotAfter", out endDataSertificate);
            certificate?.TryGetValue("CertificateSerialNumber", out certificateSerialNumber);

            string proxyNum;
            string proxyStartDate;
            string proxyEndDate;
            var proxyLine = "";
            if (proxyData != null)
            {
                proxyData.TryGetValue("Organization", out organization);
                proxyData.TryGetValue("OGRN", out ogrn);
                proxyData.TryGetValue("ProxyNum", out proxyNum);
                proxyData.TryGetValue("StartDate", out proxyStartDate);
                proxyData.TryGetValue("EndDate", out proxyEndDate);
                proxyLine = $"Доверенность №{proxyNum}. Действительна c {proxyStartDate} по {proxyEndDate}.";
            }

            var separateLine = new string('_', 141);
            var watermarkText = new StringBuilder();
            watermarkText.AppendLine($"{separateLine}");
            watermarkText.AppendLine("");
            watermarkText.AppendLine($"Документ {documentIndefNumber} от {documentRegisteredDateTimeString} зарегистрирован. Документ подписан электронной подписью:   ");
            watermarkText.AppendLine("");

            if (string.IsNullOrWhiteSpace(organization) == false)
            {
                watermarkText.AppendLine($"{organization} ОГРН {ogrn}");
            }

            watermarkText.AppendLine($"{surName} {givenName}. {proxyLine}");
            watermarkText.AppendLine($"Серийный номер сертификата {certificateSerialNumber}. Действителен c {startDataSertificate} по {endDataSertificate} ");

            // Ставим штамп с номером документа
            byte[] watermarkedPdf = AddWatermarkToPdf(pdf, watermarkText.ToString());

            return watermarkedPdf;
        }
        /// <summary>
        /// Обработка zip-архивов
        /// </summary>
        /// <param name="inputFileBytes">Содержимое архива</param>
        /// <param name="fileName">Наименование файла архива</param>
        /// <param name="documentIndefNumber">Регистрационный номер документа</param>
        /// <param name="documentRegisteredDateTime">Дата регистрации</param>
        /// <param name="throwErrorOnUnkwonType">Выбьасывать исключение при ощибке</param>
        /// <param name="certificate">Параметры организации для штампа</param>
        /// <param name="proxyData">Параметры доверенности для штампа</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static byte[] ProcessInputZipFile(
            byte[] inputFileBytes,
            string fileName,
            string documentIndefNumber,
            DateTime? documentRegisteredDateTime,
            bool throwErrorOnUnkwonType,
            Dictionary<string, string> certificate = null,
            Dictionary<string, string> proxyData = null)
        {
            // Определяем тип исходного файла
            string fileExtention = GetExtensionFromFileName(fileName);

            byte[] outZip = null;
            if (fileExtention != "zip")
                return outZip;

            var entries = new Dictionary<string, byte[]>();

            using (var zipStream = new MemoryStream(inputFileBytes))
            using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read, false, Encoding.GetEncoding("cp866")))
            {
                foreach (var zipArchiveEntry in zipArchive.Entries)
                {
                    using var memoryStream = new MemoryStream();
                    using var entryStream = zipArchiveEntry.Open();
                    {
                        fileExtention = GetExtensionFromFileName(zipArchiveEntry.FullName);
                        if (DetectFileType(fileExtention) != PrintServiceSupportedFileType.Unknown)
                        {
                            entryStream.CopyTo(memoryStream);
                            // В список добавляем только конвертируемые файлы
                            entries.Add(zipArchiveEntry.FullName, memoryStream.ToArray());
                        }
                    }
                }
            }

            if (entries.Count == 0 && throwErrorOnUnkwonType)
            { 
                throw new Exception($"Ошибка конвертации архива ({fileName}) допустимые файлы не обнаружены.");
            }
            else if (entries.Count == 1)
            {
                // странный конечно алгоритм - если в архиве один пригодный файл, то возвращаем его в несхатом виде
                var doc = entries.FirstOrDefault();
                outZip = ConvertInputFileToPdf(doc.Value, doc.Key);
                if (outZip == null && throwErrorOnUnkwonType)
                    throw new Exception($"Ошибка при формировании печатной формы документа ({doc.Key}).");

                return AddStampToPdf(outZip, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
            }
            else if (entries.Count > 1)
            {
                // а если в архиве несколько пригодных файлов, то конвертируем и пересхимаем их в новый архив
                outZip = AddPdfFilesToZip(entries, documentIndefNumber, documentRegisteredDateTime, certificate);
            }

            return outZip;
        }
        /// <summary>
        /// Конвертация файлов и добавление их в новый архив
        /// Все файлы должны быть конвертируемого типа,
        /// потому что повторная проверка не производится
        /// </summary>
        /// <param name="entries">Массив файлов из исходного архива</param>
        /// <param name="documentIndefNumber">Регистрационный номер</param>
        /// <param name="documentRegisteredDateTime">Дата регистрации</param>
        /// <param name="certificate">Параметры организации для штампа</param>
        /// <returns></returns>
        private static byte[] AddPdfFilesToZip(Dictionary<string, byte[]> entries, string documentIndefNumber, DateTime? documentRegisteredDateTime, Dictionary<string, string> certificate)
        {
            using var zipResponse = new MemoryStream();
            using (var zipFileResponse = new ZipArchive(zipResponse, ZipArchiveMode.Create))
            {
                foreach (var item in entries)
                {
                    // Превращаем его в PDF, повторная проверка не производится
                    var pdf = ConvertInputFileToPdf(item.Value, item.Key);
                    pdf = AddStampToPdf(pdf, documentIndefNumber, documentRegisteredDateTime, certificate);
                    var fileName = item.Key.Remove(item.Key.LastIndexOf('.'));
                    var file = zipFileResponse.CreateEntry(fileName + "_ПечатнаяФорма.pdf");
                    using (var stream = file.Open())
                    using (var fileMemoryStream = new MemoryStream(pdf))
                    fileMemoryStream.WriteTo(stream);
                }
            }

            return zipResponse.ToArray();
        }
    }
}
