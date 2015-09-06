alter table dbo.CorpDetailAttach
   drop constraint PK_CORPDETAILATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.CorpDetailAttach')
            and   type = 'U')
   drop table dbo.CorpDetailAttach
go

/*==============================================================*/
/* Table: CorpDetailAttach                                      */
/*==============================================================*/
create table dbo.CorpDetailAttach (
   CorpDetailAttachId   int                  identity,
   DetailId             int                  null,
   AttachId             int                  null,
   AttachType           int                  null,
   CorpDetailAttachStatus int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '客户附件表',
   'user', 'dbo', 'table', 'CorpDetailAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联表序号',
   'user', 'dbo', 'table', 'CorpDetailAttach', 'column', 'CorpDetailAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '客户明细序号',
   'user', 'dbo', 'table', 'CorpDetailAttach', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'CorpDetailAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件类型',
   'user', 'dbo', 'table', 'CorpDetailAttach', 'column', 'AttachType'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', 'dbo', 'table', 'CorpDetailAttach', 'column', 'CorpDetailAttachStatus'
go

alter table dbo.CorpDetailAttach
   add constraint PK_CORPDETAILATTACH primary key (CorpDetailAttachId)
go


/****** Object:  Stored Procedure [dbo].CorpDetailAttachGet    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachGet]
GO

/****** Object:  Stored Procedure [dbo].CorpDetailAttachLoad    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].CorpDetailAttachInsert    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].CorpDetailAttachUpdate    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].CorpDetailAttachUpdateStatus    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].CorpDetailAttachUpdateStatus    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDetailAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDetailAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDetailAttachUpdateStatus
// 存储过程功能描述：更新CorpDetailAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorpDetailAttach'

set @str = 'update [dbo].[CorpDetailAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CorpDetailAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorpDetailAttachGoBack
// 存储过程功能描述：撤返CorpDetailAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorpDetailAttach'

set @str = 'update [dbo].[CorpDetailAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CorpDetailAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorpDetailAttachGet
// 存储过程功能描述：查询指定CorpDetailAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachGet
    /*
	@CorpDetailAttachId int
    */
    @id int
AS

SELECT
	[CorpDetailAttachId],
	[DetailId],
	[AttachId],
	[AttachType],
	[CorpDetailAttachStatus]
FROM
	[dbo].[CorpDetailAttach]
WHERE
	[CorpDetailAttachId] = @id

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
// 存储过程名：[dbo].CorpDetailAttachLoad
// 存储过程功能描述：查询所有CorpDetailAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachLoad
AS

SELECT
	[CorpDetailAttachId],
	[DetailId],
	[AttachId],
	[AttachType],
	[CorpDetailAttachStatus]
FROM
	[dbo].[CorpDetailAttach]

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
// 存储过程名：[dbo].CorpDetailAttachInsert
// 存储过程功能描述：新增一条CorpDetailAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachInsert
	@DetailId int =NULL ,
	@AttachId int =NULL ,
	@AttachType int =NULL ,
	@CorpDetailAttachStatus int =NULL ,
	@CorpDetailAttachId int OUTPUT
AS

INSERT INTO [dbo].[CorpDetailAttach] (
	[DetailId],
	[AttachId],
	[AttachType],
	[CorpDetailAttachStatus]
) VALUES (
	@DetailId,
	@AttachId,
	@AttachType,
	@CorpDetailAttachStatus
)


SET @CorpDetailAttachId = @@IDENTITY

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
// 存储过程名：[dbo].CorpDetailAttachUpdate
// 存储过程功能描述：更新CorpDetailAttach
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDetailAttachUpdate
    @CorpDetailAttachId int,
@DetailId int = NULL,
@AttachId int = NULL,
@AttachType int = NULL,
@CorpDetailAttachStatus int = NULL
AS

UPDATE [dbo].[CorpDetailAttach] SET
	[DetailId] = @DetailId,
	[AttachId] = @AttachId,
	[AttachType] = @AttachType,
	[CorpDetailAttachStatus] = @CorpDetailAttachStatus
WHERE
	[CorpDetailAttachId] = @CorpDetailAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



