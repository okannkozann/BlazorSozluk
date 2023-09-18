using BlazorSozluk.Common.ViewModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient httpClient;

        public VoteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await httpClient.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote error"); //burası farklı şekillerde doldurulabilir

        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await httpClient.PostAsync($"/api/Vote/DeleteEntryVote/{entryCommentId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote error");
        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryVote(entryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryVote(entryCommentId, VoteType.DownVote);
        }



        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await httpClient.PostAsync($"/api/vote/entry{entryId}?voteType={voteType}", null);

            //todo başarılı döndü mü kontrol et

            return result;
        }

        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await httpClient.PostAsync($"/api/vote/entrycomment{entryCommentId}?voteType={voteType}", null);

            //todo başarılı döndü mü kontrol et

            return result;
        }

    }
}
