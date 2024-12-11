using Lesson24_Exam.Models;

namespace Lesson24_Exam.ViewModels
{
	public class EventUserViewModel(Event eventt, User user)
	{
		public User User { get; set; } = user;
		public bool IsRegistered { get; set; } = eventt.Users.Contains(user);
	}
}
