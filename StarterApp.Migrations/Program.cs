using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Data;
using StarterApp.Database.Models;

Console.WriteLine("Running migrations...");
using var context = new AppDbContext();
context.Database.Migrate();
Console.WriteLine("Migrations complete.");
