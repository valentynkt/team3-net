using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenreRep genreRep;
        private ISubGenreRep subGenreRep;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenreRep GenreRep
        {
            get
            {
                if (genreRep==null)
                {
                    genreRep = new GenreRep(_context);
                }

                return genreRep;
            }
        }
        public ISubGenreRep SubGenreRep
        {
            get
            {
                if (subGenreRep == null)
                {
                    subGenreRep = new SubGenreRep(_context);
                }

                return subGenreRep;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}