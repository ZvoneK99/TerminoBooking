using System.Runtime.CompilerServices;
using TerminoBooking.Domain;

namespace TerminoBooking.UnitTests
{
    public class AppointmentTests
    {
        private static readonly DateTime Now = new DateTime(2030, 6, 1, 9, 0, 0);

        [Fact]
        public void Constructor_WithValidData_CreatesConfirmedAppointment()
        {
            //Arrange
            var service = new Service("Šišanje", TimeSpan.FromMinutes(30), 15m);
            var clientId = Guid.NewGuid();
            var staffMemberId = Guid.NewGuid();
            var start = Now.AddHours(1);

            //Act
            var appointment = new Appointment(clientId, staffMemberId, service, start, Now);

            //Assert
            Assert.Equal(clientId, appointment.ClientId);
            Assert.Equal(staffMemberId, appointment.StaffMemberId);
            Assert.Equal(service.Id, appointment.ServiceId);
            Assert.Equal(start, appointment.Start);
            Assert.Equal(start.AddMinutes(30), appointment.End);
            Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
        }

        [Fact]
        public void Constructor_WithStartInPast_Throws()
        {
            //Arrange
            var service = new Service("Feniranje", TimeSpan.FromMinutes(20), 10m);
            var clientId = Guid.NewGuid();
            var staffMemberId = Guid.NewGuid();
            var start = Now.AddHours(-1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Appointment(clientId, staffMemberId, service, start, Now));

        }

        private static Appointment CreateAppointment(int hour, int minute, int durationMinutes)
        {
            var service = new Service("Usluga", TimeSpan.FromMinutes(durationMinutes), 10m);
            var start = new DateTime(2030, 6, 1, hour, minute, 0);
            return new Appointment(Guid.NewGuid(), Guid.NewGuid(), service, start, Now);
        }

        [Theory]
        [InlineData(9, 30, 30, false)]   //A
        [InlineData(9, 45, 30, true)]    //B
        [InlineData(10, 10, 10, true)]   //C
        [InlineData(10, 15, 30, true)]   //D
        [InlineData(9, 30, 90, true)]    //E
        [InlineData(10, 30, 30, false)]  //F
        public void OverlapsWith_ReturnsExpectedResult(int hour, int minute, int durationMinutes, bool expected)
        {
            // Arrange
            var existing = CreateAppointment(10, 0, 30);   // postojeći: 10:00–10:30
            var newAppointment = CreateAppointment(hour, minute, durationMinutes);

            // Act
            var result = newAppointment.OverlapsWith(existing);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Cancel_MoreThan24HoursBefore_CancelsAppointment()
        {

            //Arrange
            var appointment = CreateAppointmentOnJune2At10();
            var now = new DateTime(2030, 6, 1, 9, 0, 0);

            //Act
            appointment.Cancel(now);

            //Assert
            Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        }

        [Fact]
        public void Cancel_Exactly24HoursBefore_CancelsAppointment()
        {

            //Arrange
            var appointment = CreateAppointmentOnJune2At10();
            var now = new DateTime(2030, 6, 1, 10, 0, 0);

            //Act
            appointment.Cancel(now);

            //Assert
            Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        }

        [Fact]
        public void Cancel_LessThan24HoursBefore_Throws()
        {

            //Arrange
            var appointment = CreateAppointmentOnJune2At10();
            var now = new DateTime(2030, 6, 1, 15, 0, 0);

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.Cancel(now));
        }

        [Fact]
        public void Cancel_AlreadyCancelled_Throws()
        {

            //Arrange
            var appointment = CreateAppointmentOnJune2At10();
            var now = new DateTime(2030, 6, 1, 9, 0, 0);
            appointment.Cancel(now);

            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => appointment.Cancel(now));
        }

        [Fact]
        public void OverlapsWith_WhenExistingIsCancelled_ReturnsFalse()
        {
            // Arrange
            var existing = CreateAppointment(10, 0, 30);        
            existing.Cancel(new DateTime(2030, 5, 30, 9, 0, 0)); 
            var newAppointment = CreateAppointment(10, 0, 30);  

            // Act
            var result = newAppointment.OverlapsWith(existing);

            // Assert
            Assert.False(result);
        }


        private static Appointment CreateAppointmentOnJune2At10()
        {
            var service = new Service("Usluga", TimeSpan.FromMinutes(30), 10m);
            var start = new DateTime(2030, 6, 2, 10, 0, 0);
            return new Appointment(Guid.NewGuid(), Guid.NewGuid(), service, start, Now);
        }
    }
}
