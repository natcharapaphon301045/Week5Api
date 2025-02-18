namespace Week5.trash
{
    public class StudentDetailsDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentSurname { get; set; } = string.Empty;
        public int ProfessorID { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
        public int MajorID { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public List<int>? Scores { get; set; }
    }
}