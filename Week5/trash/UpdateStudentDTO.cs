using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Week5.Domain;
using Week5.Infrastructure;

public class StudentUpdateDTO
{
    public int StudentID { get; set; }
    public required string StudentName { get; set; }
    public required string StudentSurname { get; set; }
    public int ProfessorID { get; set; }
    public int MajorID { get; set; }
}

