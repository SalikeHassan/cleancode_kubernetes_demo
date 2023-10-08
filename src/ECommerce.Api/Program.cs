using ECommerce.Application.Commands;
using CreateCustomerCommandHandler = ECommerce.Application.CommandHandlers.CreateCustomerHandler;
using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Polly;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Access the connection string
//var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
//System.Console.WriteLine("Password is "+saPassword);
//var connectionString = $"{builder.Configuration.GetConnectionString("DefaultConnection")};Password={saPassword};TrustServerCertificate=True";
var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
System.Console.WriteLine("Password is " + saPassword);
var connectionString = $"Server=sql-server-service;Database=ecom;User Id=sa;Password={saPassword};TrustServerCertificate=True;";

// Implement a retry policy to wait for the SQL Server container to be ready
// var retryPolicy = Policy.Handle<SqlException>()
//     .WaitAndRetry(new[]
//     {
//         TimeSpan.FromSeconds(3),
//         TimeSpan.FromSeconds(5),
//         TimeSpan.FromSeconds(10)
//     }, (exception, timeSpan, retryCount, context) =>
//     {
//         System.Console.WriteLine(connectionString);
//         // Log exception information here
//         Console.WriteLine($"Retry {retryCount} encountered an exception: {exception.Message}. Waiting {timeSpan.TotalSeconds} seconds before next retry.");
//     });

// retryPolicy.Execute(() =>
// {
//     using var connection = new SqlConnection(connectionString);
//     connection.Open();
//     // Connection successful, proceed with building the app
//     Console.WriteLine("Successfully connected to the database.");
// });
System.Console.WriteLine(connectionString);
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(connectionString));
        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        void RunDatabaseMigration(IApplicationBuilder application)
        {
            using var serviceScope = application.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
            dbContext.Database.Migrate();
            Console.WriteLine("Database migration completed.");
        }

        RunDatabaseMigration(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

       // app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();