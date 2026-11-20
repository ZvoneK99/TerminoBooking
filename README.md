# TerminoBooking

An appointment booking system for small service businesses such as hair salons, physiotherapy and chiropractic studios, built with C# and .NET 10.

> 🚧 **Work in progress.** The domain model and its business rules are in place and covered by unit tests. The API, database and frontend are next.

## What it does

Businesses offer **services** (for example a 30-minute haircut), their **staff members** have **working hours**, and **clients** book **appointments**. The system makes sure bookings follow the business rules, so a staff member can never be booked twice at the same time.

## Business rules implemented so far

| Rule | Where |
|---|---|
| A service has a name, a duration between 0 and 8 hours, and a non-negative price | `Service` |
| Working hours must start before they end | `WorkingHours` |
| Staff members and clients must have a name | `StaffMember`, `Client` |
| A client must have a valid email address; the phone number is optional | `Client` |
| An appointment cannot start in the past | `Appointment` |
| An appointment's end time is calculated from the service duration, never entered by hand | `Appointment` |
| Two appointments overlap only if they actually share time; back-to-back bookings (10:00–10:30 and 10:30–11:00) are allowed | `Appointment.OverlapsWith` |

### Design decisions

- **Entities protect their own rules.** Every class validates its data in the constructor and exposes properties with private setters, so an invalid object cannot exist.
- **Time is passed in, not read from the clock.** `Appointment` receives the current time as a parameter instead of calling `DateTime.Now`, which keeps the tests deterministic no matter when they run.
- **Values that can be derived are derived.** The appointment end time and its initial status are set by the appointment itself, not by the caller.

## Tech stack

- C# / .NET 10
- xUnit for unit tests

## Project structure

```
src/
  TerminoBooking.Domain/       Entities, value objects and business rules
tests/
  TerminoBooking.UnitTests/    Unit tests for the domain
```

## Running the tests

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet test
```

## Roadmap

- [x] Domain model: services, working hours, staff members, clients, appointments
- [x] Appointment validation and overlap detection
- [ ] Cancellation rules (up to 24 hours before the appointment)
- [ ] Time blocks for breaks and holidays
- [ ] Application layer: booking, cancelling and finding free slots
- [ ] PostgreSQL with EF Core, running in Docker
- [ ] REST API with JWT authentication and roles
- [ ] Multiple businesses with isolated data (multi-tenancy)
- [ ] Safe handling of simultaneous bookings for the same slot
- [ ] Email reminders the day before an appointment
- [ ] Integration tests and CI with GitHub Actions
- [ ] Google Calendar sync
- [ ] Web frontend
