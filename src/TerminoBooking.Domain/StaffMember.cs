
namespace TerminoBooking.Domain
{
    public class StaffMember
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public StaffMember(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ime zaposlenika je obavezno.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
