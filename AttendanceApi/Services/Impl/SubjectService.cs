using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;
using AttendanceApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AttendanceApi.Service.Impl
{
    public class SubjectService : ISubjectService
    {
        private readonly AttendanceContext _context;
        private readonly IMapper _mapper;

        public SubjectService(AttendanceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public void addSubject(SubjectRequest subjectRequest)
        {
            var subjectEntity = _mapper.Map<Subject>(subjectRequest);
            _context.Subjects.Add(subjectEntity);
            _context.SaveChanges();
        }

        public string DeleteSubject(int subjectId)
        {
            var existingSubject = _context.Subjects.Find(subjectId);

            if (existingSubject == null)
            {
                return "Mon hoc không tồn tại";
            }
           
            try
            {
                _context.Subjects.Remove(existingSubject);
                _context.SaveChanges();
                return "Xoa mon hoc thanh cong";
            }
            catch (Exception ex)
            {

                return $"Lỗi khi xoa: {ex.Message}";
            }
        }

        public SubjectResponse GetSubject(int subjectId)
        {
            try
            {
                var subject = _context.Subjects.Find(subjectId);

                if (subject == null)
                {
                    return null; 
                }

                var subjectResponse = _mapper.Map<SubjectResponse>(subject);
                return subjectResponse;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error occurred while retrieving subject: " + ex.Message, ex);
            }
        }

        public List<SubjectResponse> listSubject()
        {
            try
            {
                var lSubject = _context.Subjects.ToList();
                var subjectResponses = _mapper.Map<List<SubjectResponse>>(lSubject);
                return subjectResponses;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi phù hợp
                throw new Exception("Lỗi khi lấy danh sách môn học: " + ex.Message);
            }
        }

        public string UpdateSubject(SubjectRequest subjectRequest)
        {
            var existingSubject = _context.Subjects.Find(subjectRequest.Id);

            if (existingSubject == null)
            {
                return "Mon hoc không tồn tại";
            }

          
            _mapper.Map(subjectRequest, existingSubject);

            try
            {
                _context.SaveChanges(); 
                return "Cập nhật môn học thành công";
            }
            catch (Exception ex)
            {
               
                return $"Lỗi khi cập nhật: {ex.Message}";
            }
        }

    }
}
