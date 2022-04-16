using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenreRep GenreRep { get; }

        ISubGenreRep SubGenreRep { get; }

        Task<int> SaveAsync();
    }
}
