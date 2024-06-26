using Microsoft.AspNetCore.Mvc;
using EasyCheck.Services;
using EasyCheck.Extensions;

namespace EasyCheck.Controllers;

public class TestController : Controller
{
    private readonly StudentService _studentService;
    private readonly AttendanceService _attendanceService;
    private readonly TeacherService _teacherService;

    public TestController(StudentService studentService, AttendanceService attendanceService, TeacherService teacherService)
    {
        _studentService = studentService;
        _attendanceService = attendanceService;
        _teacherService = teacherService;
    }

    public async Task<IActionResult> Index()
    {
        var teacher = await _teacherService.GetTeacherAsyncByTeacherCode("GV20200000");
        if(teacher != null)
        {
            HttpContext.Session.SetString("Fullname", teacher.FullName);
            HttpContext.Session.SetInt32("ClassId", teacher.SchoolClass.Id);
            return View(teacher);
        }
        else
        {
            return NotFound();
        }

    }
}