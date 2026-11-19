
namespace TerminoBooking.Domain
{
    public record WorkingHours
    {
        public DayOfWeek Day { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }

        public WorkingHours(DayOfWeek day, TimeOnly start, TimeOnly end) { 
             if ( start>=end ){
                throw new ArgumentException("Kraj ne može biti prije početka termina!");
            }

            Day = day;
            Start = start;
            End = end;
        }
    }
}
