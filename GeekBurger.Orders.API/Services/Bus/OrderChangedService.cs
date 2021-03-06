﻿using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.API.Services.Infra;
using GeekBurger.Orders.Contract.Enums;
using GeekBurger.Orders.Contract.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class OrderChangedService : ServiceBusPub<Order>, IOrderChangedService
    {
        public override string _topic { get; set; } = "OrderChanged";

        public OrderChangedService(IConfiguration configuration, ILogService logService)
            : base(configuration, logService)
        {}

        public override void AddToMessageList(Order order)
            => _messages.Add(GetMessage(order));

        protected override Message GetMessage(Order order)
        {
            decimal.TryParse(order.Total, out decimal valor);
            var productChanged = new OrderChangedMessage
            {
                OrderId = order.OrderId,
                State = (OrderState)order.State,
                StoreId = order.StoreId,
                Value = valor
            };
            var productChangedSerialized = JsonConvert.SerializeObject(productChanged);
            var productChangedByteArray = Encoding.UTF8.GetBytes(productChangedSerialized);

            return new Message
            {
                Body = productChangedByteArray,
                MessageId = Guid.NewGuid().ToString(),
                Label = productChanged.OrderId.ToString()
            };
        }
    }
}
