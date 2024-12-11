using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lesson24_Exam.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }
        [Required]
        public EventType Category { get; set; }
        public bool IsPublic { get; set; }
        [ValidateNever]
        public List<User> Users { get; set; }
    }
}