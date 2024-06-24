using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyCheck.Models;
using EasyCheck.Services;

namespace EasyCheck.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StudentService _studentService;

    public HomeController(ILogger<HomeController> logger, StudentService studentService)
    {
        _logger = logger;
        _studentService = studentService;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _studentService.GetStudentsAsync();
        Console.WriteLine(students.Count());
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
