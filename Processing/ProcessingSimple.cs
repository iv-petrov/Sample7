using Sample7.Convertors;
using Sample7.Detectors;
using Sample7.Enums;
using Sample7.PdfAdditions;

namespace Sample7.Processing
{
        public static partial class Processing
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
            public static byte[] ProcessSimpleFile(
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

            // Определяем тип файла
            var detectedFileType = DetectTo.DetectFileType(fileExtention);

                // тип не определился
                if (detectedFileType == PrintServiceSupportedFileType.Unknown && throwErrorOnUnkwonType)
                    throw new Exception($"Ошибка конвертации документа ({fileName}) при формировании печатной формы документа в PDF.");

                // Тип определился - превращаем его в PDF
                byte[] pdf = ConvertTo.ConvertInputFileToPdf(inputFileBytes, fileName);

                // Что-то пошло не так
                if (pdf == null)
                    throw new Exception($"Ошибка при формировании печатной формы документа.");

                // Добавляем штамп и на выход
                return AddToPdf.AddStamp(pdf, documentIndefNumber, documentRegisteredDateTime, certificate, proxyData);
            }
        }
    }

