using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Укажите название")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Укажите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Укажите цену")]
        [Range(0, int.MaxValue, ErrorMessage ="Неверно указанная сумма")]
        public int Price { get; set; }

        public bool Status { get; set; }

    }
}