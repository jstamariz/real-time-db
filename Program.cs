using real_time_db.Db;
using real_time_db.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FooContext>();
builder.Services.AddTransient<FooContext>();
builder.Services.AddSingleton<FooService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/write", (FooService service) => {
    service.Write();
    return Results.Created();
});

app.MapGet("/read", (FooService service) => {
    var result  = service.Read();
    return Results.Ok(result);
});

app.Run();