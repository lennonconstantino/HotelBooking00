
using Microsoft.EntityFrameworkCore;
using Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

# region DB wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(
    options => options.UseSqlServer(connectionString)
);
# endregion



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Cant connect to DB");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
