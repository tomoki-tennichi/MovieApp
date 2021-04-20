using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        //private readonly MovieAppContext _context;

        //public MoviesController(MovieAppContext context)
        //{
        //    _context = context;
        //}

        private MovieAppContext db = new MovieAppContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movie.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // AJAX
        // GET: Movies/Details
        public async Task<ActionResult> Details_Json(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Movie movie = db.Movie.Find(id);
            //if (movie == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(movie);

            var movie_info = await db.Movie.FirstOrDefaultAsync(m => m.Id == id);

            return PartialView("Partial", movie_info);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Published_at,Genre,Price")] Movie movie)
        {
            // *****DEBUG01
            if (movie.Title == null)
            {
                // return RedirectToAction("Create", new { movie = movie.Title });
                // どちらの return でも Movie.Ceate 画面に戻される。
                return View();
                // return View(movie);
            
            }



            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Search");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Published_at,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Search");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Search");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // Search
        public ActionResult Search([Bind(Include = "GenreName, TitleName")]　SearchViewModel model)
        {
            var sign = Request.QueryString["Sign"];

            // 公開日の項目名の右にある下向き三角形、押下時
            if (sign == "1")
            {
                model.Movies = db.Movie.OrderByDescending(a => a.Published_at).ToList();
            }
            // ジャンル名が空欄ではない場合
            // ジャンル、タイトル両方
            else if (!string.IsNullOrEmpty(model.GenreName) && !string.IsNullOrEmpty(model.TitleName))
            {
                var list = db.Movie.Where(item => item.Genre.IndexOf(model.GenreName) == 0
                                                && item.Title.IndexOf(model.TitleName) == 0).ToList();

                model.Movies = list;
            }
            else if (!string.IsNullOrEmpty(model.GenreName) && string.IsNullOrEmpty(model.TitleName))
            {
                // ジャンル名アリ、タイトル名ナシ
                // var list
                model.Movies = db.Movie.Where(item => item.Genre == model.GenreName).ToList();

                // model.Movies = list;
            }
            else if (string.IsNullOrEmpty(model.GenreName) && !string.IsNullOrEmpty(model.TitleName))
            {
                // ジャンル名ナシ、タイトル名アリ
                model.Movies = db.Movie.Where(item => item.Title == model.TitleName).ToList();


                //var list = db.Movie.Where(item => item.Title.IndexOf(model.TitleName) == 0).ToList();
                //model.Movies = list;
            }
            else
            {
                // 何も入力されていない場合
                model.Movies = db.Movie.OrderByDescending(a => a.Id).ToList();
            }

            return View(model);
        }


        // ソート処理
        [HttpGet]
        public ActionResult Sort_By_Published_At(Movie movie)
        {
            return View(db.Movie.OrderByDescending(a => a.Published_at).ToList());
        }
    }
}
