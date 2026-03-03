Create database DVLD_V1;
use  DVLD_V1;

--Create Lookup Tables

Create table Countries (
CountryID int identity(1,1) primary key Not Null,
CountryName Nvarchar(50)  Not Null 
);

Create table Genders (
GenderID int identity(1,1) primary key Not Null,
GenderName Nvarchar(20)  Not Null 
);
 
Create table ApplicationTypes (
ApplicationTypeID int identity(1,1) primary key Not Null,
ApplicationTypeTitle Nvarchar(50)  Not Null,
ApplicationFees Smallmoney  Not Null
);

Create table ApplicationStatus (
ApplicationStatusID int identity(1,1) primary key Not Null,
ApplicationStatusTitle Nvarchar(50)  Not Null 
);

Create table LicenseClasses (
LicenseClassID int identity(1,1) primary key Not Null,
LicenseClassName Nvarchar(50)  Not Null,
ClassDescription Nvarchar(500)  Not Null,
MinimumeAllowedAge tinyint  Not Null, 
DefaultValidityLength tinyint  Not Null, 
ClassFees smallmoney  Not Null 
);

Create table TestTypes (
TestTypeID int identity(1,1) primary key Not Null,
TestTypeTitle Nvarchar(50)  Not Null,
TestTypeDescription Nvarchar(500)  Not Null,
TestTypeFees smallmoney  Not Null 
);

Create table LicenseIssueReasons (
IssueReasonID int identity(1,1) primary key Not Null,
IssueReasonsTitle Nvarchar(50)  Not Null,
IssueReasonsFees smallmoney  Not Null 
);

--Create Tables

Create table people (
PersonID int identity(1,1) primary key Not Null,
NationalNo Nvarchar(20)  Not Null,
FirstName Nvarchar(20)  Not Null,
SecondName Nvarchar(20)  Not Null,
ThirdName Nvarchar(20)  Not Null,
LastName Nvarchar(20)  Not Null,
DateOfBirth DateTime  Not Null,
Phone Nvarchar(20)  Not Null,
Email Nvarchar(50)  Not Null,
Address Nvarchar(500)  Not Null,
ImagePath Nvarchar(250)  Not Null,
CountryID int  Not Null,
GenderID int  Not Null,

Constraint UQ_People_NationalNo Unique(NationalNo),
Constraint UQ_People_Email Unique(Email),

Constraint FK_Person_Contries
	Foreign key (CountryID) references Countries(CountryID),

Constraint FK_Person_Genders
	Foreign key (GenderID) references Genders(GenderID)
	);

Create table Users (
UserID int identity(1,1) primary key Not Null,
PersonID Int  Not Null,
UserName Nvarchar(50)  Not Null,
PasswordHash Nvarchar(256)  Not Null,
IsActive bit  Not Null Default 1,

Constraint UQ_Users_Person Unique(PersonID),

Constraint UQ_User_UserName Unique(UserName),

Constraint FK_User_People
	Foreign key (PersonID) references People(PersonID)
);

Create table Drivers (
DriverID int identity(1,1) primary key Not Null,
PersonID Int  Not Null,
CreatedByUserID Int  Not Null,
CreatedDate DateTime  Not Null Default GetDate(),

Constraint UQ_Drivers_Person Unique(PersonID),

Constraint FK_Driver_People
	Foreign key (PersonID) references People(PersonID),

Constraint FK_Driver_Users
	Foreign key (CreatedByUserID) references Users(UserID)
);

Create table Applications (
ApplicationID int identity(1,1) primary key Not Null,
PersonID Int  Not Null,
CreatedByUserID Int  Not Null,
ApplicationDate DateTime  Not Null Default GetDate(),
LastStatusDate DateTime  Not Null Default GetDate(),
ApplicationTypeID Int  Not Null,
ApplicationStatusID Int  Not Null,

Constraint FK_Application_People
	Foreign key (PersonID) references People(PersonID),

Constraint FK_Application_Users
	Foreign key (CreatedByUserID) references Users(UserID),

Constraint FK_Application_ApplicationTypes
	Foreign key (ApplicationTypeID) references ApplicationTypes(ApplicationTypeID),

Constraint FK_Application_ApplicationStatus
	Foreign key (ApplicationStatusID) references ApplicationStatus(ApplicationStatusID)
);

Create table LocalDrivingLicenseApplications (
LocalDrivingLicenseApplicationID int identity(1,1) primary key Not Null,
ApplicationID Int  Not Null,
LicenseClassID Int  Not Null,

Constraint FK_LocalDrivingLicenseApplication_Applications
	Foreign key (ApplicationID) references Applications(ApplicationID),

Constraint FK_LocalDrivingLicenseApplications_LicenseClasses
	Foreign key (LicenseClassID) references LicenseClasses(LicenseClassID)
);

