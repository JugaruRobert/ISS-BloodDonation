USE BloodDonation

go
CREATE TABLE Countries(
	[CountryID] uniqueidentifier,
	[Name] nvarchar(50),
	CONSTRAINT PK_Countries PRIMARY KEY ([CountryID])
)

GO

CREATE PROCEDURE Countries_Insert
(
  @CountryID uniqueidentifier,
  @Name nvarchar(50)
)
AS
BEGIN
	INSERT INTO Countries([CountryID], [Name])
    VALUES(@CountryID, @Name)
END

GO

CREATE PROCEDURE Countries_Remove
(
   @CountryID uniqueidentifier
)
AS
BEGIN
	DELETE FROM Countries
	WHERE [CountryID] = @CountryID
END

GO

CREATE PROCEDURE Countries_Update
(
   @CountryID uniqueidentifier,
   @Name nvarchar(50)
)
AS
BEGIN
	UPDATE Countries
	SET [Name] = @Name
	WHERE [CountryID] = @CountryID
END

GO

CREATE PROCEDURE Countries_ReadByID
(
  @CountryID uniqueidentifier
)
AS
BEGIN
    SELECT *
	FROM Countries
	WHERE [CountryID] = @CountryID
END

GO

CREATE PROCEDURE Countries_ReadAll
AS
BEGIN
    SELECT * 
	FROM Countries
END

GO

CREATE TABLE Cities(
   
    [CityID] uniqueidentifier,
    [CountryID] uniqueidentifier,
    [Name] nvarchar(50),
    CONSTRAINT PK_Cities PRIMARY KEY ([CityID]),
    CONSTRAINT FK_CitiesCountries FOREIGN KEY ([CountryID]) REFERENCES Countries([CountryID])
   
)
 
GO
 
CREATE PROCEDURE Cities_Insert
(
  @CityID uniqueidentifier,
  @CountryID uniqueidentifier,
  @Name nvarchar(50)
)
AS
BEGIN
    INSERT INTO Cities([CityID], [Name])
    VALUES(@CountryID, @Name)
END
 
GO
 
CREATE PROCEDURE Cities_Remove
(
  @CityID uniqueidentifier
)
AS
BEGIN
    DELETE FROM Cities
    WHERE [CityID] = @CityID
END
 
GO
 
CREATE PROCEDURE Cities_Update
(
  @CityID uniqueidentifier,
  @CountryID uniqueidentifier,
  @Name nvarchar(50)
)
AS
BEGIN
    UPDATE Cities
    SET [CountryID] = @CountryID, [Name] = @Name
    WHERE [CityID] = @CityID
END
 
GO
 
CREATE PROCEDURE Cities_ReadByID
(
  @CityID uniqueidentifier
)
AS
BEGIN
    SELECT *
    FROM Cities
    WHERE [CityID] = @CityID
END
 
GO
 
CREATE PROCEDURE Cities_ReadAll
AS
BEGIN
    SELECT *
    FROM Cities
END
 
GO

--Addresses

CREATE TABLE [Addresses] (
	[AddressID] UNIQUEIDENTIFIER,
	[Address] TEXT,
	[CityID] UNIQUEIDENTIFIER,
	CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressID]),
	CONSTRAINT [FK_Cities] FOREIGN KEY ([CityID]) REFERENCES [Cities]([CityID])
);
GO

CREATE PROCEDURE [Addresses_Insert]
	@AddressID UNIQUEIDENTIFIER,
	@Address TEXT,
	@CityID UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO
	[Addresses]([AddressID],[Address],[CityID])
	VALUES
	(@AddressID, @Address, @CityID)
END
GO

CREATE PROCEDURE [Addresses_Update]
	@AddressID UNIQUEIDENTIFIER,
	@Address TEXT,
	@CityID UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE [Addresses]
	SET [Address]=@Address, [CityID]=@CityID
	WHERE [AddressID] = @AddressID
END
GO

CREATE PROCEDURE [Addresses_Remove]
	@AddressID UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [Addresses]
	WHERE [AddressID] = @AddressID
END
GO

CREATE PROCEDURE [Addresses_ReadAll]
AS
BEGIN
	SELECT * FROM [Addresses]
END
GO

CREATE PROCEDURE [Addresses_ReadByID]
	@AddressID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT *
	FROM [Addresses]
	WHERE [AddressID] = @AddressID
END
GO


--BloodBanks

CREATE TABLE [BloodBanks]
(
	[BankID] uniqueidentifier,
	[Name] nvarchar(50),
	[AddressID] uniqueidentifier UNIQUE,

	CONSTRAINT [PK_BloodBanks] PRIMARY KEY ([BankID]),
	CONSTRAINT [FK_BloodBankAddress] FOREIGN KEY ([AddressID])
		REFERENCES [Addresses]([AddressID])
)


GO


CREATE PROCEDURE [BloodBanks_Insert]
(
	@BankID uniqueidentifier,
	@Name nvarchar(50),
	@AddressID uniqueidentifier
)
AS
BEGIN
    INSERT INTO [BloodBanks]([BankID],[Name],[AddressID]) 
	VALUES (@BankID,@Name,@AddressID)
END
GO

CREATE PROCEDURE [BloodBanks_Update]
(
	@BankID uniqueidentifier,
	@Name nvarchar(50),
	@AddressID uniqueidentifier
)
AS
BEGIN
	UPDATE [BloodBanks]
	SET [Name] = @Name,
	    [AddressID] = @AddressID
	WHERE [BankID] = @BankID
END
GO

CREATE PROCEDURE [BloodBanks_Remove]
(
	@BankID uniqueidentifier
)
AS
BEGIN
  DELETE 
  FROM [BloodBanks]
  WHERE [BankID] = @BankID
END
GO

