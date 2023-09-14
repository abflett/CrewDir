namespace CrewDir.Api.DTOs.Responses
{
    public class AppUserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public EmployeeResponse? Employee { get; set; }
    }
}
