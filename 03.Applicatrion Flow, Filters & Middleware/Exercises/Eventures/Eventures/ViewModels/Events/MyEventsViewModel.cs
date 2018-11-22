using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels.Events
{
    public class MyEventsViewModel
    {
        public ICollection<SingleEventViewModel> MyEvents { get; set; }
    }
}
