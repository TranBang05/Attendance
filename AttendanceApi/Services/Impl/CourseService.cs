using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Service.Impl
{
    public class CourseService : ICourseService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public CourseService(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void CreateCourse(CourseRequest courseRequest)
        {
            bool courseExists = _context.Courses.Any(c => c.Code == courseRequest.Code);
            if (courseExists)
            {
                throw new InvalidOperationException($"Ma code cua {courseRequest.Code} da ton tai.");
            }

            var course = _mapper.Map<Course>(courseRequest);
            course.Schedules = new List<Schedule>();
            course.StudentCourses = new List<StudentCourse>();

            DateTime startDate = courseRequest.StartDate;
            DateTime endDate = courseRequest.EndDate;

            var timeSlots = ParseTimeSlots(courseRequest.TimeSlot);
           
            var existingStudentIds = _context.Students
                                             .Where(s => courseRequest.StudentIds.Contains(s.Id))
                                             .Select(s => s.Id)
                                             .ToList();

            while (startDate <= endDate)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var scheduleDate = GetNextWeekday(startDate, timeSlot.DayOfWeek);

                    if (scheduleDate <= endDate)
                    {
                        var schedule = new Schedule
                        {
                            Date = scheduleDate,
                            Slot = timeSlot.Slot,
                            TeacherId = courseRequest.TeacherId
                        };
                        course.Schedules.Add(schedule);

                        foreach (var studentId in existingStudentIds)
                        {
                            var studentSchedule = new StudentSchedule
                            {
                                StudentId = studentId,
                                Schedule = schedule,
                                Status = AttendanceStatus.NotYet
                            };

                            if (schedule.StudentSchedules == null)
                            {
                                schedule.StudentSchedules = new List<StudentSchedule>();
                            }
                            schedule.StudentSchedules.Add(studentSchedule);
                        }
                    }
                }

                startDate = startDate.AddDays(7);
            }

            course.Schedules = course.Schedules.OrderBy(s => s.Date).ToList();

            foreach (var studentId in existingStudentIds)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    Course = course
                };
                course.StudentCourses.Add(studentCourse);
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
        }


        private List<(DayOfWeek DayOfWeek, string Slot)> ParseTimeSlots(string timeSlots)
        {
            var parsedSlots = new List<(DayOfWeek, string)>();

            if (string.IsNullOrEmpty(timeSlots) || timeSlots.Length < 3)
            {
                throw new ArgumentException("Invalid timeSlots format.");
            }

           
            if(timeSlots !="A24" && timeSlots !="A35" && timeSlots!="A46" && timeSlots!="A52" && timeSlots != "A63")
            {
                throw new ArgumentException("Invalid timeSlots format.");
            }
           
            for (int i = 1; i < timeSlots.Length; i++)
            {
                char dayChar = timeSlots[i];
                string slot = $"Slot{i}";

                switch (dayChar)
                {
                    case '2':
                        parsedSlots.Add((DayOfWeek.Monday, slot));
                        break;
                    case '3':
                        parsedSlots.Add((DayOfWeek.Tuesday, slot));
                        break;
                    case '4':
                        parsedSlots.Add((DayOfWeek.Wednesday, slot));
                        break;
                    case '5':
                        parsedSlots.Add((DayOfWeek.Thursday, slot));
                        break;
                    case '6':
                        parsedSlots.Add((DayOfWeek.Friday, slot));
                        break;
                    default:
                        throw new ArgumentException($"Invalid day character: {dayChar}");
                }
            }

            return parsedSlots;
        }



        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public List<ScheduleResponse> getListSchedule()
        {
            try
            {
                
                var schedules = _context.Schedules
                    .Include(s => s.Teacher)
                    .Include(s => s.Course)
                    .ToList();

                
                var scheduleResponses = _mapper.Map<List<ScheduleResponse>>(schedules);

                return scheduleResponses;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Lỗi khi lấy danh sách môn học: " + ex.Message);
            }
        }

        

        public List<ScheduleResponse> getSchedule(int studentId)
        {
            var schedules = _context.StudentSchedules
               .Where(ss => ss.StudentId == studentId)
               .Include(ss => ss.Schedule)
                   .ThenInclude(sch => sch.Course)
               .Include(ss => ss.Schedule)
                   .ThenInclude(sch => sch.Teacher)
               .ToList();

            var scheduleResponses = _mapper.Map<List<ScheduleResponse>>(schedules.Select(ss => ss.Schedule));
            return scheduleResponses;
        }

        public List<CourseResponse> getListCourse()
        {
            try
            {

                var schedules = _context.Courses
                    .Include(s => s.Subject)
                    .ToList();


                var scheduleResponses = _mapper.Map<List<CourseResponse>>(schedules);

                return scheduleResponses;
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi khi lấy danh sách Course: " + ex.Message);
            }
        }

        public void UpdateCourse(int id, CourseRequest courseRequest)
        {
            var course = _context.Courses.Include(c => c.Schedules).Include(c => c.StudentCourses).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                throw new InvalidOperationException("Khóa học không tồn tại.");
            }

            bool courseExists = _context.Courses.Any(c => c.Code == courseRequest.Code && c.Id != id);
            if (courseExists)
            {
                throw new InvalidOperationException($"Mã code của {courseRequest.Code} đã tồn tại.");
            }

            _mapper.Map(courseRequest, course);
            course.Schedules.Clear();
            course.StudentCourses.Clear();

            DateTime startDate = courseRequest.StartDate;
            DateTime endDate = courseRequest.EndDate;
            var timeSlots = ParseTimeSlots(courseRequest.TimeSlot);
            var existingStudentIds = _context.Students
                                              .Where(s => courseRequest.StudentIds.Contains(s.Id))
                                              .Select(s => s.Id)
                                              .ToList();

            while (startDate <= endDate)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var scheduleDate = GetNextWeekday(startDate, timeSlot.DayOfWeek);

                    if (scheduleDate <= endDate)
                    {
                        var schedule = new Schedule
                        {
                            Date = scheduleDate,
                            Slot = timeSlot.Slot,
                            TeacherId = courseRequest.TeacherId
                        };
                        course.Schedules.Add(schedule);

                        foreach (var studentId in existingStudentIds)
                        {
                            var studentSchedule = new StudentSchedule
                            {
                                StudentId = studentId,
                                Schedule = schedule,
                                Status = AttendanceStatus.NotYet
                            };

                            if (schedule.StudentSchedules == null)
                            {
                                schedule.StudentSchedules = new List<StudentSchedule>();
                            }
                            schedule.StudentSchedules.Add(studentSchedule);
                        }
                    }
                }

                startDate = startDate.AddDays(7);
            }

            course.Schedules = course.Schedules.OrderBy(s => s.Date).ToList();

            foreach (var studentId in existingStudentIds)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    Course = course
                };
                course.StudentCourses.Add(studentCourse);
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public CourseResponse getCourse(int id)
        {

            try
            {
                var course = _context.Courses
                           .Include(c => c.Subject) 
                           .Include(c => c.Schedules) 
                           .Include(c => c.StudentCourses) 
                           .FirstOrDefault(c => c.Id == id);

                if (course == null)
                {
                    return null;
                }

                var Response = _mapper.Map<CourseResponse>(course);
                return Response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while retrieving subject: " + ex.Message, ex);
            }
        }

        public List<StudentScheduleResponse> GetStudentsByScheduleId(int scheduleId)
        {
            try
            {
                var studentSchedules = _context.StudentSchedules
                    .Include(ss => ss.Student)
                    .Include(ss => ss.Schedule)
                        .ThenInclude(s => s.Course)
                    .Where(ss => ss.ScheduleId == scheduleId)
                    .ToList();

                var response = _mapper.Map<List<StudentScheduleResponse>>(studentSchedules);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving students by schedule: " + ex.Message, ex);
            }
        }
    }
}
