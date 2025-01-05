using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class Vehiclecls
    {

        public int VehicleId { set; get; }
        public int AdminId { set; get; }
        public string BrandModel { set; get; }
        public string PlateNum { set; get; }
        public string Type { set; get; }
        public string SerialNum { set; get; }
        public DateTime AcquisitionDate { set; get; }
        public decimal AcquisitionCost { set; get; }
        public DateTime LastLTORegistration { set; get; }


        public bool AddVehicle(Vehicle vehicle)
        {
            using (var data = new DataClasses1DataContext())
            {
                data.Vehicles.InsertOnSubmit(vehicle);
                data.SubmitChanges();
                return true;
            }
           
        }

        public IEnumerable<Vehicle> GetAllVehicle(int adminid)
        {
            IEnumerable<Vehicle> vehicle;

            using (var data=new DataClasses1DataContext())
            {
                // Query to get all users
               vehicle = from v in data.Vehicles
                              where v.AdminId == adminid
                              select v;

            }


            // Return the result
            return vehicle;            
        }

        

        public Vehicle GetVehicle(string plateno)
        {
            Vehicle vehicle;
            using (var data = new DataClasses1DataContext())
            {
                // Query to get all users
               vehicle = data.Vehicles.SingleOrDefault(v => v.PlateNum == plateno);
            }

                // Return the result
            return vehicle;          

        }



        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (var data = new DataClasses1DataContext())
            {
                var veh = data.Vehicles.SingleOrDefault(v => v.Vehicle_ID == vehicle.Vehicle_ID);
                if (veh == null)
                {
                    return false;
                }
                veh.AcquisitionCost = vehicle.AcquisitionCost;
                veh.AcquisitionDate = vehicle.AcquisitionDate;
                veh.Brand = vehicle.Brand;
                veh.Model = vehicle.Model;
                veh.PlateNum = vehicle.PlateNum;
                veh.SerialNum = vehicle.SerialNum;
                veh.Type = vehicle.Type;
                veh.LastLTORegistration = vehicle.LastLTORegistration;

                data.SubmitChanges();
                return true;
            }

        }

        public bool DeleteVehicle(int vehicleId)
        {
            using (var data = new DataClasses1DataContext())
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
}
