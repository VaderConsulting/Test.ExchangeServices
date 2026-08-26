using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Test.ExchangeService.Entity;
using Test.ExchangeServices.ExchangeWebServices;

using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace Test.ExchangeServices
{
    /// <summary>
    /// 
    /// </summary>
    internal class ExchangeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        internal ExchangeServiceBinding GetExchangeBinding(ExchangeCredential credentials)
        {
            /*
             * RESOURCES - 
             * http://blogs.msdn.com/ericlee/archive/2006/10/22/exchange-server-2007-for-developers.aspx 
             * 
             * 
             * 
             */

            ExchangeServiceBinding binding = new ExchangeServiceBinding();
            ServicePointManager.ServerCertificateValidationCallback =
                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    {
                        // Replace this line with code to validate server certificate.
                        return true;
                    };

            System.Net.WebProxy proxyObject = new System.Net.WebProxy();
            proxyObject.Credentials = System.Net.CredentialCache.DefaultCredentials;

            binding.Credentials = new NetworkCredential(credentials.UserName, credentials.Password, credentials.Domain);
            //binding.Url = @"https://yourserverTesting.pvt/EWS/Exchange.asmx";

            string server = ConfigurationManager.AppSettings["ExchangeServer"] as string;

            if (server == null || string.IsNullOrEmpty(server))
                throw new ArgumentNullException("The Exchange server Url could not be found.");

            binding.Url = server;
            Console.WriteLine("***** " + server);

            //binding.UseDefaultCredentials = true;
            binding.Proxy = proxyObject;

            return binding;
        }
    }
}
