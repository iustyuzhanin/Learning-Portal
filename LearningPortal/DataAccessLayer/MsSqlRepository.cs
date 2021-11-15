using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Domains;

namespace LearningPortal.DataAccessLayer
{
    public class MsSqlRepository:ILearningRepository
    {
        private readonly ApplicationDbContext _context;

        public MsSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Course> Courses => _context.Courses;
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Chapter> Chapters => _context.Chapters;
        public IQueryable<Teacher> Teachers => _context.Teachers;
        public IQueryable<Student> Students => _context.Students;
    }
}
