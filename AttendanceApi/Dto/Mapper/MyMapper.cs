using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Dto.Mapper
{
    public class MyMapper:Profile
    {
       

        public MyMapper()
        {
            CreateMap<Subject, SubjectResponse>();
            CreateMap<SubjectRequest, Subject>();
            CreateMap<Teacher, TeacherResponse>();

            CreateMap<StudentRole, StudentRoleResponse>()
                .ForMember(dest=>dest.studentName,opt=>opt.MapFrom(src=>src.Student.Name));

            CreateMap<RolePermission, RolePermissionResponse>()
                .ForMember(d => d.permisionName, opt => opt.MapFrom(s => s.Permission.Name));

            CreateMap<Role, RoleResponse>()
            .ForMember(dest => dest.StudentRoles, opt => opt.MapFrom(src => src.StudentRoles))
            .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions));

            CreateMap<Course, CourseResponse>()
           .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.StudentCourses.Select(sc => sc.Course)))
            .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.StudentSchedules.Select(ss => ss.Schedule)));

            CreateMap<CourseRequest, Course>();

            CreateMap<StudentAttendanceRequest, StudentSchedule>()
            .ForMember(dest => dest.ScheduleId, opt => opt.Ignore());

            CreateMap<Schedule, ScheduleResponse>()
            .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.Slot))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
            .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.Code))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name));


            CreateMap<StudentSchedule, StudentScheduleResponse>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Schedule.Course.Name))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Schedule.Teacher.Name))
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.Date))
             .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name)); ;


           

        }
        
    }
}
