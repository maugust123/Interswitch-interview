using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class CorporateType : ModelBase
    {
        public string Name { get; set; }

        public virtual ICollection<UserCorporateType> UserCorporateTypes { get; set; }
    }
}
