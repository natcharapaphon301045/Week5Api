namespace Week5.Domain
{
    public class Major
    {
        public int MajorID { get; set; }
        public required string MajorName { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
