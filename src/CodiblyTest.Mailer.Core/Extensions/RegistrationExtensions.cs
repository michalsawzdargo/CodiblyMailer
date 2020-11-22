using System;
using System.Collections.Generic;
using System.Text;
using CodiblyTest.Mailer.Core.Commands;
using CodiblyTest.Mailer.Core.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CodiblyTest.Mailer.Core.Extensions
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection RegisterCommandHandler<TCommand, TCommandHandler>(
            this IServiceCollection services)
            where TCommand : ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            return services.AddTransient<TCommandHandler>()
                .AddTransient<IRequestHandler<TCommand, Unit>>(sp => sp.GetService<TCommandHandler>())
                .AddTransient<ICommandHandler<TCommand>>(sp => sp.GetService<TCommandHandler>());
        }

        public static IServiceCollection RegisterCommandHandler<TCommand, TResult, TCommandHandler>(
            this IServiceCollection services)
            where TCommand : ICommand<TResult>
            where TCommandHandler : class, ICommandHandler<TCommand, TResult>
        {
            return services.AddTransient<TCommandHandler>()
                .AddTransient<IRequestHandler<TCommand, TResult>>(sp => sp.GetService<TCommandHandler>())
                .AddTransient<ICommandHandler<TCommand, TResult>>(sp => sp.GetService<TCommandHandler>());
        }

        public static IServiceCollection RegisterQueryHandler<TQuery, TResponse, TQueryHandler>(
            this IServiceCollection services)
            where TQuery : IQuery<TResponse>
            where TQueryHandler : class, IQueryHandler<TQuery, TResponse>
        {
            return services.AddTransient<TQueryHandler>()
                .AddTransient<IRequestHandler<TQuery, TResponse>>(sp => sp.GetService<TQueryHandler>())
                .AddTransient<IQueryHandler<TQuery, TResponse>>(sp => sp.GetService<TQueryHandler>());
        }
    }
}
