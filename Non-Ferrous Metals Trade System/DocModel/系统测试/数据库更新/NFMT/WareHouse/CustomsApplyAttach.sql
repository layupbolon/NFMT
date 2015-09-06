alter table St_CustomsApplyAttach
   drop constraint PK_ST_CUSTOMSAPPLYATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsApplyAttach')
            and   type = 'U')
   drop table St_CustomsApplyAttach
go

/*==============================================================*/
/* Table: St_CustomsApplyAttach                                 */
/*==============================================================*/
create table St_CustomsApplyAttach (
   CustomsApplyAttachId int                  identity,
   CustomsApplyId       int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关申请附件',
   'user', @CurrentUser, 'table', 'St_CustomsApplyAttach'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请附件序号',
   'user', @CurrentUser, 'table', 'St_CustomsApplyAttach', 'column', 'CustomsApplyAttachId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsApplyAttach', 'column', 'CustomsApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_CustomsApplyAttach', 'column', 'AttachId'
go

alter table St_CustomsApplyAttach
   add constraint PK_ST_CUSTOMSAPPLYATTACH primary key (CustomsApplyAttachId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsApplyAttachUpdateStatus
// 存储过程功能描述：更新St_CustomsApplyAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsApplyAttach'

set @str = 'update [dbo].[St_CustomsApplyAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CustomsApplyAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsApplyAttachGoBack
// 存储过程功能描述：撤返St_CustomsApplyAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsApplyAttach'

set @str = 'update [dbo].[St_CustomsApplyAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CustomsApplyAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsApplyAttachGet
// 存储过程功能描述：查询指定St_CustomsApplyAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachGet
    /*
	@CustomsApplyAttachId int
    */
    @id int
AS

SELECT
	[CustomsApplyAttachId],
	[CustomsApplyId],
	[AttachId]
FROM
	[dbo].[St_CustomsApplyAttach]
WHERE
	[CustomsApplyAttachId] = @id

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
// 存储过程名：[dbo].St_CustomsApplyAttachLoad
// 存储过程功能描述：查询所有St_CustomsApplyAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachLoad
AS

SELECT
	[CustomsApplyAttachId],
	[CustomsApplyId],
	[AttachId]
FROM
	[dbo].[St_CustomsApplyAttach]

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
// 存储过程名：[dbo].St_CustomsApplyAttachInsert
// 存储过程功能描述：新增一条St_CustomsApplyAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachInsert
	@CustomsApplyId int =NULL ,
	@AttachId int =NULL ,
	@CustomsApplyAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsApplyAttach] (
	[CustomsApplyId],
	[AttachId]
) VALUES (
	@CustomsApplyId,
	@AttachId
)


SET @CustomsApplyAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_CustomsApplyAttachUpdate
// 存储过程功能描述：更新St_CustomsApplyAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyAttachUpdate
    @CustomsApplyAttachId int,
@CustomsApplyId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_CustomsApplyAttach] SET
	[CustomsApplyId] = @CustomsApplyId,
	[AttachId] = @AttachId
WHERE
	[CustomsApplyAttachId] = @CustomsApplyAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



