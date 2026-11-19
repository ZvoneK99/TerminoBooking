
namespace TerminoBooking.Domain
{
    public class Service
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public TimeSpan Duration { get; private set; }
        public decimal Price { get; private set; }

        public Service(string name, TimeSpan duration, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Naziv usluge je obavezan.", nameof(name));
            if (price < 0)
                throw new ArgumentException("Cijena ne smije biti negativna.", nameof(price));
            if (duration <= TimeSpan.Zero || duration > TimeSpan.FromHours(8))
                throw new ArgumentException("Trajanje usluge mora biti veće od 0 i najviše 8 sati.", nameof(duration));

            Id = Guid.NewGuid();
            Name = name;
            Duration = duration;
            Price = price;
        }

    }
}
