using System.Collections.Generic;
using FluffyDuffyMunchkinCats.Models.DbModels;

namespace FluffyDuffyMunchkinCats.Models.ViewModels
{
    public class IndexViewModel
    {
        public ICollection<Cat> Cats { get; set; }
    }
}
