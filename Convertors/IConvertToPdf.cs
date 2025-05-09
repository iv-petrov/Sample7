using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample7.Enums;

namespace Sample7.Convertors
{
    public interface IConvertToPdf
    {
        public byte[] ToPdf(byte[] inputFileBytes, string inputFileName);
    }
}
