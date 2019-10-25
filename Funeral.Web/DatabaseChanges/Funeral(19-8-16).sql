GO
/****** Object:  StoredProcedure [dbo].[RPTQuotationByDate1]    Script Date: 19-08-2016 20:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[RPTQuotationByDate1] 
(
@parlourid uniqueidentifier,
	@DateFrom date=null,
	@DateTo date=null
)
AS
BEGIN
SET NOCOUNT ON;

Select SUM(SubTotal) As SubTotal,
(SUM(SubTotal) * ISNULL(Discount,0))/100 As DiscountTotal,
(SUM(SubTotal) * ISNULL(Tax,0))/100 AS TaxTotal,
(((Sum(SubTotal)+((Sum(SubTotal)*ISNULL(Tax,0))/100)) * ISNULL(Discount,0))/100) as DiscountTotal,
((Sum(SubTotal)+((Sum(SubTotal)*ISNULL(Tax,0))/100)) -((Sum(SubTotal)+((Sum(SubTotal)*ISNULL(Tax,0))/100))* ISNULL(Discount,0))/100) as GrandTotal,
Discount,
Tax,
DateOfQuotation,
ContactTitle,
ContactFirstName,
ContactLastName,
CellNumber,
QuotationID From (Select (Isnull(Quantity,0) * ISNULL(Servicerate,0)) As SubTotal,
Quotations.QuotationID,
Quotations.Discount,
Quotations.Tax,
Quotations.ContactTitle,
Quotations.ContactFirstName,
Quotations.ContactLastName,
Quotations.CellNumber,
Quotations.DateOfQuotation
from Quotations
Inner join QuotationServicesSelection ON Quotations.QuotationID = QuotationServicesSelection.QuotationID 
where  
	   Quotations.parlourid=@parlourid and
	   (@DateFrom is null  or Quotations.DateOfQuotation>=@DateFrom) AND 
	   (@DateTo is null  or Quotations.DateOfQuotation<=@DateTo)
) AS T1
Group By QuotationID,Discount,Tax,DateOfQuotation,ContactTitle,ContactFirstName,ContactLastName,CellNumber


END
Go

GO
/****** Object:  StoredProcedure [dbo].[SelectApplicationTnCByParlourId]    Script Date: 19-08-2016 20:09:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hemant Yadav
-- Create date: 10-March-2015
-- Description:	Select Members By Id
-- =============================================
Create PROCEDURE [dbo].[SelectApplicationTnCByParlourId]
	-- Add the parameters for the stored procedure here
@ParlourId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
select * from tblApplicationTermsAndCondition where parlourid=@ParlourId
END
Go