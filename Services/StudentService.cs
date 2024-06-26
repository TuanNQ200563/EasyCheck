using System.Data;
using EasyCheck.Models;
using Dapper;

namespace EasyCheck.Services;
public class StudentService
{
    private readonly IDbConnection _dbConnection;

    public StudentService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<Student>> GetStudentsByClassIdAsync(int classId)
    {
        var sql = @"
            SELECT *
            FROM students
            WHERE SchoolClassId = @ClassId
        ";
        var students = await _dbConnection.QueryAsync<Student>(sql, new { ClassId = classId });
        return students.AsList();
    }

    public async Task<Student> GetStudentByStudentCodeAsync(string studentCode)
    {
        var sql = @"
            SELECT * 
            FROM students
            WHERE StudentCode = @StudentCode
        ";
        var student = await _dbConnection.QueryAsync<Student>(sql, new { StudentCode = studentCode });
        return student.FirstOrDefault();
    }
}
