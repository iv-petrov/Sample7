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
        /// Добавление штампа в документ
        /// </summary>
        /// <param name="pdf">Содержимое документа</param>
        /// <param name="documentIndefNumber">Регистрационный номер</param>
        /// <param name="documentRegisteredDateTime">Дата регистрации документа</param>
        /// <param name="certificate">Параметры организации для штампа</param>
        /// <param name="proxyData">Параметры доверенности для штампа</param>
        /// <returns></returns>
        public static byte[] AddStamp(byte[] pdf, string documentIndefNumber, DateTime? documentRegisteredDateTime, Dictionary<string, string> certificate = null, Dictionary<string, string> proxyData = null)
        {
            var documentRegisteredDateTimeString = documentRegisteredDateTime.HasValue
                ? TimeZoneInfo.ConvertTime(documentRegisteredDateTime.Value, TimeZoneInfo.Utc, TimeZoneInfo.Local)
                    .ToString("dd.MM.yyyy HH:mm:ss")
                : "-";

            string organization = null;
            string surName = null;
            string givenName = null;
            string ogrn = null;
            string startDataSertificate = null;
            string endDataSertificate = null;
            string certificateSerialNumber = null;

            certificate?.TryGetValue("Organization", out organization);
            certificate?.TryGetValue("SurName", out surName);
            certificate?.TryGetValue("GivenName", out givenName);
            certificate?.TryGetValue("OGRN", out ogrn);
            certificate?.TryGetValue("NotBefore", out startDataSertificate);
            certificate?.TryGetValue("NotAfter", out endDataSertificate);
            certificate?.TryGetValue("CertificateSerialNumber", out certificateSerialNumber);

            string proxyNum;
            string proxyStartDate;
            string proxyEndDate;
            var proxyLine = "";
            if (proxyData != null)
            {
                proxyData.TryGetValue("Organization", out organization);
                proxyData.TryGetValue("OGRN", out ogrn);
                proxyData.TryGetValue("ProxyNum", out proxyNum);
                proxyData.TryGetValue("StartDate", out proxyStartDate);
                proxyData.TryGetValue("EndDate", out proxyEndDate);
                proxyLine = $"Доверенность №{proxyNum}. Действительна c {proxyStartDate} по {proxyEndDate}.";
            }

            var separateLine = new string('_', 141);
            var watermarkText = new StringBuilder();
            watermarkText.AppendLine($"{separateLine}");
            watermarkText.AppendLine("");
            watermarkText.AppendLine($"Документ {documentIndefNumber} от {documentRegisteredDateTimeString} зарегистрирован. Документ подписан электронной подписью:   ");
            watermarkText.AppendLine("");

            if (string.IsNullOrWhiteSpace(organization) == false)
            {
                watermarkText.AppendLine($"{organization} ОГРН {ogrn}");
            }

            watermarkText.AppendLine($"{surName} {givenName}. {proxyLine}");
            watermarkText.AppendLine($"Серийный номер сертификата {certificateSerialNumber}. Действителен c {startDataSertificate} по {endDataSertificate} ");

            // Ставим штамп с номером документа
            byte[] watermarkedPdf = AddToPdf.AddWatermark(pdf, watermarkText.ToString());

            return watermarkedPdf;
        }
    }
}
