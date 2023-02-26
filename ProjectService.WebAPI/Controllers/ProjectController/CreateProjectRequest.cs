namespace ProjectService.WebAPI.Controllers.ProjectController
{
    public record CreateProjectRequest(string Name, string? Description, string? Tags, string? ReadmeUrl);
}
