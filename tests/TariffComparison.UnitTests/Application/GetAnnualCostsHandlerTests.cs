using FluentAssertions;
using NSubstitute;
using TariffComparison.Application.Interfaces;
using TariffComparison.Application.Queries;
using TariffComparison.Application.QueryHandlers;
using TariffComparison.Domain.Entities;
using TariffComparison.Domain.Enums;
using TariffComparison.Domain.Helpers;

namespace TariffComparison.UnitTests.Application
{
    public class GetAnnualCostsHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly TariffCalculatorGenerator _tariffCalculatorFactory;

        public GetAnnualCostsHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _tariffCalculatorFactory = Substitute.For<TariffCalculatorGenerator>();
        }

        [Fact]
        public async Task GetAllTariffs_WhenElectricityTariffNameExist_ShouldReturnAllNamesEqual()
        {
            // Arrange
            var handler = new GetAnnualCostsHandler(_unitOfWorkMock, _tariffCalculatorFactory);
            var query = new GetAnnualCosts();

            var expectedResult = new List<ElectricityTariff>
            {
                new ElectricityTariff { Name = "Product A", Type = TariffType.BasicElectricityTariff, BaseCost = 5, AdditionalKwhCost = 22 },
                new ElectricityTariff { Name = "Product B", Type = TariffType.PackagedTariff, IncludedKwh = 4000, BaseCost = 800, AdditionalKwhCost = 30 }
            };

            _unitOfWorkMock.TariffRepository.GetAll().Returns(expectedResult);

            // Act
            var actualResult = await handler.Handle(query, default);

            // Assert
            Assert.Equal(expectedResult.Count, actualResult.Count);

            for (int index = 0; index < expectedResult.Count; ++index)
            {
                expectedResult[index].Name.Should().BeEquivalentTo(actualResult[index].TariffName);
            }
        }

        [Fact]
        public async Task GetAllTariffs_WhenNoElectricityTariffsExist_ShouldReturnEmptyList()
        {
            // Arrange
            var handler = new GetAnnualCostsHandler(_unitOfWorkMock, _tariffCalculatorFactory);
            var query = new GetAnnualCosts();

            _unitOfWorkMock.TariffRepository.GetAll()
                .Returns(Enumerable.Empty<ElectricityTariff>().ToList());

            // Act
            var actualResult = await handler.Handle(query, default);

            // Assert
            actualResult.Should().BeEmpty();
        }
    }
}