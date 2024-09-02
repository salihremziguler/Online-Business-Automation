using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string InvoiceSequenceNo { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string InvoiceSerialNo { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string TaxAdministration { get; set; }
        [Column(TypeName = "char")]
        [StringLength(5)]
        public string Hour { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DeliveringPerson { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ReceiverPerson { get; set; }

        public decimal TotalPrice { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }



    }
}