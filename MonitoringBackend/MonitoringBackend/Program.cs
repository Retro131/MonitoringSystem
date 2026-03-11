using System.Reflection;
using InfotecsBackend.EF;
using InfotecsBackend.Errors.Handlers;
using InfotecsBackend.ServiceExtension;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRepositories().AddServices();
builder.Services.AddSwaggerGen(options =>
{
    IncludeXml(Assembly.GetExecutingAssembly(), options);

    IncludeXml(typeof(Models.Entities.Session).Assembly, options);
    return;

    static void IncludeXml(Assembly asm, SwaggerGenOptions c)
    {
        var xml = $"{asm.GetName().Name}.xml";
        var path = Path.Combine(AppContext.BaseDirectory, xml);
        if (File.Exists(path))
            c.IncludeXmlComments(path, includeControllerXmlComments: true);
    }
});
builder.Services.AddDbContext<SessionsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SessionsDbContext)));
});

builder.Services.AddExceptionHandler<NotFoundExceptionsHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SessionsDbContext>();
        context.Database.Migrate(); 
        
        Console.WriteLine("Миграции успешно применены.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при выполнении миграций.");
    }
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.MapControllers();

app.Run();