using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionDream.Models
{
    public class CartJoin
    {
        
        public Cart cart { get; set; }
        public Product product { get; set; }

        public Variation variation { get; set; }

    }
}