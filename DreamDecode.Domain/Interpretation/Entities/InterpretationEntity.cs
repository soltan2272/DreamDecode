using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Domain.Interpretation.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using global::DreamDecode.Domain.Dream.Entities;
    using global::DreamDecode.Domain.User.Entities;

    namespace DreamDecode.Domain.Entities
    {
        public class InterpretationEntity
        {
            [Key]
            public int InterpretationId { get; set; }   // PK

            // Foreign Key to Dreams
            public int DreamId { get; set; }
            public DreamEntity Dream { get; set; }

            // Foreign Key to Users (Admin only)
            public string AdminId { get; set; }   
            public ApplicationUser Admin { get; set; }

            public string InterpretationText { get; set; }
            public DateTime InterpretedAt { get; set; }
        }
    }

}
