﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
    {
        private readonly IEntryRepository entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy)
                         .Include(i => i.EntryVotes)
                         .Where(i => i.Id == request.EntryId);

            //automapper kullanuılmadı çünkü custom atamalar var
            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,

                Subject=i.Subject,

                Content = i.Content,

                FavoritedCount = i.EntryFavorites.Count,

                CreatedDate = i.CreateDate,

                CreatedByUserName = i.CreatedBy.UserName,

                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),

                VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId) ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType : Common.ViewModels.VoteType.None

            });

            return await list.FirstOrDefaultAsync(cancellationToken:cancellationToken);

        }
    }
}
