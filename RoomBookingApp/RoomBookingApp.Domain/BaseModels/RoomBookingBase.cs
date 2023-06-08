using System.ComponentModel.DataAnnotations;

#pragma warning disable VSSpell001 // Spell Check

namespace RoomBookingApp.Domain.BaseModels
#pragma warning restore VSSpell001 // Spell Check
{
    public class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(80)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date must be in the future!", new string[] { nameof(Date) });
            }
        }
    }
}