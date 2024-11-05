namespace StorageApp
{
    /// <summary>Паллета.</summary>
    public interface IPallet : IStorageObject
    {
        /// <summary>
        /// Список коробок.
        /// </summary>
        IReadOnlyCollection<IBox> Boxes { get; }
        
        /// <summary>
        /// Добавить короку.
        /// </summary>
        /// <param name="box">Коробка.</param>
        void AddBox(IBox box);
    }
}
