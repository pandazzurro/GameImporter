using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameImporter.Persistence
{
    [Table("platform", Schema = "public")]
    public class Platform
    {
        [Key, Column("PlatformId")]
        public string PlatformId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Class")]
        public string Class { get; set; }
        [Column("ImportId")]
        public int ImportId { get; set; }
    }
}
