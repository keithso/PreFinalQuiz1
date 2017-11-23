using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreFinalQuiz1.Models
{
    public class TitlesModels
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Publisher")]
        [Required(ErrorMessage = "This is required!")]
        public int PubID { get; set; }
        public List<SelectListItem> Publishers { get; set; }
        public string Publisher { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "This is required!")]
        public int AuthorID { get; set; }
        public List<SelectListItem> Authors { get; set; }
        public string Author { get; set; }

        [Display(Name = "Title Name")]
        [MaxLength(40, ErrorMessage = "Up to 40 characters only.")]
        [Required(ErrorMessage = "This is required!")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "This is required!")]
        [DataType(DataType.Currency)]
        public string Price { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This is required!")]
        public string PubDate { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        [MaxLength(300)]
        public string Notes { get; set; }
    }
}