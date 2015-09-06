alter table dbo.St_StockOutAttach
   drop constraint PK_ST_STOCKOUTATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockOutAttach')
            and   type = 'U')
   drop table dbo.St_StockOutAttach
go

/*==============================================================*/
/* Table: St_StockOutAttach                                     */
/*==============================================================*/
create table dbo.St_StockOutAttach (
   StockOutAttachId     int                  identity,
   StockOutId           int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出库申请附件',
   'user', 'dbo', 'table', 'St_StockOutAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库申请附件序号',
   'user', 'dbo', 'table', 'St_StockOutAttach', 'column', 'StockOutAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库序号',
   'user', 'dbo', 'table', 'St_StockOutAttach', 'column', 'StockOutId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'St_StockOutAttach', 'column', 'AttachId'
go

alter table dbo.St_StockOutAttach
   add constraint PK_ST_STOCKOUTATTACH primary key (StockOutAttachId)
go


/****** Object:  Stored Procedure [dbo].St_StockOutAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockOutAttachUpdateStatus
// 存储过程功能描述：更新St_StockOutAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutAttach'

set @str = 'update [dbo].[St_StockOutAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockOutAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutAttachGoBack
// 存储过程功能描述：撤返St_StockOutAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutAttach'

set @str = 'update [dbo].[St_StockOutAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockOutAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutAttachGet
// 存储过程功能描述：查询指定St_StockOutAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachGet
    /*
	@StockOutAttachId int
    */
    @id int
AS

SELECT
	[StockOutAttachId],
	[StockOutId],
	[AttachId]
FROM
	[dbo].[St_StockOutAttach]
WHERE
	[StockOutAttachId] = @id

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
// 存储过程名：[dbo].St_StockOutAttachLoad
// 存储过程功能描述：查询所有St_StockOutAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachLoad
AS

SELECT
	[StockOutAttachId],
	[StockOutId],
	[AttachId]
FROM
	[dbo].[St_StockOutAttach]

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
// 存储过程名：[dbo].St_StockOutAttachInsert
// 存储过程功能描述：新增一条St_StockOutAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachInsert
	@StockOutId int =NULL ,
	@AttachId int =NULL ,
	@StockOutAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_StockOutAttach] (
	[StockOutId],
	[AttachId]
) VALUES (
	@StockOutId,
	@AttachId
)


SET @StockOutAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockOutAttachUpdate
// 存储过程功能描述：更新St_StockOutAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutAttachUpdate
    @StockOutAttachId int,
@StockOutId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_StockOutAttach] SET
	[StockOutId] = @StockOutId,
	[AttachId] = @AttachId
WHERE
	[StockOutAttachId] = @StockOutAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



