using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Models;

namespace LearningPortal.Domains
{
    public class Student:AppUserModel
    {
        public string Name { get; set; }
    }
}
