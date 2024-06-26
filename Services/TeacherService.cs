using System.Data;
using EasyCheck.Models;
using Dapper;

namespace EasyCheck.Services;
public class TeacherService
{
    private readonly IDbConnection _dbConnection;
    public TeacherService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Teacher> GetTeacherAsyncByTeacherCode(string teacherCode)
    {
        string sql = @"
            SELECT 
                t.Id, t.FullName, t.TeacherCode,
                sc.Id, sc.ClassName, sc.TeacherId
            FROM teachers t
            LEFT JOIN school_classes sc
            ON t.Id = sc.TeacherId
            WHERE t.TeacherCode = @TeacherCode
        ";
        var teacherDictionary = new Dictionary<int, Teacher>();

        var result = await _dbConnection.QueryAsync<Teacher, SchoolClass, Teacher>(
            sql,
            (teacher, schoolClass) => {
                if(!teacherDictionary.TryGetValue(teacher.Id, out var currentTeacher))
                {
                    currentTeacher = teacher;
                    teacherDictionary.Add(currentTeacher.Id, currentTeacher);
                }
                if(schoolClass != null)
                {
                    currentTeacher.SchoolClass = schoolClass;
                }
                return currentTeacher;
            },
            new { TeacherCode = teacherCode }
        );
        return result.FirstOrDefault();
    }
}