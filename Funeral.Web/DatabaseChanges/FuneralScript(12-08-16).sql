
Go
ALTER TABLE ApplicationSettings Add  [IsAutoGeneratedPolicyNo] bit NULL
Go
ALTER TABLE Dependencies Add  [StartDate] datetime NULL
GO
ALTER TABLE tblApplicationTermsAndCondition Add  [TermsAndConditionFuneral] nvarchar(MAX) NULL
Go
ALTER TABLE tblApplicationTermsAndCondition Add  [TermsAndConditionTombstone] nvarchar(MAX) NULL
GO
ALTER TABLE ApplicationSettings Add  [VatNo] nvarchar(50) NULL
Go
ALTER TABLE Quotations ALTER COLUMN [Tax] money NULL
Go
ALTER TABLE Quotations ALTER COLUMN [Discount] decimal(8, 2) NULL
Go
ALTER TABLE Funerals ALTER COLUMN [Tax] money NULL
Go
ALTER TABLE Funerals ALTER COLUMN [Discount] decimal(8, 2) NULL
Go
ALTER TABLE tblTombStones ALTER COLUMN [Tax] money NULL
Go
ALTER TABLE tblTombStones ALTER COLUMN [DisCount] decimal(8, 2) NULL
Go


GO
/****** Object:  StoredProcedure [dbo].[GetDependencByIDNum]    Script Date: 12-08-2016 20:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 10-March-2015
-- Description:	Select Members By Id
-- =============================================
--  [GetDependencByIDNum] '5306050381082','6dcba090-f363-47e6-93f5-6def8f80703e',201
ALTER PROCEDURE [dbo].[GetDependencByIDNum]
	-- Add the parameters for the stored procedure here
@ID nvarchar(150),
@ParlourId uniqueidentifier,
@fkiMemberID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
SELECT [pkiDependentID]
      ,[fkiMemberID]
      ,[DependencyType]
      ,[Full Name]
      ,[Surname]
      ,[Gender]
      ,[IDNumber]
      ,[Age]
      ,[Telephone]
      ,[Cellphone]
      ,[Address1]
      ,[Address2]
      ,[Address3]
      ,[Address4]
      ,[Code]
      ,[Claimnumber]
      ,[Premium]
      ,[Plan]
      ,[Date Of Birth]
      ,[parlourid]
      ,[InceptionDate]
      ,[CoverDate]
      ,[Cover]
      ,[LastModified]
      ,[ModifiedUser]
      ,[UnderwriterSplit]
      ,[Relationship]
	  ,[StartDate]
  FROM [dbo].[Dependencies] 
  WHERE [IDNumber]=@ID
		 AND [parlourid]=@ParlourId
		 AND [fkiMemberID]=@fkiMemberID
END
Go

GO
/****** Object:  StoredProcedure [dbo].[SaveFamilyDependency]    Script Date: 12-08-2016 20:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveFamilyDependency] 
	-- Add the parameters for the stored procedure here
	@parlourId uniqueidentifier,
	@FullName nvarchar(255),
	@IDNumber nvarchar(255),
	@Surname nvarchar(255),
	@DependencyType nvarchar(255),
	@fkiMemberID int,
	@Age int,
	@CoverDate datetime,
	@Premium decimal(12,2),
	@DateOfBirth datetime,
	@InceptionDate datetime,
	@Relationship int,
	@Gender nvarchar(50),
	@StartDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO [dbo].[Dependencies]
           ([fkiMemberID]
           ,[DependencyType]
           ,[Full Name]
           ,[Surname]
           ,[Gender]
           ,[IDNumber]
           ,[Age]
           
           ,[parlourid]
		   ,[Relationship]
		   ,CoverDate
		   ,InceptionDate
		   ,[Date Of Birth]
		   ,Premium 
		   ,[StartDate]
           )
     VALUES
           (@fkiMemberID
           ,@DependencyType
           ,@FullName
           ,@Surname
           ,@Gender
           ,@IDNumber
           ,@Age

		  ,@parlourid
		  ,@Relationship
		  ,@CoverDate
		  ,@InceptionDate
		  ,@DateOfBirth
		  ,@Premium
		  ,@StartDate
           )
END
GO
GO
/****** Object:  StoredProcedure [dbo].[UpdateFamilyDependency]    Script Date: 12-08-2016 20:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateFamilyDependency] 
	-- Add the parameters for the stored procedure here
	@parlourId uniqueidentifier,
	@FullName nvarchar(255),
	@IDNumber nvarchar(255),
	@Surname nvarchar(255),
	@DependencyType nvarchar(255),
	@fkiMemberID int,
	@Age int,
		@pkiDependentID int,
		@CoverDate datetime,
	@Premium int,
	@DateOfBirth datetime,
	@InceptionDate datetime,
	@Relationship int,
	@Gender nvarchar(50),
	@StartDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE [dbo].[Dependencies]
		   SET [fkiMemberID]=@fkiMemberID
           ,[DependencyType]=@DependencyType
           ,[Full Name]=@FullName
           ,[Surname]=@Surname
           ,[IDNumber]=@IDNumber
           ,[Age]=@Age
           ,[parlourid]=@parlourId
,[CoverDate]=@CoverDate
,[Premium]=@Premium
,[Date Of Birth] =@DateOfBirth
,[InceptionDate]=@InceptionDate
,[Relationship]=@Relationship
,[Gender]=@Gender
,[StartDate]=@StartDate

           	WHERE Dependencies.pkiDependentID=@pkiDependentID
     
END
GO

GO
/****** Object:  StoredProcedure [dbo].[SelectFamilyDependencyById]    Script Date: 12-08-2016 20:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SelectFamilyDependencyById] 
	-- Add the parameters for the stored procedure here
	@pkiDependentID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SELECT D.fkiMemberID as MemberId,
	 D.pkiDependentID,
	 D.DependencyType,
	 D.[Full Name] AS FullName,
	D. Surname,
	D. IDNumber,
	 D.parlourid,
	 D. Age ,
	 D. [Date Of Birth] as DateOfBirth,
	 D. CoverDate,
	 D. InceptionDate,
	 D. Premium,
	 D. Relationship,
	 D.Gender,
	 D.StartDate,
	  r.Type As RelationshipType
	  	   FROM Dependencies D	   
LEFT JOIN Relationship r ON r.ID=d.Relationship 
--LEFT JOIN DependencyType DT ON dt.DepStatus_ID= D.DependencyTypeId
WHERE d.pkiDependentID=@pkiDependentID
END
Go

GO
/****** Object:  StoredProcedure [dbo].[SaveTermsAndCondition]    Script Date: 12-08-2016 20:10:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 03-aug-2016
-- Description:	Insert & Update [SaveTermsAndCondition] 
-- =============================================
ALTER procedure [dbo].[SaveTermsAndCondition](
	
	@pkiAppTC int,
	@fkiApplicationID int,
	@TermsAndCondition nvarchar(max),
	@LastModified datetime,
	@ModifiedUser nvarchar(50),
	@parlourid uniqueidentifier,
	@TermsAndConditionFuneral nvarchar(max),
	@TermsAndConditionTombstone nvarchar(max)
)

as 
begin
SET NOCOUNT ON
DECLARE @lparlourID uniqueidentifier
		set @lparlourID = (SELECT parlourid from [dbo].[tblApplicationTermsAndCondition] where parlourid=@parlourid)

		
	IF @lparlourID is NULL

begin
INSERT INTO [tblApplicationTermsAndCondition] (

fkiApplicationID,
TermsAndCondition,
CreatedDate,
LastModified,
ModifiedUser,
parlourid,
TermsAndConditionFuneral,
TermsAndConditionTombstone
)
VALUES (
	
	@fkiApplicationID ,
	@TermsAndCondition ,
	getdate(),
	@LastModified ,
	@ModifiedUser ,
	@parlourid,
	@TermsAndConditionFuneral,
	@TermsAndConditionTombstone
)
SELECT SCOPE_IDENTITY() AS pkiAppTC
END
ELSE
	BEGIN
		UPDATE [dbo].[tblApplicationTermsAndCondition]
	SET 
	fkiApplicationID=@fkiApplicationID,
	TermsAndCondition=@TermsAndCondition,
	LastModified=@LastModified,
	ModifiedUser=@LastModified,
	parlourid = @parlourid,
	TermsAndConditionFuneral=@TermsAndConditionFuneral,
	TermsAndConditionTombstone=@TermsAndConditionTombstone
	 WHERE [parlourid]=@parlourid
	SELECT * from [tblApplicationTermsAndCondition] where [parlourid]=@parlourid
	END
END
GO

GO
/****** Object:  StoredProcedure [dbo].[SaveApplicationSetting]    Script Date: 12-08-2016 20:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveApplicationSetting]
	@ApplicationName nvarchar(255),
	@ApplicationLogoPath nvarchar(255),
	@OwnerFirstName nvarchar(255),
	@OwnerSurname nvarchar(255),
	@OwnerTelNumber nvarchar(255),
	@OwnerCellNumber nvarchar(255),
	@ManagerFirstName nvarchar(255),
	@ManageSurname nvarchar(255),
	@ManageTelNumber nvarchar(255),
	@ManageCellNumber nvarchar(255),
	@BusinessAddressLine1 nvarchar(255),
	@BusinessAddressLine2 nvarchar(255),
	@BusinessAddressLine3 nvarchar(255),
	@BusinessAddressLine4 nvarchar(255),
	@BusinessPostalCode nvarchar(255),
	@FSBNumber nvarchar(255),
	@CereliaAPIKey nvarchar(255),
	@RegistrationNumber nvarchar(255),
	@ManageSlogan nvarchar(255),
	@ManageEmail nvarchar(255),
	@ManageFaxNumber nvarchar(255),
	@OwnerEmail nvarchar(255),
	@ApplicationRules nvarchar(255),
	@VatNo nvarchar(50),
	@IsAutoGeneratedPolicyNo bit,
	--Common Data 
	
	@pkiApplicationID int

	--Extra Data

		   ,@parlourid uniqueidentifier
		  

		   

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	DECLARE @lparlourID INT
    -- Insert statements for procedure here  "00000000-0000-0000-0000-000000000000"
	IF @parlourid='00000000-0000-0000-0000-000000000000'
		 set @parlourid = (SELECT [parlourid] as parlourid from [ApplicationSettings] where [pkiApplicationID] = @pkiApplicationID)

	IF @pkiApplicationID=0 
	BEGIN
		INSERT INTO [dbo].[ApplicationSettings]
           ([ApplicationName]
           ,[ApplicationLogoPath]
           ,[OwnerFirstName]
           ,[OwnerSurname]
           ,[OwnerTelNumber]
			,[OwnerCellNumber]
           ,[ManagerFirstName]
           ,[ManageSurname]
          ,[ManageTelNumber]
           ,[ManageCellNumber]
           ,[BusinessAddressLine1]
           ,[BusinessAddressLine2]
           ,[BusinessAddressLine3]
           ,[BusinessAddressLine4]
           ,[BusinessPostalCode]
           ,[FSBNumber]
           ,[CereliaAPIKey]
           ,[RegistrationNumber]
           ,[ManageSlogan]
           ,[ManageEmail]
           ,[ManageFaxNumber]
           ,[OwnerEmail]
           ,[parlourid]
		   ,[ApplicationRules]
		   ,[VatNo]
		   ,[IsAutoGeneratedPolicyNo])
     VALUES
			(@ApplicationName
           ,@ApplicationLogoPath
           ,@OwnerFirstName
           ,@OwnerSurname
           ,@OwnerTelNumber
           ,@OwnerCellNumber
           ,@ManagerFirstName
			,@ManageSurname
           ,@ManageTelNumber
           ,@ManageCellNumber
           ,@BusinessAddressLine1
           ,@BusinessAddressLine2
           ,@BusinessAddressLine3
           ,@BusinessAddressLine4
           ,@BusinessPostalCode
           ,@FSBNumber
           ,@CereliaAPIKey
           ,@RegistrationNumber
           ,@ManageSlogan
           ,@ManageEmail
           ,@ManageFaxNumber
           ,@OwnerEmail
           ,@parlourid
		   ,@ApplicationRules
		   ,@VatNo
		   ,@IsAutoGeneratedPolicyNo)
		   SELECT * from [ApplicationSettings] where [pkiApplicationID] = SCOPE_IDENTITY()
		  --SELECT [parlourid] as parlourid from [ApplicationSettings] where [pkiApplicationID] = SCOPE_IDENTITY()
		  END
		  ELSE
	BEGIN
		UPDATE [dbo].[ApplicationSettings]
		SET [ApplicationName] = @ApplicationName
           ,[ApplicationLogoPath] = @ApplicationLogoPath
           ,[OwnerFirstName] = @OwnerFirstName
           ,[OwnerSurname] = @OwnerSurname
           ,[OwnerTelNumber] = @OwnerTelNumber
			,[OwnerCellNumber] = @OwnerCellNumber
           ,[ManagerFirstName] = @ManagerFirstName
           ,[ManageSurname] = @ManageSurname
          ,[ManageTelNumber] = @ManageTelNumber
           ,[ManageCellNumber] = @ManageCellNumber
           ,[BusinessAddressLine1] = @BusinessAddressLine1
           ,[BusinessAddressLine2] = @BusinessAddressLine2
           ,[BusinessAddressLine3] = @BusinessAddressLine3
           ,[BusinessAddressLine4] = @BusinessAddressLine4
           ,[BusinessPostalCode] = @BusinessPostalCode
           ,[FSBNumber] = @FSBNumber
           ,[CereliaAPIKey] = @CereliaAPIKey
           ,[RegistrationNumber] = @RegistrationNumber
           ,[ManageSlogan] = @ManageSlogan
           ,[ManageEmail] = @ManageEmail
           ,[ManageFaxNumber] = @ManageFaxNumber
           ,[OwnerEmail] = @OwnerEmail
           ,[parlourid] = @parlourid
		   ,[ApplicationRules] = @ApplicationRules
		   ,[VatNo]=@VatNo
		   ,[IsAutoGeneratedPolicyNo]=@IsAutoGeneratedPolicyNo
		   WHERE [pkiApplicationID]=@pkiApplicationID
		   SELECT * from [ApplicationSettings] where [pkiApplicationID] = @pkiApplicationID
		   --SELECT	@parlourid As parlourid
		 END
END
GO