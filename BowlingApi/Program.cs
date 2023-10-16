using BowlingApp.Domain.Interfaces;
using BowlingApp.Middlewares;
using BowlingApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBowlingGame, Bowling>();
builder.Services.AddControllers().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<IPWhitelistingMiddleware>(); // Un comment this line and add the whitelisted IP address in IPWhitelistingMiddleware,cs 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
