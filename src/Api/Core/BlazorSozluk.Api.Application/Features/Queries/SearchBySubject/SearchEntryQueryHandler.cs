using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            //todo burada arama yapılırken ki validtionlar eklenebilir

            //entryRepository.Get(i => i.Subject.StartsWith("")); bu metod yerine altta ki metot db için daha performanslı olduğu için kullanıldı.
            var result = entryRepository.Get(i => EF.Functions.Like(i.Subject, $"{request.SearchText}%"))
                                        .Select(i => new SearchEntryViewModel()
                                        {
                                            Id = i.Id,
                                            Subject = i.Subject
                                        });

            return await result.ToListAsync(cancellationToken);
        }
    }
}
