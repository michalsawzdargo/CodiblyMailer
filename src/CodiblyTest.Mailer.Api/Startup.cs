using System.Collections.Generic;
using CodiblyTest.Mailer.Api.Infrastructure;
using CodiblyTest.Mailer.Core.Dispatcher;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Core.Extensions;
using CodiblyTest.Mailer.Core.Services;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Query;
using CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects;
using CodiblyTest.Mailer.Domain.Mail.Commands;
using CodiblyTest.Mailer.Domain.Mail.Queries;
using CodiblyTest.Mailer.Domain.Mail.Validators;
using CodiblyTest.Mailer.Storage;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodiblyTest.Mailer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddDbContext<MailerDbContext>(options => options.UseInMemoryDatabase(databaseName: "Mailer"));

            services.AddTransient<IMailDomainValidator, MailDomainValidator>();

            services.AddScoped<IMailClient, DummyMailClient>();
            services.AddScoped<IDispatcher, Dispatcher>();

            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidationPipeline<>));

            services.RegisterQueryHandler<GetMailDetailsQuery, MailDetailsModel, MailQueryHandlers>();
            services.RegisterQueryHandler<GetMailListQuery, IReadOnlyCollection<MailListItemModel>, MailQueryHandlers>();
            services.RegisterQueryHandler<GetMailStatusQuery, MailStatus, MailQueryHandlers>();

            services.RegisterCommandHandler<CreateMailCommand, int, MailCommandHandlers>();
            services.RegisterCommandHandler<SetMailPriorityCommand, MailCommandHandlers>();
            services.RegisterCommandHandler<SendMailsCommand, MailCommandHandlers>();
            services.RegisterCommandHandler<CreateAttachment, AttachmentCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlingMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mailer API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
