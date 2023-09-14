using System.ComponentModel.DataAnnotations;

namespace CrewDir.Api.DTOs.Requests
{
    public class DepartmentRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}
