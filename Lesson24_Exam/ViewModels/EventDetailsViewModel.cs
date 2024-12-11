using Lesson24_Exam.Models;

namespace Lesson24_Exam.ViewModels
{
	public class EventDetailsViewModel(Event eventt, Page<User> usersPage)
	{
		public Event Event { get; set; } = eventt;
		public Page<User> UsersPage { get; set; } = usersPage;
	}
}
