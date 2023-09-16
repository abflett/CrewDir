namespace CrewDir.UIService
{
    public class ApiClientService
    {
        private readonly CrewDirApiClient _crewDirApiClient;

        public ApiClientService(CrewDirApiClient crewDirApiClient)
        {
            _crewDirApiClient = crewDirApiClient;
        }

        public async Task<List<DepartmentResponse>> GetDepartmentsAsync()
        {
            var departments = await _crewDirApiClient.GetDepartmentsAsync();
            return departments.ToList();
        }
    }
}
