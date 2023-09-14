namespace CrewDir.Api.DTOs.Responses
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DepartmentResponse? Department { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string OfficeLocation { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = "default.png";
        public string CompanyEmail { get; set; } = string.Empty;
    }
}
