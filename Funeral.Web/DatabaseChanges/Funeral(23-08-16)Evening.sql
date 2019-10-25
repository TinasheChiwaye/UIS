
GO
/****** Object:  StoredProcedure [dbo].[SelectApplicationList]    Script Date: 23-08-2016 19:54:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[SelectApplicationList] 
@param int ,
@parlourid uniqueidentifier,
@AppID int 
AS
BEGIN
if @param = 1
	BEGIN
	Select * from ApplicationSettings order by ApplicationName 
	End
else if @param = 2
	Begin
		select * from ApplicationSettings where pkiApplicationID=@AppID
	End
Else
	begin
	Select * from ApplicationSettings Where parlourid=@parlourid
	End
End
GO