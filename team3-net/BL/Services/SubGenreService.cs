using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BL.Services
{
    public class SubGenreService : ISubGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubGenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SubGenre>> GetAll()
        {
            var subGenres = await _unitOfWork.SubGenreRep.GetAllAsync();
            if (!subGenres.Any())
            {
                throw new ArgumentNullException($"There are no subGenre");
            }
            return subGenres;
        }

        public async Task<SubGenre> GetById(int id)
        {
            var subGenreById = await _unitOfWork.SubGenreRep.GetByIdAsync(id);
            if (subGenreById == null)
            {
                throw new ArgumentNullException($"There are no subGenre with this id: {id}");
            }
            return subGenreById;
        }

        public async Task Add(SubGenre entity)
        {
            if (string.IsNullOrEmpty(entity.Name) || entity.GenreId<=0)
            {
                throw new ArgumentNullException("Wrong data");
            }

            entity.Genre = await _unitOfWork.GenreRep.GetByIdAsync(entity.GenreId);
            await _unitOfWork.SubGenreRep.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(SubGenre entity)
        {
            if (string.IsNullOrEmpty(entity.Name) || entity.GenreId <= 0)
            {
                throw new ArgumentNullException("Wrong data");
            }
            entity.Genre = await _unitOfWork.GenreRep.GetByIdAsync(entity.GenreId);
            _unitOfWork.SubGenreRep.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(SubGenre entity)
        {
            _unitOfWork.SubGenreRep.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteById(int id)
        {
            await _unitOfWork.GenreRep.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