CREATE PROCEDURE [BloodBanks_ReadAll]
AS
BEGIN
  SELECT *
  FROM [BloodBanks]
END
GO

CREATE PROCEDURE [BloodBanks_ReadByID]
(
	@BankID uniqueidentifier
)
AS
BEGIN
  SELECT *
  FROM [BloodBanks]
  WHERE [BankID] = @BankID
END
GO

--BloodTypes

CREATE TABLE [BloodTypes]
(
	[BloodTypeID] uniqueidentifier,
	[Blood] nvarchar(3),
	[RH] bit

	CONSTRAINT [PK_BloodTypes] PRIMARY KEY ([BloodTypeID])
)

GO

CREATE PROCEDURE [BloodTypes_Insert]
(
	@BloodTypeID uniqueidentifier,
	@Blood nvarchar(3),
	@RH bit
)
AS
BEGIN
    INSERT INTO [BloodTypes]([BloodTypeID],[Blood],[RH]) 
	VALUES (@BloodTypeID,@Blood,@RH)
END
GO

CREATE PROCEDURE [BloodTypes_Update]
(
	@BloodTypeID uniqueidentifier,
	@Blood nvarchar(3),
	@RH bit
)
AS
BEGIN
	UPDATE [BloodTypes]
	SET [Blood] = @Blood,
	    [RH] = @RH
	WHERE [BloodTypeID] = @BloodTypeID
END
GO

CREATE PROCEDURE [BloodTypes_Remove]
(
	@BloodTypeID uniqueidentifier
)
AS
BEGIN
  DELETE 
  FROM [BloodTypes]
  WHERE [BloodTypeID] = @BloodTypeID
END
GO

CREATE PROCEDURE [BloodTypes_ReadAll]
AS
BEGIN
  SELECT *
  FROM [BloodTypes]
END
GO

CREATE PROCEDURE [BloodTypes_ReadByID]
(
	@BloodTypeID uniqueidentifier
)
AS
BEGIN
  SELECT *
  FROM [BloodTypes]
  WHERE [BloodTypeID] = @BloodTypeID
END
GO


--RedBloodCells

CREATE TABLE [RedBloodCells]
(
	[RedBloodCellsID] uniqueidentifier,
	[BankID] uniqueidentifier,
	[BloodTypeID] uniqueidentifier,
	[ExpirationDate] date,
	[Quantity] int 

	CONSTRAINT [PK_RedBloodCells] PRIMARY KEY ([RedBloodCellsID])
	CONSTRAINT [FK_RedBloodCells_BloodBanks] FOREIGN KEY ([BankID])
		REFERENCES [BloodBanks]([BankID]),
	CONSTRAINT [FK_RedBloodCells_BloodTypes] FOREIGN KEY ([BloodTypeID])
		REFERENCES [BloodTypes]([BloodTypeID])
)

GO

CREATE PROCEDURE [RedBloodCells_Insert]
(
    @RedBloodCellsID uniqueidentifier,
	@BankID uniqueidentifier,
	@BloodTypeID uniqueidentifier,
	@ExpirationDate date,
	@Quantity int
)
AS
BEGIN
    INSERT INTO [RedBloodCells]([RedBloodCellsID],[BankID],[BloodTypeID],[ExpirationDate],[Quantity]) 
	VALUES (@RedBloodCellsID,@BankID,@BloodTypeID,@ExpirationDate,@Quantity)
END
GO

CREATE PROCEDURE [RedBloodCells_Update]
(
    @RedBloodCellsID uniqueidentifier,
	@BankID uniqueidentifier,
	@BloodTypeID uniqueidentifier,
	@ExpirationDate date,
	@Quantity int
)
AS
BEGIN
	UPDATE [RedBloodCells]
	SET [BankID] = @BankID,
	    [BloodTypeID] = @BloodTypeID,
		[ExpirationDate] = @ExpirationDate,
		[Quantity] = @Quantity
	WHERE [RedBloodCellsID] = @RedBloodCellsID
END
GO

CREATE PROCEDURE [RedBloodCells_Remove]
(
	@RedBloodCellsID uniqueidentifier
)
AS
BEGIN
  DELETE 
  FROM [RedBloodCells]
  WHERE [RedBloodCellsID] = @RedBloodCellsID
END
GO

CREATE PROCEDURE [RedBloodCells_ReadAll]
AS
BEGIN
  SELECT *
  FROM [RedBloodCells]
END
GO

CREATE PROCEDURE [RedBloodCells_ReadByID]
(
	@RedBloodCellsID uniqueidentifier
)
AS
BEGIN
  SELECT *
  FROM [RedBloodCells]
  WHERE [RedBloodCellsID] = @RedBloodCellsID
END
GO

--Plasma

GO
CREATE TABLE Plasma
([PlasmaID] UNIQUEIDENTIFIER,
[BankID] UNIQUEIDENTIFIER,
[BloodTypeID] UNIQUEIDENTIFIER,
[ExpirationDate] DATE,
[Quantity] INT,
CONSTRAINT [PK_Plasma] PRIMARY KEY([PlasmaID]),
CONSTRAINT [FK_BloodTypeID] FOREIGN KEY([BloodTypeID]) REFERENCES [BloodTypes]([BloodTypeID]),
CONSTRAINT [FK_BankID] FOREIGN KEY([BankID]) REFERENCES [BloodBanks]([BankID])
)

--Trombocites

CREATE TABLE Trombocites
([TrombocitesID] UNIQUEIDENTIFIER,
[BankID] UNIQUEIDENTIFIER,
[BloodTypeID] UNIQUEIDENTIFIER,
[ExpirationDate] DATE,
[Quantity] INT,
CONSTRAINT [PK_Trombocites] PRIMARY KEY([TrombocitesID]),
CONSTRAINT [FK_BloodTypeIDTrombocites] FOREIGN KEY([BloodTypeID]) REFERENCES [BloodTypes]([BloodTypeID]),
CONSTRAINT [FK_BankIDTrombocites] FOREIGN KEY([BankID]) REFERENCES [BloodBanks]([BankID])
)


