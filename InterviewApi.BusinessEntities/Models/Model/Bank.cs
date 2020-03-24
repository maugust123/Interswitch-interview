using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
   public class Bank:ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BankBranch> BankBranches { get; set; }
    }
}
