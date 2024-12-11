using Lesson24_Exam.Models;

namespace Lesson24_Exam.ViewModels
{
	public class UserEventViewModel(User user, Event eventt)
	{
		public Event Event { get; set; } = eventt;
		public bool IsRegistered { get; set; } = eventt.Users.Contains(user);
	}
}
