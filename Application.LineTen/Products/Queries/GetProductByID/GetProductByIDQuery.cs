﻿using Application.LineTen.Products.DTOs;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Queries.GetProductByID
{
    public sealed class GetProductByIDQuery: IRequest<ProductDTO>
    {
        public ProductID ProductID { get; set; }
    }
}