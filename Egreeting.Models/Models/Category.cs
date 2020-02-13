using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            Ecards = new HashSet<Ecard>();
        }

        [Key]
        public int CategoryID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Slug")]
        public string CategorySlug { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Name")]
        public string CategoryName { get; set; }

        public virtual ICollection<Ecard> Ecards { get; set; }
    }
}