GO
CREATE PROCEDURE Plasma_Insert
@PlasmaID UNIQUEIDENTIFIER,
@BankID UNIQUEIDENTIFIER,
@BloodTypeID UNIQUEIDENTIFIER,
@ExpirationDate DATE,
@Quantity INT
AS
BEGIN
INSERT INTO Plasma(
[PlasmaID] ,
[BankID] ,
[BloodTypeID] ,
[ExpirationDate] ,
[Quantity] )
VALUES (
@PlasmaID,
@BankID,
@BloodTypeID,
@ExpirationDate,
@Quantity)
END



GO
CREATE PROCEDURE Trombocites_Insert
@TrombocitesID UNIQUEIDENTIFIER,
@BankID UNIQUEIDENTIFIER,
@BloodTypeID UNIQUEIDENTIFIER,
@ExpirationDate DATE,
@Quantity INT
AS
BEGIN
INSERT INTO Trombocites(
[TrombocitesID] ,
[BankID] ,
[BloodTypeID] ,
[ExpirationDate] ,
[Quantity] )
VALUES (
@TrombocitesID,
@BankID,
@BloodTypeID,
@ExpirationDate,
@Quantity)
END


GO
CREATE PROCEDURE Plasma_Update
@PlasmaID UNIQUEIDENTIFIER,
@BankID UNIQUEIDENTIFIER,
@BloodTypeID UNIQUEIDENTIFIER,
@ExpirationDate DATE,
@Quantity INT
AS
BEGIN
UPDATE Plasma
SET
[BankID] = @BankID,
[BloodTypeID] = @BloodTypeID,
[ExpirationDate] = @ExpirationDate,
[Quantity] = @Quantity
WHERE [PlasmaID] = @PlasmaID
END




GO
CREATE PROCEDURE Trombocites_Update
@TrombocitesID UNIQUEIDENTIFIER,
@BankID UNIQUEIDENTIFIER,
@BloodTypeID UNIQUEIDENTIFIER,
@ExpirationDate DATE,
@Quantity INT
AS
BEGIN
UPDATE Trombocites
SET
[BankID] = @BankID,
[BloodTypeID] = @BloodTypeID,
[ExpirationDate] = @ExpirationDate,
[Quantity] = @Quantity
WHERE [TrombocitesID] = @TrombocitesID
END




GO 
CREATE PROCEDURE Plasma_Delete
@PlasmaID UNIQUEIDENTIFIER
AS 
BEGIN
DELETE 
FROM Plasma
WHERE [PlasmaID] = @PlasmaID
END




GO 
CREATE PROCEDURE Trombocites_Delete
@TrombocitesID UNIQUEIDENTIFIER
AS 
BEGIN
DELETE 
FROM Trombocites
WHERE [TrombocitesID] = @TrombocitesID
END




GO
CREATE PROCEDURE Plasma_ReadByID
@PlasmaID UNIQUEIDENTIFIER
AS 
BEGIN
SELECT *
FROM Plasma
WHERE ([PlasmaID] = @PlasmaID)
END



GO
CREATE PROCEDURE Plasma_ReadAll
AS 
BEGIN
SELECT *
FROM Plasma
END




GO
CREATE PROCEDURE Trombocites_ReadByID
@TrombocitesID UNIQUEIDENTIFIER
AS 
BEGIN
SELECT *
FROM Trombocites
WHERE ([TrombocitesID] = @TrombocitesID)
END



GO
CREATE PROCEDURE Trombocites_ReadAll
AS 
BEGIN
SELECT *
FROM Trombocites
END


go

--Hospitals

CREATE TABLE Hospitals(
	[HospitalID] UNIQUEIDENTIFIER,	
	[Name] nvarchar(50),
	[AddressID] UNIQUEIDENTIFIER UNIQUE,
	CONSTRAINT [PK_Hospitals] Primary Key ([HospitalID]),
	CONSTRAINT [FK_HospitalsAddresses] Foreign Key ([AddressID]) REFERENCES [Addresses]([AddressID])
)

GO

CREATE PROCEDURE Hospitals_Insert
( 
@HospitalID UNIQUEIDENTIFIER, 
@Name nvarchar(50), 
@AddressID UNIQUEIDENTIFIER
)
AS
BEGIN
	INSERT INTO Hospitals([HospitalID],[Name], [AddressID]) VALUES (@HospitalID,@Name,@AddressID)
END
GO

CREATE PROCEDURE Hospitals_Update 
(
@HospitalID UNIQUEIDENTIFIER,
@Name nvarchar(50),
@AddressID UNIQUEIDENTIFIER
)
AS
BEGIN
	UPDATE Hospitals
	SET [Name] = @Name,
		[AddressID] = @AddressID
	WHERE [HospitalID] = @HospitalID
END

GO

CREATE PROCEDURE Hospitals_ReadByID 
(
@HospitalsID UNIQUEIDENTIFIER
)
AS
BEGIN
	SELECT * 
	FROM Hospitals
	WHERE [HospitalID] = @HospitalsID
END

GO

CREATE PROCEDURE Hospitals_ReadAll
AS
BEGIN
	SELECT *
	FROM Hospitals
END

GO
--Doctors

CREATE TABLE [Doctors](
	[DoctorCNP] nvarchar(15),
	[HospitalID] uniqueidentifier,
	[FirstName] nvarchar(50),
	[LastName] nvarchar(50)

	CONSTRAINT PK_Doctors PRIMARY KEY ([DoctorCNP])
	CONSTRAINT FK_Hospitals FOREIGN KEY ([HospitalID]) REFERENCES [Hospitals] ([HospitalID])
)
GO

 --Patients

