using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Api.Contracts.Mail;
using CodiblyTest.Mailer.Api.Contracts.Mail.DTO;
using CodiblyTest.Mailer.Api.Infrastructure;
using CodiblyTest.Mailer.Core.Dispatcher;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Query;
using CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodiblyTest.Mailer.Api.Controllers
{
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public MailController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("/mails")]
        [ProducesResponseType(typeof(MailListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> List([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            // TODO use some mappers
            var queryResult = await _dispatcher.Query<GetMailListQuery, IReadOnlyCollection<MailListItemModel>>(
                new GetMailListQuery { PageNumber = pageNumber, PageSize = pageSize });
            var response = new MailListResponse
            {
                Items = queryResult.Select(m => new MailListItemDTO
                {
                    Id = m.Id,
                    Sender = m.Sender,
                    Subject = m.Subject,
                    Recipients = m.Recipients,
                    Status = m.Status.ToString()
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet("/mails/{id:int}")]
        [ProducesResponseType(typeof(MailDetailsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            // TODO use some mappers
            var queryResult = await _dispatcher.Query<GetMailDetailsQuery, MailDetailsModel>(new GetMailDetailsQuery { Id = id });
            var response = new MailDetailsResponse
            {
                Mail = new MailDTO
                {
                    Id = queryResult.Id,
                    Sender = queryResult.Sender,
                    Subject = queryResult.Subject,
                    Body = queryResult.Body,
                    Recipients = queryResult.Recipients,
                    Status = queryResult.Status.ToString(),
                    Priority = queryResult.Priority.ToString(),
                    Attachments = queryResult.Attachments.Select(a => new MailAttachmentDTO { FileName = a.FileName }).ToList()
                }
            };

            return Ok(response);
        }

        [HttpGet("/mails/{id:int}/status")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatus([FromRoute] int id)
        {
            var queryResult = await _dispatcher.Query<GetMailStatusQuery, MailStatus>(new GetMailStatusQuery { Id = id });
            
            return Ok(queryResult);
        }

        [HttpPost("/mails")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateMailRequest request)
        {
            var id = await _dispatcher.Command<CreateMailCommand, int>(new CreateMailCommand
            {
                Sender = request.Sender,
                Recipients = request.Recipients,
                Subject = request.Subject,
                Body = request.Body,
                Priority = request.Priority
            }); // TODO use some mappers

            return Created(new Uri($"/mails/{id}", UriKind.Relative), new { id });
        }

        [HttpPatch("/mails/{id:int}/priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePriority([FromRoute] int id, [FromBody] SetMailPriorityRequest request)
        {
            await _dispatcher.Command(new SetMailPriorityCommand
            {
                Id = id,
                Priority = request.Priority
            });

            return NoContent();
        }

        [HttpPost("/mails/{id:int}/attachments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAttachment([FromRoute] int id, [FromForm] AddAttachmentRequest request)
        {
            if (request?.File == null || request.File.Length == 0)
                throw new InvalidOperationException("Missing attachment data.");

            byte[] data = await request.File.GetBytes();
            await _dispatcher.Command(new CreateAttachment
            {
                MailId = id,
                FileName = request.File.FileName,
                File = data
            });

            return Created(new Uri($"/mails/{id}", UriKind.Relative), new { id });
        }

        [HttpPost("/mails/send")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendMails()
        {
            await _dispatcher.Command(new SendMailsCommand());

            return NoContent();
        }
    }
}
