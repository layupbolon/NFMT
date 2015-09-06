alter table St_CustomsAttach
   drop constraint PK_ST_CUSTOMSATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsAttach')
            and   type = 'U')
   drop table St_CustomsAttach
go

/*==============================================================*/
/* Table: St_CustomsAttach                                      */
/*==============================================================*/
create table St_CustomsAttach (
   CustomsAttachId      int                  identity,
   CustomsId            int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关附件',
   'user', @CurrentUser, 'table', 'St_CustomsAttach'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关附件序号',
   'user', @CurrentUser, 'table', 'St_CustomsAttach', 'column', 'CustomsAttachId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsAttach', 'column', 'CustomsId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_CustomsAttach', 'column', 'AttachId'
go

alter table St_CustomsAttach
   add constraint PK_ST_CUSTOMSATTACH primary key (CustomsAttachId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsAttachUpdateStatus
// 存储过程功能描述：更新St_CustomsAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsAttach'

set @str = 'update [dbo].[St_CustomsAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CustomsAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsAttachGoBack
// 存储过程功能描述：撤返St_CustomsAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsAttach'

set @str = 'update [dbo].[St_CustomsAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CustomsAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsAttachGet
// 存储过程功能描述：查询指定St_CustomsAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachGet
    /*
	@CustomsAttachId int
    */
    @id int
AS

SELECT
	[CustomsAttachId],
	[CustomsId],
	[AttachId]
FROM
	[dbo].[St_CustomsAttach]
WHERE
	[CustomsAttachId] = @id

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
// 存储过程名：[dbo].St_CustomsAttachLoad
// 存储过程功能描述：查询所有St_CustomsAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachLoad
AS

SELECT
	[CustomsAttachId],
	[CustomsId],
	[AttachId]
FROM
	[dbo].[St_CustomsAttach]

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
// 存储过程名：[dbo].St_CustomsAttachInsert
// 存储过程功能描述：新增一条St_CustomsAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachInsert
	@CustomsId int =NULL ,
	@AttachId int =NULL ,
	@CustomsAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsAttach] (
	[CustomsId],
	[AttachId]
) VALUES (
	@CustomsId,
	@AttachId
)


SET @CustomsAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_CustomsAttachUpdate
// 存储过程功能描述：更新St_CustomsAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsAttachUpdate
    @CustomsAttachId int,
@CustomsId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_CustomsAttach] SET
	[CustomsId] = @CustomsId,
	[AttachId] = @AttachId
WHERE
	[CustomsAttachId] = @CustomsAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



