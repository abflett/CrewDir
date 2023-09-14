using System.ComponentModel.DataAnnotations;

namespace CrewDir.Api.DTOs.Requests
{
    public class UserEmployeeRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int EmployeeId { get; set; }
    }
}
