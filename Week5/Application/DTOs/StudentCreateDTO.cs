public class StudentCreateDTO
{
    public required string StudentName { get; set; }
    public required string StudentSurname { get; set; }
    public int ProfessorID { get; set; }
    public int MajorID { get; set; }
    public int Score { get; set; }
}
