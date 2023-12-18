﻿using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommand: IRequest<bool>
    {
        public OrderID OrderID { get; set; }
    }
}