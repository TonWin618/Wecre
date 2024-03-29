﻿using FileService.Domain;
using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure;

public class FileRepository:IFileRepository
{
    private readonly FileDbContext dbContext;
    public FileRepository(FileDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<FileItem?> FindFileAsync(string relativePath)
    {
        return await dbContext.FileItems.FirstOrDefaultAsync(
            u => u.RelativePath == relativePath);
    }

    public async Task<bool> RemoveFileAsync(FileItem fileItem)
    {
        dbContext.FileItems.Remove(fileItem);
        return true;
    }
}
