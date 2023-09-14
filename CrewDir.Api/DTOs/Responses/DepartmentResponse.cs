namespace CrewDir.Api.DTOs.Responses
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<EmployeeResponse>? Employees { get; set; }
    }
}
