using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("タイトル")]
        public string Title { get; set; }

        [DisplayName("公開日")]
        public DateTime Published_at { get; set; }

        [Required]
        [DisplayName("ジャンル")]
        public string Genre { get; set; }

        [DisplayName("金額")]
        public int Price { get; set; }
    }
}