CREATE TABLE Patients(
	CNP nvarchar(15),
	[LastName] nvarchar(50),
	[FirstName] nvarchar(50),
	HospitalID UNIQUEIDENTIFIER,
	BloodTypeID UNIQUEIDENTIFIER,
	CONSTRAINT [PK_Patients] Primary Key ([CNP]),
	CONSTRAINT [FK_PatientsHospitals] Foreign Key ([HospitalID]) REFERENCES [Hospitals]([HospitalID]),
	CONSTRAINT [FK_PatientsBloodTypes] Foreign Key ([BloodTypeID]) REFERENCES [BloodTypes]([BloodTypeID])
)



GO

go
CREATE TABLE [Requests](
	[PatientCNP] nvarchar(15),
	[DoctorCNP] nvarchar(15),
	[BloodBankID] uniqueidentifier,
	[Priority] int,
	[RedBloodCellsQuantity] int,
	[PlasmaQuantity] int,
	[TrombocitesQuantity] int

	CONSTRAINT PK_Requests PRIMARY KEY ([PatientCNP]),
	CONSTRAINT FK_Doctors FOREIGN KEY ([DoctorCNP]) REFERENCES [Doctors] ([DoctorCNP]),
	CONSTRAINT FK_BloodBank FOREIGN KEY ([BloodBankID]) REFERENCES  [BloodBanks] ([BankID]),
	CONSTRAINT FK_Patients FOREIGN KEY ([PatientCNP]) REFERENCES [Patients] ([CNP])
)

GO

CREATE PROCEDURE Patients_Insert
( 
	@CNP nvarchar(15),
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@HospitalID UNIQUEIDENTIFIER,
	@BloodTypeID UNIQUEIDENTIFIER
)
AS
BEGIN
	INSERT INTO Patients([CNP],[LastName], [FirstName], [HospitalID], [BloodTypeID]) VALUES (@CNP, @LastName, @FirstName, @HospitalID, @BloodTypeID)
END
GO
CREATE PROCEDURE Patients_Update 
(
	@CNP nvarchar(15),
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@HospitalID UNIQUEIDENTIFIER,
	@BloodTypeID UNIQUEIDENTIFIER
)
AS
BEGIN
	UPDATE Patients
	SET [LastName] = @LastName,
		[FirstName] = @FirstName,
		[HospitalID] = @HospitalID,
		[BloodTypeID] = @BloodTypeID
	WHERE [CNP] = @CNP
END

GO

CREATE PROCEDURE Patients_ReadByID 
(
@CNP nvarchar(15)
)
AS
BEGIN
	SELECT * 
	FROM Patients
	WHERE [CNP] = @CNP
END

GO

CREATE PROCEDURE Patients_ReadAll
AS
BEGIN
	SELECT *
	FROM Patients
END

GO

--Donors

CREATE TABLE [Donors] (
	[CNP] NVARCHAR(15),
	[FirstName] NVARCHAR(50),
	[LastName] NVARCHAR(50),
	[DateOfBirth] date,
	[ResidenceID] UNIQUEIDENTIFIER,
	[AddressID] UNIQUEIDENTIFIER,
	[BloodTypeID] UNIQUEIDENTIFIER,
	CONSTRAINT [PK_Donors] PRIMARY KEY ([CNP]),
	CONSTRAINT [FK_Residence] FOREIGN KEY ([ResidenceID]) REFERENCES [Addresses]([AddressID]),
	CONSTRAINT [FK_Address] FOREIGN KEY ([AddressID]) REFERENCES [Addresses]([AddressID]),
	CONSTRAINT [FK_BloodType] FOREIGN KEY ([BloodTypeID]) REFERENCES [BloodTypes]([BloodTypeID])
);

GO

CREATE PROCEDURE [Donors_Insert]
	@CNP NVARCHAR(15),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@DateOfBirth date,
	@ResidenceID UNIQUEIDENTIFIER,
	@AddressID UNIQUEIDENTIFIER,
	@BloodTypeID UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO
	[Donors]([CNP],[FirstName],[LastName],[DateOfBirth],[ResidenceID],[AddressID],[BloodTypeID])
	VALUES (@CNP, @FirstName, @LastName, @DateOfBirth, @ResidenceID, @AddressID, @BloodTypeID)
END
GO

CREATE PROCEDURE [Donors_Update]
	@CNP NVARCHAR(15),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@DateOfBirth date,
	@ResidenceID UNIQUEIDENTIFIER,
	@AddressID UNIQUEIDENTIFIER,
	@BloodTypeID UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE [Donors]
	SET [FirstName]=@FirstName, [LastName]=@LastName, [DateOfBirth]=@DateOfBirth, [ResidenceID]=@ResidenceID,
	[AddressID]=@AddressID, [BloodTypeID]=@BloodTypeID
	WHERE [CNP] = @CNP
END
GO


CREATE PROCEDURE [Donors_Remove]
	@CNP NVARCHAR(15)
AS
BEGIN
	DELETE FROM [Donors]
	WHERE [CNP] = @CNP
END
GO

CREATE PROCEDURE [Donors_ReadAll]
AS
BEGIN
	SELECT * FROM [Donors]
END
GO

CREATE PROCEDURE [Donors_ReadByID]
	@CNP NVARCHAR(15)
AS
BEGIN
	SELECT *
	FROM [Donors]
	WHERE [CNP] = @CNP
END
GO




 




--BloodDonationHistories

