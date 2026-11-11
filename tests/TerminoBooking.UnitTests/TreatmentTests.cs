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

    }
}
