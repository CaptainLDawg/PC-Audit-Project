Use Logan_Test1

Create table PCAuditHardware(
AuditDate varchar(30),
AuditorName varchar (80),
ManufacturerName varchar (70),
ModelName varchar (70),
ComputerName varchar(50),
OperatingSystem varchar (100),
OperatingArchitecture varchar (15),
ServicePack varchar (20),
SerialNumber varchar (30),
ProcessorName varchar (50),
NoProcessors int,
RamAmt float,
HardDrivesize float,
FreeHardDrive float,
Comments varchar (305))

Create table PCAuditHardwareChangeControl(
AuditDate varchar(30),
AuditorName varchar (80),
ManufacturerName varchar (70),
ModelName varchar (70),
ComputerName varchar(50),
OperatingSystem varchar (100),
OperatingArchitecture varchar (15),
ServicePack varchar (20),
SerialNumber varchar (30),
ProcessorName varchar (50),
NoProcessors int,
RamAmt float,
HardDrivesize float,
FreeHardDrive float,
Comments varchar (305))

Create table PCAuditNetwork(
ComputerName varchar(40),
NetworkField varchar (30),
NetworkVariable varchar(30))

Create table PCAuditPrinters(
ComputerName varchar(40),
PrinterName varchar (255))

Create table PCAuditPeripherals(
ComputerName varchar(40),
PeripheralName varchar (255))

Create table PCAuditSoftware(
ComputerName varchar(40),
SoftwareName varchar (255),
SoftwareVendor varchar(255),
SoftwareVersion varchar(100))
	
Select * From PCAuditHardware
Select * From PCAuditNetwork
Select * From PCAuditPrinters
Select * From PCAuditPeripherals
Select * From PCAuditSoftware

