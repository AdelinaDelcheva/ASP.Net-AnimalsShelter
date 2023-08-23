using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace AnimalsShelterSystem.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using static AnimalsShelterSystem.Common.EntityValidationConstants.Review;
    public class AddReviewViewModel
    {
        [Required]
        [StringLength(TextMaxLength, MinimumLength = TextMinLength)]
        public string Text { get; set; } = null!;


        public string AnimalId { get; set; } = null!;

        public string AnimalName { get; set;} = null!;
    }
}
