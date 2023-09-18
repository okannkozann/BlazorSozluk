using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Queries
{
    public class SearchEntryViewModel
    {
        public Guid Id { get; set; } // search sonucunda çıkan başlığa gidebilmek için tanımlandı
        public string Subject { get; set; } // seaarch yaparken buna göre yapılacak
    }
}
