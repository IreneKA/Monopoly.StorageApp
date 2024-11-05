namespace StorageApp
{
    /// <summary>Объект склада.</summary>
    public interface IStorageObject
    {
        /// <summary>
        /// ID объекта.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Ширина в метрах.
        /// </summary>
        double Width  { get; }

        /// <summary>
        /// Высота в метрах.
        /// </summary>
        double Height { get; }

        /// <summary>
        /// Глубина в метрах.
        /// </summary>
        double Depth { get; }

        /// <summary>
        /// Вес в кг.
        /// </summary>
        double Weight { get; }

        /// <summary>
        /// Дата истечения срока годности.
        /// </summary>
        DateOnly? ExpirationDate { get; }

        /// <summary>
        /// Объем.
        /// </summary>
        double Volume { get; }
    }
}
