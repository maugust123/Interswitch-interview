using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.Common
{
    //https://joonasw.net/view/aspnet-core-2-configuration-changes
    //http://www.mithunvp.com/user-secrets-asp-net-core/
    //https://www.youtube.com/watch?v=InyWktgdWJU
    public class AppSecretsConfig
    {
        //Jwt keys
        public string Key { get; set; }
        public string Issuer { get; set; }

        //Mail settings
        public string HostName { get; set; }
        public int PortNumber { get; set; }
        public string MailSender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //Application keys
        public string ASPNETCORE_ENVIRONMENT { get; set; }
        public string ApplicationName { get; set; }
        public string ClientName { get; set; }
        public string SupportGroup { get; set; }

        //Connection string
        public string DefaultConnection { get; set; }

        public string BaseUrl { get; set; }
        public string InterswitchAuth { get; set; }


    }
}
