Go
ALTER TABLE FuneralDocuments Add [DocType] int NULL
Go
ALTER TABLE FuneralDocuments Add [DocContentType] nvarchar(200) NULL
GO

GO
/****** Object:  StoredProcedure [dbo].[SaveFuneral]    Script Date: 16-08-2016 18:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 01-Aug-2016
-- Description:	Insert & update Funeral
-- =============================================
ALTER procedure [dbo].[SaveFuneral](
	@pkiFuneralID int,
	--@Title
	@FullNames nvarchar(50),
	@Surname nvarchar(50),
	@Gender nvarchar(50),
	@IDNumber nvarchar(50),
	@DateOfBirth datetime=NULL,
	@DateOfDeath datetime=NULL,
	@DateOfFuneral datetime,
	@TimeOfFuneral datetime,
	@FuneralCemetery nvarchar(100),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@Address3 nvarchar(50),
	--@Address4,
	@Code nvarchar(50),
	--@MemeberNumber,
	--@ContactPerson,
	--@ContactPersonNumber,
	@BodyCollectedFrom nvarchar(255),
	@CourseOfDearth nvarchar(255),
	--@Claimnumber,
	@BI1663 nvarchar(255),
	@DriverAndCars nvarchar(255),
	@GraveNo nvarchar(255),
	@parlourid uniqueidentifier,
	@LastModified datetime,
	@ModifiedUser varchar
--	@InvoiceNumber nvarchar(50)
)
as 
begin
SET NOCOUNT ON
Declare @InvoiceNumber_temp  Varchar(100)
IF @pkiFuneralID=0 
begin

select @InvoiceNumber_temp=(ISNULL(Max(InvoiceNumber),'0000')) from Funerals where InvoiceNumber IS not NULL and parlourid=@parlourid

set @InvoiceNumber_temp = @InvoiceNumber_temp + 1
IF @InvoiceNumber_temp<9999
set @InvoiceNumber_temp = RIGHT('000'+ISNULL(@InvoiceNumber_temp,''),4)


INSERT INTO [dbo].[Funerals] (
	--[Title],
	[Full Names],
	[Surname],
	[Gender],
	[ID Number],
	[DateOfBirth],
	[DateOfDeath],
	[DateOfFuneral],
	[TimeOfFuneral],
	[FuneralCemetery],
	[Address1],
	[Address2],
	[Address3],
	--[Address4],
	[Code],
	--[MemeberNumber],
	--[ContactPerson],
	--[ContactPersonNumber],
	[BodyCollectedFrom],
	[CourseOfDearth],
	--[Claimnumber],
	[BI1663],
	[DriverAndCars],
	[GraveNo],
	[parlourid],
	[CreatedDate],
	[InvoiceNumber]
	
)
VALUES (
	
	--@Title
	@FullNames,
	@Surname,
	@Gender,
	@IDNumber,
	CASE WHEN @DateOfBirth IS NULL
    THEN NULL
    ELSE @DateOfBirth END,
	CASE WHEN @DateOfDeath IS NULL
    THEN NULL
    ELSE @DateOfDeath END,	
	--@DateOfDeath,
	@DateOfFuneral,
	@TimeOfFuneral,
	@FuneralCemetery,
	@Address1,
	@Address2,
	@Address3,
	--@Address4,
	@Code,
	--@MemeberNumber,
	--@ContactPerson,
	--@ContactPersonNumber,
	@BodyCollectedFrom,
	@CourseOfDearth,
	--@Claimnumber,
	@BI1663,
	@DriverAndCars,
	@GraveNo,
	@parlourid,
	getdate(),
	@InvoiceNumber_temp
)
SELECT SCOPE_IDENTITY() AS pkiFuneralID
END
ELSE
	BEGIN
		UPDATE [dbo].[Funerals]
	SET 
	--[Title],
	[Full Names]=@FullNames,
	[Surname]=@Surname,
	[Gender]=Gender,
	[ID Number]=@IDNumber,
	[DateOfBirth]=@DateOfBirth,
	[DateOfDeath]=@DateOfDeath,
	[DateOfFuneral]=@DateOfFuneral,
	[TimeOfFuneral]=@TimeOfFuneral,
	[FuneralCemetery]=@FuneralCemetery,
	[Address1]=@Address1,
	[Address2]=@Address2,
	[Address3]=@Address3,
	--[Address4],
	[Code]=@Code,
	--[MemeberNumber],
	--[ContactPerson],
	--[ContactPersonNumber],
	[BodyCollectedFrom]=@BodyCollectedFrom,
	[CourseOfDearth]=@CourseOfDearth,
	--[Claimnumber],
	[BI1663]=@BI1663,
	[DriverAndCars]=@DriverAndCars,
	[GraveNo]=@GraveNo,
	[parlourid]=@parlourid,
	[LastModified]=@LastModified,
	[ModifiedUser]=@ModifiedUser
	 WHERE [pkiFuneralID] = @pkiFuneralID
	SELECT	@pkiFuneralID As pkiFuneralID
	END
END

Go

GO
/****** Object:  StoredProcedure [dbo].[SaveFuneralSupportedDocument]    Script Date: 16-08-2016 19:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 16/08/2015
-- Description:	This procedure use for Store Document Related to Funeral
-- =============================================
Create PROCEDURE [dbo].[SaveFuneralSupportedDocument]
@ImageName nvarchar(255),
@ImageFile Image,
@fkiFuneralID INT,
@CreateDate datetime,
@Parlourid uniqueidentifier,
@LastModified datetime,
@ModifiedUser nvarchar(255),
@DocContentType varchar(200),
@DocType int

AS
BEGIN

	INSERT INTO [dbo].[FuneralDocuments]
           ([ImageName]
           ,[ImageFile]
           ,[fkiFuneralID]
           ,[CreateDate]
           ,[Parlourid]
           ,[LastModified]
           ,[ModifiedUser]
		   ,[DocContentType]		
		   ,[DocType])
     VALUES
           ( @ImageName
           ,@ImageFile
           ,@fkiFuneralID
           ,@CreateDate
           ,@Parlourid
           ,@LastModified
           ,@ModifiedUser
		   ,@DocContentType
		   ,@DocType
		   )

	Select Scope_identity() As ID

END
Go

GO
/****** Object:  StoredProcedure [dbo].[SelectFuneralDocumentsByMemberId]    Script Date: 16-08-2016 19:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 16/08/2016
-- Description:	This procedure use for Select List Of MembarNotes.
-- =============================================
Create PROCEDURE [dbo].[SelectFuneralDocumentsByMemberId]
@fkiFuneralID INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * From FuneralDocuments  WHERE fkiFuneralID=@fkiFuneralID

END
GO


GO
/****** Object:  StoredProcedure [dbo].[SelectFuneralDocumentsByPKId]    Script Date: 16-08-2016 19:10:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 16/08/2016
-- Description:	This procedure use for Select List Of Funerla Docs.
-- =============================================
Create PROCEDURE [dbo].[SelectFuneralDocumentsByPKId]
@pkiFuneralPictureID INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * From FuneralDocuments  WHERE pkiFuneralPictureID=@pkiFuneralPictureID

END
Go

