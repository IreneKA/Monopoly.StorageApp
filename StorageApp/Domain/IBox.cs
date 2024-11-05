namespace StorageApp
{
    /// <summary>Коробка.</summary>
    public interface IBox : IStorageObject
    {
        /// <summary>
        /// Дата производства.
        /// </summary>
        DateOnly? ProductionDate { get; }
    }
}
