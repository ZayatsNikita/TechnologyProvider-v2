using FluentValidation.AspNetCore;
using TechnologyProvider.Cqrs.Infrastructure.ServiceCollectionExtensions;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Api.Infrastructure.Filters;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidationActionFilterAttribute));
        }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TechonologyModelValidator>());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCommandsAndQueriesHandlers();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}