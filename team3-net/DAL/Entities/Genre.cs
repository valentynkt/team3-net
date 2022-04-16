using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        //Unique
        public string Name { get; set; } = string.Empty;
        public List<SubGenre> SubGenres { get; set; } = new();
    }
}
