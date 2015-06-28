using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameImporter.Persistence
{
    [Table("genre", Schema = "public")]
    public class Genre
    {
        [Key, Column("GenreId")]
        public string GenreId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}
