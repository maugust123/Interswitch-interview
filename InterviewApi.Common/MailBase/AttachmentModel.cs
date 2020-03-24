using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InterviewApi.Common.MailBase
{
    public class AttachmentModel
    {
        public bool EnableAttachment { get; set; } = false;
        public string AttachmentPath { get; set; } = string.Empty;
        public Stream AttachmentStream { get; set; } = null;
        public string StreamName { get; set; } = "my file.pdf";
        public string ContentType { get; set; } = "application/pdf";
    }
}
