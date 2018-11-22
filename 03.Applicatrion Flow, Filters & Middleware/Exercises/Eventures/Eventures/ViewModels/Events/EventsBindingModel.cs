using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class EventsBindingModel : IValidatableObject
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = DateTime.Compare(Start, End);
            if (result >= 0)
            {
                yield return new ValidationResult("Invalid end date.");
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}
