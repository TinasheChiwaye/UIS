GO
/****** Object:  StoredProcedure [dbo].[RPTallmembers]    Script Date: 23-08-2016 11:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--      select  distinct  top(500) PolicyStatus    from Members
 --  [RPTallmembers] '6dcba090-f363-47e6-93f5-6def8f80703e','ALL','01-01-2013','01-01-2016',null,'ALL',null,null
 --  [RPTallmembers] '6dcba090-f363-47e6-93f5-6def8f80703e','All',Null,Null,null,'KV Kiaat TT','All'
  --  [RPTallmembers] '6dcba090-f363-47e6-93f5-6def8f80703e','All',Null,Null,null,'All','All'
  --  [RPTallmembers] '6dcba090-f363-47e6-93f5-6def8f80703e','All',Null,Null,null,'KV Kiaat TT','All','All'
   -- [RPTallmembers] '6dcba090-f363-47e6-93f5-6def8f80703e','All',Null,Null,'All','null','All','All'
ALTER procedure [dbo].[RPTallmembers]   

@parlourid uniqueidentifier='00000000-0000-0000-0000-000000000000', 
 @branch varchar(100) = 'ALL',
 @StartDate datetime  = null,
 @EndDate datetime  = null,
 @PolicyStatus nvarchar(100)  = null,
  @MemberPlan nvarchar(100)  = null,
  @PolicyStatusList nvarchar(1000)='All',
  @Underwriterplan nvarchar(1000)='All'
as  
IF @branch='ALL' 
BEGIN
									IF @MemberPlan='-1' 
									BEGIN
									Select * From (
SELECT  Members.MemeberNumber  As [MemeberNo],  
Plans.PlanSubscription,
Members.MemberSociety  As [Society],      
Plans.PlanName As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Members.[Full Names]) As [Name] ,      
ltrim(Members.Surname) As [Surname] ,      
Members.Gender As [Gender],      
Members.AccountNumber As [IDNo],      
cast(datepart(year,(Members.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Members.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Members.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Members.[Date Of Birth],getdate())/365  'Age',  
'Main Member' as 'Type',  
0 as MainOrder, 
Members.Telephone As [TelNo],      
Members.Cellphone As [CellNo],      
Members.Address1,      
Members.Address2,      
Members.Address3,      
Members.Address4,      
Members.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
left outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]
	where 
	Members.parlourid=@parlourid and
	Members.active=1  
	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	AND	(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
	AND	(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
UNION ALL
SELECT  Members.MemeberNumber  As [MemeberNo],  
0 AS PlanSubscription,
Members.MemberSociety  As [Society],      
Dependencies.[Plan] As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Dependencies.[Full Name]) As [Name] ,      
ltrim(Dependencies.Surname) As [Surname] ,      
Dependencies.Gender As [Gender],      
Dependencies.IDNumber As [IDNo],      
cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  
case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 
when Dependencies.DependencyType = ' ' then 'Not Specified' 
else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',   
1 as MainOrder,
Dependencies.Telephone As [TelNo],      
Dependencies.Cellphone As [CellNo],      
Dependencies.Address1,      
Dependencies.Address2,      
Dependencies.Address3,      
Dependencies.Address4,      
Dependencies.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
INNER JOIN Dependencies with (nolock)  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]      
left outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
	where 
	Members.parlourid=@parlourid and
	Members.active=1  
	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	AND	(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
	AND	(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
) AS Table3 Order By [MemeberNo],MainOrder
--									SELECT Members.MemeberNumber  As [MemeberNo],  
--							--Plans.PlanUnderwriter,
--									Plans.PlanSubscription,

--									Members.MemberSociety  As [Society],      

--									Dependencies.[Plan] As [Plan],    

--									cast ('' as varchar) Cover,      

--								   ltrim( Dependencies.[Full Name]) As [Name] ,      

--									ltrim(Dependencies.Surname) As [Surname] ,      

--									Dependencies.Gender As [Gender],      

--									Dependencies.IDNumber As [IDNo],      

--									  cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  

--									  right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  

--									right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  

--									 As [DateOfBirth],   

--			datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  

--			case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 

--				when Dependencies.DependencyType = ' ' then 'Not Specified' 

--				else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',   

--									Dependencies.Telephone As [TelNo],      

--									Dependencies.Cellphone As [CellNo],      

--									Dependencies.Address1,      

--									Dependencies.Address2,      

--									Dependencies.Address3,      

--									Dependencies.Address4,      

--									Dependencies.Code ,  

--			Members.PolicyStatus     

--			,Members.MemberBranch
--			,AdditionalMemberInfo.EasyPayNo
--FROM Members
--left JOIN Dependencies ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]     
--left outer JOIN Plans ON Plans.pkiPlanID= Members.fkiPlanId 
--left outer JOIN AdditionalMemberInfo ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
--where 
--Members.parlourid=@parlourid and
--Members.active=1  
--AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
----AND PolicyStatus like '%'+@PolicySatus+'%'
--AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
----AND MemberBranch like '%'+isnull(@branch,'')+'%'
----AND (@MemberPlan is null or Dependencies.[Plan] like '%'+isnull(@MemberPlan,'')+'%')
--AND		(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
--AND		(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
----	AND Members.CreateDate between isnull(@StartDate,cast('1753-1-1' as datetime)) AND isnull(@EndDate,cast('2099-1-1' as datetime)) 
--	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
--	order by 1,11, 4 desc 
									END
									ELSE
									BEGIN
									Select * From (
SELECT  Members.MemeberNumber  As [MemeberNo],  
Plans.PlanSubscription,
Members.MemberSociety  As [Society],      
Plans.PlanName As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Members.[Full Names]) As [Name] ,      
ltrim(Members.Surname) As [Surname] ,      
Members.Gender As [Gender],      
Members.AccountNumber As [IDNo],      
cast(datepart(year,(Members.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Members.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Members.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Members.[Date Of Birth],getdate())/365  'Age',  
'Main Member' as 'Type',   
0 as MainOrder,
Members.Telephone As [TelNo],      
Members.Cellphone As [CellNo],      
Members.Address1,      
Members.Address2,      
Members.Address3,      
Members.Address4,      
Members.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
INNER JOIN Dependencies with (nolock)  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]   
left outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]
where 
	Members.parlourid=@parlourid and
	Members.active=1  
	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
	AND	(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
	AND	(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
UNION ALL
SELECT  Members.MemeberNumber  As [MemeberNo],  
Plans.PlanSubscription,
Members.MemberSociety  As [Society],      
Dependencies.[Plan] As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Dependencies.[Full Name]) As [Name] ,      
ltrim(Dependencies.Surname) As [Surname] ,      
Dependencies.Gender As [Gender],      
Dependencies.IDNumber As [IDNo],      
cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  
case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 
when Dependencies.DependencyType = ' ' then 'Not Specified' 
else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',   
1 as MainOrder,
Dependencies.Telephone As [TelNo],      
Dependencies.Cellphone As [CellNo],      
Dependencies.Address1,      
Dependencies.Address2,      
Dependencies.Address3,      
Dependencies.Address4,      
Dependencies.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
INNER JOIN Dependencies with (nolock)  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]      
Right outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
where 
	Members.parlourid=@parlourid and
	Members.active=1  
	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
	AND	(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
	AND	(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
) AS Table2 Order By [MemeberNo],MainOrder
--									SELECT Members.MemeberNumber  As [MemeberNo],  
--							--Plans.PlanUnderwriter,
--									Plans.PlanSubscription,

--									Members.MemberSociety  As [Society],      

--									Dependencies.[Plan] As [Plan],    

--									cast ('' as varchar) Cover,      

--								   ltrim( Dependencies.[Full Name]) As [Name] ,      

--									ltrim(Dependencies.Surname) As [Surname] ,      

--									Dependencies.Gender As [Gender],      

--									Dependencies.IDNumber As [IDNo],      

--									  cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  

--									  right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  

--									right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  

--									 As [DateOfBirth],   

--			datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  

--			case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 

--				when Dependencies.DependencyType = ' ' then 'Not Specified' 

--				else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',   

--									Dependencies.Telephone As [TelNo],      

--									Dependencies.Cellphone As [CellNo],      

--									Dependencies.Address1,      

--									Dependencies.Address2,      

--									Dependencies.Address3,      

--									Dependencies.Address4,      

--									Dependencies.Code ,  

--			Members.PolicyStatus     

--			,Members.MemberBranch
--			,AdditionalMemberInfo.EasyPayNo
--									FROM Members

--									left JOIN Dependencies  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]      

--									left outer JOIN Plans ON Plans.pkiPlanID= Members.fkiPlanId 
--									left outer JOIN AdditionalMemberInfo ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
--	where 
--	Members.parlourid=@parlourid and
--	Members.active=1  
--	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
--	--AND PolicyStatus like '%'+@PolicySatus+'%'
--	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
----AND MemberBranch like '%'+isnull(@branch,'')+'%'
----AND (@MemberPlan is null or Dependencies.[Plan] like '%'+isnull(@MemberPlan,'')+'%')
--	AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
--	AND		(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
-- AND		(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
----	AND Members.CreateDate between isnull(@StartDate,cast('1753-1-1' as datetime)) AND isnull(@EndDate,cast('2099-1-1' as datetime)) 
--	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
--	order by 1,11, 4 desc 
									END
END
ELSE
BEGIN

Select * From (
SELECT  Members.MemeberNumber  As [MemeberNo],  
Plans.PlanSubscription,
Members.MemberSociety  As [Society],      
Plans.PlanName As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Members.[Full Names]) As [Name] ,      
ltrim(Members.Surname) As [Surname] ,      
Members.Gender As [Gender],      
Members.AccountNumber As [IDNo],      
cast(datepart(year,(Members.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Members.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Members.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Members.[Date Of Birth],getdate())/365  'Age',  
'Main Member' as 'Type',   
0 as MainOrder,
Members.Telephone As [TelNo],      
Members.Cellphone As [CellNo],      
Members.Address1,      
Members.Address2,      
Members.Address3,      
Members.Address4,      
Members.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
Right outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]
where 
		Members.parlourid=@parlourid and
		Members.active=1  
		AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
		AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
		--AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
		AND MemberBranch like '%'+isnull(@branch,'')+'%'
		AND	(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
		AND	(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
		AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
UNION ALL
SELECT  Members.MemeberNumber  As [MemeberNo],  
Plans.PlanSubscription,
Members.MemberSociety  As [Society],      
Dependencies.[Plan] As [Plan],    
cast ('' as varchar) Cover,      
ltrim( Dependencies.[Full Name]) As [Name] ,      
ltrim(Dependencies.Surname) As [Surname] ,      
Dependencies.Gender As [Gender],      
Dependencies.IDNumber As [IDNo],      
cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  
right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  
right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  
As [DateOfBirth],   
datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  
case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 
when Dependencies.DependencyType = ' ' then 'Not Specified' 
else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',  
1 as MainOrder,
Dependencies.Telephone As [TelNo],      
Dependencies.Cellphone As [CellNo],      
Dependencies.Address1,      
Dependencies.Address2,      
Dependencies.Address3,      
Dependencies.Address4,      
Dependencies.Code ,  
Members.PolicyStatus     
,Members.MemberBranch
,AdditionalMemberInfo.EasyPayNo
FROM Members with (nolock)
INNER JOIN Dependencies with (nolock)  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]      
Right outer JOIN Plans with (nolock) ON Plans.pkiPlanID= Members.fkiPlanId 
left outer JOIN AdditionalMemberInfo with (nolock) ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
where 
		Members.parlourid=@parlourid and
		Members.active=1  
		--AND PolicyStatus like '%'+@PolicySatus+'%'
		AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
		AND MemberBranch like '%'+isnull(@branch,'')+'%'
	--	AND (@MemberPlan is null or Dependencies.[Plan] like '%'+isnull(@MemberPlan,'')+'%')
		AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
		AND		(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
 AND		(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
		--AND Members.CreateDate between isnull(@StartDate,cast('1753-1-1' as datetime)) AND isnull(@EndDate,cast('2099-1-1' as datetime)) 
		AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
) AS Table1  Order By [MemeberNo],MainOrder
	--								SELECT  Members.MemeberNumber  As [MemeberNo],  
	--									Plans.PlanSubscription,
	--									--Plans.PlanUnderwriter,
	--									Members.MemberSociety  As [Society],      

	--									Dependencies.[Plan] As [Plan],    

	--									cast ('' as varchar) Cover,      

	--								   ltrim( Dependencies.[Full Name]) As [Name] ,      

	--									ltrim(Dependencies.Surname) As [Surname] ,      

	--									Dependencies.Gender As [Gender],      

	--									Dependencies.IDNumber As [IDNo],      

	--									  cast(datepart(year,(Dependencies.[Date Of Birth])) as varchar)+ '-' +  

	--									  right('0'+ cast(datepart(month,(Dependencies.[Date Of Birth])) as varchar),2)+ '-' +  

	--									right('0'+ cast(datepart(day,(Dependencies.[Date Of Birth])) as varchar),2)  

	--									 As [DateOfBirth],   

	--			datediff(day,Dependencies.[Date Of Birth],getdate())/365  'Age',  

	--			case when Dependencies.DependencyType = 'Spouse' then 'A Spouse' 

	--				when Dependencies.DependencyType = ' ' then 'Not Specified' 

	--				else coalesce(Dependencies.DependencyType,'Not Specified') end 'Type',   

	--									Dependencies.Telephone As [TelNo],      

	--									Dependencies.Cellphone As [CellNo],      

	--									Dependencies.Address1,      

	--									Dependencies.Address2,      

	--									Dependencies.Address3,      

	--									Dependencies.Address4,      

	--									Dependencies.Code ,  

	--			Members.PolicyStatus     

	--			,Members.MemberBranch
	--			,AdditionalMemberInfo.EasyPayNo
	--									FROM Members

	--								left JOIN Dependencies  ON Members.[pkiMemberID] = Dependencies.[fkiMemberID]      

	--								left outer JOIN Plans ON Plans.pkiPlanID= Members.fkiPlanId 
	--									left outer JOIN AdditionalMemberInfo ON Members.[pkiMemberID] = AdditionalMemberInfo.[fkiMemberID]  
	--	where 
	--	Members.parlourid=@parlourid and
	--	Members.active=1  
	--	--AND PolicyStatus like '%'+@PolicySatus+'%'
	--	AND (@PolicyStatus='All' OR PolicyStatus like '%'+isnull( @PolicyStatus,'')+'%')
	--AND (@Underwriterplan='All' OR Plans.PlanUnderwriter like '%'+isnull(@Underwriterplan,'')+'%')
	----	AND MemberBranch like '%'+isnull(@branch,'')+'%'
	----	AND (@MemberPlan is null or Dependencies.[Plan] like '%'+isnull(@MemberPlan,'')+'%')
	--	AND ((@MemberPlan = 'ALL' OR @MemberPlan is null ) OR Dependencies.[Plan] like '%'+@MemberPlan+'%')
	--	AND		(@StartDate IS NULL OR Members.CreateDate >= @StartDate)
 --AND		(@EndDate IS NULL OR  Members.CreateDate <= @EndDate)
	--	--AND Members.CreateDate between isnull(@StartDate,cast('1753-1-1' as datetime)) AND isnull(@EndDate,cast('2099-1-1' as datetime)) 
	--	AND (@PolicyStatusList='All' OR PolicyStatus IN (select * from dbo.fnSplitString( @PolicyStatusList , ',' ) ))
	--	order by 1,11, 4 desc 
END
Go