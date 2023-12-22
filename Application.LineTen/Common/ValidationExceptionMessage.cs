using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LineTen.Common
{
    internal static class ValidationExceptionMessage
    {
        public static string Message(List<FluentValidation.Results.ValidationFailure> Errors)
        {
            var result = "";
            foreach (var error in Errors) 
            {
                if (result.Length > 0) result += " ";
                result += $"{error.PropertyName}: {error.ErrorMessage}";
            }
            return result;
        }
    }
}
