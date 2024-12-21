using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class SchedMaintenancecls
    {
        public int SchedMain_ID { set; get; }
        public int Vehicle_ID { set; get; }
        public int Admin_ID { set; get; }
        public DateTime Sched_Date { set; get; }
        public int MainInterval_Days { set; get; }
        public DateTime NextSched_Date { set; get; }
        public string Remarks { set; get; }

        public bool CreateSchedMaintenance(SchedMaintenance schedmain)
        {
            using (var data = new DataClasses1DataContext())
            {
                data.SchedMaintenances.InsertOnSubmit(schedmain);
                data.SubmitChanges();
            }

            return true;
        }

        public IEnumerable<SchedMaintenance> GetAll(int vehicleid)
        {
            using (var data=new DataClasses1DataContext())
            {
                var sch = from s in data.SchedMaintenances
                          where s.Vehicle_ID==vehicleid
                          select s;

                return sch;
            }
        }

        public SchedMaintenance GetSched(int schedid)
        {
            using (var data = new DataClasses1DataContext())
            {
                var sch =data.SchedMaintenances.SingleOrDefault(s=>s.SchedMain_ID==schedid);

                return sch;
            }
        }

        public bool UpdateSched(SchedMaintenance schedmain)
        {
            using (var data=new DataClasses1DataContext())
            {
                var sch = data.SchedMaintenances.SingleOrDefault(s => s.SchedMain_ID == schedmain.SchedMain_ID);
                if (sch==null)
                {
                    return false;
                }

                sch.MainInterval_Days = schedmain.MainInterval_Days;
                sch.NextSched_Date = schedmain.NextSched_Date;
                sch.Remarks = schedmain.Remarks;
                sch.Sched_Date = schedmain.Sched_Date;
                data.SubmitChanges();
                return true;
            }
        }

        public bool DeleteSched(int SchedId)
        {
            using (var data=new DataClasses1DataContext())
            {
                var sch = data.SchedMaintenances.SingleOrDefault(s => s.SchedMain_ID == SchedId);
                if (sch==null)
                {
                    return false;
                }

                data.SchedMaintenances.DeleteOnSubmit(sch);
                data.SubmitChanges();
            }
            return true;
        }
    }
}
