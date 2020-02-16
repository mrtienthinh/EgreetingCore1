using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class OrderDetail : BaseModel
    {
        [Key]
        public int OrderDetailID { get; set; }

        [DisplayName("Sending Status")]
        public bool SendStatus { get; set; }

        [DisplayName("Sended Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}", ApplyFormatInEditMode = true)]
        public DateTime? SendTime { get; set; }

        public virtual Ecard Ecard { get; set; }

        public int? OrderID { get; set; }

        public virtual Order Order { get; set; }


    }
}
