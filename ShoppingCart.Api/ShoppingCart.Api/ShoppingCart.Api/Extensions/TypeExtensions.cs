using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Extensions
{
    public static class TypeExtensions
    {
        public static string SwaggerName(this Type type) => type.Name.Replace("Dto", "");
    }
}
