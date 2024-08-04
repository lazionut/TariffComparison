using FluentAssertions;
using TariffComparison.Domain.Models;

namespace TariffComparison.UnitTests.Domain
{
    public class TariffTests
    {
        [Theory]
        [InlineData(3500, 830)]
        [InlineData(4500, 1050)]
        public void BasicTariff_CalculateAnnualCosts_ShouldReturnExpectedCost(int consumption, double expected)
        {
            // Arrange
            var tariff = new BasicTariffCalculator(5, 22);

            // Act
            var result = tariff.CalculateAnnualCosts(consumption);

            // Assert
            expected.Should().Be(result);
        }

        [Theory]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        public void PackagedTariff_CalculateAnnualCosts_ShouldReturnExpectedCost(int consumption, double expected)
        {
            // Arrange
            var tariff = new PackagedTariffCalculator(800, 4000, 30);

            // Act
            var result = tariff.CalculateAnnualCosts(consumption);

            // Assert
            expected.Should().Be(result);
        }
    }
}