﻿using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Customers;
using MediatR;

namespace Application.LineTen.Customers.Queries.GetCustomerByID
{
    public sealed record GetCustomerByIDQuery(CustomerID ID) : IRequest<CustomerDTO>;
}
