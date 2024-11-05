namespace StorageApp
{
    public class BoxEntity : StorageEntity
    {
        public double Weight { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly? ProductionDate { get; set; }

        public Guid? PalletId { get; set; }

        public PalletEntity? Pallet { get; set; }
    }
}
