using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egreeting.Models.Models
{
    public class Ecard : BaseModel
    {
        public Ecard()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        public int EcardID { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long!")]
        [DisplayName("Ecard's name")]
        public string EcardName { get; set; }

        [Index(IsUnique = true)]
        [StringLength(200, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [MinLength(5, ErrorMessage = "The {0} must be at least {1} characters long!")]
        [DisplayName("Slug")]
        public string EcardSlug { get; set; }

        //Enum EcardType
        [Required]
        [Range(1, 4, ErrorMessage = "Ecard type not exist!")]
        [DisplayName("Ecard type")]
        public int EcardType { get; set; }

        [StringLength(150, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Ecard's link")]
        public string EcardUrl { get; set; }

        [StringLength(150, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Thumbnail")]
        public string ThumbnailUrl { get; set; }

        [Required]
        [DisplayName("Ecard's price")]
        public double Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
