USE [SVRML_DB]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrator](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[AdminPw] [varchar](50) NOT NULL,
	[NoDaysNotifyBeforeSched] [int] NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NotificationTbl]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NotificationTbl](
	[NotiId] [int] IDENTITY(1000,1) NOT NULL,
	[Details] [varchar](max) NOT NULL,
	[DateNotified] [datetime] NOT NULL,
	[SchedID] [int] NOT NULL,
 CONSTRAINT [PK_NotificationTbl] PRIMARY KEY CLUSTERED 
(
	[NotiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RepairMaintenanceLog]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RepairMaintenanceLog](
	[RepairLog_ID] [int] IDENTITY(1,1) NOT NULL,
	[Vehicle_ID] [int] NOT NULL,
	[Admin_ID] [int] NOT NULL,
	[Repair_Date] [date] NOT NULL,
	[Milage] [int] NOT NULL,
	[Remarks] [varchar](max) NULL,
 CONSTRAINT [PK_RepairMaintenance_Log] PRIMARY KEY CLUSTERED 
(
	[RepairLog_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RepairType]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RepairType](
	[RepairType_ID] [int] IDENTITY(1,1) NOT NULL,
	[RepairLogID] [int] NOT NULL,
	[Change_Oil] [bit] NULL,
	[Replace_Tire] [bit] NULL,
	[Replace_Breakpads] [bit] NULL,
	[Replace_AirFilter] [bit] NULL,
	[Replace_Drivebelt] [bit] NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[OtherTypes] [bit] NULL,
 CONSTRAINT [PK_Repair_Type] PRIMARY KEY CLUSTERED 
(
	[RepairType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SchedMaintenance]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchedMaintenance](
	[SchedMain_ID] [int] IDENTITY(1,1) NOT NULL,
	[Vehicle_ID] [int] NOT NULL,
	[Admin_ID] [int] NOT NULL,
	[Sched_Date] [datetime] NOT NULL,
	[MainInterval_Days] [int] NOT NULL,
	[NextSched_Date] [datetime] NULL,
	[Remarks] [text] NULL,
 CONSTRAINT [PK_Sched_Maintenance] PRIMARY KEY CLUSTERED 
(
	[SchedMain_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 05/01/2025 12:33:44 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Vehicle_ID] [int] IDENTITY(1,1) NOT NULL,
	[AdminId] [int] NOT NULL,
	[Brand] [varchar](50) NOT NULL,
	[Model] [varchar](50) NULL,
	[PlateNum] [varchar](50) NOT NULL,
	[Type] [varchar](max) NOT NULL,
	[SerialNum] [varchar](50) NOT NULL,
	[AcquisitionDate] [date] NULL,
	[AcquisitionCost] [decimal](18, 2) NOT NULL,
	[LastLTORegistration] [date] NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Vehicle_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[NotificationTbl]  WITH CHECK ADD  CONSTRAINT [FK_NotificationTbl_NotificationTbl] FOREIGN KEY([SchedID])
REFERENCES [dbo].[SchedMaintenance] ([SchedMain_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NotificationTbl] CHECK CONSTRAINT [FK_NotificationTbl_NotificationTbl]
GO
ALTER TABLE [dbo].[RepairMaintenanceLog]  WITH CHECK ADD  CONSTRAINT [FK_RepairMaintenance_Log_Administrator] FOREIGN KEY([Admin_ID])
REFERENCES [dbo].[Administrator] ([AdminId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RepairMaintenanceLog] CHECK CONSTRAINT [FK_RepairMaintenance_Log_Administrator]
GO
ALTER TABLE [dbo].[RepairMaintenanceLog]  WITH CHECK ADD  CONSTRAINT [FK_RepairMaintenance_Log_Vehicle] FOREIGN KEY([Vehicle_ID])
REFERENCES [dbo].[Vehicle] ([Vehicle_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RepairMaintenanceLog] CHECK CONSTRAINT [FK_RepairMaintenance_Log_Vehicle]
GO
ALTER TABLE [dbo].[RepairType]  WITH CHECK ADD  CONSTRAINT [FK_Repair_Type_RepairMaintenance_Log] FOREIGN KEY([RepairLogID])
REFERENCES [dbo].[RepairMaintenanceLog] ([RepairLog_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RepairType] CHECK CONSTRAINT [FK_Repair_Type_RepairMaintenance_Log]
GO
ALTER TABLE [dbo].[SchedMaintenance]  WITH CHECK ADD  CONSTRAINT [FK_Sched_Maintenance_Administrator1] FOREIGN KEY([Admin_ID])
REFERENCES [dbo].[Administrator] ([AdminId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SchedMaintenance] CHECK CONSTRAINT [FK_Sched_Maintenance_Administrator1]
GO
ALTER TABLE [dbo].[SchedMaintenance]  WITH CHECK ADD  CONSTRAINT [FK_Sched_Maintenance_Vehicle] FOREIGN KEY([Vehicle_ID])
REFERENCES [dbo].[Vehicle] ([Vehicle_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SchedMaintenance] CHECK CONSTRAINT [FK_Sched_Maintenance_Vehicle]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Administrator] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Administrator] ([AdminId])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_Administrator]
GO
