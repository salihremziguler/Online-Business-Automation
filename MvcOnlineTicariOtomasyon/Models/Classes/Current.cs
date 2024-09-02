using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Current
    {
        [Key]
        public int CurrentID { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="En fazla 30 mesaj yazabilirsiniz.")]
        public string CurrentName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        public string CurrentSurname { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CurrentCity { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [EmailAddress]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CurrentMail { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CurrentPassword { get; set; }
        public bool Status { get; set; }
        public string CurrentImage { get; set; }
        public ICollection<SalesMovement> SalesMovements { get; set; }


    }
}