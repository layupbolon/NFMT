alter table dbo.St_PledgeAttach
   drop constraint PK_ST_PLEDGEATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_PledgeAttach')
            and   type = 'U')
   drop table dbo.St_PledgeAttach
go

/*==============================================================*/
/* Table: St_PledgeAttach                                       */
/*==============================================================*/
create table dbo.St_PledgeAttach (
   PledgeAttachId       int                  not null,
   PledgeId             int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '质押附件',
   'user', 'dbo', 'table', 'St_PledgeAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '质押附件序号',
   'user', 'dbo', 'table', 'St_PledgeAttach', 'column', 'PledgeAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '质押序号',
   'user', 'dbo', 'table', 'St_PledgeAttach', 'column', 'PledgeId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'St_PledgeAttach', 'column', 'AttachId'
go

alter table dbo.St_PledgeAttach
   add constraint PK_ST_PLEDGEATTACH primary key (PledgeAttachId)
go

/****** Object:  Stored Procedure [dbo].St_PledgeAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_PledgeAttachUpdateStatus
// 存储过程功能描述：更新St_PledgeAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeAttach'

set @str = 'update [dbo].[St_PledgeAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PledgeAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_PledgeAttachGoBack
// 存储过程功能描述：撤返St_PledgeAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeAttach'

set @str = 'update [dbo].[St_PledgeAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PledgeAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_PledgeAttachGet
// 存储过程功能描述：查询指定St_PledgeAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachGet
    /*
	@PledgeAttachId int
    */
    @id int
AS

SELECT
	[PledgeAttachId],
	[PledgeId],
	[AttachId]
FROM
	[dbo].[St_PledgeAttach]
WHERE
	[PledgeAttachId] = @id

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
// 存储过程名：[dbo].St_PledgeAttachLoad
// 存储过程功能描述：查询所有St_PledgeAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachLoad
AS

SELECT
	[PledgeAttachId],
	[PledgeId],
	[AttachId]
FROM
	[dbo].[St_PledgeAttach]

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
// 存储过程名：[dbo].St_PledgeAttachInsert
// 存储过程功能描述：新增一条St_PledgeAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachInsert
	@PledgeId int =NULL ,
	@AttachId int =NULL ,
	@PledgeAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_PledgeAttach] (
	[PledgeId],
	[AttachId]
) VALUES (
	@PledgeId,
	@AttachId
)


SET @PledgeAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_PledgeAttachUpdate
// 存储过程功能描述：更新St_PledgeAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeAttachUpdate
    @PledgeAttachId int,
@PledgeId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_PledgeAttach] SET
	[PledgeId] = @PledgeId,
	[AttachId] = @AttachId
WHERE
	[PledgeAttachId] = @PledgeAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



