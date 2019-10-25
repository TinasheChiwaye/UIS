USE [DevData]
GO
/****** Object:  StoredProcedure [dbo].[GET_Dependent_And_Extended_Family]    Script Date: 01-09-2016 13:17:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GET_Dependent_And_Extended_Family] 
	-- Add the parameters for the stored procedure here
	@parlourId uniqueidentifier,
	@MemberId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT D.fkiMemberID as MemberId,
	 D.pkiDependentID,
	 D.DependencyType,
	 ltrim(D.[Full Name]) AS FullName,
	ltrim(D. Surname) As Surname,
	D. IDNumber,
	 D.parlourid,
	 D. Age ,
	 D. [Date Of Birth] as DateOfBirth,
	 D. CoverDate,
	 D. InceptionDate,
	 D. Premium,
	 D. Relationship,
	 D.Gender,
	  r.Type As RelationshipType
	  --DT.DepStatus_Code as DTCode
	   FROM Dependencies D
	   LEFT JOIN Relationship r ON r.ID=d.Relationship 
	   ---LEFT JOIN DependencyType DT ON dt.DepStatus_ID= D.DependencyType
	WHERE D.parlourid=@parlourId
	AND  D.fkiMemberID=@MemberId
END
GO