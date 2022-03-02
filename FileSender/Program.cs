using FileSender.EfModels;
using FileSender.Repositories;
using FileSender.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    //.AllowCredentials()
    );
});

builder.Services.AddTransient<SendDbContext>();

//repositories
builder.Services.AddTransient<IFileUploadRepository, FileUploadRepository>();
builder.Services.AddTransient<IFileContentsRepository, FileContentsRepository>();

//services
builder.Services.AddTransient<IFileUploadService,FileUploadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
