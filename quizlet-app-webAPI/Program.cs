using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://www.contoso.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod(); ;
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<WordsModuleAPIDbContext>(options => options.UseInMemoryDatabase("WordsModulesDb"));
builder.Services.AddDbContext<WordsModuleAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WordsModulesConnectionString")));

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
