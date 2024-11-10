using System.ComponentModel.DataAnnotations;

namespace WebApplicationSports.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public string TeamCity { get; set; }

        [Required]
        public string TeamRegion { get; set; }

        [Required]
        public string TeamCountry { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string MascotName { get; set; }
    }
}
