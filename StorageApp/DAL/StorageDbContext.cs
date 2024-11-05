using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StorageApp
{
    public class StorageDbContext: DbContext
    {
        public DbSet<BoxEntity> Boxes => Set<BoxEntity>();

        public DbSet<PalletEntity> Pallets => Set<PalletEntity>();

        public StorageDbContext(DbContextOptions<StorageDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        private void ConfigureBoxEntity(EntityTypeBuilder<BoxEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(box => box.Id);
        }

        private void ConfigurePalletEntity(EntityTypeBuilder<PalletEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(pallet => pallet.Id);
            entityTypeBuilder
                .HasMany(p => p.Boxes)
                .WithOne(p => p.Pallet)
                .HasForeignKey("PalletId")
                .IsRequired(false); ;
        }
    }
}
