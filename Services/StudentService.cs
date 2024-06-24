using System.Data;
using EasyCheck.Models;
using Dapper;

namespace EasyCheck.Services
{
    public class StudentService
    {
        private readonly IDbConnection _dbConnection;

        public StudentService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var sql = @"
                SELECT
                    id AS Id,
                    full_name AS FullName,
                    dob AS DateOfBirth,
                    student_code AS StudentCode,
                    parent_contact AS ParentContact,
                    class_id AS ClassId
                FROM students";
            return await _dbConnection.QueryAsync<Student>(sql);
        }
    }
}