using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class SubGenreRep : ISubGenreRep
    {
        private readonly AppDbContext _context;

        public SubGenreRep(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubGenre>> GetAllAsync()
        {
            return await _context.SubGenres.Include(x => x.Genre).AsNoTracking().ToListAsync();
        }

        public async Task<SubGenre> GetByIdAsync(int id)
        {
            return await _context.SubGenres.Where(x => x.Id == id).Include(x => x.Genre).AsNoTracking().FirstAsync();
        }

        public async Task<IEnumerable<SubGenre>> FindByConditionAsync(Expression<Func<SubGenre, bool>> expression)
        {
            return await _context.SubGenres.Where(expression).Include(x => x.Genre).AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(SubGenre entity)
        {
            await _context.SubGenres.AddAsync(entity);
        }

        public void Update(SubGenre entity)
        {
            _context.SubGenres.Update(entity);
        }

        public void Delete(SubGenre entity)
        {
            _context.SubGenres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var genre = await _context.SubGenres.FirstAsync(x => x.Id == id);
            _context.SubGenres.Remove(genre);
        }
    }
}
