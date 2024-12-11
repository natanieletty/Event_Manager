using Lesson24_Exam.Data;
using Lesson24_Exam.Interfaces;
using Lesson24_Exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lesson24_Exam.Repositories
{
    public class EventRepository(ApplicationContext context, UserManager<User> userManager) : IEvent
    {
        private readonly ApplicationContext _context = context;
        private readonly UserManager<User> _userManager = userManager;
        public async Task<Event> AddAsync(Event eventt)
        {
            _context.Events.Add(eventt);
            await _context.SaveChangesAsync();
            return eventt;
        }
        public async Task<Event> DeleteAsync(Event eventt)
        {
            _context.Events.Remove(eventt);
            await _context.SaveChangesAsync();
            return eventt;
        }
        public IQueryable<Event> GetAsTracking()
        {
            return _context.Events.Include(e => e.Users);
        }
        public async Task<List<Event>> GetAsync()
        {
            return await _context.Events.ToListAsync();
        }
        public async Task<Event?> GetAsync(string id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id.ToString() == id);
        }
        public async Task<List<Event>> GetWithUsersAsync()
        {
            return await _context.Events.Include(e => e.Users).ToListAsync();
        }
        public async Task<Event?> GetWithUsersAsync(string id)
        {
            return await _context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id.ToString() == id);
        }
		public async Task<bool> IsRegisteredAsync(Event eventt, Claim username)
		{
			User? user = await _userManager.FindByNameAsync(username.Value);
			if (user == null || !eventt.Users.Contains(user))
			{
				return false;
			}
            return true;
		}
		public async Task<bool> RegisterAsync(Event eventt, Claim username)
        {
            User? user = await _userManager.FindByNameAsync(username.Value);
            if (user == null || eventt.Users.Contains(user) || eventt.MaxParticipants <= eventt.Users.Count)
            {
                return false;
            }
            eventt.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
		}
		public async Task<bool> UnregisterAsync(Event eventt, Claim username)
		{
			User? user = await _userManager.FindByNameAsync(username.Value);
			if (user == null || !eventt.Users.Contains(user))
			{
				return false;
			}
			eventt.Users.Remove(user);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<Event> UpdateAsync(Event eventt)
        {
            _context.Events.Update(eventt);
            await _context.SaveChangesAsync();
            return eventt;
        }
    }
}
