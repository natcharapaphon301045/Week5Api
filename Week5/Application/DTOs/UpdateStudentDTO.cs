namespace Week5.Application.DTOs
{
    public class StudentUpdateDTO
    {
        public int StudentID { get; set; }
        public required string StudentName { get; set; }
        public required string StudentSurname { get; set; }
        public int ProfessorID { get; set; }
        public int MajorID { get; set; }

        public List<StudentClassDTO>? StudentClass { get; set; }
        public List<BehaviorScoreDTO>? BehaviorScore { get; set; }
    }
    public class BehaviorScoreDTO
    {
        public int ScoreID { get; set; }
        public int StudentID { get; set; }
        public int Score { get; set; }
    }
    public class StudentClassDTO
    {
        public int ClassID { get; set; }
    }



}
