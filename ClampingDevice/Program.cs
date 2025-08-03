using ClampingDevice.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseCors(x => x
//    .AllowAnyHeader()
//    .AllowAnyMethod()
//    .AllowCredentials()
//    .WithOrigins("http://localhost:5173", "https://localhost:5173")
//    );
app.UseCors("AllowVueDevClient");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
