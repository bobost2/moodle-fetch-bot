using Microsoft.EntityFrameworkCore;
using MoodleFetchBotAPI.Controllers;
using MoodleFetchBotAPI.Models.Tables;
using MoodleFetchBotAPI.Services;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin()));
//builder.Services.AddCors(options => options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()));


Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("[!] Running migrations. Please wait...");
try
{
    MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
    context.Database.Migrate();
}
catch (MySqlConnector.MySqlException) //In case DB has been modified externally it throws an exception
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[!] There was an error while running migration. Please wait while we fix the error...");

    MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
    context.EfmigrationsHistories.RemoveRange(context.EfmigrationsHistories); //Remove previous versions

    string[] migrationsDirectory = Directory.GetFiles(@"Migrations\", "*.cs"); //Get all migrations

    for (int i = 0; i < migrationsDirectory.Length; i++)
    {
        migrationsDirectory[i] = migrationsDirectory[i].Remove(0, 11); //Remove directory from string
    }

    foreach (var migration in migrationsDirectory)
    {
        //Regex to exclude the files that are not migrations  
        if (Regex.IsMatch(migration, "^((?!((MoodleFetchBotDBContextModelSnapshot.cs)|(Designer.cs))).)*$"))
        {
            context.EfmigrationsHistories.Add(new EfmigrationsHistory
            {
                ProductVersion = "5.0.13", //Current version that could change
                MigrationId = migration.Remove(migration.Length - 3) //Removes .cs since we dont need it
            }); ;
        }
    }
    context.SaveChanges();
}
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();

DiscordBotService.RunBot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings => {
        settings.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        {
            ["activated"] = true //was false for testing
        };
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();

