﻿using System.ComponentModel.DataAnnotations;
using Week5.Domain_Layer.Entity;

namespace Week5.Application_Layer.DTOs
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public required string StudentName { get; set; }
        public required string StudentSurname { get; set; }

        [Required(ErrorMessage = "Professor is required")]
        public int ProfessorID { get; set; }

        [Required(ErrorMessage = "Major is required")]
        public int MajorID { get; set; }

        public List<StudentClass>? StudentClass { get; set; }
        public List<BehaviorScore>? BehaviorScore { get; set; }
    }
}
