using Sample7.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample7.Detectors;

namespace Sample7.Convertors
{
    public class ConvertImage : IConvertToPdf
    {
        public byte[] ToPdf(byte[] inputFileBytes, string inputFileName)
        {
            ImageFormat? format = Detector.DetectImageFormat(inputFileName);
            //специфика конвертации изображения в pdf
            Console.WriteLine("{0}{1}", inputFileBytes, format);
            throw new NotImplementedException();
        }
    }
}
