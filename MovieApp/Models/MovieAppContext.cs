using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MovieApp.Models
{
    public class MovieAppContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
    }
}