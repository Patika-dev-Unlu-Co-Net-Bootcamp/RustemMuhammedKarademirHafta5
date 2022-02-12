using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Services
{
    public class DBLoggerService : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DBLogged] - " + message);
        }
    }
}
