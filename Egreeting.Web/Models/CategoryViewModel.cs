using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Egreeting.Web.Models
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public int EcardsCount { get; set; }
    }
}