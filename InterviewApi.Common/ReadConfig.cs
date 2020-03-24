using Microsoft.Extensions.Options;

namespace InterviewApi.Common
{
    public class ReadConfig
    {
        
        private readonly IOptions<AppSecretsConfig> _options;
        public ReadConfig(IOptions<AppSecretsConfig> options)
        {
            _options = options;
        }

        public string BaseUrl => _options.Value.BaseUrl;

        public string HostName => _options.Value.HostName;
        public int PortNumber => _options.Value.PortNumber;
        public string MailSender => _options.Value.MailSender;
        public string UserName => _options.Value.UserName;
        public string Password => _options.Value.Password;

        public string InterswitchAuth => _options.Value.InterswitchAuth;

        public string EmailSubject()
        {
            var hostName = System.Environment.MachineName;

            var environment = _options.Value.ASPNETCORE_ENVIRONMENT;
            var appName = _options.Value.ApplicationName;
            var client = _options.Value.ClientName;

            return $"{client} - {appName} {environment} Error - {hostName}";
        }

        public string SupportGroupEmail => _options.Value.SupportGroup;

    }
}
