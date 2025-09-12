using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Application.Dream.DTOs
{
    public class DreamDto
    {
        public int DreamId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsInterpreted { get; set; }
        public bool IsPaid { get; set; }
    }
}
