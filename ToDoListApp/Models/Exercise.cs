using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Title { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Description value cannot be exceed 100 characters")]
        public string? Descriptions { get; set; }

        public bool Done { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

