using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Duty
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Detail { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
