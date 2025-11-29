using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Domain.User.Entities;

namespace DreamDecode.Domain.Dream.Entities
{
    public class DreamEntity
    {
        [Key]
        public int DreamId { get; set; }

        public string UserId { get; set; } // FK -> ApplicationUser
        public ApplicationUser User { get; set; } // Navigation Property

        public string Title { get; set; }       // Short title for dream
        public string Description { get; set; } // Full dream text

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow; // Auto-set

        public bool IsInterpreted { get; set; } = false; // Default false

        public string? InterpretationText { get; set; }
        public bool IsPaid { get; set; } = false;        // Default false
    }
}
