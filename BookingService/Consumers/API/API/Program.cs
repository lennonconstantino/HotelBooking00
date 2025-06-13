using Application.Booking;
using Application.Booking.Ports;
using Application.Guest;
using Application.Guest.Ports;
using Application.MercadoPago;
using Application.Payment;
using Application.Room;
using Application.Room.Ports;
using Data;
using Data.Booking;
using Data.Guest;
using Data.Room;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;
using Payment.Application.MercadoPago;
//using PaymentService = Application.MercadoPago;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

# region IoC
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IMercadoPagoPaymentService, MercadoPagoAdapter>();
# endregion

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
