namespace Week5.Domain
{
    public class Major
    {
        public int MajorID { get; set; }
        public required string MajorName { get; set; }

        public ICollection<StudentClass> Student { get; set; } = new List<StudentClass>();
    }
}
