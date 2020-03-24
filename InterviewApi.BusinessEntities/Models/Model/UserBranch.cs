using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class UserBranch : ModelBase
    {
        public string UserId { get; set; }
        public Int64 BankBranchId { get; set; }

        public virtual User User { get; set; }
        public virtual BankBranch BankBranch { get; set; }
    }
}
