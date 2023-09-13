using CrewDir.Api.Data.Identity;
using CrewDir.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CrewDir.Api.Models
{
    public class Employee
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
        //public Department Department { get; set; } = new();

        private string _phone = string.Empty;
        [Required]
        [Phone]
        public string Phone {
            get { return PhoneNumber.FormatPhoneNumberForDisplay(_phone); }
            set { _phone = PhoneNumber.FormatPhoneNumberForStorage(value); }
        }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string OfficeLocation { get; set; } = string.Empty;

        [Required]
        public string ProfilePicture { get; set; } = "default.png";

        [Required]
        public string AppUserId { get; set; } = string.Empty;
        //public AppUser? AppUser { get; set; }
    }
}
