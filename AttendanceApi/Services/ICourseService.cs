using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;

namespace AttendanceApi.Service
{
    public interface ICourseService
    {
        public void CreateCourse(CourseRequest courseRequest);
        //public void UpdateCourse(int id, CourseRequest courseRequest);
        //public void DeleteCourse(int id);
        //public CourseResponse GetCourse(int id);
        //public List<CourseResponse> GetAllCourses();

        public List<ScheduleResponse> getListSchedule();

        public List<ScheduleResponse> getSchedule(int studentId);

        public List<CourseResponse> getListCourse();

        public void UpdateCourse(int id, CourseRequest courseRequest);

        public CourseResponse getCourse(int id);

        List<StudentScheduleResponse> GetStudentsByScheduleId(int scheduleId);
    }
}
