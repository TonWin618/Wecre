using FileService.Domain;
using FileService.Infrastructure;
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
        public async Task<FileExistsResponse> FileExists(long fileSize,string sha256Hash)
        {
            var item = await repository.FindFileAsync(fileSize, sha256Hash);
            if(item == null)
            {
                return new FileExistsResponse(false, null);
            }
            else
            {
                return new FileExistsResponse(true, item.remoteUrl);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Uri>> Upload([FromForm]UploadRequest req, CancellationToken cancellationToken = default)
        {
            var file = req.File;
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();
            var uploadItem = await domainService.UpLoadAsync(stream, fileName,cancellationToken);
            dbContext.Add(uploadItem);
            return uploadItem.remoteUrl;
        }
    }
}