CREATE TABLE BloodDonationHistories(
  
  [DonorCNP] nvarchar(15),
  [DonatedForCNP] nvarchar(15),
  [DonationID] uniqueidentifier,
  [Date] date,
  [Quantity] int,
  CONSTRAINT PK_BloodDonationHistories PRIMARY KEY ([DonationID]),
  CONSTRAINT FK_BloodDonationHistoriesDonors FOREIGN KEY ([DonorCNP]) REFERENCES Donors([CNP]),
  CONSTRAINT FK_BloodDonationHistoriesPatients FOREIGN KEY ([DonatedForCNP]) REFERENCES Patients([CNP]),

)

GO

CREATE PROCEDURE BloodDonationHistories_InsertEmpty
(
  @DonorCNP nvarchar(15),
  @DonationID uniqueidentifier,
  @Quantity int
)
AS
BEGIN
	INSERT INTO BloodDonationHistories([DonorCNP], [DonationID],[Quantity])
    VALUES(@DonorCNP, @DonationID,@Quantity)
END

GO
CREATE PROCEDURE BloodDonationHistories_InsertEmptySpecific
(
  @DonorCNP nvarchar(15),
  @DonatedForCNP nvarchar(15),
  @DonationID uniqueidentifier,
  @Quantity int
)
AS
BEGIN
	INSERT INTO BloodDonationHistories([DonorCNP], [DonatedForCNP], [DonationID],[Quantity])
    VALUES(@DonorCNP, @DonatedForCNP, @DonationID,@Quantity)
END

GO
CREATE PROCEDURE BloodDonationHistories_Insert
(
  @DonorCNP nvarchar(15),
  @DonatedForCNP nvarchar(15),
  @DonationID uniqueidentifier,
  @Date date,
  @Quantity int
)
AS
BEGIN
	INSERT INTO BloodDonationHistories([DonorCNP], [DonatedForCNP], [DonationID], [Date], [Quantity])
    VALUES(@DonorCNP, @DonatedForCNP, @DonationID, @Date, @Quantity)
END

GO


CREATE PROCEDURE BloodDonationHistories_Remove
(
	@DonationID uniqueidentifier
)
AS
BEGIN
	DELETE FROM BloodDonationHistories
	WHERE [DonationID] = @DonationID
END

GO

CREATE PROCEDURE BloodDonationHistories_Update
(
  @DonorCNP nvarchar(15),
  @DonatedForCNP nvarchar(15),
  @DonationID uniqueidentifier,
  @Date date,
  @Quantity int
)
AS
BEGIN
	IF (@DonatedForCNP = '')
		SET @DonatedForCNP = NULL;
   UPDATE BloodDonationHistories
   SET [DonorCNP] = @DonorCNP, [DonatedForCNP] = @DonatedForCNP, [Date] = @Date, [Quantity] = @Quantity
   WHERE [DonationID] = @DonationID
END

GO

CREATE PROCEDURE BloodDonationHistories_ReadByID
(
  @DonationID uniqueidentifier
)
AS
BEGIN
    SELECT *
	FROM BloodDonationHistories
	WHERE [DonationID] = @DonationID
END

GO

CREATE PROCEDURE BloodDonationHistories_ReadAll
AS
BEGIN
    SELECT * 
	FROM BloodDonationHistories
END

--Notifications

GO

CREATE TABLE [Notifications]
(
	[NotificationID] uniqueidentifier,
	[DonorCNP] nvarchar(15),
	[Description] text,
	[Status] nvarchar(50)

	CONSTRAINT [PK_Notifications] PRIMARY KEY ([NotificationID])
	CONSTRAINT [FK_Notifications] FOREIGN KEY ([DonorCNP])
		REFERENCES [Donors]([CNP])
)
GO

CREATE PROCEDURE [Notifications_Insert]
(
	@NotificationID uniqueidentifier,
	@DonorCNP nvarchar(15),
	@Description text,
	@Status varchar(50)
)
AS
BEGIN
    INSERT INTO [Notifications]([NotificationID],[DonorCNP],[Description],[Status]) 
	VALUES (@NotificationID,@DonorCNP,@Description,@Status)
END
GO

CREATE PROCEDURE [Notifications_Update]
(
	@NotificationID uniqueidentifier,
	@DonorCNP nvarchar(15),
	@Description text,
	@Status varchar(50)
)
AS
BEGIN
	UPDATE [Notifications]
	SET [DonorCNP] = @DonorCNP,
	    [Description] = @Description,
		[Status] = @Status
	WHERE [NotificationID] = @NotificationID
END
GO

CREATE PROCEDURE [Notifications_Remove]
(
	@NotificationID uniqueidentifier
)
AS
BEGIN
  DELETE 
  FROM [Notifications]
  WHERE [NotificationID] = @NotificationID
END
GO

CREATE PROCEDURE [Notifications_ReadAll]
AS
BEGIN
  SELECT *
  FROM [Notifications]
END
GO

CREATE PROCEDURE [Notifications_ReadByID]
(
	@NotificationID uniqueidentifier
)
AS
BEGIN
  SELECT *
  FROM [Notifications]
  WHERE [NotificationID] = @NotificationID
END
GO


--Diseases

CREATE TABLE Diseases
(
  [DiseaseID] uniqueidentifier,
  [Name] nvarchar(100),
  CONSTRAINT PK_Diseases PRIMARY KEY ([DiseaseID])

)

GO

CREATE PROCEDURE Diseases_Insert
(
  @DiseaseID uniqueidentifier,
  @Name nvarchar(100)
)
AS
BEGIN
	INSERT INTO Diseases([DiseaseID], [Name])
    VALUES(@DiseaseID, @Name)
END

GO

CREATE PROCEDURE Diseases_Remove
(
	@DiseaseID uniqueidentifier
)
AS
BEGIN
	DELETE FROM Diseases
	WHERE [DiseaseID] = @DiseaseID
