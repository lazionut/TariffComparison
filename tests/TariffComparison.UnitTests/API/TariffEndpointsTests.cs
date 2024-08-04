using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using TariffComparison.API.Endpoints;

// really tricky to test minimal APIs even this much, honestly I was just curious
// how they work, so the following tests are just proof of concept and it seems they
// aren't production ready yet (?)

namespace TariffComparison.UnitTests.API
{
    public class TariffEndpointsTests
    {
        private readonly IMediator _mediatorMock;

        public TariffEndpointsTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
        }

        [Fact]
        public void GetCalculationResults_ValidConsumption_ReturnsIResult()
        {
            // Arrange
            var consumption = 20;

            // Act
            var result = TariffEndpoints.GetComparedTariffsSorted(_mediatorMock, consumption);

            // Assert
            result.Should().BeOfType<Task<IResult>>();
        }

        [Fact]
        public void GetCalculationResults_InvalidConsumption_ReturnsIResult()
        {
            // Arrange
            var consumption = -1;

            // Act
            var result = TariffEndpoints.GetComparedTariffsSorted(_mediatorMock, consumption);

            // Assert
            result.Should().BeOfType<Task<IResult>>();
        }
    }
}