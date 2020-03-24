using Microsoft.AspNetCore.Http;
using System.Net;

namespace InterviewApi.Common.Extensions
{
    public static class HttpRequestExtensions
    {

        private const string NullIpAddress = "::1";

        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress.IsSet())
            {
                //Check if the request has a set IP address
                return connection.LocalIpAddress.IsSet()
                    //The request is local if LocalIpAddress == RemoteIpAddress
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    //else request is remote if the remote IP address is not a loopback address
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        private static bool IsSet(this IPAddress address)
        {
            //Check if current IP is null or empty
            return address != null && address.ToString() != NullIpAddress;
        }


        //public static bool IsLocal(this HttpRequest req)
        //{
        //    var connection = req.HttpContext.Connection;
        //    if (connection.RemoteIpAddress != null)
        //    {
        //        if (connection.LocalIpAddress != null)
        //        {
        //            return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
        //        }
        //        else
        //        {
        //            return IPAddress.IsLoopback(connection.RemoteIpAddress);
        //        }
        //    }

        //    // for in memory TestServer or when dealing with default connection info
        //    if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
