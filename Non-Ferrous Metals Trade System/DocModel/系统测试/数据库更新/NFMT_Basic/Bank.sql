alter table Bank
   drop constraint PK_BANK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Bank')
            and   type = 'U')
   drop table Bank
go

/*==============================================================*/
/* Table: Bank                                                  */
/*==============================================================*/
create table Bank (
   BankId               int                  identity,
   BankName             varchar(50)          null,
   BankEname            varchar(50)          null,
   BankFullName         varchar(100)         null,
   BankShort            varchar(20)          null,
   CapitalType          int                  null,
   BankLevel            int                  null,
   ParentId             int                  null,
   BankStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '银行',
   'user', @CurrentUser, 'table', 'Bank'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行序号',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行名称',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行英文名称',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankEname'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行全称',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankFullName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行缩写',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankShort'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资本类型',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'CapitalType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行级别',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上级银行序号',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'ParentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行状态',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'BankStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Bank', 'column', 'LastModifyTime'
go

alter table Bank
   add constraint PK_BANK primary key (BankId)
go


/****** Object:  Stored Procedure [dbo].BankGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankGet]
GO

/****** Object:  Stored Procedure [dbo].BankLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankLoad]
GO

/****** Object:  Stored Procedure [dbo].BankInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankInsert]
GO

/****** Object:  Stored Procedure [dbo].BankUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankUpdate]
GO

/****** Object:  Stored Procedure [dbo].BankUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BankUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankUpdateStatus
// 存储过程功能描述：更新Bank中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Bank'

set @str = 'update [dbo].[Bank] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BankId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BankGoBack
// 存储过程功能描述：撤返Bank，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Bank'

set @str = 'update [dbo].[Bank] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BankId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BankGet
// 存储过程功能描述：查询指定Bank的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankGet
    /*
	@BankId int
    */
    @id int
AS

SELECT
	[BankId],
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bank]
WHERE
	[BankId] = @id

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
// 存储过程名：[dbo].BankLoad
// 存储过程功能描述：查询所有Bank记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankLoad
AS

SELECT
	[BankId],
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bank]

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
// 存储过程名：[dbo].BankInsert
// 存储过程功能描述：新增一条Bank记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankInsert
	@BankName varchar(50) ,
	@BankEname varchar(50) ,
	@BankFullName varchar(100) =NULL ,
	@BankShort varchar(20) =NULL ,
	@CapitalType int =NULL ,
	@BankLevel int =NULL ,
	@ParentId int =NULL ,
	@BankStatus int ,
	@CreatorId int ,
	@BankId int OUTPUT
AS

INSERT INTO [dbo].[Bank] (
	[BankName],
	[BankEname],
	[BankFullName],
	[BankShort],
	[CapitalType],
	[BankLevel],
	[ParentId],
	[BankStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BankName,
	@BankEname,
	@BankFullName,
	@BankShort,
	@CapitalType,
	@BankLevel,
	@ParentId,
	@BankStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BankId = @@IDENTITY

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
// 存储过程名：[dbo].BankUpdate
// 存储过程功能描述：更新Bank
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankUpdate
    @BankId int,
@BankName varchar(50),
@BankEname varchar(50),
@BankFullName varchar(100) = NULL,
@BankShort varchar(20) = NULL,
@CapitalType int = NULL,
@BankLevel int = NULL,
@ParentId int = NULL,
@BankStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Bank] SET
	[BankName] = @BankName,
	[BankEname] = @BankEname,
	[BankFullName] = @BankFullName,
	[BankShort] = @BankShort,
	[CapitalType] = @CapitalType,
	[BankLevel] = @BankLevel,
	[ParentId] = @ParentId,
	[BankStatus] = @BankStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BankId] = @BankId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



