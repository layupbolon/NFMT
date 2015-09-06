alter table dbo.Fun_CashInAttach
   drop constraint PK_FUN_CASHINATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_CashInAttach')
            and   type = 'U')
   drop table dbo.Fun_CashInAttach
go

/*==============================================================*/
/* Table: Fun_CashInAttach                                      */
/*==============================================================*/
create table dbo.Fun_CashInAttach (
   CashInAttachId       int                  identity,
   AttachId             int                  null,
   CashInId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '收款附件',
   'user', 'dbo', 'table', 'Fun_CashInAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款登记附件序号',
   'user', 'dbo', 'table', 'Fun_CashInAttach', 'column', 'CashInAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Fun_CashInAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款登记序号',
   'user', 'dbo', 'table', 'Fun_CashInAttach', 'column', 'CashInId'
go

alter table dbo.Fun_CashInAttach
   add constraint PK_FUN_CASHINATTACH primary key (CashInAttachId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInAttachUpdateStatus
// 存储过程功能描述：更新Fun_CashInAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInAttach'

set @str = 'update [dbo].[Fun_CashInAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CashInAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInAttachGoBack
// 存储过程功能描述：撤返Fun_CashInAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInAttach'

set @str = 'update [dbo].[Fun_CashInAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CashInAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInAttachGet
// 存储过程功能描述：查询指定Fun_CashInAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachGet
    /*
	@CashInAttachId int
    */
    @id int
AS

SELECT
	[CashInAttachId],
	[AttachId],
	[CashInId]
FROM
	[dbo].[Fun_CashInAttach]
WHERE
	[CashInAttachId] = @id

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
// 存储过程名：[dbo].Fun_CashInAttachLoad
// 存储过程功能描述：查询所有Fun_CashInAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachLoad
AS

SELECT
	[CashInAttachId],
	[AttachId],
	[CashInId]
FROM
	[dbo].[Fun_CashInAttach]

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
// 存储过程名：[dbo].Fun_CashInAttachInsert
// 存储过程功能描述：新增一条Fun_CashInAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachInsert
	@AttachId int =NULL ,
	@CashInId int =NULL ,
	@CashInAttachId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInAttach] (
	[AttachId],
	[CashInId]
) VALUES (
	@AttachId,
	@CashInId
)


SET @CashInAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_CashInAttachUpdate
// 存储过程功能描述：更新Fun_CashInAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAttachUpdate
    @CashInAttachId int,
@AttachId int = NULL,
@CashInId int = NULL
AS

UPDATE [dbo].[Fun_CashInAttach] SET
	[AttachId] = @AttachId,
	[CashInId] = @CashInId
WHERE
	[CashInAttachId] = @CashInAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



