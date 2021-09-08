using MediatR;
using FluentValidation;
using System.Reflection;
using Ordering.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
  public static class ApplicationServiceRegistration
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddMediatR(Assembly.GetExecutingAssembly());

      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

      return services;
    }
  }
}
