using System;
using System.Collections.Generic;
using System.Text;

namespace Egreeting.Models.Models
{
    public class CategoryEcard
    {
        public int CategoryId { get; set; }
        public int EcardId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Ecard Ecard { get; set; }
    }
}
