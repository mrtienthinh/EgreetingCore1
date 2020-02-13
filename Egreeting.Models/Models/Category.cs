using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egreeting.Models.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            CategoryEcards = new HashSet<CategoryEcard>();
        }

        [Key]
        public int CategoryID { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Slug")]
        public string CategorySlug { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Name")]
        public string CategoryName { get; set; }

        public virtual ICollection<CategoryEcard> CategoryEcards { get; set; }
    }
}
