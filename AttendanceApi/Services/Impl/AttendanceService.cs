using AttendanceApi.Dto.Request;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Service.Impl
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public AttendanceService(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string AddAttendance(AttendanceRequest attendanceRequest)
        {
            bool anyChanges = false;

            foreach (var studentAttendance in attendanceRequest.Students)
            {
                var existingAttendance = _context.StudentSchedules
                    .FirstOrDefault(a => a.StudentId == studentAttendance.StudentId
                                        && a.ScheduleId == attendanceRequest.ScheduleId
                                        && a.Status == AttendanceStatus.NotYet);

                if (existingAttendance != null)
                {
                    existingAttendance.Status = studentAttendance.Status;
                    anyChanges = true;
                }


            }

            if (anyChanges)
            {
                _context.SaveChanges();
                return "Thực hiện them điểm danh thành công";
            }
            else
            {
                return "Không thể them được diem danh này";
            }
        }

        public bool UpdateAttendance(AttendanceRequest attendanceRequest)
        {
            bool anyChanges = false;

            foreach (var studentAttendance in attendanceRequest.Students)
            {
                var existingAttendance = _context.StudentSchedules
                    .FirstOrDefault(a => a.StudentId == studentAttendance.StudentId
                                        && a.ScheduleId == attendanceRequest.ScheduleId);

                if (existingAttendance != null)
                {
                    existingAttendance.Status = studentAttendance.Status;
                    anyChanges = true;
                }
            }

            if (anyChanges)
            {
                _context.SaveChanges();
                return true;
            }

            return false;
        }



    }
}
