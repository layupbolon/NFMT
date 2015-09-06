alter table Pri_PricingPerson
   drop constraint PK_PRI_PRICINGPERSON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PricingPerson')
            and   type = 'U')
   drop table Pri_PricingPerson
go

/*==============================================================*/
/* Table: Pri_PricingPerson                                     */
/*==============================================================*/
create table Pri_PricingPerson (
   PersoinId            int                  identity,
   BlocId               int                  null,
   CorpId               int                  null,
   PricingName          varchar(50)          null,
   Job                  varchar(20)          null,
   PricingPhone         varchar(20)          null,
   Phone2               varchar(20)          null,
   PersoinStatus        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '点价权限人',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限人序号',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'PersoinId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '归属集团',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'BlocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '归属公司',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'CorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '姓名',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'PricingName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '职位',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'Job'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价电话',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'PricingPhone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价电话2',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'Phone2'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限人状态',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'PersoinStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PricingPerson', 'column', 'LastModifyTime'
go

alter table Pri_PricingPerson
   add constraint PK_PRI_PRICINGPERSON primary key (PersoinId)
go

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingPersonUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingPersonGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingPersonGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonUpdateStatus
// 存储过程功能描述：更新Pri_PricingPerson中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingPerson'

set @str = 'update [dbo].[Pri_PricingPerson] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PersoinId = '+ Convert(varchar,@id) 
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonGoBack
// 存储过程功能描述：撤返Pri_PricingPerson，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingPerson'

set @str = 'update [dbo].[Pri_PricingPerson] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PersoinId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO









SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonGet
// 存储过程功能描述：查询指定Pri_PricingPerson的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonGet
    /*
	@PersoinId int
    */
    @id int
AS

SELECT
	[PersoinId],
	[BlocId],
	[CorpId],
	[PricingName],
	[Job],
	[PricingPhone],
	[Phone2],
	[PersoinStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingPerson]
WHERE
	[PersoinId] = @id

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonLoad
// 存储过程功能描述：查询所有Pri_PricingPerson记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonLoad
AS

SELECT
	[PersoinId],
	[BlocId],
	[CorpId],
	[PricingName],
	[Job],
	[PricingPhone],
	[Phone2],
	[PersoinStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingPerson]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO







SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonInsert
// 存储过程功能描述：新增一条Pri_PricingPerson记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonInsert
	@BlocId int =NULL ,
	@CorpId int =NULL ,
	@PricingName varchar(50) =NULL ,
	@Job varchar(20) =NULL ,
	@PricingPhone varchar(20) =NULL ,
	@Phone2 varchar(20) =NULL ,
	@PersoinStatus int =NULL ,
	@CreatorId int =NULL ,
	@PersoinId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PricingPerson] (
	[BlocId],
	[CorpId],
	[PricingName],
	[Job],
	[PricingPhone],
	[Phone2],
	[PersoinStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BlocId,
	@CorpId,
	@PricingName,
	@Job,
	@PricingPhone,
	@Phone2,
	@PersoinStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PersoinId = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingPersonUpdate
// 存储过程功能描述：更新Pri_PricingPerson
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingPersonUpdate
    @PersoinId int,
@BlocId int = NULL,
@CorpId int = NULL,
@PricingName varchar(50) = NULL,
@Job varchar(20) = NULL,
@PricingPhone varchar(20) = NULL,
@Phone2 varchar(20) = NULL,
@PersoinStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PricingPerson] SET
	[BlocId] = @BlocId,
	[CorpId] = @CorpId,
	[PricingName] = @PricingName,
	[Job] = @Job,
	[PricingPhone] = @PricingPhone,
	[Phone2] = @Phone2,
	[PersoinStatus] = @PersoinStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PersoinId] = @PersoinId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



