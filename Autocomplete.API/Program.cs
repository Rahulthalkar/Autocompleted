using Autocomplete.BLL;
using Autocomplete.DAL;
using Autocomplete.DAL.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string corsDomains = "http://localhost:5000,http://localhost:4200";
string[] domains = corsDomains.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
{
    builder
    .WithOrigins(domains)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAutoSearchRepository, SearchRepository>();
builder.Services.AddScoped<AutoSerachService>();
builder.Services.AddScoped<UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AppCORSPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
