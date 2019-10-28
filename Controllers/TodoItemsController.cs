﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bau302TodoList.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Bau302TodoList.Controllers
{
    [Authorize]

    public class TodoItemsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: TodoItems
        public async Task<ActionResult> Index()
        {
            var todoItems = db.TodoItems.Include(t => t.Category).Include(t => t.Customer).Include(t => t.Department).Include(t => t.Manager).Include(t => t.Organizator).Include(t => t.Side);
            return View(await todoItems.ToListAsync());
        }

        // GET: TodoItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // GET: TodoItems/Create
        public ActionResult Create()
        {
            var todoItem = new TodoItem();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName");
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName");
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name");
            return View(todoItem);
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Status,CategoryId,Attachment,DepartmentId,SideId,CustomerId,ManagerId,OrganizatorId,MeetingDate,PlanedDate,FinishedDate,ReviseDate,ConversationSubject,SupporterChompany,SupporterDoctor,ConversationAttendeeCount,ScheduledOrganizationDate,MailingSubject,PosterSubject,PosterCount,Elearning,TypesOfScans,AsoCountInScans,TypesOfOrganization,AsoCountInOrganization,TypesOfVaccinationOrganization,AsoCountInVaccinationOrganization,AmountOfCompansationForPoster,CorparateProductivityReport,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.CreateDate = DateTime.Now;
                todoItem.CreatedBy = "Unknown";
                todoItem.UpdateDate = DateTime.Now;
                todoItem.UpdatedBy = "Unknown";
                db.TodoItems.Add(todoItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", todoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", todoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", todoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", todoItem.SideId);
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", todoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", todoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", todoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", todoItem.SideId);
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Status,CategoryId,Attachment,DepartmentId,SideId,CustomerId,ManagerId,OrganizatorId,MeetingDate,PlanedDate,FinishedDate,ReviseDate,ConversationSubject,SupporterChompany,SupporterDoctor,ConversationAttendeeCount,ScheduledOrganizationDate,MailingSubject,PosterSubject,PosterCount,Elearning,TypesOfScans,AsoCountInScans,TypesOfOrganization,AsoCountInOrganization,TypesOfVaccinationOrganization,AsoCountInVaccinationOrganization,AmountOfCompansationForPoster,CorparateProductivityReport,CreateDate,CreatedBy,UpdateDate,UpdatedBy")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.UpdateDate = DateTime.Now;
                todoItem.UpdatedBy = "Unknown";
                db.Entry(todoItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", todoItem.CategoryId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", todoItem.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", todoItem.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.ManagerId);
            ViewBag.OrganizatorId = new SelectList(db.Contacts, "Id", "FirstName", todoItem.OrganizatorId);
            ViewBag.SideId = new SelectList(db.Sides, "Id", "Name", todoItem.SideId);
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            db.TodoItems.Remove(todoItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from todoitems in db.TodoItems.ToList()
                              select new
                              {
                                  Baslik = todoitems.Title,
                                  Aciklama = todoitems.Description,
                                  Kategori = todoitems.Category.Name,
                                  DosyaEki = todoitems.Attachment,
                                  Departman = todoitems.Department.Name,
                                  Taraf = todoitems.Side.Name,
                                  Müsteri = todoitems.Customer.Name,
                                  Yonetici = todoitems.Manager.FirstName,
                                  Organizator = todoitems.Organizator.FirstName,
                                  Durum = todoitems.Status,
                                  ToplantiTarihi = todoitems.MeetingDate,
                                  PlanlananTarih = todoitems.PlanedDate,
                                  BitirilmeTarihi = todoitems.FinishedDate,
                                  RevizeTarihi = todoitems.ReviseDate,
                                  GorusmeKonusu = todoitems.ConversationSubject,
                                  DestekleyenFirma = todoitems.SupporterChompany,
                                  DestekleyenHekim = todoitems.SupporterDoctor,
                                  GorusmeKatilimciSayisi = todoitems.ConversationAttendeeCount,
                                  PlanlananOrganizasyonTarihi = todoitems.ScheduledOrganizationDate,
                                  MailKonuları = todoitems.MailingSubject,
                                  AfisKonusu = todoitems.PosterSubject,
                                  AfisSayisi = todoitems.PosterCount,
                                  Elearning = todoitems.Elearning,
                                  YapilanTaramalarınTurleri = todoitems.TypesOfScans,
                                  YapilanTaramalardakiAsoSayisi = todoitems.AsoCountInScans,
                                  OrganizasyonTurleri = todoitems.TypesOfOrganization,
                                  OrganizasyondakiAsoSayisi = todoitems.AsoCountInOrganization,
                                  AsıOrganizasyonTurleri = todoitems.TypesOfVaccinationOrganization,
                                  AsıOrganizasyonundakiAsoSayisi = todoitems.AsoCountInVaccinationOrganization,
                                  AfisicinTazminatMiktari = todoitems.AmountOfCompansationForPoster,
                                  KurumsalVerimlilikRaporu = todoitems.CorparateProductivityReport,
                                  Olusturulmatarihi = todoitems.CreateDate,
                                  OlusturanKullanici = todoitems.CreatedBy,
                                  GuncellenmeTarihi = todoitems.UpdateDate,
                                  GuncelleyenKullanici = todoitems.UpdatedBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=yapilacaklar.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            grid.RenderControl(hw);

            Response.Write(sw.ToString());
            Response.End();
        }
        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            Response.ClearContent();
            sw.WriteLine("Baslik,Aciklama,Kategori,DosyaEki,Departman,Taraf,Müsteri,Yonetici,Organizator,Durum,ToplantiTarihi,PlanlananTarih,BitirilmeTarihi,RevizeTarihi,GorusmeKonusu,DestekleyenFirma,DestekleyenHekim,GorusmeKatilimciSayisi,PlanlananOrganizasyonTarihi,MailKonuları,AfisKonusu,AfisSayisi,Elearning,YapilanTaramalarınTurleri,YapilanTaramalardakiAsoSayisi,OrganizasyonTurleri,OrganizasyondakiAsoSayisi,AsıOrganizasyonTurleri,AsıOrganizasyonundakiAsoSayisi,AfisicinTazminatMiktari,KurumsalVerimlilikRaporu,Olusturulmatarihi,OlusturanKullanici,GuncellenmeTarihi,GuncelleyenKullanici");
            Response.AddHeader("content-disposition", "attachment;filename=yapilacaklar.csv");
            Response.ContentType = "text/csv";
            var todoitem = db.TodoItems.ToList();
            foreach (var todoitems in todoitem)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34}",

                    todoitems.Title,
                    todoitems.Description,
                    todoitems.Category.Name,
                    todoitems.Attachment,
                    todoitems.Department.Name,
                    todoitems.Side.Name,
                    todoitems.Customer.Name,
                    todoitems.Manager.FirstName,
                    todoitems.Organizator.FirstName,
                    todoitems.Status,
                    todoitems.MeetingDate,
                    todoitems.PlanedDate,
                    todoitems.FinishedDate,
                    todoitems.ReviseDate,
                    todoitems.ConversationSubject,
                    todoitems.SupporterChompany,
                    todoitems.SupporterDoctor,
                    todoitems.ConversationAttendeeCount,
                    todoitems.ScheduledOrganizationDate,
                    todoitems.MailingSubject,
                    todoitems.PosterSubject,
                    todoitems.PosterCount,
                    todoitems.Elearning,
                    todoitems.TypesOfScans,
                    todoitems.AsoCountInScans,
                    todoitems.TypesOfOrganization,
                    todoitems.AsoCountInOrganization,
                    todoitems.TypesOfVaccinationOrganization,
                    todoitems.AsoCountInVaccinationOrganization,
                    todoitems.AmountOfCompansationForPoster,
                    todoitems.CorparateProductivityReport,
                    todoitems.CreateDate,
                    todoitems.CreatedBy,
                    todoitems.UpdateDate,
                    todoitems.UpdatedBy
                    )
                    );
            }
            Response.Write(sw.ToString());
            Response.End();
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
