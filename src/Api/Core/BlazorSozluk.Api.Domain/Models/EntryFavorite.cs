using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryFavorite :BaseEntity
    {
        public Guid EntryId { get; set; } // hangi entrye ait olduğu
        public Guid CreatedById { get; set; } // favoriye ekleyen kullanıcı

        public virtual Entry Entry { get; set; }
        public virtual User CreatedUser { get; set; }
    }
}
