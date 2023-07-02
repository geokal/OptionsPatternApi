using Microsoft.Extensions.Options;
using OptionsPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMyService(options =>
{
    options.Option1 = "100 push-ups";
    options.Option2 = false;
});

var app = builder.Build();

//var option1 = app.Services.GetRequiredService<IOptionsMonitor<MyServiceOptions>>().CurrentValue.Option1;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", (HttpContext context) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();

    var myServiceOptions = myService.GetOptions();

    return Results.Ok(myServiceOptions);
})
.WithName("GetMyServiceOptions")
.WithOpenApi();

app.Run();