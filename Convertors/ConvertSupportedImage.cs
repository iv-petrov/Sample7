using Sample7.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.Convertors
{
    public static partial class ConvertTo
    {
        public static byte[] ConvertSupportedImageToPdf(byte[] inputFileBytes, ImageFormat? inputImageFormat)
        {
            //специфика конвертации изображения в pdf
            Console.WriteLine("{0}{1}", inputFileBytes, inputImageFormat);
            throw new NotImplementedException();
        }
    }
}
