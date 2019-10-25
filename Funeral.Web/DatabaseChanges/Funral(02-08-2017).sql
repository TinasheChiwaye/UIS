GO
INSERT INTO [dbo].[tblPages]
           ([PageName]
           ,[PageTitle]
           ,[URL]
           ,[IsActive]
           ,[IsExcludedRight]
           ,[ParentRoleId]
           ,[IconClass]
           ,[MenuOrder]
           ,[MenuLevel]
           ,[IsMenu])
     VALUES
           ('Underwriter'
           ,'Underwriter'
           ,'/Tools/Underwriter.aspx'
           ,1
           ,0
           ,13
           ,''
           ,17
           ,1
           ,1)
GO
Go
ALTER TABLE [dbo].[Plans] ADD CashPayout money null

Go
GO
/****** Object:  StoredProcedure [dbo].[SavePlanDetails]    Script Date: 08/02/2017 12.13.42 PM ******/
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
		  ,@CashPayout decimal(18,2)= null

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
		,[UnderwriterId]
		,[CashPayout])
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
		  ,@UnderwriterId
		  ,@CashPayout)
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
		,[CashPayout]=@CashPayout
		   WHERE [pkiPlanID] = @pkiPlanID
		   SELECT	@pkiPlanID As pkiPlanID
		 END
END
Go
GO
/****** Object:  StoredProcedure [dbo].[EditPlanbyID]    Script Date: 08/02/2017 12.14.16 PM ******/
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
	  ,[CashPayout]
  FROM [dbo].[Plans] WHERE [pkiPlanID]=@ID
  AND parlourid=@ParlourId
END
Go
GO
/****** Object:  StoredProcedure [dbo].[MemberSelectList]    Script Date: 08/02/2017 12.14.49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 16-July-2016
-- Description:	Select Members and plolicy details By Id
-- =============================================
ALTER PROCEDURE [dbo].[MemberSelectList]
	-- Add the parameters for the stored procedure here
@ID int,
@ParlourId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
SELECT DISTINCT[pkiMemberID]
      ,[CreateDate]
      ,[MemberType]
      ,[Title]
      ,[Full Names] as FullNames
      ,[Surname]
      ,[Gender]
      ,[ID Number] as IDNumber
      ,[Date Of Birth] as DateOfBirth
      ,[Telephone]
      ,[Cellphone]
      ,[Address1]
      ,[Address2]
      ,[Address3]
      ,[Address4]
      ,[Code]
      ,[MemeberNumber]
      ,[MemberSociety]
      ,[fkiPlanID]
      ,[Active]
      ,[InceptionDate]
      ,[Claimnumber]
      ,[PolicyStatus]
      ,M.[parlourid]
      ,[Agent]
      ,[AccountHolder]
      ,[Bank]
      ,[BranchCode]
      ,[Branch]
      ,[AccountNumber]
      ,[AccountType]
      ,[DebitDate]
      ,[MemberBranch]
      ,[CoverDate]
      ,M.[LastModified]
      ,M.[ModifiedUser]
      ,[Email]
	   ,[Citizenship]
	  ,[Passport]
	  ,AMI.EasyPayNo,pkiAdditionalMemberInfo
	  , PlanName
	  ,PlanSubscription
	  ,[Cover]
	  ,BusinessAddressLine1
	  ,BusinessAddressLine2
	  ,BusinessAddressLine3
	  ,BusinessAddressLine4
	  ,ManageTelNumber,
	  CustomId1,
	  CustomId2,
	  CustomId3,
	  PlanUnderwriter,
	  CashPayout
  FROM [dbo].[Members] M
  LEFT JOIN [dbo].[AdditionalMemberInfo] AMI ON AMI.fkiMemberID=M.pkiMemberID 
  LEFT JOIN Plans P on M.fkiPlanID=p.pkiPlanID
  LEFT JOIN ApplicationSettings A on A.parlourid=M.parlourid
  WHERE [pkiMemberID]=@ID
  --AND Members.parlourid=@ParlourId
END
Go
