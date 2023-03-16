using System.Collections.Generic;
using System.Linq;

namespace UPM
{
    public partial class Abonent
    {
        public string FIO => Surname + " " + Name[0] + "." + Patronumic[0] + ".";
        public string ListServices
        {
            get
            {
                List<ConectService> services = MainWindow.DB.ConectService.Where(x => x.AbonentID == AbonentID).ToList();
                string strServices = "";
                for (int i = 0; i < services.Count; i++)
                {
                    if (i == services.Count - 1)
                    {
                        strServices += services[i].Services.Title;
                    }
                    else
                    {
                        strServices = strServices + services[i].Services.Title + ", ";
                    }
                }
                return strServices;
            }

        }
    }
}
