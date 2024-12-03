using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb.Models
{
    public class OrderDetailsViewModel
    {


        public List<Product> Products { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

}


