using Syncfusion.Maui.Calendar;

namespace WeekSelection
{
    public class CalendarBehavior : Behavior<SfCalendar>
    {
        private SfCalendar calendar;

        protected override void OnAttachedTo(SfCalendar bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable;
            this.calendar.SelectionChanged += Calendar_SelectionChanged;
        }

        private void Calendar_SelectionChanged(object sender, CalendarSelectionChangedEventArgs e)
        {
            int firstDayOfWeek = (int)DayOfWeek.Sunday % 7;
            int lastDayOfWeek = (firstDayOfWeek - 1) % 7;
            lastDayOfWeek = lastDayOfWeek < 0 ? 7 + lastDayOfWeek : lastDayOfWeek;
            CalendarDateRange range = (CalendarDateRange)e.NewValue;
            var startDate = (DateTime)range.StartDate;
            var endDate = (DateTime)(range.EndDate ?? range.StartDate);
            if (startDate.CompareTo(endDate) > 0)
            {
                var date = startDate;
                startDate = endDate;
                endDate = date;
            }

            int day1 = (int)startDate.DayOfWeek % 7;
            int day2 = (int)endDate.DayOfWeek % 7;
            var date1 = startDate.AddDays(firstDayOfWeek - day1);
            var date2 = endDate.AddDays(lastDayOfWeek - day2);
            if ((date1.Date != startDate) || (date2.Date != endDate))
            {
                this.calendar.SelectedDateRange = new CalendarDateRange(date1, date2);
            }
        }

        protected override void OnDetachingFrom(SfCalendar bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.calendar != null)
            {
                this.calendar.SelectionChanged -= Calendar_SelectionChanged;
            }

            this.calendar = null;
        }
    }
}
