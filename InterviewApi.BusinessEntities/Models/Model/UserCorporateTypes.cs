using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class UserCorporateType : ModelBase
    {
        public string UserId { get; set; }
        public Int64 CorporateTypeId { get; set; }

        public virtual User User { get; set; }
        public virtual CorporateType CorporateType { get; set; }
    }
}
