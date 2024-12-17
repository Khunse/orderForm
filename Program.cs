using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using orderform.External.Data;
using orderform.External.DataAccess;
using orderform.Models;
using orderform.Services.OrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IDataAccess,DataAccess>();
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(
    options => {

        options.UseSqlServer(

            builder.Configuration.GetConnectionString("Default")

        );

    }
);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors( options => {
    options.AddPolicy("Frontend", policyBuilder => {
        policyBuilder.WithOrigins( "http://localhost:5173/" );
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });

});

var app = builder.Build();

app.UseDefaultFiles( new DefaultFilesOptions
{
    DefaultFileNames = new List<string>{ "index.html" }
});

app.UseStaticFiles( new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"dist")),
    RequestPath = ""
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/api/getOrders", async ([FromBody] RequestModel request, IOrderService orderService) => {

    var getOrders = await orderService.GetOrders(request.orderSearchFilter);

    var resp = new ResponseModel<List<Order>>
    {
        respData = getOrders,
        respCode = "I0000",
        respMsg = "Success!",
    };


    return resp;
});

app.MapPost("/api/addOrder",  async ([FromBody] RequestModel request, IOrderService orderService) => {

    var saveData = await orderService.AddOrder(request.req);
    var datetimes = DateTime.Now;
    var resp = new ResponseModel<bool>
    {
        respData = saveData,
        respCode = saveData ?  "I0000" : "E0001",
        respMsg = saveData ?  "Success!" : "Fail"
    };

    return resp;
});

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("index.html",new StaticFileOptions{
         FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "dist"))
    });
    
    endpoints.MapControllers();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
