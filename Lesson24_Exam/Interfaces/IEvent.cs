using Lesson24_Exam.Models;
using System.Security.Claims;

namespace Lesson24_Exam.Interfaces
{
    public interface IEvent
    {
        public IQueryable<Event> GetAsTracking();
        public Task<List<Event>> GetAsync();
		public Task<Event?> GetAsync(string id);
		public Task<List<Event>> GetWithUsersAsync();
		public Task<Event?> GetWithUsersAsync(string id);
        public Task<Event> AddAsync(Event eventt);
        public Task<Event> UpdateAsync(Event eventt);
        public Task<Event> DeleteAsync(Event eventt);
		public Task<bool> RegisterAsync(Event eventt, Claim username);
		public Task<bool> UnregisterAsync(Event eventt, Claim username);
		public Task<bool> IsRegisteredAsync(Event eventt, Claim username);
	}
}
