using Lesson24_Exam.Interfaces;
using Lesson24_Exam.ModelFilters;
using Lesson24_Exam.Models;
using Lesson24_Exam.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Lesson24_Exam.Controllers
{
    [Authorize]
    public class EventsController(IEvent events, UserManager<User> userManager) : Controller
    {
        private readonly IEvent _events = events;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<IActionResult> Index(Page<Event> page, EventFilter filter)
        {
            User user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Name).Value);
			if (filter != null)
			{
				filter.CategoriesSelectList = filter.CategoriesSelectList.Select(e => new SelectListItem(e.Text, e.Value, filter.SelectedCategories.Contains(int.Parse(e.Value))));
			}
			page.Filter = filter ?? new EventFilter();
            page.Filter.Original = _events.GetAsTracking();
			if (!User.IsInRole("admin"))
			{
				page.Filter.Original = page.Filter.Original.Where(e => e.IsPublic);
			}
			ViewBag.User = user;
            return View(page);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Event eventt)
        {
            if (eventt.DateTime < DateTime.Now)
            {
                ModelState.AddModelError(nameof(eventt.DateTime), "Event cannot be held in the past.");
            }
            if (!ModelState.IsValid)
            {
                return View(eventt);
            }
            await _events.AddAsync(eventt);
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Details(string id, Page<User> page, UserFilter filter)
		{
            Event? eventt = await _events.GetWithUsersAsync(id);
            if (eventt == null)
            {
                return NotFound();
            }
			page.Filter = filter ?? new UserFilter();
			page.Filter.Original = _userManager.Users;
			ViewBag.Event = eventt;
			return View(page);
		}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
			Event? eventt = await _events.GetAsync(id);
			if (eventt == null)
			{
				return NotFound();
			}
			return View(eventt);
		}

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event eventt)
        {
            if (eventt.DateTime < DateTime.Now)
            {
                ModelState.AddModelError(nameof(eventt.DateTime), "Event cannot be held in the past.");
            }
            if (!ModelState.IsValid)
            {
                return View(eventt);
            }
            await _events.UpdateAsync(eventt);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
		{
			Event? eventt = await _events.GetWithUsersAsync(id);
			if (eventt == null)
			{
				return NotFound();
			}
			return View(eventt);
		}

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Event eventt)
        {
            await _events.DeleteAsync(eventt);
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Register(string id)
		{
			Event? eventt = await _events.GetWithUsersAsync(id);
			if (eventt == null)
			{
				return NotFound();
			}
			return View(eventt);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Event eventt)
		{
			bool result = await _events.RegisterAsync(await _events.GetWithUsersAsync(eventt.Id.ToString()), User.FindFirst(ClaimTypes.Name));
            if (result)
			{
				return View("RegistrationSuccess");
			}
            return View("RegistrationError");
		}

		public async Task<IActionResult> Unregister(string id)
		{
			Event? eventt = await _events.GetWithUsersAsync(id);
			if (eventt == null)
			{
				return NotFound();
			}
			return View(eventt);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Unregister(Event eventt)
		{
			bool result = await _events.UnregisterAsync(await _events.GetWithUsersAsync(eventt.Id.ToString()), User.FindFirst(ClaimTypes.Name));
			if (result)
			{
				return View("UnregistrationSuccess");
			}
			return View("UnregistrationError");
		}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RegisterUser(string EventId, string UserName)
		{
			Event? eventt = await _events.GetWithUsersAsync(EventId);
			User? user = await _userManager.FindByNameAsync(UserName);
			if (eventt == null || user == null)
			{
				return NotFound();
			}
            ViewBag.EventId = EventId;
            return View(new EventUserTupleViewModel { Event = eventt, User = user });
		}

        [Authorize(Roles = "admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegisterUser(EventUserTupleViewModel tuple)
        {
            ViewBag.EventId = tuple.Event.Id;
            bool result = await _events.RegisterAsync(await _events.GetWithUsersAsync(tuple.Event.Id.ToString()), new Claim(ClaimTypes.Name, tuple.User.UserName));
			if (result)
			{
				return View("UserRegistrationSuccess");
			}
            return View("UserRegistrationError");
		}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UnregisterUser(string EventId, string UserName)
		{
			Event? eventt = await _events.GetWithUsersAsync(EventId);
			User? user = await _userManager.FindByNameAsync(UserName);
			if (eventt == null || user == null)
			{
				return NotFound();
			}
			ViewBag.EventId = EventId;
			return View(new EventUserTupleViewModel { Event = eventt, User = user });
		}

        [Authorize(Roles = "admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UnregisterUser(EventUserTupleViewModel tuple)
        {
            ViewBag.EventId = tuple.Event.Id;
            bool result = await _events.UnregisterAsync(await _events.GetWithUsersAsync(tuple.Event.Id.ToString()), new Claim(ClaimTypes.Name, tuple.User.UserName));
			if (result)
			{
				return View("UserUnregistrationSuccess");
			}
            return View("UserUnregistrationError");
		}
	}
}
