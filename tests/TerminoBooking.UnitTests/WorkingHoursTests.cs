using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class WorkingHoursTests
    {

        [Fact]
        public void Constructor_WithValidHours_CreatesWorkingHours()
        {
            //Arrange
            var start = new TimeOnly(8, 0);
            var end = new TimeOnly(16, 0);

            //Act
            var workingHours = new WorkingHours(DayOfWeek.Monday, start, end);

            //Assert
            Assert.Equal(DayOfWeek.Monday, workingHours.Day);
            Assert.Equal(start, workingHours.Start);
            Assert.Equal(end, workingHours.End);
        }

    }
}
