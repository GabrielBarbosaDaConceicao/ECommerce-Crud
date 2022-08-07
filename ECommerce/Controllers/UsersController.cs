using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ECommerce.Classes;
using ECommerce.Models;
using static ECommerce.Classes.UserHelper;

namespace ECommerce.Controllers
{
    public class UsersController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        //CONTROLE DE LIST VIEW EM CASCATA
        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.DepartamentsId == departmentId);
            return Json(cities);
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Cities).Include(u => u.Company).Include(u => u.Departments);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanys(), "CompanyId", "Name");
            ViewBag.DepartamentsId = new SelectList(CombosHelper.GetDepartments(), "DepartamentsId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                UsersHelper.CreateUserASP(user.UserName, "User");

                if (user.PhotoFile != null)
                {

                    var pic = string.Empty;
                    var folder = "~/Content/Users";
                    var file = string.Format("{0}.jpg", user.UserId);

                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        user.Photo = pic;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanys(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentsId = new SelectList(CombosHelper.GetDepartments(), "DepartamentsId", "Name", user.DepartamentsId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanys(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentsId = new SelectList(CombosHelper.GetDepartments(), "DepartamentsId", "Name", user.DepartamentsId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Users";
                    var file = string.Format("{0}.jpg", user.UserId);

                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        user.Photo = pic;
                    }

                }

                var db2 = new EcommerceContext();
                var currentUser = db2.Users.Find(user.UserId);
                if (currentUser.UserName != user.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, user.UserName);
                }
                db2.Dispose();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(CombosHelper.GetCompanys(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentsId = new SelectList(CombosHelper.GetDepartments(), "DepartamentsId", "Name", user.DepartamentsId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            UsersHelper.DeleteUser(user.UserName);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
