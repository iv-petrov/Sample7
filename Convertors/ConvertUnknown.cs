using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public class ConvertUnknown : IConvertToPdf
    {
        public byte[] ToPdf(byte[] inputFileBytes, string inputFileName)
        {
            //специфика конвертации неизвестного формата в pdf
            Console.WriteLine("{0}", inputFileBytes);
            throw new Exception($"Ошибка конвертации документа ({inputFileName}) при формировании печатной формы документа в PDF.");
        }
    }
}
