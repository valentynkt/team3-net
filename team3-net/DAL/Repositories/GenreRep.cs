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
    public class GenreRep : IGenreRep
    {
        private readonly AppDbContext _context;

        public GenreRep(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.Include(x=>x.SubGenres).AsNoTracking().ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.Where(x => x.Id == id).Include(x => x.SubGenres).AsNoTracking().FirstAsync();
        }

        public async Task<IEnumerable<Genre>> FindByConditionAsync(Expression<Func<Genre, bool>> expression)
        {
            return await _context.Genres.Where(expression).Include(x => x.SubGenres).AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Genre entity)
        {
            await _context.Genres.AddAsync(entity);
        }

        public void Update(Genre entity)
        {
            _context.Genres.Update(entity);
        }

        public void Delete(Genre entity)
        {
            _context.Genres.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var genre = await _context.Genres.FirstAsync(x => x.Id == id);
            _context.Genres.Remove(genre);
        }
    }
}
