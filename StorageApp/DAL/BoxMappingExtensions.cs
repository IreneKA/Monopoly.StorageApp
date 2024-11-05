namespace StorageApp
{
    internal static class BoxMappingExtensions
    {
        internal static Box ToBox(this BoxEntity entity) => new Box
        (
            entity.Id,
            entity.Width,
            entity.Depth,
            entity.Height,
            entity.Weight,
            entity.ProductionDate,
            entity.ExpirationDate
        );
    }
}
