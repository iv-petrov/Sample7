using Sample7.Convertors;
using Sample7.Detectors;
using Sample7.Enums;
using Sample7.PdfAdditions;
using System.IO.Compression;
using System.Text;

namespace Sample7
{
    public static partial class PrintService
    {
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
        public static byte[] ProcessInputZipFile(
            byte[] inputFileBytes,
            string fileName,
            string documentIndefNumber,
            DateTime? documentRegisteredDateTime,
            bool throwErrorOnUnkwonType,
            Dictionary<string, string> certificate = null,
            Dictionary<string, string> proxyData = null)
        {
            // Определяем тип исходного файла
            string fileExtention = ConvertTo.GetExtensionFromFileName(fileName);

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
                        fileExtention = ConvertTo.GetExtensionFromFileName(zipArchiveEntry.FullName);
                        if (DetectTo.DetectFileType(fileExtention) != PrintServiceSupportedFileType.Unknown)
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
                outZip = ConvertTo.ConvertInputFileToPdf(doc.Value, doc.Key);
                if (outZip == null && throwErrorOnUnkwonType)
                    throw new Exception($"Ошибка при формировании печатной формы документа ({doc.Key}).");

                return AddToPdf.AddStamp(outZip, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
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
                    var pdf = ConvertTo.ConvertInputFileToPdf(item.Value, item.Key);
                    pdf = AddToPdf.AddStamp(pdf, documentIndefNumber, documentRegisteredDateTime, certificate);
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
