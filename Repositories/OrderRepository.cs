﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppRestaurantDemoApp.Models;
using WebAppRestaurantDemoApp.ViewModel;

namespace WebAppRestaurantDemoApp.Repositories
{
    public class OrderRepository
    {
        private RestaurantDBEntities objRestaurantDBEntities;

        public OrderRepository()
        {
            objRestaurantDBEntities = new RestaurantDBEntities();
        }

        public bool AddOrder(OrderViewModel objOrderViewModel) {

            Order objOrder = new Order();
            objOrder.CusotmerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = string.Format("{0:ddmmmyyyyhhmmss}",DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestaurantDBEntities.Orders.Add(objOrder);
            objRestaurantDBEntities.SaveChanges();
            int OrderId = objOrder.OrderId;


            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objRestaurantDBEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantDBEntities.SaveChanges();

                Transaction objTransaction = new Transaction();
                objTransaction.ItemId = item.ItemId;
                objTransaction.Quantity = (-1)*item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objRestaurantDBEntities.Transactions.Add(objTransaction);
                objRestaurantDBEntities.SaveChanges();
                     



            }
            return true;

        }
    }
}