﻿using Application.LineTen.Common.Interfaces;
using Domain.LineTen.Customers;
using Persistence.LineTen.Repositories;

namespace Persistence.LineTen.Tests.Customers
{
    public class CustomersRepository_Create_Tests
    {
        private readonly LineTenDB _db;
        private readonly UnitOfWork _unitOfWork;
        private readonly CustomersRepository _repo;
        private readonly CustomerTestData _customerTestData;


        public CustomersRepository_Create_Tests()
        {
            _db = TestDBContext.GetTestDBContext();
            _unitOfWork = new UnitOfWork(_db);
            _repo = new CustomersRepository(_db);
            _customerTestData = new CustomerTestData();
        }

        [Fact]
        public async Task Create_Should_CreateAClient()
        {
            // Act
            _repo.Create(_customerTestData.Customer1);
            await _unitOfWork.SaveChangesAsync();
            var verifyCustomer = _repo.GetById(_customerTestData.Customer1.ID);

            // Assert
            Assert.Equal(expected: _customerTestData.Customer1.ID.value, actual: verifyCustomer.ID.value);
            Assert.Equal(expected: _customerTestData.Customer1.FirstName, actual: verifyCustomer.FirstName);
            Assert.Equal(expected: _customerTestData.Customer1.LastName, actual: verifyCustomer.LastName);
            Assert.Equal(expected: _customerTestData.Customer1.Phone, actual: verifyCustomer.Phone);
            Assert.Equal(expected: _customerTestData.Customer1.Email, actual: verifyCustomer.Email);
        }
    }
}
