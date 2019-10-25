Go
Alter Table Claims Add [CreatedDate] datetime NULL
Go
 Alter Table Claims Add [IsDeleted] Bit NULL
 Go
 Alter Table Claims Add [DeletedBy] nvarchar(50) NULL
 Go
 Alter Table Claims Add [DeletedDate] datetime NULL
 Go

GO
/****** Object:  StoredProcedure [dbo].[SelectAllClaimsByParlourId]    Script Date: 22-08-2016 18:55:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SelectAllClaimsByParlourId] 
	-- Add the parameters for the stored procedure here
--	@ParlourId UniqueIdentifier
	@ParlourId uniqueIdentifier='00000000-0000-0000-0000-000000000000',
	@pagesize int=10,
	@pagenum int=1,
	@keyword nvarchar(max)='',
	@field nvarchar(max)='pkiClaimID',
	@orderby varchar(255)='ASC'
	
AS
BEGIN
SET NOCOUNT ON;


DECLARE @Init int ,@LastRow int, @query nvarchar(4000)
SET NOCOUNT ON;


IF @pagenum <=0  
BEGIN
 SET @pagenum=1
END 

SET @Init=(@pagesize*(@pagenum-1))+1
SET @LastRow=(@pagenum*@pagesize)

SET @query='SELECT * FROM (SELECT 
pkiClaimID,
	fkiMemberID,
MemberNumber,
ClaimDate,
ClaimNotes,
CourseOfDearth,
HostingFuneral,
ClaimantTitle,
ClaimantFullname,
ClaimantSurname,
ClaimantIDNumber,
ClaimantDateOfBirth,
ClaimantGender,
ClaimantAddressLine1,
ClaimantAddressLine2,
ClaimantAddressLine3,
ClaimantAddressLine4,
ClaimantCode,
ClaimantContactNumber,
BeneficiaryBank,
BeneficiaryAccountHolder,
BeneficiaryAccountNumber,
BeneficiaryBankBranch,
BeneficiaryBranchCode,
BeneficiaryAccountType,
LoggedBy,
Cover,
BodyCollectedFrom,
ClaimingFor,
parlourid,
CreatedDate
	ROW_NUMBER() OVER (ORDER BY [pkiClaimID] asc) as RowNo
	FROM [dbo].[Claims]  WHERE [parlourid]='''+cast(@ParlourId as nvarchar(100))+''' and IsDeleted IS NULL'

if @keyword !=''
BEGIN
 SET @query = @query +' AND (( [MemberNumber] like ''%'+@keyword+'%'')
 OR ( [ClaimDate] like ''%'+@keyword+'%'')
  OR ( [ClaimNotes] like ''%'+@keyword+'%'')
  OR ( [ClaimantFullname] like ''%'+@keyword+'%'')
  OR ( [ClaimantIDNumber] like ''%'+@keyword+'%''))
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
/****** Object:  StoredProcedure [dbo].[SaveClaims]    Script Date: 22-08-2016 17:52:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SaveClaims]
@pkiClaimID int
,@fkiMemberID int
,@MemberNumber nvarchar(255)
,@ClaimDate datetime
,@ClaimNotes nvarchar(255)
,@CourseOfDearth nvarchar(255)
,@HostingFuneral bit 
,@ClaimantTitle nvarchar(255)
,@ClaimantFullname nvarchar(255) 
,@ClaimantSurname nvarchar(255)
,@ClaimantIDNumber nvarchar(255) 
,@ClaimantDateOfBirth datetime
,@ClaimantGender nvarchar(255)
,@ClaimantAddressLine1 nvarchar(255)
,@ClaimantAddressLine2 nvarchar(255)
,@ClaimantAddressLine3 nvarchar(255)
,@ClaimantAddressLine4 nvarchar(255)
,@ClaimantCode  nvarchar(255)
,@ClaimantContactNumber nvarchar(255)
,@BeneficiaryBank nvarchar(255)
,@BeneficiaryAccountHolder nvarchar(255)
,@BeneficiaryAccountNumber nvarchar(255)
,@BeneficiaryBankBranch nvarchar(255)
,@BeneficiaryBranchCode nvarchar(255)
,@BeneficiaryAccountType nvarchar(255)
,@LoggedBy nvarchar(255)
,@Cover money
,@BodyCollectedFrom nvarchar(255)
,@ClaimingFor nvarchar(255)
,@parlourid uniqueidentifier
,@ModifiedUser varchar(50)


as
begin
	IF @pkiClaimID =0
	begin
		Insert Into [dbo].[Claims]
		(
fkiMemberID,
MemberNumber,
ClaimDate,
ClaimNotes,
CourseOfDearth,
HostingFuneral,
ClaimantTitle,
ClaimantFullname,
ClaimantSurname,
ClaimantIDNumber,
ClaimantDateOfBirth,
ClaimantGender,
ClaimantAddressLine1,
ClaimantAddressLine2,
ClaimantAddressLine3,
ClaimantAddressLine4,
ClaimantCode,
ClaimantContactNumber,
BeneficiaryBank,
BeneficiaryAccountHolder,
BeneficiaryAccountNumber,
BeneficiaryBankBranch,
BeneficiaryBranchCode,
BeneficiaryAccountType,
LoggedBy,
Cover,
BodyCollectedFrom,
ClaimingFor,
parlourid,
CreatedDate
)
Values
(
@fkiMemberID,
@MemberNumber,
@ClaimDate,
@ClaimNotes,
@CourseOfDearth,
@HostingFuneral,
@ClaimantTitle,
@ClaimantFullname,
@ClaimantSurname,
@ClaimantIDNumber,
@ClaimantDateOfBirth,
@ClaimantGender,
@ClaimantAddressLine1,
@ClaimantAddressLine2,
@ClaimantAddressLine3,
@ClaimantAddressLine4,
@ClaimantCode,
@ClaimantContactNumber,
@BeneficiaryBank,
@BeneficiaryAccountHolder,
@BeneficiaryAccountNumber,
@BeneficiaryBankBranch,
@BeneficiaryBranchCode,
@BeneficiaryAccountType,
@LoggedBy,
@Cover,
@BodyCollectedFrom,
@ClaimingFor,
@parlourid,
Getdate()
)
SELECT SCOPE_IDENTITY() AS pkiClaimID
	END
	Else
	Begin
		Update [dbo].[Claims]
		Set
		fkiMemberID=@fkiMemberID,
MemberNumber=@MemberNumber,
ClaimDate=@ClaimDate,
ClaimNotes=@ClaimNotes,
CourseOfDearth=@CourseOfDearth,
HostingFuneral=@HostingFuneral,
ClaimantTitle=@ClaimantTitle,
ClaimantFullname=@ClaimantFullname,
ClaimantSurname=@ClaimantSurname,
ClaimantIDNumber=@ClaimantIDNumber,
ClaimantDateOfBirth=@ClaimantDateOfBirth,
ClaimantGender=@ClaimantGender,
ClaimantAddressLine1=@ClaimantAddressLine1,
ClaimantAddressLine2=@ClaimantAddressLine2,
ClaimantAddressLine3=@ClaimantAddressLine3,
ClaimantAddressLine4=@ClaimantAddressLine4,
ClaimantCode=@ClaimantCode,
ClaimantContactNumber=@ClaimantContactNumber,
BeneficiaryBank=@BeneficiaryBank,
BeneficiaryAccountHolder=@BeneficiaryAccountHolder,
BeneficiaryAccountNumber=@BeneficiaryAccountNumber,
BeneficiaryBankBranch=@BeneficiaryBankBranch,
BeneficiaryBranchCode=@BeneficiaryBranchCode,
BeneficiaryAccountType=@BeneficiaryAccountType,
LoggedBy=@LoggedBy,
Cover=@Cover,
BodyCollectedFrom=@BodyCollectedFrom,
ClaimingFor=@ClaimingFor,
parlourid=@parlourid,
ModifiedUser=@ModifiedUser,
LastModified=Getdate()
Where [pkiClaimID]=@pkiClaimID
select @pkiClaimID As [pkiClaimID]
	End
END
Go