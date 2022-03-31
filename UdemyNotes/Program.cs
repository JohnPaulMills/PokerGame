using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNotes
{
    public enum ShippingMethod
    {
        RegularAirmail = 1,
        RegisteredAirMail = 2,
        Express = 3
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var method = ShippingMethod.Express;
            Console.WriteLine((int)method);

            var methodId = 3;
            Console.WriteLine((ShippingMethod)methodId);

            Console.WriteLine(method.ToString());
            var test = (int) Enum.Parse(typeof(ShippingMethod), method.ToString());
            Console.WriteLine(test);
        }
    }
}
