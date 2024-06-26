using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyCheck.Models;
using EasyCheck.Services;
using EasyCheck.Extensions;

namespace EasyCheck.Controllers;

public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentService _studentService;
    private readonly AttendanceService _attendanceService;

    public StudentController(ILogger<StudentController> logger, StudentService studentService, AttendanceService attendanceService)
    {
        _logger = logger;
        _studentService = studentService;
        _attendanceService = attendanceService;
    }

    public async Task<IActionResult> Index()
    {
        var classId = HttpContext.Session.GetInt32("ClassId");
        if (classId.HasValue)
        {
            var students = await _studentService.GetStudentsByClassIdAsync(classId.Value);
            return View(students);
        }
        else
        {
            return NotFound();
        }
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
