using FileService.Domain;
using FileService.Domain.Entities;
using FileService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileService.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UploaderController : ControllerBase
    {
        private readonly FileDbContext dbContext;
        private readonly FileDomainService domainService;
        private readonly IFileRepository repository;

        public UploaderController(FileDbContext dbContext, FileDomainService domainService, IFileRepository repository)
        {
            this.dbContext = dbContext;
            this.domainService = domainService;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<FileExistsResponse> FileExists(string fileName,string hash)
        {
            var item = await repository.FindFileAsync(fileName,hash);
            if(item == null)
            {
                return new FileExistsResponse(false, null);
            }
            else
            {
                return new FileExistsResponse(true, item.RemoteUrl);
            }
        }

        //TODO: returned messages
        [HttpPost]
        public async Task<ActionResult<Uri>> Upload(IFormFile file, string fileName, CancellationToken cancellationToken = default)
        {
            //string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using Stream stream = file.OpenReadStream();
            FileItem fileItem = await domainService.UpLoadAsync(fileName, stream, cancellationToken);
            await dbContext.AddAsync(fileItem);
            await dbContext.SaveChangesAsync();
            return fileItem.RemoteUrl;
        }
    }
}
