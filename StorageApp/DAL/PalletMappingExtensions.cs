namespace StorageApp
{
    internal static class PalletMappingExtensions
    {
        internal static Pallet ToPallet(this PalletEntity entity)
        {
            var pallet = new Pallet (entity.Id, entity.Width, entity.Depth, entity.Height);
            foreach (BoxEntity box in entity.Boxes)
            {
                pallet.AddBox(box.ToBox());
            }

            return pallet;
        }
    }
}
