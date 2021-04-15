using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [DisplayName("タイトル")]
        public string Title { get; set; }

        [DisplayName("公開日")]
        public DateTime Published_at { get; set; }

        [DisplayName("ジャンル")]
        public string Genre { get; set; }

        [DisplayName("金額")]
        public int Price { get; set; }
    }
}