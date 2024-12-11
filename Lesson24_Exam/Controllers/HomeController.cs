using Microsoft.AspNetCore.Mvc;

namespace Lesson24_Exam.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                // Для адміністратора просто передаємо текстові дані
                ViewData["Message"] = "Welcome, Admin! Manage events and participants.";
            }
            else
            {
                // Для звичайного користувача передаємо базове повідомлення
                ViewData["Message"] = "Welcome to the event manager! Browse and join events.";
            }

            return View();
        }
    }
}
