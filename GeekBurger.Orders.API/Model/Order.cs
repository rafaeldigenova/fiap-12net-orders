﻿using GeekBurger.Orders.Contract.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeekBurger.Orders.API.Model
{
    public class Order 
    {
        [Key]
        public Guid OrderId { get; set; }
        public string Total { get; set; }
        public List<int> Products{get; set;}
        public List<int> ProductionIds { get; set; }
        public OrderState State { get; set; }

        public void SetState(OrderState state) => State = state;

    }
}
