using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScottBrady91.AspNetCore.Identity;
using TicketSupportSystem.Data;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.Interfaces;
using TicketSupportSystem.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TicketSupportSystemContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