END

GO

CREATE PROCEDURE Diseases_Update
(
  @DiseaseID uniqueidentifier,
  @Name nvarchar(100)
)
AS
BEGIN
   UPDATE Diseases
   SET [Name] = @Name
   WHERE [DiseaseID] = @DiseaseID
END

GO

CREATE PROCEDURE Diseases_ReadByID
(
  @DiseaseID uniqueidentifier
)
AS
BEGIN
    SELECT *
	FROM Diseases
	WHERE [DiseaseID] = @DiseaseID
END

GO

CREATE PROCEDURE Diseases_ReadAll
AS
BEGIN
    SELECT * 
	FROM Diseases
END

GO

--DiseasesResults

CREATE TABLE DiseasesResults(

  [DiseaseID] uniqueidentifier,
  [DonationID] uniqueidentifier,
  [JustForModels] int NULL,
  CONSTRAINT PK_DiseasesResults PRIMARY KEY ([DiseaseID], [DonationID]),
  CONSTRAINT FK_DiseasesResultsDiaseases FOREIGN KEY ([DiseaseID])  REFERENCES Diseases([DiseaseID]),
  CONSTRAINT FK_DiseasesResultsBloodDonationHistories FOREIGN KEY ([DonationID])  REFERENCES BloodDonationHistories([DonationID])
  
)

GO

CREATE PROCEDURE DiseasesResults_Insert
(
  @DiseaseID uniqueidentifier,
  @DonationID uniqueidentifier
)
AS
BEGIN
	INSERT INTO DiseasesResults([DiseaseID], [DonationID])
    VALUES(@DiseaseID, @DonationID)
END

GO

CREATE PROCEDURE DiseasesResults_Remove
(
   @DiseaseID uniqueidentifier,
   @DonationID uniqueidentifier
)
AS
BEGIN
	DELETE FROM DiseasesResults
	WHERE [DiseaseID] = @DiseaseID and [DonationID] = @DonationID
END

GO

CREATE PROCEDURE DiseasesResults_ReadByID
(
  @DiseaseID uniqueidentifier,
  @DonationID uniqueidentifier
)
AS
BEGIN
    SELECT *
	FROM DiseasesResults
	WHERE [DiseaseID] = @DiseaseID and [DonationID] = @DonationID
END

GO

CREATE PROCEDURE DiseasesResults_ReadAll
AS
BEGIN
    SELECT * 
	FROM DiseasesResults
END

GO

create PROC [Requests_Insert]
	@PatientCNP nvarchar(15),
	@DoctorCNP nvarchar(15),
	@BloodBankID uniqueidentifier,
	@Priority int,
	@RedBloodCellsQuantity int,
	@PlasmaQuantity int,
	@TrombocitesQuantity int
AS
	BEGIN
	INSERT INTO [Requests](PatientCNP, DoctorCNP, BloodBankID, Priority, RedBloodCellsQuantity, PlasmaQuantity, TrombocitesQuantity)
		VALUES (@PatientCNP,@DoctorCNP,@BloodBankID,@Priority,@RedBloodCellsQuantity,@PlasmaQuantity,@TrombocitesQuantity)

	END

go

create PROC [Doctors_Insert]
	@DoctorCNP nvarchar(15),
	@HospitalID uniqueidentifier,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
	BEGIN	
	INSERT INTO [Doctors](DoctorCNP, HospitalID, FirstName, LastName)
		VALUES(@DoctorCNP,@HospitalID,@FirstName,@LastName)


	END



go

create PROC [Requests_Remove]
	@PatientCNP nvarchar(15)
AS
	BEGIN
	DELETE FROM [Requests] WHERE @PatientCNP = [PatientCNP]

	END


go

create PROC [Doctors_Remove]
	@DoctorCNP nvarchar(15)
AS
	BEGIN
	DELETE 
	FROM [Doctors] WHERE @DoctorCNP=[DoctorCNP]
	END

go

create PROC [Requests_Update]
	@PatientCNP nvarchar(15),
	@DoctorCNP nvarchar(15),
	@BloodBankID uniqueidentifier,
	@Priority int,
	@RedBloodCellsQuantity int,
	@PlasmaQuantity int,
	@TrombocitesQuantity int
AS
	BEGIN
	UPDATE [Requests]
		SET [DoctorCNP] = @DoctorCNP, BloodBankID=@BloodBankID,[Priority]=@Priority,[RedBloodCellsQuantity]=@RedBloodCellsQuantity,PlasmaQuantity=@PlasmaQuantity,TrombocitesQuantity=@TrombocitesQuantity
		WHERE [PatientCNP]=@PatientCNP
	END


go

create PROC [Doctors_Update]
	@DoctorCNP nvarchar(15),
	@HospitalID uniqueidentifier,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
	BEGIN
	UPDATE [Doctors]
		SET [HospitalID]=@HospitalID,[FirstName]=@FirstName,[LastName]=@LastName
		WHERE [DoctorCNP]=@DoctorCNP

	END


go


CREATE PROC [Doctors_ReadAll]
AS
	BEGIN
	SELECT * FROM [Doctors]
	END


go

CREATE PROC [Requests_ReadAll]
AS
	BEGIN
	SELECT * FROM [Requests]
	END


go

create PROC [Doctors_ReadByID]
	@DoctorCNP nvarchar(15)
AS
	BEGIN
	SELECT * FROM [Doctors] WHERE [DoctorCNP]=@DoctorCNP
	END


go

create PROC [Requests_ReadByID]
	@PatientCNP nvarchar(15)
AS
	BEGIN
	SELECT * FROM [Requests]
	WHERE [PatientCNP]=@PatientCNP

	END

GO 

