using System.ComponentModel.DataAnnotations;

namespace CrewDir.Api.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public List<Employee>? Employees { get; set; }
    }
}
