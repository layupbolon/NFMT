alter table dbo.St_StockInAttach
   drop constraint PK_ST_STOCKINATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockInAttach')
            and   type = 'U')
   drop table dbo.St_StockInAttach
go

/*==============================================================*/
/* Table: St_StockInAttach                                      */
/*==============================================================*/
create table dbo.St_StockInAttach (
   StockInAttachId      int                  identity,
   StockInId            int                  null,
   AttachId             int                  null,
   AttachType           int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '入库登记附件',
   'user', 'dbo', 'table', 'St_StockInAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '入库登记附件序号',
   'user', 'dbo', 'table', 'St_StockInAttach', 'column', 'StockInAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '入库登记序号',
   'user', 'dbo', 'table', 'St_StockInAttach', 'column', 'StockInId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'St_StockInAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件类型',
   'user', 'dbo', 'table', 'St_StockInAttach', 'column', 'AttachType'
go

alter table dbo.St_StockInAttach
   add constraint PK_ST_STOCKINATTACH primary key (StockInAttachId)
go


/****** Object:  Stored Procedure [dbo].St_StockInAttachGet    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockInAttachLoad    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockInAttachInsert    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockInAttachUpdate    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockInAttachUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockInAttachUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockInAttachUpdateStatus
// 存储过程功能描述：更新St_StockInAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockInAttach'

set @str = 'update [dbo].[St_StockInAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockInAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockInAttachGoBack
// 存储过程功能描述：撤返St_StockInAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockInAttach'

set @str = 'update [dbo].[St_StockInAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockInAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockInAttachGet
// 存储过程功能描述：查询指定St_StockInAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachGet
    /*
	@StockInAttachId int
    */
    @id int
AS

SELECT
	[StockInAttachId],
	[StockInId],
	[AttachId],
	[AttachType]
FROM
	[dbo].[St_StockInAttach]
WHERE
	[StockInAttachId] = @id

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
// 存储过程名：[dbo].St_StockInAttachLoad
// 存储过程功能描述：查询所有St_StockInAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachLoad
AS

SELECT
	[StockInAttachId],
	[StockInId],
	[AttachId],
	[AttachType]
FROM
	[dbo].[St_StockInAttach]

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
// 存储过程名：[dbo].St_StockInAttachInsert
// 存储过程功能描述：新增一条St_StockInAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachInsert
	@StockInId int =NULL ,
	@AttachId int =NULL ,
	@AttachType int =NULL ,
	@StockInAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_StockInAttach] (
	[StockInId],
	[AttachId],
	[AttachType]
) VALUES (
	@StockInId,
	@AttachId,
	@AttachType
)


SET @StockInAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockInAttachUpdate
// 存储过程功能描述：更新St_StockInAttach
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInAttachUpdate
    @StockInAttachId int,
@StockInId int = NULL,
@AttachId int = NULL,
@AttachType int = NULL
AS

UPDATE [dbo].[St_StockInAttach] SET
	[StockInId] = @StockInId,
	[AttachId] = @AttachId,
	[AttachType] = @AttachType
WHERE
	[StockInAttachId] = @StockInAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



