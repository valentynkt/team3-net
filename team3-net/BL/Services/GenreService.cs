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
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _unitOfWork.GenreRep.GetAllAsync();
            if (!genres.Any())
            {
                throw new ArgumentNullException($"There are no genre");
            }
            return genres;
        }

        public async Task<Genre> GetById(int id)
        {
            var genreById = await _unitOfWork.GenreRep.GetByIdAsync(id);
            if (genreById == null)
            {
                throw new ArgumentNullException($"There are no genre with this id: {id}");
            }
            return genreById;
        }

        public async Task Add(Genre entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ArgumentNullException("Wrong data");
            }
            await _unitOfWork.GenreRep.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(Genre entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ArgumentNullException("Wrong data");
            }
            _unitOfWork.GenreRep.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Genre entity)
        {
            var subGenresByGenre=await _unitOfWork.SubGenreRep.FindByConditionAsync(x=>x.GenreId==entity.Id);
            foreach (var subGenre in subGenresByGenre)
            {
                _unitOfWork.SubGenreRep.Delete(subGenre);
            }
            _unitOfWork.GenreRep.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteById(int id)
        {
            var subGenresByGenre = await _unitOfWork.SubGenreRep.FindByConditionAsync(x => x.GenreId == id);
            foreach (var subGenre in subGenresByGenre)
            {
                _unitOfWork.SubGenreRep.Delete(subGenre);
            }
            await _unitOfWork.GenreRep.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
