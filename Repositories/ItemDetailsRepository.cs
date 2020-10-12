using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using ShopBridge.Models;


namespace ShopBridge.Repositories
{
    public class ItemDetailsRepository
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();
        public int SaveDetails(HttpPostedFileBase file, ItemDetail itemDetail)
        {
            itemDetail.StreamId = ConvertToBytes(file);
            var Content = new ItemDetail
            {
                Name = itemDetail.Name,
                Description = itemDetail.Description,
                Price = itemDetail.Price,
                StreamId = itemDetail.StreamId
            };
            db.ItemDetails.Add(Content);
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }public int EditDetails(HttpPostedFileBase file, ItemDetail itemDetail)
        {
            if(file.InputStream.Length > 0)
            {
                itemDetail.StreamId = ConvertToBytes(file);
            }
            
            var Content = new ItemDetail
            {
                Name = itemDetail.Name,
                Description = itemDetail.Description,
                Price = itemDetail.Price,
                StreamId = itemDetail.StreamId
            };

            db.Entry(itemDetail).State = EntityState.Modified;
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}