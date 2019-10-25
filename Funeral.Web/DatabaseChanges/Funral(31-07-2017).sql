GO

/****** Object:  Table [dbo].[Underwriter]    Script Date: 07/31/2017 2.45.49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Underwriter](
	[PkiUnderwriterId] [int] IDENTITY(1,1) NOT NULL,
	[UnderwriterName] [nvarchar](50) NULL,
	[PlanName] [nvarchar](50) NULL,
	[Premium] [nvarchar](50) NULL,
	[CoverAmount] [nvarchar](50) NULL,
	[Spouse] [int] NULL,
	[Children] [int] NULL,
	[Adults] [int] NULL,
	[Cover] [money] NULL,
	[UnderwriterSplit] [money] NULL,
	[ManagerSplit] [money] NULL,
	[SpouseCover] [money] NULL,
	[ChildCover] [money] NULL,
	[AdultCover] [money] NULL,
	[Parlourid] [uniqueidentifier] NULL,
	[PlanUnderwriter] [nvarchar](50) NULL,
	[LastModified] [datetime] NULL,
	[ModifiedUser] [nvarchar](50) NULL,
	[CreateddDate] [datetime] NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[Cover0to5year] [money] NULL,
	[Cover6to13year] [money] NULL,
	[Cover14to21year] [money] NULL,
	[Cover22to40year] [money] NULL,
	[Cover41to59year] [money] NULL,
	[Cover60to65year] [money] NULL,
	[Cover66to75year] [money] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Underwriter] PRIMARY KEY CLUSTERED 
(
	[PkiUnderwriterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Plans] ADD UnderwriterId int null

Go
USE [DevData]
GO

/****** Object:  StoredProcedure [dbo].[SelectUnderwriterBypkid]    Script Date: 07/31/2017 2.48.06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hemant Yadav
-- Create date: 29-july-2017
-- Description:	Select  Funeral by primaryKey
-- =============================================
Create procedure [dbo].[SelectUnderwriterBypkid](
	@PkiUnderwriterId int,	
	@Parlourid uniqueidentifier	
)
as 
begin
SET NOCOUNT ON
	select *  from [dbo].[Underwriter] where PkiUnderwriterId=@PkiUnderwriterId And Parlourid=@Parlourid
END


GO

/****** Object:  StoredProcedure [dbo].[SaveUnderwriter]    Script Date: 07/31/2017 2.48.06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveUnderwriter]
           @PkiUnderwriterId int
		  ,@UnderwriterName nvarchar(50)= NULL
		  ,@PlanName nvarchar(50)= NULL
		  ,@Premium nvarchar(50)= NULL
		  ,@CoverAmount nvarchar(50)= NULL
		  ,@Spouse int= NULL
		  ,@Children int= NULL
		  ,@Adults int= NULL
		  ,@Cover decimal(18,2)= NULL
		  ,@SpouseCover decimal(18,2)= NULL
		  ,@ChildCover decimal(18,2)= NULL
		  ,@AdultCover decimal(18,2)= NULL
		  ,@Parlourid uniqueidentifier= NULL
		  ,@PlanUnderwriter nvarchar(50) = NULL
		  ,@LastModified datetime	= NULL
		  ,@ModifiedUser nvarchar(50)= NULL
		  ,@CreateddDate datetime= NULL
		  ,@CreatedUser nvarchar(50)= NULL
		  ,@Cover0to5year decimal(18,2)= NULL
		  ,@Cover6to13year decimal(18,2)= NULL
		  ,@Cover14to21year decimal(18,2)= NULL
		  ,@Cover22to40year decimal(18,2)= NULL
		  ,@Cover41to59year decimal(18,2)= NULL
		  ,@Cover60to65year decimal(18,2)= NULL
		  ,@Cover66to75year decimal(18,2)= NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	--DECLARE @lpkiMemberID INT
    -- Insert statements for procedure here
	
	IF @PkiUnderwriterId=0 
	BEGIN
		INSERT INTO [dbo].[Underwriter]
           (
				[UnderwriterName],
				[PlanName],
				[Premium],
				[CoverAmount],
				[Spouse],
				[Children],
				[Adults],
				[Cover],
				[SpouseCover],
				[ChildCover],
				[AdultCover],
				[Parlourid],
				[PlanUnderwriter],
				[CreateddDate],
				[CreatedUser],
				[Cover0to5year],
				[Cover6to13year],
				[Cover14to21year],
				[Cover22to40year],
				[Cover41to59year],
				[Cover60to65year],
				[Cover66to75year]
		)
     VALUES
		(
			@UnderwriterName,
			@PlanName,
			@Premium,
			@CoverAmount,
			@Spouse,
			@Children,
			@Adults,
			@Cover,
			@SpouseCover,
			@ChildCover,
			@AdultCover,
			@Parlourid,
			@PlanUnderwriter,
			@CreateddDate,
			@CreatedUser,
			@Cover0to5year,
			@Cover6to13year,
			@Cover14to21year,
			@Cover22to40year,
			@Cover41to59year,
			@Cover60to65year,
			@Cover66to75year
	    )
		  SELECT	SCOPE_IDENTITY() As PkiUnderwriterId
		  END
		  ELSE
	BEGIN
		UPDATE [dbo].[Underwriter] SET
				[UnderwriterName]=@UnderwriterName,
				[PlanName]=@PlanName,
				[Premium]=@Premium,
				[CoverAmount]=@CoverAmount,
				[Spouse]=@Spouse,
				[Children]=@Children,
				[Adults]=@Adults,
				[Cover]=@Cover,
				[SpouseCover]=@SpouseCover,
				[ChildCover]=@ChildCover,
				[AdultCover]=@AdultCover,
				[PlanUnderwriter]=@PlanUnderwriter,
				[LastModified]=@LastModified,
				[ModifiedUser]=@ModifiedUser,
				[Cover0to5year]=@Cover0to5year,
				[Cover6to13year]=@Cover6to13year,
				[Cover14to21year]=@Cover14to21year,
				[Cover22to40year]=@Cover22to40year,
				[Cover41to59year]=@Cover41to59year,
				[Cover60to65year]=@Cover60to65year,
				[Cover66to75year]=@Cover66to75year
		   WHERE PkiUnderwriterId = @PkiUnderwriterId
		   SELECT	@PkiUnderwriterId As PkiUnderwriterId
		 END
END
GO

/****** Object:  StoredProcedure [dbo].[SelectAllUnderwriterByParlourId]    Script Date: 07/31/2017 2.48.06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectAllUnderwriterByParlourId] 
	-- Add the parameters for the stored procedure here
--	@ParlourId UniqueIdentifier
	@ParlourId uniqueIdentifier='00000000-0000-0000-0000-000000000000',
	@pagesize int=10,
	@pagenum int=1,
	@keyword nvarchar(max)='',
	@field nvarchar(max)='pkiFuneralID',
	@orderby varchar(255)='ASC'
	-- Add the parameters for the stored procedure here
AS
BEGIN
SET NOCOUNT ON;
--Select * from Quotations where parlourid = @ParlourId order by DateOfQuotation desc

DECLARE @Init int ,@LastRow int, @query nvarchar(4000)
SET NOCOUNT ON;


IF @pagenum <=0  
BEGIN
 SET @pagenum=1
END 

SET @Init=(@pagesize*(@pagenum-1))+1
SET @LastRow=(@pagenum*@pagesize)

SET @query='SELECT * FROM (SELECT 
[PkiUnderwriterId],
[UnderwriterName],
[PlanName],
[Premium],
[CoverAmount],
[Spouse],
[Children],
[Adults],
[Cover],
[UnderwriterSplit],
[ManagerSplit],
[SpouseCover],
[ChildCover],
[AdultCover],
[Parlourid],
[PlanUnderwriter],
[LastModified],
[ModifiedUser],
[CreateddDate],
[CreatedUser],
[Cover0to5year],
[Cover6to13year],
[Cover14to21year],
[Cover22to40year],
[Cover41to59year],
[Cover60to65year],
[Cover66to75year],
	ROW_NUMBER() OVER (ORDER BY [PkiUnderwriterId] asc) as RowNo
	FROM [dbo].[Underwriter]  WHERE [Parlourid]='''+cast(@ParlourId as nvarchar(100))+''' and IsDeleted IS NULL'

if @keyword !=''
BEGIN
 SET @query = @query +' AND (( [UnderwriterName] like ''%'+@keyword+'%'')
 OR ( [PlanName] like ''%'+@keyword+'%'')
  OR ( [Premium] like ''%'+@keyword+'%'')
  OR ( [CoverAmount] like ''%'+@keyword+'%''))
  AND IsDeleted IS NULL '
  
  END 
 SET @query = @query +' ) TB'

  SET @query = @query + ' ORDER BY '+@field+' '+@orderby
  print @query
  EXECUTE( @query)

  select 0 as TotalRecord

END


GO

GO
/****** Object:  StoredProcedure [dbo].[SavePlanDetails]    Script Date: 07/31/2017 2.50.51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SavePlanDetails]
           @pkiPlanID int
		  ,@PlanName nvarchar(255)
		  ,@PlanDesc nvarchar(255)
		  ,@PlanSubscription decimal(18,2)
		  ,@Cover decimal(18,2)
		  ,@Spouse int
		  ,@Children int
		  ,@Adults int
		  ,@WaitingPeriod int
		  ,@UnderwriterSplit decimal(18,2)
		  ,@AgentSplit decimal(18,2)
		  ,@PolicyLaps int
		  ,@parlourid uniqueidentifier
		  ,@PlanUnderwriter nvarchar(255)
		  ,@JoiningFee decimal(18,2)
		  ,@LastModified DateTime
		  ,@ModifiedUser nvarchar(255)
		  ,@HeadManagerSplit decimal(18,2)
		  ,@OfficeSplit decimal(18,2)
		  ,@AdminSplit decimal(18,2)
		  --,@MainMember int
		  ,@CompanySplit decimal(18,2)
		  ,@Cover0to5year decimal(18,2)
		  ,@Cover6to13year decimal(18,2)
		  ,@Cover14to21year decimal(18,2)
		  ,@AdultCover decimal(18,2)
		  ,@SpouseCover decimal(18,2)
		  ,@ChildCover decimal(18,2)
		  ,@Cover22to40year decimal(18,2)
		  ,@Cover41to59year decimal(18,2)
		  ,@Cover60to65year decimal(18,2)
		  ,@Cover66to75year decimal(18,2)
		  ,@UnderwriterId int = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	--DECLARE @lpkiMemberID INT
    -- Insert statements for procedure here
	
	IF @pkiPlanID=0 
	BEGIN
		INSERT INTO [dbo].[Plans]
           ([PlanName]
           ,[PlanDesc]
           ,[PlanSubscription]
		   ,[Cover]
           ,[Spouse]
           ,[Children]
           ,[Adults]
           ,[WaitingPeriod]
           ,[UnderwriterSplit]
           ,[AgentSplit]
           ,[PolicyLaps]
           ,[parlourid]
           ,[PlanUnderwriter]
           ,[JoiningFee]
           ,[LastModified]
           ,[ModifiedUser]
           ,[HeadManagerSplit]
           ,[OfficeSplit]
           ,[AdminSplit]
          -- ,[MainMember]
           ,[CompanySplit]
           ,[Cover0to5year]
           ,[Cover6to13year]
           ,[Cover14to21year]
		   ,[AdultCover]
		   ,[SpouseCover]
		   ,[ChildCover]
		   ,[Cover22to40year]
		   ,[Cover41to59year]
			,[Cover60to65year]
		,[Cover66to75year]
		,[UnderwriterId])
     VALUES
			(@PlanName
		  ,@PlanDesc
		  ,@PlanSubscription
		  ,@Cover
		  ,@Spouse
		  ,@Children
		  ,@Adults
		  ,@WaitingPeriod
		  ,@UnderwriterSplit
		  ,@AgentSplit
		  ,@PolicyLaps
		  ,@parlourid
		  ,@PlanUnderwriter
		  ,@JoiningFee
		  ,@LastModified
		  ,@ModifiedUser
		  ,@HeadManagerSplit
		  ,@OfficeSplit
		  ,@AdminSplit
		  --,@MainMember
		  ,@CompanySplit
		  ,@Cover0to5year
		  ,@Cover6to13year
		  ,@Cover14to21year
		  ,@AdultCover
		  ,@SpouseCover
		  ,@ChildCover
		  ,@Cover22to40year 
		  ,@Cover41to59year
		  ,@Cover60to65year
		  ,@Cover66to75year
		  ,@UnderwriterId)
		  SELECT	SCOPE_IDENTITY() As pkiPlanID
		  END
		  ELSE
	BEGIN
		UPDATE [dbo].[Plans]
				SET [PlanName] = @PlanName
			  ,[PlanDesc] = @PlanDesc
			  ,[PlanSubscription] = @PlanSubscription
			  ,[Cover] = @Cover
			  ,[Spouse] = @Spouse
			  ,[Children] = @Children
			  ,[Adults] = @Adults
			  ,[WaitingPeriod] = @WaitingPeriod
			  ,[UnderwriterSplit] = @UnderwriterSplit
			  ,[AgentSplit] = @AgentSplit
			  ,[PolicyLaps] = @PolicyLaps
			  ,[parlourid] = @parlourid
			  ,[PlanUnderwriter] = @PlanUnderwriter
			  ,[JoiningFee] = @JoiningFee
			  ,[LastModified] = @LastModified
			  ,[ModifiedUser] = @ModifiedUser
			  ,[HeadManagerSplit] = @HeadManagerSplit
			  ,[OfficeSplit] = @OfficeSplit
			  ,[AdminSplit] = @AdminSplit
			 -- ,[MainMember] = @MainMember
			  ,[CompanySplit] = @CompanySplit
			  ,[Cover0to5year] = @Cover0to5year
			  ,[Cover6to13year] = @Cover6to13year
			  ,[Cover14to21year] = @Cover14to21year
			  ,[AdultCover] = @AdultCover
			  ,[SpouseCover] = @SpouseCover
			  ,[ChildCover] = @ChildCover
			  ,[Cover22to40year]=@Cover22to40year
		   ,[Cover41to59year]=@Cover41to59year
			,[Cover60to65year]=@Cover60to65year
		,[Cover66to75year]=Cover66to75year
		,[UnderwriterId]=@UnderwriterId
		   WHERE [pkiPlanID] = @pkiPlanID
		   SELECT	@pkiPlanID As pkiPlanID
		 END
END
Go
GO
/****** Object:  StoredProcedure [dbo].[EditPlanbyID]    Script Date: 07/31/2017 2.54.05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 10-March-2015
-- Description:	Select EditPlanbyID
-- =============================================
ALTER PROCEDURE [dbo].[EditPlanbyID]
	-- Add the parameters for the stored procedure here
@ID int,
@ParlourId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
SELECT [pkiPlanID]
      ,[PlanName]
      ,[PlanDesc]
      ,[PlanSubscription]
      ,[Spouse]
      ,[Children]
      ,[Adults]
      ,[Cover]
      ,[WaitingPeriod]
      ,[UnderwriterSplit]
      ,[ManagerSplit]
      ,[AgentSplit]
      ,[PolicyLaps]
      ,[SpouseCover]
      ,[ChildCover]
      ,[AdultCover]
      ,[parlourid]
      ,[PlanUnderwriter]
      ,[JoiningFee]
      ,[LastModified]
      ,[ModifiedUser]
      ,[HeadManagerSplit]
      ,[Manager2Split]
      ,[OfficeSplit]
      ,[AdminSplit]
      ,[MainMember]
      ,[CompanySplit]
      ,[Cover0to5year]
      ,[Cover6to13year]
      ,[Cover14to21year]
	  ,[UnderwriterId]
  FROM [dbo].[Plans] WHERE [pkiPlanID]=@ID
  AND parlourid=@ParlourId
END
Go

