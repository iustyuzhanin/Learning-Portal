using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Domains;

namespace LearningPortal.DataAccessLayer
{
    interface ILearningRepository
    {
        IQueryable<Course> Courses { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Chapter> Chapters { get; }
        IQueryable<Teacher> Teachers { get; }
        IQueryable<Student> Students { get; }
    }
}
