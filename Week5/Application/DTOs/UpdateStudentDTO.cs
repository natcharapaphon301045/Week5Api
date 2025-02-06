namespace Week5.Application.DTOs
{
    public class UpdateStudentDTO
    {
        public int StudentID { get; set; }
        public required string StudentName { get; set; }
        public required string StudentSurname { get; set; }
        public int ProfessorID { get; set; }
        public int MajorID { get; set; }
    }
}
