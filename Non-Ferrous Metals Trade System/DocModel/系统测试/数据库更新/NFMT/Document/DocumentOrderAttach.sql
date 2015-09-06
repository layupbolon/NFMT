alter table dbo.Doc_DocumentOrderAttach
   drop constraint PK_DOC_DOCUMENTORDERATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentOrderAttach')
            and   type = 'U')
   drop table dbo.Doc_DocumentOrderAttach
go

/*==============================================================*/
/* Table: Doc_DocumentOrderAttach                               */
/*==============================================================*/
create table dbo.Doc_DocumentOrderAttach (
   OrderAttachId        int                  identity,
   OrderId              int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单指令附件',
   'user', 'dbo', 'table', 'Doc_DocumentOrderAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单指令附件序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderAttach', 'column', 'OrderAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单指令',
   'user', 'dbo', 'table', 'Doc_DocumentOrderAttach', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderAttach', 'column', 'AttachId'
go

alter table dbo.Doc_DocumentOrderAttach
   add constraint PK_DOC_DOCUMENTORDERATTACH primary key (OrderAttachId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderAttach'

set @str = 'update [dbo].[Doc_DocumentOrderAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OrderAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Doc_DocumentOrderAttachGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderAttach'

set @str = 'update [dbo].[Doc_DocumentOrderAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OrderAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Doc_DocumentOrderAttachGet
// 存储过程功能描述：查询指定Doc_DocumentOrderAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachGet
    /*
	@OrderAttachId int
    */
    @id int
AS

SELECT
	[OrderAttachId],
	[OrderId],
	[AttachId]
FROM
	[dbo].[Doc_DocumentOrderAttach]
WHERE
	[OrderAttachId] = @id

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
// 存储过程名：[dbo].Doc_DocumentOrderAttachLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachLoad
AS

SELECT
	[OrderAttachId],
	[OrderId],
	[AttachId]
FROM
	[dbo].[Doc_DocumentOrderAttach]

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
// 存储过程名：[dbo].Doc_DocumentOrderAttachInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachInsert
	@OrderId int =NULL ,
	@AttachId int =NULL ,
	@OrderAttachId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderAttach] (
	[OrderId],
	[AttachId]
) VALUES (
	@OrderId,
	@AttachId
)


SET @OrderAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Doc_DocumentOrderAttachUpdate
// 存储过程功能描述：更新Doc_DocumentOrderAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderAttachUpdate
    @OrderAttachId int,
@OrderId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderAttach] SET
	[OrderId] = @OrderId,
	[AttachId] = @AttachId
WHERE
	[OrderAttachId] = @OrderAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



