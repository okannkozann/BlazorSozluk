using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Queries
{
    public class GetEntriesViewModel // Sitede ki ekranın solunda ki kısım. KonuId, Konu , count 
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public int CommentCount { get; set; }
    }
}
