using BlazorSozluk.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryVote:BaseEntity
    {
        public Guid EntryId { get; set; } // Oyun kullanıldığı entry
        public VoteType VoteType { get; set; } // Kullanılan oyun yönü
        public Guid CreatedById { get; set; }   // favoriye ekleyen kullanıcı

        public virtual Entry Entry { get; set; }
    }
}
