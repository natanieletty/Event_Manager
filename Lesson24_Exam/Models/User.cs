using Microsoft.AspNetCore.Identity;

namespace Lesson24_Exam.Models
{
    public class User : IdentityUser
    {
        public List<Event> Events { get; set; }
    }
}
