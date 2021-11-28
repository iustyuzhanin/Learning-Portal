using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LearningPortal.Models
{
    public class AppUserRoleModel
    {
        public List<AppUserModel> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
