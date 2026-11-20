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


    }
}
