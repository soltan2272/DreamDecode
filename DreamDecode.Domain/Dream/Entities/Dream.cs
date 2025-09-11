using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Domain.Dream.Entities
{
    using System;
    using global::DreamDecode.Domain.User.Entities;

    namespace DreamDecode.Domain.Dreams.Entities
    {
        public class Dream
        {
            public int DreamId { get; set; }   

            public string UserId { get; set; } // FK -> ApplicationUser
            public ApplicationUser User { get; set; } // Navigation Property

            public string Title { get; set; }       // Short title for dream
            public string Description { get; set; } // Full dream text

            public DateTime SubmittedAt { get; set; } = DateTime.UtcNow; // Auto-set

            public bool IsInterpreted { get; set; } = false; // Default false
            public bool IsPaid { get; set; } = false;        // Default false
        }
    }

}
