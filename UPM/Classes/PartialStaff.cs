using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPM
{
    partial class Staff
    {
        public string StaffName
        {
            get 
            { 
                return Name+" "+LastName + " " + Surname ;
            }
        }
    }
}
