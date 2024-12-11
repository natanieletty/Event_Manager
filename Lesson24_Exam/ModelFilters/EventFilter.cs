using Lesson24_Exam.Interfaces;
using Lesson24_Exam.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lesson24_Exam.ModelFilters
{
    public class EventFilter : IFilter<Event>
    {
        public string? Title { get; set; }
        public DateTime? DateTimeFrom { get; set; }
        public DateTime? DateTimeTo { get; set; }
        public int? MinParticipants { get; set; }
        public int? MaxParticipants { get; set; }
        public int? MinMaxParticipants { get; set; }
        public int? MaxMaxParticipants { get; set; }
        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; } = Enum.GetValues<EventType>().Select(e => new SelectListItem(e.ToString(), ((int)e).ToString()));
        public int[] SelectedCategories { get; set; } = [];
        public int IsPublic { get; set; } = 0;
        public IQueryable<Event> Original { get; set; } = new List<Event>().AsQueryable();
        public IQueryable<Event> Filtered
        {
            get
            {
                IQueryable<Event> filtered = Original;
                if (Title != null)
                {
                    filtered = filtered.Where(e => EF.Functions.Like(e.Title, $"%{Title}%"));
                }
                if (DateTimeFrom != null)
                {
                    filtered = filtered.Where(e => e.DateTime >= DateTimeFrom);
                }
                if (DateTimeTo != null)
                {
                    filtered = filtered.Where(e => e.DateTime <= DateTimeTo);
                }
                if (MinParticipants != null)
                {
                    filtered = filtered.Where(e => e.Users.Count >= MinParticipants);
                }
                if (MaxParticipants != null)
                {
                    filtered = filtered.Where(e => e.Users.Count <= MaxParticipants);
                }
                if (MinMaxParticipants != null)
                {
                    filtered = filtered.Where(e => e.MaxParticipants >= MinMaxParticipants);
                }
                if (MaxMaxParticipants != null)
                {
                    filtered = filtered.Where(e => e.MaxParticipants <= MaxMaxParticipants);
                }
                if (SelectedCategories.Any())
                {
                    filtered = filtered.Where(e => SelectedCategories.Contains((int)e.Category));
                }
                if (IsPublic != 0)
                {
                    Expression<Func<Event, bool>> predicate = IsPublic switch
                    {
                        1 => e => e.IsPublic,
                        -1 => e => !e.IsPublic,
                        _ => e => true
                    };
                    filtered = filtered.Where(predicate);
                }
                return filtered;
            }
        }
		public bool IsActive =>
            Title != null
            || DateTimeFrom != null
            || DateTimeTo != null
            || MinParticipants != null
			|| MaxParticipants != null
			|| MinMaxParticipants != null
			|| MaxMaxParticipants != null
			|| CategoriesSelectList.Any(e => e.Selected);
	}
}
