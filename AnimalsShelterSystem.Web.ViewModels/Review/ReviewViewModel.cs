using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
        public string Creator { get; set; } = null!;
    }
}
