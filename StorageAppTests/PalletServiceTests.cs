using Moq;
using StorageApp;

namespace StorageAppTests
{
    [TestFixture]
    public class PalletServiceTests
    {
        [Test]
        public async Task GetGroupedByExpirationDateAsyncTest()
        {
            // Arrange
            var repositoryMock = new Mock<IPalletRepository>();
            var pallets = CreatePallets(createBoxAction: weight => CreateBox(DateOnly.MinValue, weight: weight))
                .Concat(CreatePallets(createBoxAction: weight => CreateBox(DateOnly.MaxValue, weight: weight)));
            repositoryMock.Setup(rep => rep.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(pallets);
            var service = new PalletService(repositoryMock.Object);

            // Act
            var result = await service.GetGroupedByExpirationDateAsync();

            // Assert
            Assert.IsNotEmpty(result);
            Assert.That(result.First().ExpirationDate, Is.EqualTo(DateOnly.MinValue));
            Assert.That(result.Last().ExpirationDate, Is.EqualTo(DateOnly.MaxValue));
            Assert.That(result.First().Pallets.First().Weight, Is.EqualTo(31));
            Assert.That(result.First().Pallets.Last().Weight, Is.EqualTo(35));
        }

        [Test]
        public async Task GetTopByExpirationDateAsyncTest()
        {
            // Arrange
            var repositoryMock = new Mock<IPalletRepository>();
            var pallets = CreatePallets(3, 1, 1, _ => CreateBox(DateOnly.MaxValue, 3, 1, 1), 1) // ־בתול = 6
                .Concat(CreatePallets(2, 1, 1, _ => CreateBox(DateOnly.MaxValue.AddDays(-1), 2, 1, 1), 1)) // ־בתול = 4
                .Concat(CreatePallets(1, 1, 1, _ => CreateBox(DateOnly.MaxValue.AddDays(-2), 1, 1, 1), 1)) // ־בתול = 2
                .Concat(CreatePallets(createBoxAction: _ => CreateBox(DateOnly.MinValue), count: 7));
            repositoryMock.Setup(rep => rep.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(pallets);
            var service = new PalletService(repositoryMock.Object);

            // Act
            var result = await service.GetTopByExpirationDateAsync();

            // Assert
            Assert.IsNotEmpty(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Volume, Is.EqualTo(2));
            Assert.That(result.Last().Volume, Is.EqualTo(6));
        }

        private static IEnumerable<IPallet> CreatePallets(double? width = null, double? depth = null, double? height = null, Func<double, IBox>? createBoxAction = null, int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                var pallet = new Pallet(
                    Guid.NewGuid(), 
                    width ?? Random.Shared.NextDouble() + 1, 
                    depth ?? Random.Shared.NextDouble() + 1, 
                    height ?? 0.2);
                if (createBoxAction != null)
                {
                    pallet.AddBox(createBoxAction.Invoke(i + 1));
                }
                
                yield return pallet;
            }
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
    }
}
