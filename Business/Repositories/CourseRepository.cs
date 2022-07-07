using Business.Services;
using DAL.Data;
using DAL.Models;
using Exceptions.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class CourseRepository : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Course> Get(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var data = await _context.Courses.Where(s => !s.IsDeleted && s.Id == id)
                                             .Include(n => n.Image)
                                             .Include(n => n.CourseFeature)
                                             .ThenInclude(n => n.SkillLevel)
                                             .Include(n => n.CourseFeature.CourseFeatureLanguages)
                                             .ThenInclude(n => n.Language)
                                             .Include(n => n.CourseCategories)
                                             .ThenInclude(n => n.Category)
                                             .Include(n => n.CourseTags)
                                             .ThenInclude(n => n.Tag)
                                             .FirstOrDefaultAsync();

            if (data is null)
            {
                throw new EntityIsNullException();
            }

            return data;
        }

        public async Task<List<Course>> GetAll()
        {
            var data = await _context.Courses.Where(s => !s.IsDeleted)
                                             .Include(n => n.Image)
                                             .Include(n => n.CourseFeature)
                                             .Include(n => n.CourseCategories)
                                             .ThenInclude(n => n.Category)
                                             .Include(n => n.CourseTags)
                                             .ThenInclude(n => n.Tag)
                                             .ToListAsync();

            if (data is null)
            {
                throw new EntityIsNullException();
            }

            return data;
        }

        public async Task Create(Course entity)
        {
            entity.CreatedDate = DateTime.UtcNow.AddHours(4);
            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Course entity)
        {
            var data = await Get(id);
            data.Name = entity.Name;
            data.Content = entity.Content;
            data.ImageId = entity.ImageId;
            data.CourseFeatureId = entity.CourseFeatureId;
            data.CourseCategories = entity.CourseCategories;
            data.CourseTags = entity.CourseTags;
            entity.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
