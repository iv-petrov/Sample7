using Sample7.Convertors;
using Sample7.Detectors;
using Sample7.Enums;
using Sample7.PdfAdditions;
using System.IO.Compression;
using System.Text;

namespace Sample7
{
    public static class Processing
    {
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
        public static byte[] SimpleFile(
            byte[] inputFileBytes,
            string fileName,
            string documentIndefNumber,
            DateTime? documentRegisteredDateTime,
            bool throwErrorOnUnkwonType,
            Dictionary<string, string> certificate = null,
            Dictionary<string, string> proxyData = null)
        {
            IConvertToPdf converter = ConvertInputFile.Converter(inputFileBytes, fileName);
            byte[] pdf = converter.ToPdf(inputFileBytes, fileName);

            // Что-то пошло не так
            if (pdf == null)
                throw new Exception($"Ошибка при формировании печатной формы документа.");

            // Добавляем штамп и на выход
            return AddToPdf.AddStamp(pdf, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
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
        public static byte[] ZipFile(
            byte[] inputFileBytes,
            string fileName,
            string documentIndefNumber,
            DateTime? documentRegisteredDateTime,
            bool throwErrorOnUnkwonType,
            Dictionary<string, string> certificate = null,
            Dictionary<string, string> proxyData = null)
        {
            // Определяем тип исходного файла
            string fileExtention = Path.GetExtension(fileName.Trim()).Substring(1).ToLower();

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
                        fileExtention = Path.GetExtension(fileName.Trim()).Substring(1).ToLower();
                        if (Detector.DetectFileType(fileExtention) != SupportedFileType.Unknown)
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
                return SimpleFile(inputFileBytes, fileName, documentIndefNumber, documentRegisteredDateTime, throwErrorOnUnkwonType, certificate, proxyData);
            }
            else if (entries.Count > 1)
            {
                // а если в архиве несколько пригодных файлов, то конвертируем и пересхимаем их в новый архив
                outZip = AddPdfFilesToZip(entries, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
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
        /// <returns>Новый архив из преобрахованных файлов</returns>
        private static byte[] AddPdfFilesToZip(Dictionary<string, byte[]> entries, string documentIndefNumber, DateTime? documentRegisteredDateTime, Dictionary<string, string> certificate, Dictionary<string, string> proxyData)
        {
            bool throwErrorOnUnkwonType = false;

            using var zipResponse = new MemoryStream();
            using (var zipFileResponse = new ZipArchive(zipResponse, ZipArchiveMode.Create))
            {
                foreach (var item in entries)
                {
                    // Превращаем его в PDF, повторная проверка не производится, потому что в списке только преобразуемые файлы
                    var pdf = SimpleFile(item.Value, item.Key, documentIndefNumber, documentRegisteredDateTime, throwErrorOnUnkwonType, certificate, proxyData);

                    var fileName = Path.GetFileNameWithoutExtension(item.Key);
                    var file = zipFileResponse.CreateEntry(fileName + "_ПечатнаяФорма.pdf");

                    using var stream = file.Open();
                    using var fileMemoryStream = new MemoryStream(pdf);
                    fileMemoryStream.WriteTo(stream);
                }
            }

            return zipResponse.ToArray();
        }
    }
}

