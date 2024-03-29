﻿using Microsoft.EntityFrameworkCore;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture;
public class ProjectDbContext:DbContext
{
    public DbSet<Project> Projects { get;private set; }
    public DbSet<ProjectVersion> ProjectVersions { get;private set; }
    public DbSet<FirmwareVersion> FirmwareVerisions { get;private set; }
    public DbSet<ModelVersion> ModelVersions { get;private set; }
    public DbSet<ProjectFile> ProjectFiles { get;private set; }
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}