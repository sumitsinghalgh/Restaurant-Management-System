using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurantDemoApp.Models;
using WebAppRestaurantDemoApp.Repositories;
using WebAppRestaurantDemoApp.ViewModel;

namespace WebAppRestaurantDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities objRestaurantDBEntities;

        public HomeController()
        {
            objRestaurantDBEntities = new RestaurantDBEntities();
        }

        public ActionResult Index()
        {

            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRepository.GetAllCutomers(),objItemRepository.GetAllItems(),objPaymentTypeRepository.GetAllPaymentType());

            return View(objMultipleModels);
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objRestaurantDBEntities.Items.Single(model => model.ItemID==itemId).ItemPrice;
            return Json(UnitPrice,JsonRequestBehavior.AllowGet);
        
        }

        [HttpPost]
        public JsonResult  Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);            
            return Json(data:"Your Order has been Successfully Placed.", JsonRequestBehavior.AllowGet);

        }



    }
}