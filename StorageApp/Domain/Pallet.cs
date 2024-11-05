namespace StorageApp
{
    /// <inheritdoc cref="IPallet" />
    internal class Pallet : IPallet
    {
        private readonly List<IBox> boxes = new ();

        public Pallet(Guid id, double width, double depth, double height)
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

            Id = id;
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IBox> Boxes => boxes;

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public double Width { get; }

        /// <inheritdoc/>
        public double Height { get; }

        /// <inheritdoc/>
        public double Depth { get; }

        /// <inheritdoc/>
        public double Weight => Boxes.Sum(box => box.Weight) + 30;

        /// <inheritdoc/>
        public DateOnly? ExpirationDate => Boxes.Min(box => box.ExpirationDate);

        /// <inheritdoc/>
        public double Volume => Width * Height * Depth + Boxes.Sum(box => box.Volume);

        /// <inheritdoc/>
        public void AddBox(IBox box)
        {
            if ((box.Width > this.Width || box.Depth > this.Depth) && (box.Depth > this.Width || box.Width > this.Depth))
            {
                throw new ArgumentException("Коробка не должна превышать по размерам паллету (по ширине и глубине)");
            }
           
            this.boxes.Add(box);
        }
    }
}