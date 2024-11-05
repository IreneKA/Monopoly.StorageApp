using Microsoft.EntityFrameworkCore;

namespace StorageApp
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var Guids = Enumerable.Range(0, 10).Select(_ => Guid.NewGuid()).ToList();

            modelBuilder.Entity<PalletEntity>().HasData(
                Enumerable.Range(0, 10)
                .Select(index => new PalletEntity()
                    { 
                        Id = Guids[index],
                        Width = Random.Shared.NextDouble() + 1,
                        Depth = Random.Shared.NextDouble() + 1,
                        Height = Random.Shared.NextDouble() + 1 
                    })
            );

            modelBuilder.Entity<BoxEntity>().HasData(
                Enumerable.Range(0, 20)
                .Select(index => new BoxEntity()
                {
                    Id = Guid.NewGuid(),
                    Width = Random.Shared.NextDouble(),
                    Depth = Random.Shared.NextDouble(),
                    Height = Random.Shared.NextDouble(),
                    ProductionDate = DateOnly.MinValue.AddYears(Random.Shared.Next(1900, 2023)).AddDays(Random.Shared.Next(0, 200)),
                    Weight = Random.Shared.NextDouble() * 10,
                    PalletId = Guids[index/2]
                })
            );
        }
    }
}
