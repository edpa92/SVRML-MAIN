using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class RepairTypecls
    {
        public int RepaireTypeId { set; get; }
        public int RepaireLogId { set; get; }
        public bool ChangeOil { set; get; }
        public bool ReplaceTire { set; get; }
        public bool ReplaceBreakpad { set; get; }
        public bool ReplaceAircleaner { set; get; }
        public bool ReplaceDrivebelt { set; get; }
        public decimal Cost { set; get; }


        public bool AddRepairType(RepairType reptype)
        {
            using (var data=new DataClasses1DataContext())
            {
                data.RepairTypes.InsertOnSubmit(reptype);
                data.SubmitChanges();

                return true;
            }
        }

        public IEnumerable<RepairType> GetAllRepType(int maintlogid)
        {
            using (var data = new DataClasses1DataContext())
            {
                var re = from r in data.RepairTypes
                         where r.RepairLogID == maintlogid
                         select r;
                return re;
            }
        }


        public RepairType GetRepType(int maintlogid)
        {
            using (var data = new DataClasses1DataContext())
            {
                var re = data.RepairTypes.SingleOrDefault(t=>t.RepairLogID==maintlogid);
                return re;
            }
        }

        public bool EditReptype(RepairType reptype)
        {
            using (var data=new DataClasses1DataContext())
            {
                var rt = data.RepairTypes.SingleOrDefault(r => r.RepairType_ID == reptype.RepairType_ID);
                if (rt==null)
                {
                    return false;
                }

                rt.Change_Oil = reptype.Change_Oil;
                rt.Replace_Tire = reptype.Replace_Tire;
                rt.Replace_Breakpads = reptype.Replace_Breakpads;
                rt.Replace_AirFilter= reptype.Replace_AirFilter;
                rt.OtherTypes = reptype.OtherTypes;
                rt.Replace_Drivebelt = reptype.Replace_Drivebelt;
                rt.Cost = reptype.Cost;
                data.SubmitChanges();
                return true;
            }
        }

        public bool DeleteReptype(int reptype)
        {
            using (var data=new DataClasses1DataContext())
            {
                var rt = data.RepairTypes.SingleOrDefault(r => r.RepairType_ID == reptype);
                if (rt==null)
                {
                    return false;
                }
                data.RepairTypes.DeleteOnSubmit(rt);

                return true;
            }
        }


    }
}
