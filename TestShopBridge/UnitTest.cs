using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.Controllers;
using ShopBridge.Models;

namespace TestShopBridge
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
       public void GetDetails()
        {
            int id = 1;
            ItemDetailsController controller = new ItemDetailsController();
            ActionResult result =  controller.Details(id);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void CreateNewItem()
        {
            ItemDetail itemDetail = new ItemDetail();
            itemDetail.Name = "Apple Watch";
            itemDetail.Description = "Apple Watch Description";
            itemDetail.Price = 650;
            ItemDetailsController cnt = new ItemDetailsController();
            ActionResult result = cnt.Create(itemDetail);
            Assert.IsNotNull(result);
        }
        
        
        [TestMethod]
        public void editItem()
        {
            int id = 3;
            ItemDetail itemDetail = new ItemDetail();
            itemDetail.Name = "iPhone";
            itemDetail.Description = "iPhone Description";
            itemDetail.Price = 6500;
            ItemDetailsController cnt = new ItemDetailsController();
            ActionResult result = cnt.Edit(itemDetail);
            Assert.IsNotNull(result);
        }

    }
}
