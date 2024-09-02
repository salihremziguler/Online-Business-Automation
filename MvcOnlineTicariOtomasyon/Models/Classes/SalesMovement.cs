using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class SalesMovement
    {
        [Key]
        public int SalesID { get; set; } // PK

        public DateTime Date { get; set; }

        public int Piece { get; set; }

        public int Price { get; set; }

        public decimal TotalPrice { get; set; }

        public int ProductID { get; set; } // FK
        public int CurrentID { get; set; } // FK
        public int PersonelID { get; set; } // FK
        public virtual Product Product { get; set; }

        public virtual Current Current { get; set; }

        public virtual Personel Personel { get; set; }

    }
}