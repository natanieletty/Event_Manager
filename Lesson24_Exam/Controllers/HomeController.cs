using Microsoft.AspNetCore.Mvc;

namespace Lesson24_Exam.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                // ��� ������������ ������ �������� ������� ���
                ViewData["Message"] = "Welcome, Admin! Manage events and participants.";
            }
            else
            {
                // ��� ���������� ����������� �������� ������ �����������
                ViewData["Message"] = "Welcome to the event manager! Browse and join events.";
            }

            return View();
        }
    }
}
