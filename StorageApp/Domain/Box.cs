namespace StorageApp
{
    /// <inheritdoc cref="IBox" />
    internal class Box : IBox
    {
        public Box(Guid id, double width, double depth, double height, double weight, DateOnly? productionDate, DateOnly? expirationDate)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Ширина должна быть больше 0", nameof(width));
            }

            if (depth <= 0)
            {
                throw new ArgumentException("Глубина должна быть больше 0", nameof(depth));
            }

            if (height <= 0)
            {
                throw new ArgumentException("Высота должна быть больше 0", nameof(height));
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Вес должен быть больше 0", nameof(weight));
            }

            if (productionDate == null && expirationDate == null) 
            {
                throw new ArgumentException($"У коробки должен быть указан срок годности (параметр {nameof(expirationDate)}) или дата производства (параметр {nameof(productionDate)})");
            }

            this.ProductionDate = productionDate;
            this.ExpirationDate = expirationDate ?? productionDate?.AddDays(100);
            this.Id = id;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Weight = weight;
        }

        /// <inheritdoc/>
        public DateOnly? ProductionDate { get; }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public double Width { get; }

        /// <inheritdoc/>
        public double Height { get; }

        /// <inheritdoc/>
        public double Depth { get; }

        /// <inheritdoc/>
        public double Weight { get; }

        /// <inheritdoc/>
        public DateOnly? ExpirationDate { get; }

        /// <inheritdoc/>
        public double Volume => Width * Height * Depth;
    }
}
