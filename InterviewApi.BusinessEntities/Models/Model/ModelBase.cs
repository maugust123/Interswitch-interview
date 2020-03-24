using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InterviewApi.BusinessEntities.Models.Model
{
    /// <summary>
    /// Holds all shared model/entity properties
    /// </summary>
    public class ModelBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime IssueDate { get; set; } = DateTime.Now;

    }
}
