namespace Week5.Domain
{
    public class Professor
    {
        public int ProfessorID { get; set; }
        public required string ProfessorName { get; set; }
        public required string ProfessorSurname { get; set; }

        public ICollection<StudentClass> Student { get; set; } = new List<StudentClass>();
    }
}
