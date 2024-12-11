using Lesson24_Exam.Interfaces;
using Lesson24_Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson24_Exam.ModelFilters
{
    public class UserFilter : IFilter<User>
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public IQueryable<User> Original { get; set; } = new List<User>().AsQueryable();
        public IQueryable<User> Filtered
        {
            get
            {
                IQueryable<User> filtered = Original;
                if (Login != null)
                {
                    filtered = filtered.Where(e => EF.Functions.Like(e.UserName, $"%{Login}%"));
                }
                if (Email != null)
                {
                    filtered = filtered.Where(e => EF.Functions.Like(e.Email, $"%{Email}%"));
                }
                if (Phone != null)
                {
                    filtered = filtered.Where(e => EF.Functions.Like(e.PhoneNumber, $"%{Phone}%"));
                }
                return filtered;
            }
        }
		public bool IsActive =>
			Login != null
			|| Email != null
			|| Phone != null;
	}
}
