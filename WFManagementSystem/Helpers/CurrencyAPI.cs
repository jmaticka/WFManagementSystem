using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace WFManagementSystem.Helpers
{
    class CurrencyAPI
    {

        private XmlDocument envelope;

        /// <summary>
        /// Prepares SOAP envelope for request on CurrencyAPI. Envelop is for one time use.
        /// </summary>
        /// <param name="ammount">Ammount of money you want to exchange</param>
        /// <param name="from">Currency you want convert from</param>
        /// <param name="to">Currency you want to convert to</param>
        public void prepareEnvelope(float amount, string from = "CZK", string to = "EUR")
        {
            envelope = new XmlDocument();
            DateTime date = DateTime.Now.Date;
            string dateString = date.Year + "-" + date.Month + "-" + date.Day;

            string xmlContent = $@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:tem=""http://tempuri.org/"">
                <soap:Header/>
                    <soap:Body>
                        <tem:GetConversionAmount>
                            <tem:CurrencyFrom>{from}</tem:CurrencyFrom>
                            <tem:CurrencyTo>{to}</tem:CurrencyTo>
                            <tem:RateDate>{dateString}</tem:RateDate>
                            <tem:Amount>{amount}</tem:Amount>
                        </tem:GetConversionAmount>
                    </soap:Body>
                </soap:Envelope>";
            envelope.LoadXml(xmlContent);
        }

        /// <summary>
        /// Sends request to webservice, converst result and return amount.
        /// Method is using prepared envelope.
        /// </summary>
        /// <returns>Converted currency amount</returns>
        /// <exception>If no envelop is prepared, returns exception.</exception>
        public double sendRequest()
        {
            if (envelope == null)
            {
                throw new Exception("No envelope was created for request.");
            }

            // Vytvoření hlavičky http requestu
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://currencyconverter.kowabunga.net/converter.asmx");
            request.Headers.Add(@"SOAPAction", "GetConversionAmount");
            //request.Headers.Add(@"SOAP:Action");
            request.ContentType = "application/soap+xml;charset=\"utf-8\"";
            request.Accept = "application/soap+xml";
            request.Method = "POST";

            using (Stream stream = request.GetRequestStream())
            {
                envelope.Save(stream);
            }

            string soapResult = null;

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }

            envelope = null;

            int start = soapResult.IndexOf("<GetConversionAmountResult>") + "<GetConversionAmountResult>".Length;
            int len = soapResult.IndexOf("</GetConversionAmountResult>") - start;
            string sub = soapResult.Substring(start, len);
            sub = sub.Replace('.', ',');

            var result = double.Parse(sub);
            return Math.Round(result);
        }
    }
}
