using AnonForum.BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnonForum.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostBLL _postBLL;
        public PostController(IPostBLL postBLL)
        {
            _postBLL = postBLL;
        }

        // GET: PostController
        public ActionResult Index()
        {
            var posts = _postBLL.GetAllPosts();
            return View(posts);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var post = _postBLL.GetPostbyID(id);
            return View(post);
        }

        // GET: PostController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
