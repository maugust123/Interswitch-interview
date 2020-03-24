using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    public class UserType : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
