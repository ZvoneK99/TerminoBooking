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

        public Appointment(Guid clientId, Guid staffMemberId, Service service, DateTime start, DateTime now)
        {
            if (start < now)
            {
                throw new ArgumentException("Početak termina ne može biti u prošlosti!", nameof(start));
            }

            Id = Guid.NewGuid();
            ClientId = clientId;
            StaffMemberId = staffMemberId;
            ServiceId = service.Id;
            Start = start;
            End = start + service.Duration;
            Status = AppointmentStatus.Confirmed;
        }
        public bool OverlapsWith(Appointment other)
        {
            if (Status == AppointmentStatus.Cancelled || other.Status == AppointmentStatus.Cancelled)
            {
                return false;
            }

            return Start < other.End && other.Start < End;
        }

        public void Cancel(DateTime now)
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Može se otkazati samo potvrđen termin!");

            if (now > Start.AddHours(-24))
            {
                throw new InvalidOperationException("Termin se može otkazati najkasnije 24 sata prije početka!");
            }

            Status = AppointmentStatus.Cancelled;
        }

    }
}