Go
Alter Table Members Add [StartDate] datetime NULL
GO


GO
/****** Object:  StoredProcedure [dbo].[SaveMembers]    Script Date: 20-08-2016 19:48:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveMembers]
	-- Add the parameters for the stored procedure here
	--Tab 1 Data
	@surname nvarchar(255),
	@FullNames nvarchar(255),
	@IDNumber nvarchar(255),
	@DateOfBirth  datetime,
	@Gender bit,
	@CellPhone nvarchar(255),
	@TelePhone nvarchar(255),
	@Email nvarchar(255),
	@Citizenship nvarchar(255),
	@Passport nvarchar(255),
	
	--Tab 2 Data
	@Address1 text,
	@Address3 nvarchar(255),--TOWN
	@Province nvarchar(255),--
	@Code nvarchar(255),
	@Address2 text,

	--Tab 3 Data
	@Policyname nvarchar(255),--
	@PolicyPremium nvarchar(255),--
	@PolicyNo nvarchar(255),--
	@MemberBranch nvarchar(255),
	@Society nvarchar(255),
	@InceptionDate datetime,
	@PolicyUnderwrittenDate nvarchar(255),--
	@Agent nvarchar(255),


	--Tab 4 Data
	@AccountHolder nvarchar(255),
	@Bank nvarchar(255),
	@BankBranch nvarchar(255),
	@BranchCode nvarchar(255),
	@AccountNumber nvarchar(255),
	@Accounttype nvarchar(255),

	--Common Data 
	
	@pkiMemberID int

	--Extra Data
			,@CreateDate DATEtime
           ,@MemberType nvarchar(255)
		   ,@MemberSociety nvarchar(255)
           ,@Title nvarchar(255)
		   ,@Address4 text
		   ,@MemeberNumber nvarchar(255)
		   ,@fkiPlanID int
		   ,@Active bit
		   ,@Claimnumber nvarchar(255)
		   ,@PolicyStatus nvarchar(255)
		   ,@parlourid uniqueidentifier
		   ,@DebitDate  datetime=null
		   ,@CoverDate  datetime=Null
		   ,@LastModified datetime
		  , @ModifiedUser nvarchar(255)
		  ,@EasyPayNo nvarchar(255)
		  ,@pkiAdditionalMemberInfo uniqueidentifier
		  ,@StartDate datetime

		   

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	DECLARE @lpkiMemberID INT
    -- Insert statements for procedure here
	IF @pkiMemberID=0 
	BEGIN
		INSERT INTO [dbo].[Members]
           ([CreateDate]
           ,[MemberType]
           ,[Title]
           ,[Full Names]
           ,[Surname]
			,[Gender]
           ,[ID Number]
           ,[Date Of Birth]
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
           ,[parlourid]
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
           ,[LastModified]
           ,[ModifiedUser]
		   ,[Email]
		   ,[Citizenship]
		   ,[Passport]
		   ,[StartDate])
     VALUES
			(@CreateDate
           ,@MemberType
           ,@Title
           ,@FullNames
           ,@surname
           ,@Gender
           ,@IDNumber
			,@DateOfBirth
           ,@TelePhone
           ,@CellPhone
           ,@Address1
           ,@Address2
           ,@Address3
           ,@Address4
           ,@Code
           ,@MemeberNumber
           ,@MemberSociety
           ,@fkiPlanID
           ,@Active
           ,@InceptionDate
           ,@Claimnumber
            ,@PolicyStatus
            ,@parlourid
           ,@Agent
           ,@AccountHolder
           ,@Bank
			,@BranchCode
           ,@BankBranch
           ,@AccountNumber
           ,@AccountType
           ,@DebitDate
           ,@MemberBranch
          ,@CoverDate
		   ,@LastModified
		  , @ModifiedUser
		   ,@Email
		   ,@Citizenship
		   ,@Passport
		   ,@StartDate)
		   --SELECT	SCOPE_IDENTITY() As pkiMemberID
		  set @lpkiMemberID=( Select SCOPE_IDENTITY() as ID)
		   		  
	IF (@MemeberNumber IS NULL OR  @MemeberNumber='')
	BEGIN
			select @MemeberNumber=[dbo].fnReturnPolicyNumber(@parlourid)
			update [Members] set MemeberNumber=@MemeberNumber Where pkiMemberID=@lpkiMemberID and parlourid=@parlourid
	END


			 INSERT INTO [dbo].[AdditionalMemberInfo]
		   ([pkiAdditionalMemberInfo]
		   ,[fkiMemberID]
           ,[EasyPayNo]
		   ,[IDNumberVerified]
		   ,[AccountNumberVerified]
           ,[BrokerID]
		   ,[BrokerPlanID]
		   ,[RefNumber]
           ,[LastModified]
		   ,[ModifiedUser])
		   VALUES
		   (@pkiAdditionalMemberInfo
		   ,@lpkiMemberID
           , case when (@EasyPayNo IS NULL OR  @EasyPayNo='') THEN (Select [dbo].fnReturnEasypaynummber(@parlourid,@lpkiMemberID)) ELSE @EasyPayNo END
		   ,0
		   ,0
		   
           ,CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		   ,CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		   ,NULL
		   ,getdate()
           ,'admin')
		   Select @lpkiMemberID as pkiMemberID
		   END
ELSE
	BEGIN
		UPDATE [dbo].[Members]
   SET [CreateDate] =@CreateDate
      ,[MemberType] = @MemberType
      ,[Title] =@Title
      ,[Full Names] =@FullNames
      ,[Surname] =@surname
      ,[Gender] =@Gender
      ,[ID Number] = @IDNumber
      ,[Date Of Birth] = @DateOfBirth
      ,[Telephone] = @TelePhone
      ,[Cellphone] = @CellPhone
      ,[Address1] = @Address1
      ,[Address2] = @Address2
      ,[Address3] =@Address3
      ,[Address4] = @Address4
      ,[Code] = @Code
      ,[MemeberNumber] =@MemeberNumber
      ,[MemberSociety] =@MemberSociety
      ,[fkiPlanID] = @fkiPlanID
      ,[Active] = @Active
      ,[InceptionDate] = @InceptionDate
      ,[Claimnumber] = @Claimnumber
      ,[PolicyStatus] = @PolicyStatus
      ,[parlourid] = @parlourid
      ,[Agent] = @Agent
      ,[AccountHolder] =@AccountHolder
      ,[Bank] = @Bank
      ,[BranchCode] = @BranchCode
      ,[Branch] =@BankBranch
      ,[AccountNumber] = @AccountNumber
      ,[AccountType] =@AccountType
      ,[DebitDate] =@DebitDate
      ,[MemberBranch] = @MemberBranch
      ,[CoverDate] = @CoverDate
      ,[LastModified] = @LastModified
      ,[ModifiedUser] = @ModifiedUser
      ,[Email] = @Email
	  ,[Citizenship] = @Citizenship
	  ,[Passport] = @Passport
	  ,[StartDate] = @StartDate
 WHERE pkiMemberID=@pkiMemberID
		SELECT	@pkiMemberID As pkiMemberID

		if EXISTS(Select fkiMemberID from AdditionalMemberInfo where fkiMemberID=@pkiMemberID)
		BEGIN 
		UPDATE [dbo].[AdditionalMemberInfo]
		 SET [pkiAdditionalMemberInfo] =@pkiAdditionalMemberInfo
		 ,[fkiMemberID] = @pkiMemberID
		 ,[EasyPayNo] =@EasyPayNo
		 ,[IDNumberVerified]=0
		 ,[AccountNumberVerified]=0
         ,[BrokerID]=CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		 ,[BrokerPlanID]=CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		 ,[RefNumber]=NULL
         ,[LastModified]=getdate()
		 ,[ModifiedUser]='admin'

		 WHERE fkiMemberID=@pkiMemberID
		SELECT	@pkiAdditionalMemberInfo As pkiAdditionalMemberInfo
		END
		Else
		BEGIN 
		INSERT INTO [dbo].[AdditionalMemberInfo]
		   ([pkiAdditionalMemberInfo]
		   ,[fkiMemberID]
           ,[EasyPayNo]
		   ,[IDNumberVerified]
		   ,[AccountNumberVerified]
           ,[BrokerID]
		   ,[BrokerPlanID]
		   ,[RefNumber]
           ,[LastModified]
		   ,[ModifiedUser])
		   VALUES
		   (@pkiAdditionalMemberInfo
		   ,@pkiMemberID
          , case when (@EasyPayNo IS NULL OR  @EasyPayNo='') THEN (Select [dbo].fnReturnEasypaynummber(@parlourid,@pkiMemberID)) ELSE @EasyPayNo END
		   ,0
		   ,0
		   
           ,CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		   ,CONVERT(uniqueidentifier, '00000000-0000-0000-0000-000000000000')
		   ,NULL
		   ,getdate()
           ,'admin')
		   SELECT	@pkiAdditionalMemberInfo As pkiAdditionalMemberInfo
	
		END
	END

	
END
Go

GO
/****** Object:  StoredProcedure [dbo].[MemberSelect]    Script Date: 20-08-2016 19:48:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 10-March-2015
-- Description:	Select Members By Id
-- =============================================
ALTER PROCEDURE [dbo].[MemberSelect]
	-- Add the parameters for the stored procedure here
@ID int,
@ParlourId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
SELECT [pkiMemberID]
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
      ,[parlourid]
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
      ,Members.[LastModified]
      ,Members.[ModifiedUser]
      ,[Email]
	   ,[Citizenship]
	  ,[Passport]
	  ,Members.[StartDate]
	  ,AMI.EasyPayNo,pkiAdditionalMemberInfo
  FROM [dbo].[Members] LEFT JOIN [dbo].[AdditionalMemberInfo] AMI ON AMI.fkiMemberID=Members.pkiMemberID WHERE [pkiMemberID]=@ID
  --AND Members.parlourid=@ParlourId
END
Go