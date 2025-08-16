using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Domain.User.Entities;

namespace DreamDecode.Domain.User.Factory
{
    public interface IJwtFactory
    {
        string Create(ApplicationUser user, string role);
    }
}
