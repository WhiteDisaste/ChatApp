using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public partial class Employee
    {
        public string GetHour
        {
            get
            {
                string name = "Доброй ночи!";
                if(DateTime.Now.Hour >=6 && DateTime.Now.Hour < 12)
                {
                    name = "Доброе утро!";
                    return name;
                }
                if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    name = "Добрый день!";
                    return name;
                }
                if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 23)
                {
                    name = "Добрый вечер!";
                    return name;
                }
                return name;
            }
        }

        public string Hello
        {
            get
            {
                string helloName = $"{GetHour} {Name}";
                return helloName;
            }
        }
       
    }
}
