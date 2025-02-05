namespace Week5.Domain
{
    public class Class
    {
        public int ClassID { get; set; }
        public required string ClassName { get; set; }
        public ICollection<StudentClass> StudentClass { get; set; } = new List<StudentClass>();
    }
}
