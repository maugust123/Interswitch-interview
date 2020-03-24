using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class BankBranch : ModelBase
    {
        public Int64 BankId { get; set; }
        public string Name { get; set; }

        public virtual Bank Bank { get; set; }
    }
}
