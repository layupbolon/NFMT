alter table dbo.Con_LcAttach
   drop constraint PK_CON_LCATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_LcAttach')
            and   type = 'U')
   drop table dbo.Con_LcAttach
go

/*==============================================================*/
/* Table: Con_LcAttach                                          */
/*==============================================================*/
create table dbo.Con_LcAttach (
   LcAttachId           int                  identity,
   SubId                int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '信用证附件',
   'user', 'dbo', 'table', 'Con_LcAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证附件序号',
   'user', 'dbo', 'table', 'Con_LcAttach', 'column', 'LcAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Con_LcAttach', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Con_LcAttach', 'column', 'AttachId'
go

alter table dbo.Con_LcAttach
   add constraint PK_CON_LCATTACH primary key (LcAttachId)
go


/****** Object:  Stored Procedure [dbo].Con_LcAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Con_LcAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_LcAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_LcAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_LcAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_LcAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_LcAttachUpdateStatus
// 存储过程功能描述：更新Con_LcAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_LcAttach'

set @str = 'update [dbo].[Con_LcAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where LcAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_LcAttachGoBack
// 存储过程功能描述：撤返Con_LcAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_LcAttach'

set @str = 'update [dbo].[Con_LcAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where LcAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_LcAttachGet
// 存储过程功能描述：查询指定Con_LcAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachGet
    /*
	@LcAttachId int
    */
    @id int
AS

SELECT
	[LcAttachId],
	[SubId],
	[AttachId]
FROM
	[dbo].[Con_LcAttach]
WHERE
	[LcAttachId] = @id

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
// 存储过程名：[dbo].Con_LcAttachLoad
// 存储过程功能描述：查询所有Con_LcAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachLoad
AS

SELECT
	[LcAttachId],
	[SubId],
	[AttachId]
FROM
	[dbo].[Con_LcAttach]

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
// 存储过程名：[dbo].Con_LcAttachInsert
// 存储过程功能描述：新增一条Con_LcAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachInsert
	@SubId int =NULL ,
	@AttachId int =NULL ,
	@LcAttachId int OUTPUT
AS

INSERT INTO [dbo].[Con_LcAttach] (
	[SubId],
	[AttachId]
) VALUES (
	@SubId,
	@AttachId
)


SET @LcAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Con_LcAttachUpdate
// 存储过程功能描述：更新Con_LcAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcAttachUpdate
    @LcAttachId int,
@SubId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Con_LcAttach] SET
	[SubId] = @SubId,
	[AttachId] = @AttachId
WHERE
	[LcAttachId] = @LcAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



