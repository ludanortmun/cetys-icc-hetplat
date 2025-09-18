using System.ComponentModel.DataAnnotations;

namespace FormsSampleApp.Models
{
    public class GreetingViewModel
    {
        [EmailAddress]
        [Required]
        public string From { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string To { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Message { get; set; } = string.Empty;
    }
}
