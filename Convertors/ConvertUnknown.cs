using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public static partial class ConvertTo
    {
        public static byte[] ConvertUnknownToPdf(byte[] inputFileBytes)
        {
            //специфика конвертации неизвестного формата в pdf
            Console.WriteLine("{0}", inputFileBytes);
            throw new NotImplementedException();
        }
    }
}
