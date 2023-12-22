using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LineTen.Products.Exceptions 
{
    public sealed class ProductValidationException : Exception
    {
        public ProductValidationException(string message)
            : base(message)
        {
        }
    }
}
