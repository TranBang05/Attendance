using AttendanceApi.Dto.Request;
using AttendanceApi.Dto.Response;

namespace AttendanceApi.Service
{
    public interface ISubjectService
    {
        public List<SubjectResponse> listSubject();
        public void addSubject(SubjectRequest subjectRequest);

        public string UpdateSubject(SubjectRequest subjectRequest);
        public string DeleteSubject(int subjectId);

        public SubjectResponse GetSubject(int subjectId);
    }
}
