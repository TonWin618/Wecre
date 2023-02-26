using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain.Entities
{
    public record FileIdentifier(string UserName, string ProjectName, FileType FileType, string VersionName, string FileName);
}