Create table TestAppointments (
TestAppointmentID int identity(1,1) primary key Not Null,
TestTypeID Int  Not Null,
LocalDrivingLicenseApplicationID Int  Not Null,
AppointmentDate DateTime  Not Null,
PaidFees Smallmoney  Not Null,
IsLocked bit  Not Null default 0,
CreatedByUserID Int  Not Null,
RetakeTestApplicationID Int  Null,

Constraint FK_TsetAppointment_TestTypes
	Foreign key (TestTypeID) references TestTypes(TestTypeID),

Constraint FK_TsetAppointment_LocalDrivingLicenseApplications
	Foreign key (LocalDrivingLicenseApplicationID) references LocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID),
	
Constraint FK_TsetAppointment_Users
	Foreign key (CreatedByUserID) references Users(UserID),

Constraint FK_TsetAppointment_Applications
	Foreign key (RetakeTestApplicationID) references Applications(ApplicationID)
);

Create table Tests (
TestID int identity(1,1) primary key Not Null,
TestAppointmentID Int  Not Null,
TestResult bit  Not Null,
Notes Nvarchar(500)  Null,
CreatedByUserID Int  Not Null,

Constraint FK_Test_TestAppointments
	Foreign key (TestAppointmentID) references TestAppointments(TestAppointmentID),
	
Constraint FK_Test_Users
	Foreign key (CreatedByUserID) references Users(UserID)
);

Create table Licenses (
LicenseID int identity(1,1) primary key Not Null,
ApplicationID Int  Not Null,
DriverID Int  Not Null,
LicenseClassID Int  Not Null,
IssueDate DateTime  Not Null,
ExpirationDate DateTime  Not Null,
Notes Nvarchar(500)  Null,
PaidFees Smallmoney  Not Null,
IsActive bit  Not Null Default 1,
IssueReasonID Int  Not Null,
CreatedByUserID Int  Not Null,

Constraint CK_License_Data
	Check(ExpirationDate > IssueDate),

Constraint UQ_License_Application Unique(ApplicationID),

Constraint FK_License_ApplicationID
	Foreign key (ApplicationID) references Applications(ApplicationID),

	Constraint FK_License_Drivers
	Foreign key (DriverID) references Drivers(DriverID),

Constraint FK_License_LicenseClasses
	Foreign key (LicenseClassID) references LicenseClasses(LicenseClassID),

Constraint FK_License_LicenseIssueReasons
	Foreign key (IssueReasonID) references LicenseIssueReasons(IssueReasonID),

Constraint FK_Licens_Users
	Foreign key (CreatedByUserID) references Users(UserID)

);

Create table InternationalLicenses (
InternationalLicensesID int identity(1,1) primary key Not Null,
LocalLicens Int  Not Null,
ApplicationID Int  Not Null,
DriverID Int  Not Null,
IssueDate DateTime  Not Null,
ExpirationDate DateTime  Not Null,
Notes Nvarchar(500)  Null,
PaidFees Smallmoney  Not Null,
IsActive bit  Not Null Default 1,
CreatedByUserID Int  Not Null,

Constraint CK_InternationalLicense_Data
	Check(ExpirationDate > IssueDate),

Constraint UQ_InternationalLicense_Application Unique(ApplicationID),

Constraint FK_InternationalLicense_ApplicationID
	Foreign key (ApplicationID) references Applications(ApplicationID),

	Constraint FK_InternationalLicense_Drivers
	Foreign key (DriverID) references Drivers(DriverID),

Constraint FK_InternationalLicense_Licenses
	Foreign key (LocalLicens) references Licenses(LicenseID),

Constraint FK_InternationalLicense_Users
	Foreign key (CreatedByUserID) references Users(UserID)

);

Create table DetainedLicenses (
DetainedID int identity(1,1) primary key Not Null,
LicenseID Int  Not Null,
DetainedDate DateTime  Not Null,
FineFees Smallmoney  Not Null,
IsReleased bit Not  Null default 0,
ReleasedDate DateTime  Null,
CreatedByUserID Int  Not Null,
ReleasedByUserID Int  Null,
ReleasedApplicationID Int  Null,

Constraint FK_DetainedLicense_Licenses
	Foreign key (LicenseID) references Licenses(LicenseID),

Constraint FK_DetainedLicense_CreatedByUser
	Foreign key (CreatedByUserID) references Users(UserID),

Constraint FK_DetainedLicense_ReleasedByUser
	Foreign key (ReleasedByUserID) references Users(UserID),

Constraint FK_DetainedLicense_Applications
	Foreign key (ReleasedApplicationID) references Applications(ApplicationID)	
);