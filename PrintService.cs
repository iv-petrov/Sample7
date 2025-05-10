using Sample7.Convertors;

namespace Sample7 { 

    public static class PrintService
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
            string fileExtention = Path.GetExtension(fileName.Trim()).Substring(1).ToLower();

            // Для архивов отдельный обработчик
            if (fileExtention == "zip")
                return Processing.ZipFile(inputFileBytes, fileName, documentIndefNumber, documentRegisteredDateTime, throwErrorOnUnkwonType, certificate, proxyData);

            return Processing.SimpleFile(inputFileBytes, fileName, documentIndefNumber, documentRegisteredDateTime, throwErrorOnUnkwonType, certificate, proxyData);
        }
    }
}
