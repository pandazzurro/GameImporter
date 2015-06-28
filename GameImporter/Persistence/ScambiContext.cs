using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using Npgsql;

namespace GameImporter.Persistence
{
    public class ScambiContext : DbContext
    {
        ScambiContext(DbConnection connection)
        : base(connection, true)
        {  }

        public static ScambiContext CreateContext()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=azzurro;Database=nodejs;");
            conn.Open();

            return new ScambiContext(conn);
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Game>()
                   .HasKey(game => game.IdGame);

            builder.Entity<Genre>().HasKey(genre => genre.IdGenre);
            builder.Entity<Platform>().HasKey(platform => platform.IdPlatform);

            //builder.Entity<Game>()
            //       .HasOptional<Genre>(game => game.Genre)
            //       .WithOptionalPrincipal();

            //builder.Entity<Game>()
            //       .HasOptional<Platform>(game => game.Platform)
            //       .WithOptionalPrincipal();

            base.OnModelCreating(builder);
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
    }
}
