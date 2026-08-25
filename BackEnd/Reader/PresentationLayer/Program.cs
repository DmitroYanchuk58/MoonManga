using Amazon.S3;
using BusinessLogicLayer.Services;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Services;
using DatabaseLogicLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReaderDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoonMangaDBConnection")));

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = builder.Configuration["Minio:ServiceUrl"] ?? "http://localhost:9000",
        ForcePathStyle = true
    };
    return new AmazonS3Client(
        builder.Configuration["Minio:AccessKey"] ?? "minioadmin",
        builder.Configuration["Minio:SecretKey"] ?? "minioadminpassword",
        config
    );
});

builder.Services.AddScoped<IStorageService, MinioStorageService>();
builder.Services.AddScoped(typeof(ICRUD_Repository<>), typeof(CRUD_Repository<>));

builder.Services.AddScoped<IPageService, PageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
