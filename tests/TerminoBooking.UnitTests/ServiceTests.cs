using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class TreatmentTests
    {
        [Fact]
        public void Constructor_WithValidData_CreatesTreatment()
        {
            //Arrange
            var duration = TimeSpan.FromMinutes(45);

            //Act
            var treatment = new Treatment("Prvi pregled", duration, 50m);

            //Assert
            Assert.Equal("Prvi pregled", treatment.Name);
            Assert.Equal(duration, treatment.Duration);
            Assert.Equal(50m, treatment.Price);
            Assert.NotEqual(Guid.Empty, treatment.Id);
        }

        [Fact]
        public void Constructor_WithNegativePrice_Throws()
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(45);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Treatment("Prvi pregled", duration, -10m));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_Throws(string? name)
        {
            // Arrange
            var duration = TimeSpan.FromMinutes(45);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Treatment(name!, duration, 50m));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-15)]
        [InlineData(540)]
        public void Constructor_WithInvalidDuration_Throws(int minutes)
        {
            var duration = TimeSpan.FromMinutes(minutes);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Treatment("Prvi pregled", duration, 50m));
        }


    }
}
