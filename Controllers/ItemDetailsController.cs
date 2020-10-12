using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopBridge.Models;
using ShopBridge.Repositories;

namespace ShopBridge.Controllers
{
    public class ItemDetailsController : Controller
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        // GET: ItemDetails
        public ActionResult Index()
        {
            return View(db.ItemDetails.ToList());
        }

        /// </summary>
        /// get item details from the inventory
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDetail itemDetail = db.ItemDetails.Find(id);
            if (itemDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ItemDetails", itemDetail);
        }

        /// </summary>
        /// get create item partial view
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_CreateItem", new ItemDetail());
        }


        /// </summary>
        /// create a new item into the inventory
        /// <param name="itemDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ItemDetail itemDetail)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ItemDetailsRepository repo = new ItemDetailsRepository();
            int i = repo.SaveDetails(file, itemDetail);

            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            return View(itemDetail);
        }


        /// </summary>
        /// Edit the item details
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemDetail itemDetail = db.ItemDetails.Find(id);
            if (itemDetail == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditItemDetails", itemDetail);
        }


        /// </summary>
        /// Delete the items from inventory
        /// <param name="itemDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemDetail itemDetail)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ItemDetailsRepository repo = new ItemDetailsRepository();
            if (ModelState.IsValid)
            {
                int i = repo.EditDetails(file, itemDetail);

                if (i == 1)
                {
                    return RedirectToAction("Index");
                }
           
            }
            return PartialView("_EditItemDetails", itemDetail);
        }


        /// </summary>
        /// Delete the items from inventory on confirmation
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public void DeleteConfirmed(int id)
        {
            ItemDetail itemDetail = db.ItemDetails.Find(id);
            db.ItemDetails.Remove(itemDetail);
            db.SaveChanges();
            //return RedirectToAction("Index");
        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }


        /// </summary>
        /// get image from stream id
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.ItemDetails where temp.ItemId == Id select temp.StreamId;
            byte[] cover = q.First();
            return cover;
        }



        /// </summary>
        /// dispose
        /// <param name="disposing"></param>
        /// <returns></returns>
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
