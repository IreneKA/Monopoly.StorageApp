namespace StorageApp
{
    /// <inheritdoc cref="IPalletService" />
    internal class PalletService : IPalletService
    {
        private readonly IPalletRepository palletRepository;

        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="palletRepository"><see cref="IPalletRepository"/>.</param>
        public PalletService(IPalletRepository palletRepository)
        {
            this.palletRepository = palletRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<(DateOnly? ExpirationDate, IEnumerable<IPallet> Pallets)>> GetGroupedByExpirationDateAsync(CancellationToken cancellationToken = default)
        {
            var pallets = await palletRepository.GetAllAsync(cancellationToken);
            return pallets
                .GroupBy(pallet => pallet.ExpirationDate, (date, pallets) => (ExpirationDate: date, Pallets: pallets.OrderBy(pallet => pallet.Weight).AsEnumerable()))
                .OrderBy(group => group.ExpirationDate);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IPallet>> GetTopByExpirationDateAsync(int count = 3, CancellationToken cancellationToken = default)
        {
            var pallets = await palletRepository.GetAllAsync(cancellationToken);
            return pallets
                .OrderByDescending(pallet => pallet.Boxes.Max(box => box.ExpirationDate))
                .Take(count)
                .OrderBy(pallet => pallet.Volume);
        }
    }
}
