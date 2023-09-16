namespace CrewDir.Api.DTOs.Responses
{
    public class AppUserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public EmployeeResponse? Employee { get; set; }
    }
}
