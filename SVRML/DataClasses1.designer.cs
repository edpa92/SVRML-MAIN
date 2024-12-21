﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SVRML
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SVRML_DB")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAdministrator(Administrator instance);
    partial void UpdateAdministrator(Administrator instance);
    partial void DeleteAdministrator(Administrator instance);
    partial void InsertRepairMaintenanceLog(RepairMaintenanceLog instance);
    partial void UpdateRepairMaintenanceLog(RepairMaintenanceLog instance);
    partial void DeleteRepairMaintenanceLog(RepairMaintenanceLog instance);
    partial void InsertRepairType(RepairType instance);
    partial void UpdateRepairType(RepairType instance);
    partial void DeleteRepairType(RepairType instance);
    partial void InsertSchedMaintenance(SchedMaintenance instance);
    partial void UpdateSchedMaintenance(SchedMaintenance instance);
    partial void DeleteSchedMaintenance(SchedMaintenance instance);
    partial void InsertVehicle(Vehicle instance);
    partial void UpdateVehicle(Vehicle instance);
    partial void DeleteVehicle(Vehicle instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::SVRML.Properties.Settings.Default.SVRML_DBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Administrator> Administrators
		{
			get
			{
				return this.GetTable<Administrator>();
			}
		}
		
		public System.Data.Linq.Table<RepairMaintenanceLog> RepairMaintenanceLogs
		{
			get
			{
				return this.GetTable<RepairMaintenanceLog>();
			}
		}
		
		public System.Data.Linq.Table<RepairType> RepairTypes
		{
			get
			{
				return this.GetTable<RepairType>();
			}
		}
		
		public System.Data.Linq.Table<SchedMaintenance> SchedMaintenances
		{
			get
			{
				return this.GetTable<SchedMaintenance>();
			}
		}
		
		public System.Data.Linq.Table<Vehicle> Vehicles
		{
			get
			{
				return this.GetTable<Vehicle>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Administrator")]
	public partial class Administrator : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _AdminId;
		
		private string _Username;
		
		private string _AdminPw;
		
		private EntitySet<RepairMaintenanceLog> _RepairMaintenanceLogs;
		
		private EntitySet<SchedMaintenance> _SchedMaintenances;
		
		private EntitySet<Vehicle> _Vehicles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAdminIdChanging(int value);
    partial void OnAdminIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnAdminPwChanging(string value);
    partial void OnAdminPwChanged();
    #endregion
		
		public Administrator()
		{
			this._RepairMaintenanceLogs = new EntitySet<RepairMaintenanceLog>(new Action<RepairMaintenanceLog>(this.attach_RepairMaintenanceLogs), new Action<RepairMaintenanceLog>(this.detach_RepairMaintenanceLogs));
			this._SchedMaintenances = new EntitySet<SchedMaintenance>(new Action<SchedMaintenance>(this.attach_SchedMaintenances), new Action<SchedMaintenance>(this.detach_SchedMaintenances));
			this._Vehicles = new EntitySet<Vehicle>(new Action<Vehicle>(this.attach_Vehicles), new Action<Vehicle>(this.detach_Vehicles));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdminId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int AdminId
		{
			get
			{
				return this._AdminId;
			}
			set
			{
				if ((this._AdminId != value))
				{
					this.OnAdminIdChanging(value);
					this.SendPropertyChanging();
					this._AdminId = value;
					this.SendPropertyChanged("AdminId");
					this.OnAdminIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdminPw", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string AdminPw
		{
			get
			{
				return this._AdminPw;
			}
			set
			{
				if ((this._AdminPw != value))
				{
					this.OnAdminPwChanging(value);
					this.SendPropertyChanging();
					this._AdminPw = value;
					this.SendPropertyChanged("AdminPw");
					this.OnAdminPwChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_RepairMaintenanceLog", Storage="_RepairMaintenanceLogs", ThisKey="AdminId", OtherKey="Admin_ID")]
		public EntitySet<RepairMaintenanceLog> RepairMaintenanceLogs
		{
			get
			{
				return this._RepairMaintenanceLogs;
			}
			set
			{
				this._RepairMaintenanceLogs.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_SchedMaintenance", Storage="_SchedMaintenances", ThisKey="AdminId", OtherKey="Admin_ID")]
		public EntitySet<SchedMaintenance> SchedMaintenances
		{
			get
			{
				return this._SchedMaintenances;
			}
			set
			{
				this._SchedMaintenances.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_Vehicle", Storage="_Vehicles", ThisKey="AdminId", OtherKey="AdminId")]
		public EntitySet<Vehicle> Vehicles
		{
			get
			{
				return this._Vehicles;
			}
			set
			{
				this._Vehicles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RepairMaintenanceLogs(RepairMaintenanceLog entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = this;
		}
		
		private void detach_RepairMaintenanceLogs(RepairMaintenanceLog entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = null;
		}
		
		private void attach_SchedMaintenances(SchedMaintenance entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = this;
		}
		
		private void detach_SchedMaintenances(SchedMaintenance entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = null;
		}
		
		private void attach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = this;
		}
		
		private void detach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.Administrator = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.RepairMaintenanceLog")]
	public partial class RepairMaintenanceLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RepairLog_ID;
		
		private int _Vehicle_ID;
		
		private int _Admin_ID;
		
		private System.DateTime _Repair_Date;
		
		private int _Milage;
		
		private System.Data.Linq.Binary _Remarks;
		
		private EntitySet<RepairType> _RepairTypes;
		
		private EntityRef<Administrator> _Administrator;
		
		private EntityRef<Vehicle> _Vehicle;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRepairLog_IDChanging(int value);
    partial void OnRepairLog_IDChanged();
    partial void OnVehicle_IDChanging(int value);
    partial void OnVehicle_IDChanged();
    partial void OnAdmin_IDChanging(int value);
    partial void OnAdmin_IDChanged();
    partial void OnRepair_DateChanging(System.DateTime value);
    partial void OnRepair_DateChanged();
    partial void OnMilageChanging(int value);
    partial void OnMilageChanged();
    partial void OnRemarksChanging(System.Data.Linq.Binary value);
    partial void OnRemarksChanged();
    #endregion
		
		public RepairMaintenanceLog()
		{
			this._RepairTypes = new EntitySet<RepairType>(new Action<RepairType>(this.attach_RepairTypes), new Action<RepairType>(this.detach_RepairTypes));
			this._Administrator = default(EntityRef<Administrator>);
			this._Vehicle = default(EntityRef<Vehicle>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RepairLog_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int RepairLog_ID
		{
			get
			{
				return this._RepairLog_ID;
			}
			set
			{
				if ((this._RepairLog_ID != value))
				{
					this.OnRepairLog_IDChanging(value);
					this.SendPropertyChanging();
					this._RepairLog_ID = value;
					this.SendPropertyChanged("RepairLog_ID");
					this.OnRepairLog_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vehicle_ID", DbType="Int NOT NULL")]
		public int Vehicle_ID
		{
			get
			{
				return this._Vehicle_ID;
			}
			set
			{
				if ((this._Vehicle_ID != value))
				{
					if (this._Vehicle.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVehicle_IDChanging(value);
					this.SendPropertyChanging();
					this._Vehicle_ID = value;
					this.SendPropertyChanged("Vehicle_ID");
					this.OnVehicle_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Admin_ID", DbType="Int NOT NULL")]
		public int Admin_ID
		{
			get
			{
				return this._Admin_ID;
			}
			set
			{
				if ((this._Admin_ID != value))
				{
					if (this._Administrator.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAdmin_IDChanging(value);
					this.SendPropertyChanging();
					this._Admin_ID = value;
					this.SendPropertyChanged("Admin_ID");
					this.OnAdmin_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Repair_Date", DbType="Date NOT NULL")]
		public System.DateTime Repair_Date
		{
			get
			{
				return this._Repair_Date;
			}
			set
			{
				if ((this._Repair_Date != value))
				{
					this.OnRepair_DateChanging(value);
					this.SendPropertyChanging();
					this._Repair_Date = value;
					this.SendPropertyChanged("Repair_Date");
					this.OnRepair_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Milage", DbType="Int NOT NULL")]
		public int Milage
		{
			get
			{
				return this._Milage;
			}
			set
			{
				if ((this._Milage != value))
				{
					this.OnMilageChanging(value);
					this.SendPropertyChanging();
					this._Milage = value;
					this.SendPropertyChanged("Milage");
					this.OnMilageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remarks", DbType="VarBinary(50)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Remarks
		{
			get
			{
				return this._Remarks;
			}
			set
			{
				if ((this._Remarks != value))
				{
					this.OnRemarksChanging(value);
					this.SendPropertyChanging();
					this._Remarks = value;
					this.SendPropertyChanged("Remarks");
					this.OnRemarksChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="RepairMaintenanceLog_RepairType", Storage="_RepairTypes", ThisKey="RepairLog_ID", OtherKey="RepairLogID")]
		public EntitySet<RepairType> RepairTypes
		{
			get
			{
				return this._RepairTypes;
			}
			set
			{
				this._RepairTypes.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_RepairMaintenanceLog", Storage="_Administrator", ThisKey="Admin_ID", OtherKey="AdminId", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Administrator Administrator
		{
			get
			{
				return this._Administrator.Entity;
			}
			set
			{
				Administrator previousValue = this._Administrator.Entity;
				if (((previousValue != value) 
							|| (this._Administrator.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Administrator.Entity = null;
						previousValue.RepairMaintenanceLogs.Remove(this);
					}
					this._Administrator.Entity = value;
					if ((value != null))
					{
						value.RepairMaintenanceLogs.Add(this);
						this._Admin_ID = value.AdminId;
					}
					else
					{
						this._Admin_ID = default(int);
					}
					this.SendPropertyChanged("Administrator");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_RepairMaintenanceLog", Storage="_Vehicle", ThisKey="Vehicle_ID", OtherKey="Vehicle_ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Vehicle Vehicle
		{
			get
			{
				return this._Vehicle.Entity;
			}
			set
			{
				Vehicle previousValue = this._Vehicle.Entity;
				if (((previousValue != value) 
							|| (this._Vehicle.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Vehicle.Entity = null;
						previousValue.RepairMaintenanceLogs.Remove(this);
					}
					this._Vehicle.Entity = value;
					if ((value != null))
					{
						value.RepairMaintenanceLogs.Add(this);
						this._Vehicle_ID = value.Vehicle_ID;
					}
					else
					{
						this._Vehicle_ID = default(int);
					}
					this.SendPropertyChanged("Vehicle");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RepairTypes(RepairType entity)
		{
			this.SendPropertyChanging();
			entity.RepairMaintenanceLog = this;
		}
		
		private void detach_RepairTypes(RepairType entity)
		{
			this.SendPropertyChanging();
			entity.RepairMaintenanceLog = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.RepairType")]
	public partial class RepairType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RepairType_ID;
		
		private int _RepairLogID;
		
		private System.Nullable<bool> _Change_Oil;
		
		private System.Nullable<bool> _Replace_Tire;
		
		private System.Nullable<bool> _Replace_Breakpads;
		
		private System.Nullable<bool> _Replace_Aircleaner;
		
		private System.Nullable<bool> _Replace_Drivebelt;
		
		private decimal _Cost;
		
		private EntityRef<RepairMaintenanceLog> _RepairMaintenanceLog;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRepairType_IDChanging(int value);
    partial void OnRepairType_IDChanged();
    partial void OnRepairLogIDChanging(int value);
    partial void OnRepairLogIDChanged();
    partial void OnChange_OilChanging(System.Nullable<bool> value);
    partial void OnChange_OilChanged();
    partial void OnReplace_TireChanging(System.Nullable<bool> value);
    partial void OnReplace_TireChanged();
    partial void OnReplace_BreakpadsChanging(System.Nullable<bool> value);
    partial void OnReplace_BreakpadsChanged();
    partial void OnReplace_AircleanerChanging(System.Nullable<bool> value);
    partial void OnReplace_AircleanerChanged();
    partial void OnReplace_DrivebeltChanging(System.Nullable<bool> value);
    partial void OnReplace_DrivebeltChanged();
    partial void OnCostChanging(decimal value);
    partial void OnCostChanged();
    #endregion
		
		public RepairType()
		{
			this._RepairMaintenanceLog = default(EntityRef<RepairMaintenanceLog>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RepairType_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int RepairType_ID
		{
			get
			{
				return this._RepairType_ID;
			}
			set
			{
				if ((this._RepairType_ID != value))
				{
					this.OnRepairType_IDChanging(value);
					this.SendPropertyChanging();
					this._RepairType_ID = value;
					this.SendPropertyChanged("RepairType_ID");
					this.OnRepairType_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RepairLogID", DbType="Int NOT NULL")]
		public int RepairLogID
		{
			get
			{
				return this._RepairLogID;
			}
			set
			{
				if ((this._RepairLogID != value))
				{
					if (this._RepairMaintenanceLog.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRepairLogIDChanging(value);
					this.SendPropertyChanging();
					this._RepairLogID = value;
					this.SendPropertyChanged("RepairLogID");
					this.OnRepairLogIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Change_Oil", DbType="Bit")]
		public System.Nullable<bool> Change_Oil
		{
			get
			{
				return this._Change_Oil;
			}
			set
			{
				if ((this._Change_Oil != value))
				{
					this.OnChange_OilChanging(value);
					this.SendPropertyChanging();
					this._Change_Oil = value;
					this.SendPropertyChanged("Change_Oil");
					this.OnChange_OilChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Replace_Tire", DbType="Bit")]
		public System.Nullable<bool> Replace_Tire
		{
			get
			{
				return this._Replace_Tire;
			}
			set
			{
				if ((this._Replace_Tire != value))
				{
					this.OnReplace_TireChanging(value);
					this.SendPropertyChanging();
					this._Replace_Tire = value;
					this.SendPropertyChanged("Replace_Tire");
					this.OnReplace_TireChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Replace_Breakpads", DbType="Bit")]
		public System.Nullable<bool> Replace_Breakpads
		{
			get
			{
				return this._Replace_Breakpads;
			}
			set
			{
				if ((this._Replace_Breakpads != value))
				{
					this.OnReplace_BreakpadsChanging(value);
					this.SendPropertyChanging();
					this._Replace_Breakpads = value;
					this.SendPropertyChanged("Replace_Breakpads");
					this.OnReplace_BreakpadsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Replace_Aircleaner", DbType="Bit")]
		public System.Nullable<bool> Replace_Aircleaner
		{
			get
			{
				return this._Replace_Aircleaner;
			}
			set
			{
				if ((this._Replace_Aircleaner != value))
				{
					this.OnReplace_AircleanerChanging(value);
					this.SendPropertyChanging();
					this._Replace_Aircleaner = value;
					this.SendPropertyChanged("Replace_Aircleaner");
					this.OnReplace_AircleanerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Replace_Drivebelt", DbType="Bit")]
		public System.Nullable<bool> Replace_Drivebelt
		{
			get
			{
				return this._Replace_Drivebelt;
			}
			set
			{
				if ((this._Replace_Drivebelt != value))
				{
					this.OnReplace_DrivebeltChanging(value);
					this.SendPropertyChanging();
					this._Replace_Drivebelt = value;
					this.SendPropertyChanged("Replace_Drivebelt");
					this.OnReplace_DrivebeltChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cost", DbType="Decimal(18,2) NOT NULL")]
		public decimal Cost
		{
			get
			{
				return this._Cost;
			}
			set
			{
				if ((this._Cost != value))
				{
					this.OnCostChanging(value);
					this.SendPropertyChanging();
					this._Cost = value;
					this.SendPropertyChanged("Cost");
					this.OnCostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="RepairMaintenanceLog_RepairType", Storage="_RepairMaintenanceLog", ThisKey="RepairLogID", OtherKey="RepairLog_ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public RepairMaintenanceLog RepairMaintenanceLog
		{
			get
			{
				return this._RepairMaintenanceLog.Entity;
			}
			set
			{
				RepairMaintenanceLog previousValue = this._RepairMaintenanceLog.Entity;
				if (((previousValue != value) 
							|| (this._RepairMaintenanceLog.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._RepairMaintenanceLog.Entity = null;
						previousValue.RepairTypes.Remove(this);
					}
					this._RepairMaintenanceLog.Entity = value;
					if ((value != null))
					{
						value.RepairTypes.Add(this);
						this._RepairLogID = value.RepairLog_ID;
					}
					else
					{
						this._RepairLogID = default(int);
					}
					this.SendPropertyChanged("RepairMaintenanceLog");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SchedMaintenance")]
	public partial class SchedMaintenance : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SchedMain_ID;
		
		private int _Vehicle_ID;
		
		private int _Admin_ID;
		
		private System.DateTime _Sched_Date;
		
		private int _MainInterval_Days;
		
		private System.Nullable<System.DateTime> _NextSched_Date;
		
		private string _Remarks;
		
		private EntityRef<Administrator> _Administrator;
		
		private EntityRef<Vehicle> _Vehicle;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSchedMain_IDChanging(int value);
    partial void OnSchedMain_IDChanged();
    partial void OnVehicle_IDChanging(int value);
    partial void OnVehicle_IDChanged();
    partial void OnAdmin_IDChanging(int value);
    partial void OnAdmin_IDChanged();
    partial void OnSched_DateChanging(System.DateTime value);
    partial void OnSched_DateChanged();
    partial void OnMainInterval_DaysChanging(int value);
    partial void OnMainInterval_DaysChanged();
    partial void OnNextSched_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnNextSched_DateChanged();
    partial void OnRemarksChanging(string value);
    partial void OnRemarksChanged();
    #endregion
		
		public SchedMaintenance()
		{
			this._Administrator = default(EntityRef<Administrator>);
			this._Vehicle = default(EntityRef<Vehicle>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SchedMain_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SchedMain_ID
		{
			get
			{
				return this._SchedMain_ID;
			}
			set
			{
				if ((this._SchedMain_ID != value))
				{
					this.OnSchedMain_IDChanging(value);
					this.SendPropertyChanging();
					this._SchedMain_ID = value;
					this.SendPropertyChanged("SchedMain_ID");
					this.OnSchedMain_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vehicle_ID", DbType="Int NOT NULL")]
		public int Vehicle_ID
		{
			get
			{
				return this._Vehicle_ID;
			}
			set
			{
				if ((this._Vehicle_ID != value))
				{
					if (this._Vehicle.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVehicle_IDChanging(value);
					this.SendPropertyChanging();
					this._Vehicle_ID = value;
					this.SendPropertyChanged("Vehicle_ID");
					this.OnVehicle_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Admin_ID", DbType="Int NOT NULL")]
		public int Admin_ID
		{
			get
			{
				return this._Admin_ID;
			}
			set
			{
				if ((this._Admin_ID != value))
				{
					if (this._Administrator.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAdmin_IDChanging(value);
					this.SendPropertyChanging();
					this._Admin_ID = value;
					this.SendPropertyChanged("Admin_ID");
					this.OnAdmin_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sched_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Sched_Date
		{
			get
			{
				return this._Sched_Date;
			}
			set
			{
				if ((this._Sched_Date != value))
				{
					this.OnSched_DateChanging(value);
					this.SendPropertyChanging();
					this._Sched_Date = value;
					this.SendPropertyChanged("Sched_Date");
					this.OnSched_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MainInterval_Days", DbType="Int NOT NULL")]
		public int MainInterval_Days
		{
			get
			{
				return this._MainInterval_Days;
			}
			set
			{
				if ((this._MainInterval_Days != value))
				{
					this.OnMainInterval_DaysChanging(value);
					this.SendPropertyChanging();
					this._MainInterval_Days = value;
					this.SendPropertyChanged("MainInterval_Days");
					this.OnMainInterval_DaysChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NextSched_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> NextSched_Date
		{
			get
			{
				return this._NextSched_Date;
			}
			set
			{
				if ((this._NextSched_Date != value))
				{
					this.OnNextSched_DateChanging(value);
					this.SendPropertyChanging();
					this._NextSched_Date = value;
					this.SendPropertyChanged("NextSched_Date");
					this.OnNextSched_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remarks", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string Remarks
		{
			get
			{
				return this._Remarks;
			}
			set
			{
				if ((this._Remarks != value))
				{
					this.OnRemarksChanging(value);
					this.SendPropertyChanging();
					this._Remarks = value;
					this.SendPropertyChanged("Remarks");
					this.OnRemarksChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_SchedMaintenance", Storage="_Administrator", ThisKey="Admin_ID", OtherKey="AdminId", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Administrator Administrator
		{
			get
			{
				return this._Administrator.Entity;
			}
			set
			{
				Administrator previousValue = this._Administrator.Entity;
				if (((previousValue != value) 
							|| (this._Administrator.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Administrator.Entity = null;
						previousValue.SchedMaintenances.Remove(this);
					}
					this._Administrator.Entity = value;
					if ((value != null))
					{
						value.SchedMaintenances.Add(this);
						this._Admin_ID = value.AdminId;
					}
					else
					{
						this._Admin_ID = default(int);
					}
					this.SendPropertyChanged("Administrator");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_SchedMaintenance", Storage="_Vehicle", ThisKey="Vehicle_ID", OtherKey="Vehicle_ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Vehicle Vehicle
		{
			get
			{
				return this._Vehicle.Entity;
			}
			set
			{
				Vehicle previousValue = this._Vehicle.Entity;
				if (((previousValue != value) 
							|| (this._Vehicle.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Vehicle.Entity = null;
						previousValue.SchedMaintenances.Remove(this);
					}
					this._Vehicle.Entity = value;
					if ((value != null))
					{
						value.SchedMaintenances.Add(this);
						this._Vehicle_ID = value.Vehicle_ID;
					}
					else
					{
						this._Vehicle_ID = default(int);
					}
					this.SendPropertyChanged("Vehicle");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Vehicle")]
	public partial class Vehicle : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Vehicle_ID;
		
		private int _AdminId;
		
		private string _BrandModel;
		
		private string _PlateNum;
		
		private string _Type;
		
		private string _SerialNum;
		
		private System.Nullable<System.DateTime> _AcquisitionDate;
		
		private decimal _AcquisitionCost;
		
		private EntitySet<RepairMaintenanceLog> _RepairMaintenanceLogs;
		
		private EntitySet<SchedMaintenance> _SchedMaintenances;
		
		private EntityRef<Administrator> _Administrator;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnVehicle_IDChanging(int value);
    partial void OnVehicle_IDChanged();
    partial void OnAdminIdChanging(int value);
    partial void OnAdminIdChanged();
    partial void OnBrandModelChanging(string value);
    partial void OnBrandModelChanged();
    partial void OnPlateNumChanging(string value);
    partial void OnPlateNumChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnSerialNumChanging(string value);
    partial void OnSerialNumChanged();
    partial void OnAcquisitionDateChanging(System.Nullable<System.DateTime> value);
    partial void OnAcquisitionDateChanged();
    partial void OnAcquisitionCostChanging(decimal value);
    partial void OnAcquisitionCostChanged();
    #endregion
		
		public Vehicle()
		{
			this._RepairMaintenanceLogs = new EntitySet<RepairMaintenanceLog>(new Action<RepairMaintenanceLog>(this.attach_RepairMaintenanceLogs), new Action<RepairMaintenanceLog>(this.detach_RepairMaintenanceLogs));
			this._SchedMaintenances = new EntitySet<SchedMaintenance>(new Action<SchedMaintenance>(this.attach_SchedMaintenances), new Action<SchedMaintenance>(this.detach_SchedMaintenances));
			this._Administrator = default(EntityRef<Administrator>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Vehicle_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Vehicle_ID
		{
			get
			{
				return this._Vehicle_ID;
			}
			set
			{
				if ((this._Vehicle_ID != value))
				{
					this.OnVehicle_IDChanging(value);
					this.SendPropertyChanging();
					this._Vehicle_ID = value;
					this.SendPropertyChanged("Vehicle_ID");
					this.OnVehicle_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdminId", DbType="Int NOT NULL")]
		public int AdminId
		{
			get
			{
				return this._AdminId;
			}
			set
			{
				if ((this._AdminId != value))
				{
					if (this._Administrator.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAdminIdChanging(value);
					this.SendPropertyChanging();
					this._AdminId = value;
					this.SendPropertyChanged("AdminId");
					this.OnAdminIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BrandModel", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string BrandModel
		{
			get
			{
				return this._BrandModel;
			}
			set
			{
				if ((this._BrandModel != value))
				{
					this.OnBrandModelChanging(value);
					this.SendPropertyChanging();
					this._BrandModel = value;
					this.SendPropertyChanged("BrandModel");
					this.OnBrandModelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlateNum", DbType="VarChar(50)")]
		public string PlateNum
		{
			get
			{
				return this._PlateNum;
			}
			set
			{
				if ((this._PlateNum != value))
				{
					this.OnPlateNumChanging(value);
					this.SendPropertyChanging();
					this._PlateNum = value;
					this.SendPropertyChanged("PlateNum");
					this.OnPlateNumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SerialNum", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string SerialNum
		{
			get
			{
				return this._SerialNum;
			}
			set
			{
				if ((this._SerialNum != value))
				{
					this.OnSerialNumChanging(value);
					this.SendPropertyChanging();
					this._SerialNum = value;
					this.SendPropertyChanged("SerialNum");
					this.OnSerialNumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AcquisitionDate", DbType="Date")]
		public System.Nullable<System.DateTime> AcquisitionDate
		{
			get
			{
				return this._AcquisitionDate;
			}
			set
			{
				if ((this._AcquisitionDate != value))
				{
					this.OnAcquisitionDateChanging(value);
					this.SendPropertyChanging();
					this._AcquisitionDate = value;
					this.SendPropertyChanged("AcquisitionDate");
					this.OnAcquisitionDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AcquisitionCost", DbType="Decimal(18,2) NOT NULL")]
		public decimal AcquisitionCost
		{
			get
			{
				return this._AcquisitionCost;
			}
			set
			{
				if ((this._AcquisitionCost != value))
				{
					this.OnAcquisitionCostChanging(value);
					this.SendPropertyChanging();
					this._AcquisitionCost = value;
					this.SendPropertyChanged("AcquisitionCost");
					this.OnAcquisitionCostChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_RepairMaintenanceLog", Storage="_RepairMaintenanceLogs", ThisKey="Vehicle_ID", OtherKey="Vehicle_ID")]
		public EntitySet<RepairMaintenanceLog> RepairMaintenanceLogs
		{
			get
			{
				return this._RepairMaintenanceLogs;
			}
			set
			{
				this._RepairMaintenanceLogs.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Vehicle_SchedMaintenance", Storage="_SchedMaintenances", ThisKey="Vehicle_ID", OtherKey="Vehicle_ID")]
		public EntitySet<SchedMaintenance> SchedMaintenances
		{
			get
			{
				return this._SchedMaintenances;
			}
			set
			{
				this._SchedMaintenances.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Administrator_Vehicle", Storage="_Administrator", ThisKey="AdminId", OtherKey="AdminId", IsForeignKey=true)]
		public Administrator Administrator
		{
			get
			{
				return this._Administrator.Entity;
			}
			set
			{
				Administrator previousValue = this._Administrator.Entity;
				if (((previousValue != value) 
							|| (this._Administrator.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Administrator.Entity = null;
						previousValue.Vehicles.Remove(this);
					}
					this._Administrator.Entity = value;
					if ((value != null))
					{
						value.Vehicles.Add(this);
						this._AdminId = value.AdminId;
					}
					else
					{
						this._AdminId = default(int);
					}
					this.SendPropertyChanged("Administrator");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_RepairMaintenanceLogs(RepairMaintenanceLog entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = this;
		}
		
		private void detach_RepairMaintenanceLogs(RepairMaintenanceLog entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = null;
		}
		
		private void attach_SchedMaintenances(SchedMaintenance entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = this;
		}
		
		private void detach_SchedMaintenances(SchedMaintenance entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = null;
		}
	}
}
#pragma warning restore 1591
