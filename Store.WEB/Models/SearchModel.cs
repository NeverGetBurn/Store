using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.WEB.Models
{
    public class SearchModel
    {
        public SearchType Action { get; set; }
        [MinLength(3, ErrorMessage ="Нужно ввести минимум 3 символа")]
        public string SearchString { get; set; }
    }
}