CREATE TABLE [Users](
	[Username] varchar(20),
	[Password] varchar(20),
	[Email] varchar(50),
	[Role] varchar(10),
	[CNP] varchar(20),
 CONSTRAINT [pk_users] PRIMARY KEY([Username])
);

GO

create PROCEDURE [Users_Insert]
@Username varchar(20),
@Password varchar(20),
@Email varchar(50),
@Role varchar(10),
@CNP varchar(20)
AS
BEGIN
INSERT INTO Users(
[Username] ,
[Password] ,
[Email] ,
[Role] ,
[CNP] )
VALUES (
@Username,
@Password,
@Email,
@Role,
@CNP)
END

GO

create PROCEDURE [Users_ReadAll]
AS
BEGIN
	SELECT * FROM [Users]
END

GO

create PROCEDURE [Users_ReadByID]
	@Username varchar(20)
AS
BEGIN
	SELECT *
	FROM [Users]
	WHERE [Username] LIKE @Username
END

GO

create PROCEDURE [Users_Remove]
	@Username varchar(20)
AS
BEGIN
	DELETE FROM [Users]
	WHERE [Username] LIKE @Username
END

GO

create PROCEDURE [Users_Update]
	@Username varchar(20),
	@Password varchar(20),
	@Email varchar(50),
	@Role varchar(10),
	@CNP varchar(20)
AS
BEGIN
	UPDATE [Users]
	SET [Password] = @Password, [Email]=@Email, [Role]=@Role, [CNP]=@CNP
	WHERE [Username] LIKE @Username
END

GO

create procedure GetBloodHistoryForDonor
	@CNP NVARCHAR(15)
as
begin
	select *
	from BloodDonationHistories
	where DonorCNP like @CNP
end

GO

create procedure GetCountryByName
	@Name NVARCHAR(50)
as
begin
	select *
	from Countries
	where Name LIKE @Name
end

GO

create procedure GetCityByName
	@Name NVARCHAR(50),
	@CountryID UNIQUEIDENTIFIER
as
begin
	select *
	from Cities
	where Name LIKE @Name and CountryID LIKE @CountryID
end

exec GetCityByName 'Cluj', '0AB411AE-1B46-46EB-B45F-D7EE537FF1DC'

GO

create procedure GetCitiesByCountry
	@CountryID varchar(50)
as begin
	select *
	from Cities
	where CountryID = @CountryID
end
GO

GO

CREATE PROCEDURE GetHasDiseases
(
	@DonorCNP nvarchar(15)
)
AS
BEGIN
	SELECT TOP 1 bdh.[DonorCNP],bdh.[DonatedForCNP],bdh.[DonationID],bdh.[Date],bdh.[Quantity]
	FROM [BloodDonationHistories] bdh
			INNER JOIN [DiseasesResults] dr ON bdh.[DonationID] = dr.[DonationID]
	WHERE bdh.[DonorCNP] = @DonorCNP
END

GO

CREATE PROCEDURE GetLatestDonation
(
	@DonorCNP nvarchar(15)
)
AS
BEGIN
	SELECT TOP 1 [DonorCNP],[DonatedForCNP],[DonationID],[Date],[Quantity]
	FROM [BloodDonationHistories]
	WHERE [DonorCNP] = @DonorCNP
END

GO
create procedure GetAllRequestsByDoctorCNP
	@DoctorCNP nvarchar(15)
as begin
	SELECT * FROM Requests 
	WHERE DoctorCNP = @DoctorCNP
END
GO

create procedure RefreshBloodStock
as begin
	delete from Trombocites where ExpirationDate <= GETDATE()
	delete from RedBloodCells where ExpirationDate <= GETDATE()
	delete from Plasma where ExpirationDate <= GETDATE()
end
go

insert into Plasma values ('710D7F5F-5B07-442B-B3A8-400B0ABF9A5F', 'DA464E8F-F981-44FC-AD08-9F8F39773FC6', '710D7F5F-5B07-442B-B3A8-400B0ABF9A5F', '2018-06-02', 100)
insert into Users(Username, Password, Email, Role, CNP) values('Emy', '123', 'emi@gmail.com', 'Admin', '1234567891')
insert into Users(Username, Password, Email, Role, CNP) values('George', '123', 'george@gmail.com', 'Doctor', '4321987654')
insert into Users(Username, Password, Email, Role, CNP) values('Naomi', '123', 'naomi@gmail.com', 'Donor', '1234567894')
insert into Users(Username, Password, Email, Role, CNP) values('Iistr', 'pass', 'iistrate@yahoo.com', 'Doctor', '2721128049222')

insert into BloodTypes(BloodTypeID, Blood, RH) values('710D7F5F-5B07-442B-B3A8-400B0ABF9A5F', 'A', 1)
insert into BloodTypes(BloodTypeID, Blood, RH) values('DBE83047-8457-40C3-9F13-CEE9F497B62C', 'AB', 0)
insert into BloodTypes(BloodTypeID, Blood, RH) values('40D85F24-2C84-42DC-A21A-F15D2261459D', 'B', 1)
insert into BloodTypes(BloodTypeID, Blood, RH) values('00000000-0000-0000-0000-000000000000', 'DEF', 0)

insert into Countries(CountryID, [Name]) values('0AB411AE-1B46-46EB-B45F-D7EE537FF1DC', 'Romania');
insert into Countries(CountryID, [Name]) values('8E40053D-2BCD-43FE-B729-70A5C09D63A0', 'Hungary');

