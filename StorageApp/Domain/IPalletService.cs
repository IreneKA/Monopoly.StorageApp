namespace StorageApp
{
    /// <summary>
    /// Сервис работы с паллетами.
    /// </summary>
    public interface IPalletService
    {
        /// <summary>
        /// Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Группы паллет по сроку годности.</returns>
        Task<IEnumerable<(DateOnly? ExpirationDate, IEnumerable<IPallet> Pallets)>> GetGroupedByExpirationDateAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.
        /// </summary>
        /// <param name="count">Количество возвращаемых сущностей.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Список паллет.</returns>
        Task<IEnumerable<IPallet>> GetTopByExpirationDateAsync(int count = 3, CancellationToken cancellationToken = default);
    }
}
