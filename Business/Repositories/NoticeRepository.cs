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
    public class NoticeRepository : INoticeService
    {
        private readonly AppDbContext _context;

        public NoticeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notice> Get(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var data = await _context.Notices.Where(s => !s.IsDeleted && s.Id == id)
                                             .FirstOrDefaultAsync();

            if (data is null)
            {
                throw new EntityIsNullException();
            }

            return data;
        }

        public async Task<List<Notice>> GetAll()
        {
            var data = await _context.Notices.Where(s => !s.IsDeleted)
                                             .OrderByDescending(n => n.CreatedDate).ToListAsync();

            if (data is null)
            {
                throw new EntityIsNullException();
            }

            return data;
        }

        public async Task Create(Notice entity)
        {
            entity.CreatedDate = DateTime.UtcNow.AddHours(4);
            await _context.Notices.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Notice entity)
        {
            var data = await Get(id);
            data.Content = entity.Content;
            entity.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var entity = await Get(id);
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
