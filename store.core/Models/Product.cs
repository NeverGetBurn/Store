﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Store.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Укажите название")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Укажите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Укажите цену")]
        [Range(0.0, Double.MaxValue, ErrorMessage ="Неверно указанная сумма")]
        [DataType(DataType.Text,ErrorMessage = "Неверный формат")]
        public decimal Price { get; set; }

        public bool Status { get; set; }

    }
}