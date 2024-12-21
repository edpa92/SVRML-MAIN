using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class RepairMaintenanceLogcls
    {
        public int ReportLogID { set; get; }
        public int VehicleID { set; get; }
        public int AdminID { set; get; }
        public DateTime RepairDate { set; get; }
        public int Milage { set; get; }
        public string Remarks { set; get; }

        public bool AddRepairLog(RepairMaintenanceLog replog)
        {
            using (var data=new DataClasses1DataContext())
            {
                data.RepairMaintenanceLogs.InsertOnSubmit(replog);
                data.SubmitChanges();

                return true;
            }

        }

        public IEnumerable<RepairMaintenanceLog> GetAllRepairLog(int vehicleid)
        {
            using (var data=new DataClasses1DataContext())
            {
                var rl = from r in data.RepairMaintenanceLogs
                         where r.Vehicle_ID == vehicleid
                         select r;

                return rl;
            }
        }

        public RepairMaintenanceLog GetRepairLog(int id)
        {
            using (var data = new DataClasses1DataContext())
            {
                var rl = data.RepairMaintenanceLogs.SingleOrDefault(r=>r.RepairLog_ID==id);

                return rl;
            }
        }


        public bool EditRepairLog(RepairMaintenanceLog replog)
        {
            using (var data = new DataClasses1DataContext())
            {
                var rl=data.RepairMaintenanceLogs.SingleOrDefault(r => r.RepairLog_ID == replog.RepairLog_ID);
                if (rl==null)
                {
                    return false;
                }
                rl.Repair_Date = replog.Repair_Date;
                rl.Milage = replog.Milage;
                rl.Remarks = replog.Remarks;

                data.SubmitChanges();
                return true;
            }

        }

        public bool DeleteRepairLog(int id)
        {
            using (var data = new DataClasses1DataContext())
            {
                var rl = data.RepairMaintenanceLogs.SingleOrDefault(r => r.RepairLog_ID == id);
                if (rl == null)
                {
                    return false;
                }
                data.RepairMaintenanceLogs.DeleteOnSubmit(rl);
                data.SubmitChanges();
                return true;
            }

        }

    }
}
