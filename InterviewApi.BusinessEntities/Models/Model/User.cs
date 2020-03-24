using System;
using Microsoft.AspNetCore.Identity;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class User : IdentityUser
    {
        public Int64 UserTypeId { get; set; }
        public Int64 InstitutionId { get; set; }

        public virtual UserType UserType { get; set; }
        public Institution Institution { get; set; }
    }
}
