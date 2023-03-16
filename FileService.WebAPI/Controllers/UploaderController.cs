using FileService.Domain;
using FileService.Domain.Entities;
using FileService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileService.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public async Task<FileExistsResponse> FileExists(string relativePath)
        {
            var item = await repository.FindFileAsync(relativePath);
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
        public async Task<FileUploadResponse> Upload(IFormFile file, string relativePath, CancellationToken cancellationToken = default)
        {
            using Stream stream = file.OpenReadStream();
            FileItem fileItem = await domainService.UpLoadAsync(relativePath, stream, cancellationToken);
            await dbContext.AddAsync(fileItem);
            await dbContext.SaveChangesAsync();
            return new FileUploadResponse(fileItem.FileSizeInBytes, fileItem.RemoteUrl);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string relativePath)
        {
            var item = await repository.FindFileAsync(relativePath);
            if(item == null)
            {
                return NotFound();
            }
            await domainService.DeleteFileAsync(relativePath);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
