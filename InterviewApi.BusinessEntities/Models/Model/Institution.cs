using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
   public class Institution: ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
