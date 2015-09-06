alter table dbo.Fun_FundsLogAttach
   drop constraint PK_FUN_FUNDSLOGATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_FundsLogAttach')
            and   type = 'U')
   drop table dbo.Fun_FundsLogAttach
go

/*==============================================================*/
/* Table: Fun_FundsLogAttach                                    */
/*==============================================================*/
create table dbo.Fun_FundsLogAttach (
   FundsLogAttachId     int                  identity,
   AttachId             int                  null,
   FundsLogId           int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '资金流水附件',
   'user', 'dbo', 'table', 'Fun_FundsLogAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金流水附件序号',
   'user', 'dbo', 'table', 'Fun_FundsLogAttach', 'column', 'FundsLogAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Fun_FundsLogAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', 'dbo', 'table', 'Fun_FundsLogAttach', 'column', 'FundsLogId'
go

alter table dbo.Fun_FundsLogAttach
   add constraint PK_FUN_FUNDSLOGATTACH primary key (FundsLogAttachId)
go


/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_FundsLogAttachUpdateStatus
// 存储过程功能描述：更新Fun_FundsLogAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_FundsLogAttach'

set @str = 'update [dbo].[Fun_FundsLogAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FundsLogAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsLogAttachGoBack
// 存储过程功能描述：撤返Fun_FundsLogAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_FundsLogAttach'

set @str = 'update [dbo].[Fun_FundsLogAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FundsLogAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsLogAttachGet
// 存储过程功能描述：查询指定Fun_FundsLogAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachGet
    /*
	@FundsLogAttachId int
    */
    @id int
AS

SELECT
	[FundsLogAttachId],
	[AttachId],
	[FundsLogId]
FROM
	[dbo].[Fun_FundsLogAttach]
WHERE
	[FundsLogAttachId] = @id

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
// 存储过程名：[dbo].Fun_FundsLogAttachLoad
// 存储过程功能描述：查询所有Fun_FundsLogAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachLoad
AS

SELECT
	[FundsLogAttachId],
	[AttachId],
	[FundsLogId]
FROM
	[dbo].[Fun_FundsLogAttach]

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
// 存储过程名：[dbo].Fun_FundsLogAttachInsert
// 存储过程功能描述：新增一条Fun_FundsLogAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachInsert
	@AttachId int =NULL ,
	@FundsLogId int =NULL ,
	@FundsLogAttachId int OUTPUT
AS

INSERT INTO [dbo].[Fun_FundsLogAttach] (
	[AttachId],
	[FundsLogId]
) VALUES (
	@AttachId,
	@FundsLogId
)


SET @FundsLogAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_FundsLogAttachUpdate
// 存储过程功能描述：更新Fun_FundsLogAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogAttachUpdate
    @FundsLogAttachId int,
@AttachId int = NULL,
@FundsLogId int = NULL
AS

UPDATE [dbo].[Fun_FundsLogAttach] SET
	[AttachId] = @AttachId,
	[FundsLogId] = @FundsLogId
WHERE
	[FundsLogAttachId] = @FundsLogAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



