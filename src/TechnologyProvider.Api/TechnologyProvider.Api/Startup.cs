using FluentValidation.AspNetCore;
using TechnologyProvider.Api.Infrastructure.Filters;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

/// <summary>
/// Class for web host configuration.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">Program configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    /// <summary>
    /// Gets program configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configuration services.
    /// </summary>
    /// <param name="services">Services colleciton.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidationActionFilterAttribute));
        }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TechonologyModelValidator>());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });

        var connectionString = this.Configuration["ConnectionStrings:PostgreDbConnectionString"];
        services.AddCommandsAndQueriesHandlers(connectionString);
    }

    /// <summary>
    /// Application configuration.
    /// </summary>
    /// <param name="app">WebApplication object.</param>
    /// <param name="env">WebHostEnvironment object.</param>
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}