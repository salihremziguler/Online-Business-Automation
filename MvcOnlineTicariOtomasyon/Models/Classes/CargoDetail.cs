using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class CargoDetail
    {
        [Key]
        public int CargoDetailID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10, ErrorMessage = "You can type up to 10 characters.")]
        [Required(ErrorMessage = "You cannot pass this field blank.")]
        public string TrackingCode { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20, ErrorMessage = "You can type up to 20 characters.")]
        [Required(ErrorMessage = "You cannot pass this field blank.")]
        public string Personel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20, ErrorMessage = "You can type up to 20 characters.")]
        [Required(ErrorMessage = "You cannot pass this field blank.")]
        public string Receiver { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Açıklama")]
        [Column(TypeName = "VarChar")]
        [StringLength(300)]
        public string Explanation { get; set; }



    }
}