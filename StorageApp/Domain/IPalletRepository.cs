namespace StorageApp
{
    /// <summary>
    /// Репозиторий для паллет.
    /// </summary>
    public interface IPalletRepository
    {
        /// <summary>
        /// Получить все паллеты.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Список паллет.</returns>
        Task<IEnumerable<IPallet>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить паллеты, которые содержат коробки с наибольшим сроком годности.
        /// </summary>
        /// <param name="count">Количество возвращаемых сущностей.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Список паллет.</returns>
        Task<IEnumerable<IPallet>> GetTopByExpirationDateAsync(int count = 3, CancellationToken cancellationToken = default);
    }
}
