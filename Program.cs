using dotnet_fancy_swagger;
using dotnet_fancy_swagger.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    foreach (Document document in Document.GetAll())
    {
        c.SwaggerDoc(document.Name, new OpenApiInfo
        {
            Title = document.Title,
            Version = document.Version,
            Description = document.Description
        });
    }

    c.DocumentFilter<AddTagsFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Make each document available in dropdown
        foreach (Document document in Document.GetAll())
        {
            c.SwaggerEndpoint($"/swagger/{document.Name}/swagger.json", $"{document.Title} {document.Version}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
