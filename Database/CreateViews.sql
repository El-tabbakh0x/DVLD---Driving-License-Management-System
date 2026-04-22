Use DVLD_V1 ;

Create View vwLocalDrivingLicenseApplications AS
SELECT    L.LocalDrivingLicenseApplicationID, LC.LicenseClassName, P.NationalNo,
	 { fn CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) } AS FullName,
	 App.ApplicationDate, S.ApplicationStatusTitle AS ApplicationStatus,
         (SELECT        COUNT(*) AS Expr1
         FROM            dbo.Tests AS T 
		INNER JOINdbo.TestAppointments AS TA 
		ON TA.TestAppointmentID = T.TestAppointmentID
         WHERE (TA.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID) AND (T.TestResult = 1)) AS PassedTestCount
FROM            dbo.LocalDrivingLicenseApplications AS L 
	INNER JOIN dbo.LicenseClasses AS LC 
		ON L.LicenseClassID = LC.LicenseClassID 
	INNER JOIN dbo.Applications AS App 
		ON L.ApplicationID = App.ApplicationID 
	INNER JOIN dbo.people AS P 
		ON P.PersonID = App.PersonID 
	INNER JOIN dbo.ApplicationStatus AS S 
		ON App.ApplicationStatusID = S.ApplicationStatusID ;


Create view vwTestAppointments AS
SELECT TA.TestAppointmentID, TA.LocalDrivingLicenseApplicationID, TT.TestTypeTitle, LC.LicenseClassName, 
		TA.AppointmentDate, TA.PaidFees,CONCAT(P.FirstName,' ', P.SecondName,' ',P.ThirdName,' ',P.LastName) AS FullName ,
		TA.IsLocked
FROM TestAppointments TA 
	INNER JOIN TestTypes TT 
		ON 	TA.TestTypeID = TT.TestTypeID 
	INNER JOIN LocalDrivingLicenseApplications L
		ON TA.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID 
	INNER JOIN Applications App
		ON L.ApplicationID = App.ApplicationID 
	INNER JOIN people P
		ON App.PersonID = P.PersonID
	INNER JOIN LicenseClasses LC
		ON L.LicenseClassID = LC.LicenseClassID ;


Create View vwDriver AS
Select D.DriverID, D.PersonID, P.NationalNo, 
		CONCAT(P.FirstName,' ', P.SecondName,' ',P.ThirdName,' ',P.LastName) AS FullName ,
		D.CreatedDate, 
	(Select COUNT(LicenseID) AS NoumberOfActiveLicenses
	From Licenses 
	Where (IsActive=1) And (DriverID =D.DriverID)) AS NoumberOfActiveLicenses
From Drivers D 
		Inner Join people P
ON D.PersonID = P.PersonID ;


Create View vwDetainedLicense AS
Select DL.DetainedID, DL.LicenseID, DL.DetainedDate, DL.IsReleased,
		DL.FineFees, DL.ReleasedDate, 
		CONCAT(P.FirstName,' ', P.SecondName,' ',P.ThirdName,' ',P.LastName) AS FullName ,
		DL.ReleasedApplicationID
From people P
	Inner Join Drivers D
		ON P.PersonID = D.PersonID
	Inner Join Licenses L
		ON D.DriverID = L.DriverID 
	Right Outer Join DetainedLicenses DL
		ON L.LicenseID = DL.LicenseID ;


