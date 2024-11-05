using StorageApp;

namespace StorageAppTests
{
    [TestFixture]
    public class BoxTests
    {
        /// <summary>
        /// � ������� ������ ���� ������ ���� �������� ��� ���� ������������. ���� ������� ���� ������������, �� ���� �������� ����������� �� ���� ������������ ���� 100 ����.
        /// </summary>
        /// <param name="haveBoxes">�� ������� ���� �������.</param>
        /// <returns><see cref="Task"></returns>
        [Test]
        [TestCaseSource(nameof(TestCases))]
        public async Task ExpirationDateTest(DateOnly? productionDate, DateOnly? expirationDate, bool isValidDate)
        {
            // Arrange
            // Act
            var action = () => CreateBox(productionDate, expirationDate);

            // Assert
            if (isValidDate)
            {
                Assert.That(action, Throws.Nothing);
                if (expirationDate == null)
                {
                    Assert.That(action.Invoke().ExpirationDate, Is.EqualTo(DateOnly.MinValue.AddDays(100)));
                }
            }
            else
            {
                Assert.That(action, Throws.TypeOf<ArgumentException>());
            }
        }

        /// <summary>
        /// ����� ������� ����������� ��� ������������ ������, ������ � �������.
        /// </summary>
        /// <returns><see cref="Task"></returns>
        [Test]
        public async Task VolumeTest()
        {
            // Arrange
            var box = CreateBox(null, DateOnly.MaxValue, 3, 3, 0.1);

            // Act
            // Assert
            Assert.That(box.Volume, Is.EqualTo(0.9).Within(0.1));
        }

        private static IBox CreateBox(DateOnly? prodDate, DateOnly? expDate, double? width = null, double? depth = null, double? height = null, double? weight = null)
        {
            return new Box(
                    Guid.NewGuid(),
                    width ?? Random.Shared.NextDouble(),
                    depth ?? Random.Shared.NextDouble(),
                    height ?? Random.Shared.NextDouble(),
                    weight ?? Random.Shared.NextDouble() * 10,
                    prodDate,
                    expDate);
        }

        private static object[] TestCases =
        {
            new object[] { null, DateOnly.MaxValue, true },
            new object[] { DateOnly.MinValue, null, true },
            new object[] { DateOnly.MinValue, DateOnly.MaxValue, true },
            new object[] { null, null, false }
        };
    }
}
