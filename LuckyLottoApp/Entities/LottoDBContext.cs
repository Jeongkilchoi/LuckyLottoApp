using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Entities
{
    public class LottoDBContext : DbContext
    {
        public DbSet<BasicTbl> BasicTbl { get; set; }
        public DbSet<AppearTbl> AppearTbl { get; set; }
        public DbSet<ChulsuTbl> ChulsuTbl { get; set; }
        public DbSet<FixChulsuTbl> FixChulsuTbl { get; set; }
        public DbSet<ForeignTbl> ForeignTbl { get; set; }
        public DbSet<GridTbl> GridTbl { get; set; }
        public DbSet<HorflowTbl> HorflowTbl { get; set; }
        public DbSet<InnerBoxTbl> InnerBoxTbl { get; set; }
        public DbSet<NonChulsuTbl> NonChulsuTbl { get; set; }
        public DbSet<ShowOrderTbl> ShowOrderTbl {  get; set; }
        public DbSet<SingleTbl> SingleTbl { get; set; }
        public DbSet<TypeTbl> TypeTbl { get; set; }
        public DbSet<VerflowTbl> VerflowTbl { get; set; }
        public DbSet<PolygonTbl> PolygonTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=CHOI-PC;Database=LottoDB;Trusted_Connection=True;",
                    options => options.EnableRetryOnFailure());
        }
    }
}
