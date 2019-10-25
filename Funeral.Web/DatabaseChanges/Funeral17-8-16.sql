Go
ALTER TABLE Quotations Add  [QuotationStatus] nvarchar(50) NULL
Go

GO
/****** Object:  StoredProcedure [dbo].[SaveFuneralManageService]    Script Date: 17-08-2016 20:15:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 08-aug-2016
-- Description:	Insert & Update [SaveFuneralManageService] 
-- =============================================
Create procedure [dbo].[SaveFuneralManageService](
	
	@pkiServiceID int,
	@ServiceName nvarchar(50),
	@ServiceDesc nvarchar(50),
	@ServiceCost money,
	@QTY int,
	@parlourid uniqueidentifier,
	@ModifiedUser varchar(50)
)

as 
begin
SET NOCOUNT ON

IF @pkiServiceID=0 
begin
INSERT INTO [FuneralServices] (
	[ServiceName],
	[ServiceDesc],
	[ServiceCost],
	[QTY],
	[ModifiedUser],
	[parlourid],
	[LastModified]
)
VALUES (
	@ServiceName,
	@ServiceDesc,
	@ServiceCost,
	@QTY,
	@ModifiedUser,
	@parlourid,	
	GETDATE()
)
SELECT SCOPE_IDENTITY() AS pkiServiceID
END
ELSE
	BEGIN
		UPDATE [dbo].[FuneralServices]
	SET 
	[ServiceName]=@ServiceName,
	[ServiceDesc]=@ServiceDesc,
	[ServiceCost]=@ServiceCost,
	[QTY]=@QTY,
	[ModifiedUser]=@ModifiedUser,
	[parlourid]=@parlourid,
	[LastModified]=GETDATE()
	 WHERE [pkiServiceID] = @pkiServiceID
	SELECT	@pkiServiceID As [pkiServiceID]
	END
END
Go

GO
/****** Object:  StoredProcedure [dbo].[SelectFuneralManageServiceByParlANdID]    Script Date: 17-08-2016 20:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 17-aug-2016
-- Description:	Insert & Update [SelectFuneralManageService] 
-- =============================================
Create procedure [dbo].[SelectFuneralManageServiceByParlANdID](
	
	@pkiServiceID int,
	@parlourid uniqueidentifier
)

as 
begin
SET NOCOUNT ON

	select * from FuneralServices where pkiServiceID=@pkiServiceID AND parlourid=@parlourid
END
Go

GO
/****** Object:  StoredProcedure [dbo].[SelectFuneralManageServiceByParlID]    Script Date: 17-08-2016 20:17:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 17-aug-2016
-- Description:	Insert & Update [SelectFuneralManageServiceByParlID] 
-- =============================================
Create procedure [dbo].[SelectFuneralManageServiceByParlID](
	@parlourid uniqueidentifier ='00000000-0000-0000-0000-000000000000',
	@pagesize int=10,
	@pagenum int=1,
	@keyword nvarchar(max)='',
	@field nvarchar(max)='pkiServiceID',
	@orderby varchar(255)='ASC'
)

as 
begin
SET NOCOUNT ON
DECLARE @Init int ,@LastRow int, @query nvarchar(4000)
SET NOCOUNT ON;


IF @pagenum <=0  
BEGIN
 SET @pagenum=1
END 

SET @Init=(@pagesize*(@pagenum-1))+1
SET @LastRow=(@pagenum*@pagesize)

SET @query='SELECT * FROM (SELECT 
[pkiServiceID],
[ServiceName],
[ServiceDesc],
[ServiceCost],
[QTY],
[parlourid],
[LastModified],
[ModifiedUser],
	ROW_NUMBER() OVER (ORDER BY [pkiServiceID] asc) as RowNo
	FROM [dbo].[FuneralServices]  WHERE [parlourid]='''+cast(@parlourid as nvarchar(100))+''''

if @keyword !=''
BEGIN
 SET @query = @query +' AND ( [ServiceName] like ''%'+@keyword+'%'')'
 -- OR ( [ServiceCost] like ''%'+@keyword+'%'')
 -- OR ( [QTY] like ''%'+@keyword+'%'')'
  
  END 
 SET @query = @query +' ) TB'

  SET @query = @query + ' ORDER BY '+@field+' '+@orderby
  print @query
  EXECUTE( @query)

  select 0 as TotalRecord

	
END
Go


GO
/****** Object:  StoredProcedure [dbo].[SelectAllByParlourId]    Script Date: 17-08-2016 20:18:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SelectAllByParlourId] 
	-- Add the parameters for the stored procedure here
--	@ParlourId UniqueIdentifier
	@ParlourId uniqueIdentifier='00000000-0000-0000-0000-000000000000',
	@pagesize int=10,
	@pagenum int=1,
	@keyword nvarchar(max)='',
	@field nvarchar(max)='QuotationID',
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
	[QuotationID],
	[ContactTitle],
	[ContactFirstName],
	[ContactLastName],
	[TelNumber],
	[CellNumber],
	[AddressLine1],
	[AddressLine2],
	[AddressLine3],
	[AddressLine4],
	[Code],
	[DateOfQuotation],
	[parlourid],
	[LastModified],
	[ModifiedUser],
	[QuotationStatus],
	ROW_NUMBER() OVER (ORDER BY [QuotationID] asc) as RowNo
	FROM [dbo].[Quotations]  WHERE [parlourid]='''+cast(@ParlourId as nvarchar(100))+''' and IsDeleted IS NULL'

if @keyword !=''
BEGIN
 SET @query = @query +' AND (( [ContactFirstName] like ''%'+@keyword+'%'')
 OR ( [ContactLastName] like ''%'+@keyword+'%'')
  OR ( [QuotationID] like ''%'+@keyword+'%'')
  OR ( [TelNumber] like ''%'+@keyword+'%'')
  OR ( [Code] like ''%'+@keyword+'%''))
  AND IsDeleted IS NULL '
  
  END 
 SET @query = @query +' ) TB'

  SET @query = @query + ' ORDER BY '+@field+' '+@orderby
  print @query
  EXECUTE( @query)

  select 0 as TotalRecord

END

Go
