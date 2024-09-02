using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ProductName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Brand { get; set; }

        public short Stock { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalesPrice { get; set; }

        public bool Status { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string ProductImagePath { get; set; }

      

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }


        public ICollection<SalesMovement> SalesMovements { get; set; }

    }
}