insert into Cities(CityID, CountryID, [Name]) values('36EB2023-B2CB-4ACD-A63B-E6851858C6EF', '0AB411AE-1B46-46EB-B45F-D7EE537FF1DC', 'Cluj')
insert into Cities(CityID, CountryID, [Name]) values('93ABDB7A-A766-4D31-87A5-3E2520DDBA71', '0AB411AE-1B46-46EB-B45F-D7EE537FF1DC', 'Brasov')
insert into Cities(CityID, CountryID, [Name]) values('1B00D10C-3AC2-4B74-8F82-198B00868BFB', '0AB411AE-1B46-46EB-B45F-D7EE537FF1DC', 'Suceava')
insert into Cities(CityID, CountryID, [Name]) values('2F7FAB91-FD77-4784-A789-A07F152D3D1D', '0AB411AE-1B46-46EB-B45F-D7EE537FF1DC', 'Satu Mare')

insert into Addresses(AddressID, [Address], CityID) values('F053F2FF-C77B-4593-AEF6-D904F50BD3B7', 'Str Principala', '36EB2023-B2CB-4ACD-A63B-E6851858C6EF') 
insert into Addresses(AddressID, [Address], CityID) values('BA6EECD3-7723-4642-A38D-FA3E49C7A4D7', 'Str Avram Iancu', '36EB2023-B2CB-4ACD-A63B-E6851858C6EF') 
insert into Addresses(AddressID, [Address], CityID) values('2CB169FD-DF01-4BF4-9A57-8F07AF9472FC', 'Str Horea', '36EB2023-B2CB-4ACD-A63B-E6851858C6EF') 
insert into Addresses(AddressID, [Address], CityID) values('4AA2746F-35C5-4F5D-BEC5-4CA5F9D88358', 'Str Observatorului', '36EB2023-B2CB-4ACD-A63B-E6851858C6EF') 

insert into Donors(CNP, FirstName, LastName, DateOfBirth, ResidenceID, AddressID, BloodTypeID)
values('1234567894', 'Naomi', 'Moisuc', '1998-10-10', 'F053F2FF-C77B-4593-AEF6-D904F50BD3B7', 'F053F2FF-C77B-4593-AEF6-D904F50BD3B7', '710D7F5F-5B07-442B-B3A8-400B0ABF9A5F')

insert into BloodDonationHistories(DonorCNP, DonatedForCNP, DonationID, Date, Quantity) values
('1234567894', null, 'A64D3379-3800-46FE-9D7A-D72EE95A9E01', '2018-05-20', 450)

insert into BloodBanks(BankID, Name, AddressID) values ('DA464E8F-F981-44FC-AD08-9F8F39773FC6', 'Blood Donation Center', '2CB169FD-DF01-4BF4-9A57-8F07AF9472FC')

insert into Hospitals(HospitalID, Name, AddressID) values ('AFF84613-F87B-4D7A-8CC3-497856AF79FA', 'Spitalul de Recuperare', '4AA2746F-35C5-4F5D-BEC5-4CA5F9D88358')
insert into Patients(CNP, FirstName, LastName, HospitalID, BloodTypeID) values ('1921126040011', 'Jane', 'Doe', 'AFF84613-F87B-4D7A-8CC3-497856AF79FA','40D85F24-2C84-42DC-A21A-F15D2261459D')

insert into Doctors(DoctorCNP, HospitalID, FirstName, LastName) values ('2721128049222', 'AFF84613-F87B-4D7A-8CC3-497856AF79FA', 'Ioana' , 'Istrate')
insert into Doctors(DoctorCNP, HospitalID, FirstName, LastName) values ('4321987654', 'AFF84613-F87B-4D7A-8CC3-497856AF79FA', 'George' , 'Popescu')

INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('4858F04D-2522-45D7-B854-B22933FB7624','Hepatitis')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('4AB7F706-EE5D-49BC-8D2E-2479C16AA811','TB')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('F9736DBE-DD09-4172-8610-C24D48392E65','Pox')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('AEAFD9AF-18AC-4D7B-8F87-A61AA9C560B9','Malaria')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('4836D208-E6B4-4EB1-BA6F-8D37E66DB9E6','Epilepsy or other neurological diseases')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('B94992F7-20BD-45C7-A6DE-FA8BB5044354','Mental illness')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('7BEB12F5-3FAB-4379-BED8-7CE8B9C915CD','Brucellosis')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('6A0E33D2-D167-4CDA-9E3F-5584AFE3FEE0','Ulcer')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('CF40F2D5-D7B0-4962-A6EA-1953198F299C','Diabetes')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('7B796695-CE6A-4836-B6EA-6A5056A49A4A','Heart diseases')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('14BCACB5-A1CA-4F53-8EBB-8548A134EB3C','Skin diseases: psoriasis, vitiligo')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('6F760600-7026-48FD-AE17-37E359078B4C','Myopia over (-) 6 diopters')
INSERT INTO Diseases(DiseaseID, [Name]) VALUES ('B6472C66-32A8-48B4-BB0C-D5D859B2D224','Cancer')

insert into Doctors(DoctorCNP, HospitalID, FirstName, LastName) values ('1754253025586', 'AFF84613-F87B-4D7A-8CC3-497856AF79FA', 'Mihai' , 'Popescu')
insert into Users(Username, Password, Email, Role, CNP) values('Mihai', 'pass', 'mihai@gmail.com', 'Doctor', '1754253025586')
insert into Patients(CNP, FirstName, LastName, HospitalID, BloodTypeID) values ('0965236955212', 'Floricica', 'Neagoe', 'AFF84613-F87B-4D7A-8CC3-497856AF79FA','DBE83047-8457-40C3-9F13-CEE9F497B62C')
insert into Requests VALUES ('1921126040011','1754253025586','DA464E8F-F981-44FC-AD08-9F8F39773FC6',1,20,20,20)
insert into Requests VALUES ('0965236955212','1754253025586','DA464E8F-F981-44FC-AD08-9F8F39773FC6',2,30,40,20)
---
