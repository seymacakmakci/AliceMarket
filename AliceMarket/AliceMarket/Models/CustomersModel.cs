﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceMarket.Models
{
    public class CustomersModel
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
