using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class Role : IdentityRole
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
