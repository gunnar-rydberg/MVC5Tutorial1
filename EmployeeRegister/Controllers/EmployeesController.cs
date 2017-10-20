using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeRegister.DataAccessLayer;
using EmployeeRegister.Models;
using EmployeeRegister.Util;

namespace EmployeeRegister.Controllers
{
    public class EmployeesController : Controller
    {
        private RegisterContext db = new RegisterContext();

        private const int noOfEntriesPerPage = 6;

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Economics()
        {
            var model = db.Employees.Where(x => x.Department == "Economics").ToList();
            return View(model);
        }

        public ActionResult IndexWithPagination(int page = 1,
                                                string sortOrder = "Id")
        {
            // Setup sort-order dropdown-box
            // ? Where to inititate this ? Include and instanciate in (a)   Model ?
            // refactor: 
            var dropDownBox = new DropDownListInfo();
            dropDownBox.Item.Add(
                new DropDownListInfo.DropDownListItem() { DisplayName = "First Name", ModelName = "FirstName" });
            dropDownBox.Item.Add(
                new DropDownListInfo.DropDownListItem() { DisplayName = "Last Name", ModelName = "LastName" });
            dropDownBox.Item.Add(
                new DropDownListInfo.DropDownListItem() { DisplayName = "Id", ModelName = "Id" });
            dropDownBox.Title = "Sort Order : " + dropDownBox.Item.Where(x => x.ModelName == sortOrder).FirstOrDefault().DisplayName;

            ViewBag.DropDownBox = dropDownBox;

            // Sorting: refoactor into 
            IOrderedQueryable<Employee> query;
            switch(sortOrder)
            {
                case "FirstName":
                    query = db.Employees.OrderBy(x => x.FirstName);
                    break;
                case "LastName":
                    query = db.Employees.OrderBy(x => x.LastName);
                    break;
                case "Id":
                default:
                    query = db.Employees.OrderBy(x => x.Id);
                    break;
            }
            //TODO reverse query.

            var model = query.Skip(noOfEntriesPerPage * (page - 1))
                             .Take(noOfEntriesPerPage);

            ViewBag.pagingInfo = new PagingInfo {
                CurrentPage = page,
                ItemsPerPage = noOfEntriesPerPage,
                TotalItems = db.Employees.Count()};

            ViewBag.sortOrderCurrent = sortOrder;
            //ViewBag.sortOrderList = new SelectList(new List<string>{
            //    "FirstName", "LastName"});

            return View(model);

        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Salary,Position,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Salary,Position,Department")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
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
