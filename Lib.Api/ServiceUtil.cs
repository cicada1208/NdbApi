using System;
using System.ServiceModel;

namespace Lib.Api
{
    public class ServiceUtil
    {
        private static ADWebService.AdUserIdentifySoapClient _ADClient;
        /// <summary>
        /// AD WebService
        /// </summary>
        public static ADWebService.AdUserIdentifySoapClient ADClient
        {
            get
            {
                if (_ADClient == null ||
                    _ADClient.State == CommunicationState.Closed ||
                    _ADClient.State == CommunicationState.Faulted)
                {
                    _ADClient?.Abort();
                    string baseAddress = "";
                    EndpointAddress endpoint = new EndpointAddress(baseAddress);
                    BasicHttpBinding binding = new BasicHttpBinding();
                    //binding.SendTimeout = new TimeSpan(0,1,0);
                    //binding.OpenTimeout = new TimeSpan(0, 1, 0);
                    //binding.MaxReceivedMessageSize = 65536;
                    _ADClient = new ADWebService.AdUserIdentifySoapClient(binding, endpoint);
                }
                return _ADClient;
            }
        }

        private static UdMisPicWebService.WebUdMisPicSoapClient _UdMisPicClient;
        /// <summary>
        /// UdPic WebService
        /// </summary>
        public static UdMisPicWebService.WebUdMisPicSoapClient UdMisPicClient
        {
            get
            {
                if (_UdMisPicClient == null ||
                    _UdMisPicClient.State == CommunicationState.Closed ||
                    _UdMisPicClient.State == CommunicationState.Faulted)
                {
                    _UdMisPicClient?.Abort();
                    string baseAddress = "";
                    EndpointAddress endpoint = new EndpointAddress(baseAddress);
                    BasicHttpBinding binding = new BasicHttpBinding();
                    binding.SendTimeout = new TimeSpan(0, 0, 2);
                    _UdMisPicClient = new UdMisPicWebService.WebUdMisPicSoapClient(binding, endpoint);
                }
                return _UdMisPicClient;
            }
        }

        //private static ARWcfService.ARServiceClient _ARClient;
        ///// <summary>
        ///// NIS ARWcfService
        ///// </summary>
        //public static ARWcfService.ARServiceClient ARClient
        //{
        //    get
        //    {
        //        if (_ARClient == null ||
        //            _ARClient.State == CommunicationState.Closed ||
        //            _ARClient.State == CommunicationState.Faulted)
        //        {
        //            _ARClient?.Abort();
        //            String baseAddress = "";
        //            Uri uri = new Uri(baseAddress);
        //            EndpointIdentity identity = new DnsEndpointIdentity("localhost");
        //            EndpointAddress endpoint = new EndpointAddress(uri, identity);
        //            WSHttpBinding binding = new WSHttpBinding();
        //            binding.Security = new WSHttpSecurity();
        //            binding.Security.Mode = SecurityMode.None;
        //            _ARClient = new ARWcfService.ARServiceClient(binding, endpoint);
        //        }
        //        return _ARClient;
        //    }
        //}

    }
}
