using StorageApp;

namespace StorageAppTests
{
    [TestFixture]
    public class PalletTests
    {
        /// <summary>
        /// Срок годности паллеты вычисляется из наименьшего срока годности коробки, вложенной в паллету.
        /// </summary>
        /// <param name="haveBoxes">На паллете есть коробки.</param>
        /// <returns><see cref="Task"></returns>
        [Test]
        public async Task ExpirationDateTest([Values] bool haveBoxes)
        {
            // Arrange
            var pallet = CreatePallet();
            if (haveBoxes) 
            {
                pallet.AddBox(CreateBox(DateOnly.MinValue));
                pallet.AddBox(CreateBox(DateOnly.MaxValue));
            }

            // Act
            var result = pallet.ExpirationDate;

            // Assert
            if (haveBoxes)
            {
                Assert.IsNotNull(result);
                Assert.That(result, Is.EqualTo(DateOnly.MinValue));
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        /// <summary>
        /// Вес паллеты вычисляется из суммы веса вложенных коробок + 30кг.
        /// </summary>
        /// <param name="haveBoxes">На паллете есть коробки.</param>
        /// <returns><see cref="Task"></returns>
        [Test]
        public async Task WeightTest([Values] bool haveBoxes)
        {
            // Arrange
            var pallet = CreatePallet();
            if (haveBoxes)
            {
                pallet.AddBox(CreateBox(DateOnly.MinValue, weight: 4.5));
                pallet.AddBox(CreateBox(DateOnly.MaxValue, weight: 11));
            }

            // Act
            var result = pallet.Weight;

            // Assert
            if (haveBoxes)
            {
                Assert.That(result, Is.EqualTo(45.5).Within(0.1));
            }
            else
            {
                Assert.That(result, Is.EqualTo(30).Within(0.1));
            }
        }

        /// <summary>
        /// Объем паллеты вычисляется как сумма объема всех находящихся на ней коробок и произведения ширины, высоты и глубины паллеты.
        /// </summary>
        /// <param name="haveBoxes">На паллете есть коробки.</param>
        /// <returns><see cref="Task"></returns>
        [Test]
        public async Task VolumeTest([Values] bool haveBoxes)
        {
            // Arrange
            var pallet = CreatePallet(3, 3, 0.1);
            if (haveBoxes)
            {
                pallet.AddBox(CreateBox(DateOnly.MinValue, 1, 1, 1));
                pallet.AddBox(CreateBox(DateOnly.MaxValue, 2, 2, 2));
            }

            // Act
            var result = pallet.Volume;

            // Assert
            if (haveBoxes)
            {
                Assert.That(result, Is.EqualTo(9.9).Within(0.1));
            }
            else
            {
                Assert.That(result, Is.EqualTo(0.9).Within(0.1));
            }
        }

        /// <summary>
        /// Каждая коробка не должна превышать по размерам паллету (по ширине и глубине).
        /// </summary>
        /// <param name="boxSize">Размеры коробки.</param>
        /// <param name="isValidSize">Корректны ли размеры коробки.</param>
        /// <returns><see cref="Task"></returns>
        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async Task AddBoxTest((double Width, double Depth) boxSize, bool isValidSize)
        {
            // Arrange
            var pallet = CreatePallet(1, 2, 0.1);
            
            // Act
            var action = () => pallet.AddBox(CreateBox(DateOnly.MinValue, boxSize.Width, boxSize.Depth));

            // Assert
            if (isValidSize)
            {
                Assert.That(action, Throws.Nothing);
            }
            else
            {
                Assert.That(action, Throws.TypeOf<ArgumentException>());
            }
        }

        private static IPallet CreatePallet(double? width = null, double? depth = null, double? height = null)
        {
            var pallet = new Pallet(
                    Guid.NewGuid(),
                    width ?? Random.Shared.NextDouble() + 1,
                    depth ?? Random.Shared.NextDouble() + 1,
                    height ?? 0.2);

            return pallet;
        }

        private static IBox CreateBox(DateOnly expDate, double? width = null, double? depth = null, double? height = null, double? weight = null)
        {
            return new Box(
                    Guid.NewGuid(),
                    width ?? Random.Shared.NextDouble(),
                    depth ?? Random.Shared.NextDouble(),
                    height ?? Random.Shared.NextDouble(),
                    weight ?? Random.Shared.NextDouble() * 10,
                    null,
                    expDate);
        }

        private static object[] TestCases =
        {
            new object[] { (1d, 2d), true },
            new object[] { (0.9, 1.9), true },
            new object[] { (1.2, 0.2), true },
            new object[] { (3d, 0.2), false },
            new object[] { (0.2, 3d), false },
        };
    }
}
