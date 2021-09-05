using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurantDemoApp.Models;

namespace WebAppRestaurantDemoApp.Repositories
{
    public class CustomerRepository
    {

        private RestaurantDBEntities objRestaurantDBEntities;
        public CustomerRepository()
        {


            objRestaurantDBEntities = new RestaurantDBEntities();

        }

        public IEnumerable<SelectListItem> GetAllCutomers()
        {

            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestaurantDBEntities.Customers

                                  select new SelectListItem()

                                  {
                                      Text = obj.CustomerName,
                                      Value = obj.CustomerID.ToString(),
                                      Selected = true
                                  }).ToList();

            return objSelectListItems;
        }
    }
}