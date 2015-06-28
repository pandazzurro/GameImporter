using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GameImporter.Persistence
{
    [Table("game", Schema = "public")]
    [DataContract(Name = "game")]
    public class Game
    {
        [Key, Column("GameId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember(Name = "GameId", EmitDefaultValue = false)]
        public int GameId { get; set; }

        [Column("shortName")]
        [DataMember(Name = "shortName", EmitDefaultValue = false)]
        public string shortName { get; set; }

        [Column("Title")]
        [DataMember(Name = "Title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [Column("Description")]
        [DataMember(Name = "Description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [ForeignKey("GenreId")]
        [Required]
        [DataMember(Name = "Genre", EmitDefaultValue = false)]
        public virtual Genre Genre { get; set; }

        [DataMember(Name = "GenreId", EmitDefaultValue = false)]
        public string GenreId
        {
            get
            {
                if (Genre != null)
                    return Genre.GenreId;
                return string.Empty;
            }
            set
            {
                Genre = new Genre() { GenreId = value };
            }
        }

        [ForeignKey("PlatformId")]
        [Required]
        [DataMember(Name = "Platform", EmitDefaultValue = false)]
        public virtual Platform Platform { get; set; }

        [DataMember(Name = "PlatformId", EmitDefaultValue = false)]
        public string PlatformId
        {
            get
            {
                if (Platform != null)
                    return Platform.PlatformId;
                return string.Empty;
            }
            set
            {
                Platform = new Platform() { PlatformId = value };
            }
        }

        [Column("Image")]
        [DataMember(Name = "Image", EmitDefaultValue = false)]
        public byte[] Image { get; set; }

        [Column("ImportId")]
        [DataMember(Name = "ImportId", EmitDefaultValue = false)]
        public int? ImportId { get; set; }
        public int index { get; set; } 
    }

    public class GameIdentity
    {
        [DataMember(Name = "GameId", EmitDefaultValue = false)]
        public int GameId { get; set; }
    }
}
