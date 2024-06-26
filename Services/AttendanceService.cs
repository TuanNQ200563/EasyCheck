using EasyCheck.Models;
using EasyCheck.Contexts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EasyCheck.Services
{
    public class AttendanceService
    {
        private readonly IMongoCollection<AttendanceRecord> _attendanceCollection;

        public AttendanceService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _attendanceCollection = mongoDatabase.GetCollection<AttendanceRecord>("attendances");
        }

        public async Task<List<AttendanceRecord>> GetAsync() => await _attendanceCollection.Find(_ => true).ToListAsync();
        public async Task<AttendanceRecord?> GetAsync(string id) => await _attendanceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<AttendanceRecord>> GetAttendanceHistoryByStudentCode(string studentCode)
        {
            var filter = Builders<AttendanceRecord>.Filter.Eq(ar => ar.StudentCode, studentCode);
            var sort = Builders<AttendanceRecord>.Sort.Descending(ar => ar.CheckInTime);
            var attendanceHistory = await _attendanceCollection.Find(filter).Sort(sort).ToListAsync();
            
            return attendanceHistory;
        }

    }
}