﻿using Application.LineTen.Orders.DTOs;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderSummary
{
    public sealed record GetOrderSummaryQuery(Guid ID) : IRequest<OrderSummaryDTO>;
}
