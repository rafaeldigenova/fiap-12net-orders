﻿using GeekBurger.Orders.API.Model;
using System;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IOrderRepository
    {
        Order GetProductById(int orderId);
        Task SaveAsync(Order order);
        Task UpdateAsync(Order order);
    }
}
