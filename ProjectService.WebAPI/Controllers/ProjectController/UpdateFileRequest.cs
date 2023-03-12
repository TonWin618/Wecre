namespace ProjectService.WebAPI.Controllers.ProjectController
{
    public record UpdateProjectRequest(string? Description, List<string>? Tags, List<Guid> ReadmeFiles);
}
