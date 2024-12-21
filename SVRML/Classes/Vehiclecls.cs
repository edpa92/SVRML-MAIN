using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class Vehiclecls
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public int VehicleId { set; get; }
        public int AdminId { set; get; }
        public string BrandModel { set; get; }
        public string PlateNum { set; get; }
        public string Type { set; get; }
        public string SerialNum { set; get; }
        public DateTime AcquisitionDate { set; get; }
        public decimal AcquisitionCost { set; get; }


        public bool AddVehicle(Vehicle vehicle)
        {
                data.Vehicles.InsertOnSubmit(vehicle);
                data.SubmitChanges();
                return true;
        }

        public IEnumerable<Vehicle> GetAllVehicle(int adminid)
        {
                           
                // Query to get all users
                var vehicle = from v in data.Vehicles
                              where v.AdminId ==adminid
                              select v;
                              

                // Return the result
                return vehicle;
        }

        

        public Vehicle GetVehicle(int id)
        {
           
                // Query to get all users
                var vehicle = data.Vehicles.SingleOrDefault(v=>v.Vehicle_ID==id);

                // Return the result
                return vehicle;
            
        }



        public bool UpdateVehicle(Vehicle vehicle)
        {
          
                var veh=data.Vehicles.SingleOrDefault(v => v.Vehicle_ID == vehicle.Vehicle_ID);
                if (veh==null)
                {
                    return false;
                }
                veh.AcquisitionCost = vehicle.AcquisitionCost;
                veh.AcquisitionDate = vehicle.AcquisitionDate;
                veh.BrandModel = vehicle.BrandModel;
                veh.PlateNum = vehicle.PlateNum;
                veh.SerialNum = vehicle.SerialNum;
                veh.Type = vehicle.Type;

                data.SubmitChanges();
                return true;

        }

        public bool DeleteVehicle(int vehicleId)
        {
          
                var veh = data.Vehicles.SingleOrDefault(v => v.Vehicle_ID == vehicleId);
                if (veh == null)
                {
                    return false;
                }
                data.Vehicles.DeleteOnSubmit(veh);
                data.SubmitChanges();
                return true;

        }
    }
}
