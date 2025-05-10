using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public class ConvertPdf : IConvertToPdf
    {
        public byte[] ToPdf(byte[] inputFileBytes, string inpuFileName)
        {
            return inputFileBytes;
        }
    }
}
