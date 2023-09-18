using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChangedAddress = string.CompareOrdinal(dbEmailAddress, request.EmailAddress)!=0; //emailin değişip değişmediğini kontrol ediyoruz. compareordinal büyük küçük harf duyarlılığı yoksayma


            mapper.Map(request, dbUser);

            //dbUser = mapper.Map<Domain.Models.User>(request); bunun yerine yukarıdakini yazabiliriz. burda user tekrar oluşturuluyor. yukarıda db den çekilen hazır olan kullanılıyor

            var rows = await userRepository.UpdateAsync(dbUser);


            //RabbitMq

            if (emailChangedAddress && rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress,
                };

                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName, 
                                                   exchangeType: SozlukConstants.DefaultExchangeType, 
                                                   queueName: SozlukConstants.UserEmailChangedQueueName, obj: @event);

                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);

            }

            return dbUser.Id;


        }
    }
}
