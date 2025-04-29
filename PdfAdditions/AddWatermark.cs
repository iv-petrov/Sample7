using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.PdfAdditions
{
    public static partial class AddToPdf
    {
        /// <summary>
        /// Добавление водяных знаков в файл pdf
        /// </summary>
        /// <param name="pdfBytes">Содержимое исодного файла</param>
        /// <param name="additionalText">Текст водяного знака</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Пока не реализовано</exception>
        public static byte[] AddWatermark(byte[] pdfBytes, string additionalText)
        {
            //модификация уже сконвертиованного PDF, на каждую страницу в колонтитул добавляется информация об электронных подписях документа
            Console.WriteLine("{0}{1}", pdfBytes, additionalText);
            throw new NotImplementedException();
        }
    }
}
