using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Application.Dream.DTOs
{
    public class PendingDreamsDto
    {
        public int DreamId { get; set; }
        public string UserFullName { get; set; }
        public string  Email { get; set; }
        public string DreamTitle { get; set; }
        public string DreamDescription { get; set; }
        public bool isInterpreted { get; set; }
        public bool isPaid { get; set; }
    }
}
