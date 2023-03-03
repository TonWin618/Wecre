using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectService.WebAPI.Controllers.VersionController
{
    public record CreateVersionRequest(string version,string description, IEnumerable<Guid> firmwares, IEnumerable<Guid> models);
}
