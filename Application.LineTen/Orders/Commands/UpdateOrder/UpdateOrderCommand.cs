﻿using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommand : IRequest<bool>
    {
        public OrderID OrderID { get; set; }
        public OrderStatus Status { get; set; }
    }
}