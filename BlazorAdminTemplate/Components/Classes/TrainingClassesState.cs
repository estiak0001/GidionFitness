using BlazorAdminTemplate.Domain.Entities;
using Heron.MudCalendar;
using MudBlazor;

namespace BlazorAdminTemplate.Components.Classes
{
    public class TrainingClassesState
    {
        public DateTime? SelectedDate { get; set; }
        public IReadOnlyCollection<FilterItem> SelectedFilters { get; set; } = new HashSet<FilterItem>();
        public List<TrainingClasses> AllClasses { get; set; } = new();

        public List<TrainingClasses> FilteredClasses { get; private set; } = new();
        public List<BookedClasses> BookedClasses { get; private set; } = new();
        public HashSet<string> BookedClassGuids => new(BookedClasses.Select(b => b.ClassGUID));
        public List<ClassTypes>? AvailableClassTypes { get; set; }
        public List<GuestTrainingClasses>? AvailableOrganizations { get; set; }

        // Calendar view state
        public CalendarView CurrentCalendarView { get; set; } = CalendarView.Month;
        public DateRange? CurrentViewRange { get; set; }

        public event Action? OnChange;

        public void UpdateFilteredClasses(List<TrainingClasses> classes)
        {
            FilteredClasses = classes ?? new();
            NotifyStateChanged();
        }

        public void UpdateBookedClasses(List<BookedClasses> booked)
        {
            BookedClasses = booked ?? new();
            NotifyStateChanged();
        }

        public void SetSelectedDate(DateTime? date)
        {
            SelectedDate = date;
            NotifyStateChanged();
        }

        public void SetSelectedFilters(IReadOnlyCollection<FilterItem> filters)
        {
            SelectedFilters = filters ?? new HashSet<FilterItem>();
            NotifyStateChanged();
        }

        public void SetCalendarView(CalendarView view)
        {
            CurrentCalendarView = view;
            NotifyStateChanged();
        }

        public void SetViewRange(DateRange? range)
        {
            CurrentViewRange = range;
            NotifyStateChanged();
        }

        public void ResetFilters()
        {
            SelectedDate = null;
            SelectedFilters = new HashSet<FilterItem>();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
