using Autocomplete.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ACEntities>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AC"));
});

var app = builder.Build();


app.Run();
