using Microsoft.EntityFrameworkCore;

namespace StorageApp
{
    /// <summary>
    /// Репозиторий для паллет.
    /// </summary>
    internal class PalletRepository : IPalletRepository
    {
        private readonly StorageDbContext storageContext;

        public PalletRepository(StorageDbContext storageContext)
        {
            this.storageContext = storageContext ?? throw new ArgumentNullException(nameof(storageContext));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IPallet>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await this.storageContext.Pallets.AsNoTracking().Include(p => p.Boxes).Select(p => p.ToPallet()).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IPallet>> GetTopByExpirationDateAsync(int count = 3, CancellationToken cancellationToken = default)
        {
            return await this.storageContext.Pallets.AsNoTracking()
                .OrderByDescending(pallet => pallet.Boxes.Max(box => box.ExpirationDate))
                .Take(count)
                .Select(p => p.ToPallet())
                .ToListAsync(cancellationToken);
        }
    }
}
