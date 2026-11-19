using System.Net.Mail;

namespace TerminoBooking.Domain
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Telephone { get; private set; }

        public Client(string name, string email, string? telephone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ime je obavezno.", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email je obavezan.", nameof(email));

            email = email.Trim();
            if (!MailAddress.TryCreate(email, out var parsed)
                || !string.Equals(parsed.Address, email, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("E-mail nije ispravan.", nameof(email));

            Id = Guid.NewGuid();
            Name = name.Trim();
            Email = email;
            Telephone = string.IsNullOrWhiteSpace(telephone) ? null : telephone.Trim();
        }
    }
}
