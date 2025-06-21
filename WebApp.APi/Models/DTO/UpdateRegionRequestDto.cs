using System.ComponentModel.DataAnnotations;

namespace WebApp.APi.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [StringLength(3, ErrorMessage = "Code must be exactly 3 characters long.", MinimumLength = 3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
