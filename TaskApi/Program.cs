var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Оставляем UseSwagger пустым, он сам сгенерирует правильную версию
app.UseSwagger();

app.UseSwaggerUI(options => 
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API V1");
    options.RoutePrefix = "swagger"; 
});

app.MapControllers(); 

app.Run();