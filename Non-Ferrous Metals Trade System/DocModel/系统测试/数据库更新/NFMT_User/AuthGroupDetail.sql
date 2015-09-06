alter table AuthGroupDetail
   drop constraint PK_AUTHGROUPDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AuthGroupDetail')
            and   type = 'U')
   drop table AuthGroupDetail
go

/*==============================================================*/
/* Table: AuthGroupDetail                                       */
/*==============================================================*/
create table AuthGroupDetail (
   DetailId             int                  identity,
   AuthGroupId          int                  null,
   EmpId                int                  null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '员工权限组关联表',
   'user', @CurrentUser, 'table', 'AuthGroupDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限组序号',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'AuthGroupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '员工序号',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'AuthGroupDetail', 'column', 'LastModifyTime'
go

alter table AuthGroupDetail
   add constraint PK_AUTHGROUPDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].AuthGroupDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailGet]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailUpdateStatus
// 存储过程功能描述：更新AuthGroupDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthGroupDetail'

set @str = 'update [dbo].[AuthGroupDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthGroupDetailGoBack
// 存储过程功能描述：撤返AuthGroupDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthGroupDetail'

set @str = 'update [dbo].[AuthGroupDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthGroupDetailGet
// 存储过程功能描述：查询指定AuthGroupDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroupDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].AuthGroupDetailLoad
// 存储过程功能描述：查询所有AuthGroupDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailLoad
AS

SELECT
	[DetailId],
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroupDetail]

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
// 存储过程名：[dbo].AuthGroupDetailInsert
// 存储过程功能描述：新增一条AuthGroupDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailInsert
	@AuthGroupId int =NULL ,
	@EmpId int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[AuthGroupDetail] (
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuthGroupId,
	@EmpId,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].AuthGroupDetailUpdate
// 存储过程功能描述：更新AuthGroupDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupDetailUpdate
    @DetailId int,
@AuthGroupId int = NULL,
@EmpId int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthGroupDetail] SET
	[AuthGroupId] = @AuthGroupId,
	[EmpId] = @EmpId,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



