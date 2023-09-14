using System.ComponentModel.DataAnnotations;

namespace CrewDir.Api.DTOs.Requests
{
    public class EmployeeRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string OfficeLocation { get; set; } = string.Empty;

        [Required]
        public string ProfilePicture { get; set; } = "default.png";

        [Required]
        [EmailAddress]
        public string CompanyEmail { get; set; } = string.Empty;
    }
}
