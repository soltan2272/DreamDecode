using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Application.Dream.DTOs
{
    public class DreamCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }
    }

}
