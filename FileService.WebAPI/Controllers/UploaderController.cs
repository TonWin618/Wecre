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
        public async Task<FileExistsResponse> FileExists(FileIdentifier fileIdentifier)
        {
            var item = await repository.FindFileAsync(fileIdentifier);
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
        public async Task<ActionResult<Uri>> Upload([FromForm]UploadRequest req, CancellationToken cancellationToken = default)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FileIdentifier fileIdentifier = new(userName, req.ProjectName, req.FileType, req.VersionName, req.File.FileName);
            using Stream stream = req.File.OpenReadStream();
            FileItem fileItem = await domainService.UpLoadAsync(fileIdentifier, stream, cancellationToken);
            await dbContext.AddAsync(fileItem);
            await dbContext.SaveChangesAsync();
            return fileItem.RemoteUrl;
        }

        //[HttpPost]
        //public async Task<ActionResult> ChangeFileName(ChangeFileNameRequest req)
        //{
        //    Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    UploadItem item = await repository.FindFileAsync(req.fileSize, req.sha256Hash);
        //    if(null == item)
        //    {
        //        return NotFound("target file not found");
        //    }
        //    if (item.UserId != userId)
        //    {
        //        return Unauthorized("you are not the owner of this file");
        //    }
        //    //TODO: Data validation
        //    item.ChangeFileName(req.newFileName);
        //    await dbContext.SaveChangesAsync();
        //    return Ok("filename changed");
        //}
    }
}
