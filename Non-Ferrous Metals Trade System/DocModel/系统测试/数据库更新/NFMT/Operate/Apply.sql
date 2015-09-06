alter table dbo.Apply
   drop constraint PK_APPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Apply')
            and   type = 'U')
   drop table dbo.Apply
go

/*==============================================================*/
/* Table: Apply                                                 */
/*==============================================================*/
create table dbo.Apply (
   ApplyId              int                  identity,
   ApplyNo              varchar(20)          null,
   ApplyType            int                  null,
   EmpId                int                  null,
   ApplyTime            datetime             null,
   ApplyStatus          int                  null,
   ApplyDept            int                  null,
   ApplyCorp            int                  null,
   ApplyDesc            varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '申请',
   'user', 'dbo', 'table', 'Apply'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请序号',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请编号',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请类型',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyType'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请人',
   'user', 'dbo', 'table', 'Apply', 'column', 'EmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请时间',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请状态',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请部门',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请公司',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', 'dbo', 'table', 'Apply', 'column', 'ApplyDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Apply', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Apply', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Apply', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Apply', 'column', 'LastModifyTime'
go

alter table dbo.Apply
   add constraint PK_APPLY primary key (ApplyId)
go


/****** Object:  Stored Procedure [dbo].ApplyGet    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyGet]
GO

/****** Object:  Stored Procedure [dbo].ApplyLoad    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].ApplyInsert    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].ApplyUpdate    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].ApplyUpdateStatus    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ApplyUpdateStatus    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ApplyUpdateStatus
// 存储过程功能描述：更新Apply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Apply'

set @str = 'update [dbo].[Apply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ApplyGoBack
// 存储过程功能描述：撤返Apply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Apply'

set @str = 'update [dbo].[Apply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ApplyGet
// 存储过程功能描述：查询指定Apply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyGet
    /*
	@ApplyId int
    */
    @id int
AS

SELECT
	[ApplyId],
	[ApplyNo],
	[ApplyType],
	[EmpId],
	[ApplyTime],
	[ApplyStatus],
	[ApplyDept],
	[ApplyCorp],
	[ApplyDesc],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Apply]
WHERE
	[ApplyId] = @id

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
// 存储过程名：[dbo].ApplyLoad
// 存储过程功能描述：查询所有Apply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyLoad
AS

SELECT
	[ApplyId],
	[ApplyNo],
	[ApplyType],
	[EmpId],
	[ApplyTime],
	[ApplyStatus],
	[ApplyDept],
	[ApplyCorp],
	[ApplyDesc],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Apply]

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
// 存储过程名：[dbo].ApplyInsert
// 存储过程功能描述：新增一条Apply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyInsert
	@ApplyNo varchar(20) =NULL ,
	@ApplyType int =NULL ,
	@EmpId int =NULL ,
	@ApplyTime datetime =NULL ,
	@ApplyStatus int =NULL ,
	@ApplyDept int =NULL ,
	@ApplyCorp int =NULL ,
	@ApplyDesc varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@ApplyId int OUTPUT
AS

INSERT INTO [dbo].[Apply] (
	[ApplyNo],
	[ApplyType],
	[EmpId],
	[ApplyTime],
	[ApplyStatus],
	[ApplyDept],
	[ApplyCorp],
	[ApplyDesc],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyNo,
	@ApplyType,
	@EmpId,
	@ApplyTime,
	@ApplyStatus,
	@ApplyDept,
	@ApplyCorp,
	@ApplyDesc,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ApplyId = @@IDENTITY
exec CreateApplyNo @applyType=@ApplyType,@identity =@ApplyId
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
// 存储过程名：[dbo].ApplyUpdate
// 存储过程功能描述：更新Apply
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ApplyUpdate
    @ApplyId int,
@ApplyNo varchar(20) = NULL,
@ApplyType int = NULL,
@EmpId int = NULL,
@ApplyTime datetime = NULL,
@ApplyStatus int = NULL,
@ApplyDept int = NULL,
@ApplyCorp int = NULL,
@ApplyDesc varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Apply] SET
	[ApplyNo] = @ApplyNo,
	[ApplyType] = @ApplyType,
	[EmpId] = @EmpId,
	[ApplyTime] = @ApplyTime,
	[ApplyStatus] = @ApplyStatus,
	[ApplyDept] = @ApplyDept,
	[ApplyCorp] = @ApplyCorp,
	[ApplyDesc] = @ApplyDesc,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ApplyId] = @ApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



