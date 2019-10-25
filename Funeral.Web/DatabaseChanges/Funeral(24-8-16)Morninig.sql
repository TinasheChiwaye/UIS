
GO
/****** Object:  StoredProcedure [dbo].[SelectFuneralManageServiceByParlANdID]    Script Date: 24-08-2016 10:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 17-aug-2016
-- Description:	Insert & Update [SelectFuneralManageService] 
-- =============================================
ALTER procedure [dbo].[SelectFuneralManageServiceByParlANdID](
	
	@pkiServiceID int,
	@parlourid uniqueidentifier
)

as 
begin
SET NOCOUNT ON
if @parlourid = null
	begin
	select * from FuneralServices where pkiServiceID=@pkiServiceID AND parlourid=@parlourid
	End
Else
	begin
	select * from FuneralServices where pkiServiceID=@pkiServiceID 
	End
END

