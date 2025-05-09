using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public class ConvertTxt : IConvertToPdf
    {
        public byte[] ToPdf(byte[] inputFileBytes, string inpuFileName)
        {
            //специфика конвертации txt в pdf
            Console.WriteLine(inputFileBytes);
            throw new NotImplementedException();
        }
    }
}
