﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.UserService.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateConfirmationLink(Guid confirmationId)
        {
            var baseUrl = configuration["ConfirmationLinkBase"] + confirmationId;

            return baseUrl;
        }

        public Task SendEmail(string toEmailAddress,string content)
        {
            //send email process
            logger.LogInformation($"Email sent to {toEmailAddress} with content of {content}");

            return Task.CompletedTask;
        }
    }
}
