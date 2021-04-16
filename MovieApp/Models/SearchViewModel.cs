using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class SearchViewModel
    {
        [DisplayName("ジャンル名:")]
        public string GenreName { get; set; }

        [DisplayName("タイトル名:")]
        public string TitleName { get; set; }

        public List<Movie> Movies { get; set; }
    }
}