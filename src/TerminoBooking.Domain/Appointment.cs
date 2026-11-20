namespace TerminoBooking.Domain
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid StaffMemberId { get; private set; }
        public Guid ServiceId { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public AppointmentStatus Status { get; private set; }

        public Appointment(Guid clientId, Guid staffMemberId, Service service, DateTime start)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            StaffMemberId = staffMemberId;
            ServiceId = service.Id;
            Start = start;
            End = start + service.Duration;
            Status = AppointmentStatus.Confirmed;
        }
    }
}