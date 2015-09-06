alter table dbo.Fun_PayApplyAttach
   drop constraint PK_FUN_PAYAPPLYATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PayApplyAttach')
            and   type = 'U')
   drop table dbo.Fun_PayApplyAttach
go

/*==============================================================*/
/* Table: Fun_PayApplyAttach                                    */
/*==============================================================*/
create table dbo.Fun_PayApplyAttach (
   PayApplyAttachId     int                  identity,
   AttachId             int                  null,
   PayApplyId           int                  null,
   AttachType           int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '付款申请附件',
   'user', 'dbo', 'table', 'Fun_PayApplyAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请附件序号',
   'user', 'dbo', 'table', 'Fun_PayApplyAttach', 'column', 'PayApplyAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Fun_PayApplyAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PayApplyAttach', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件类型',
   'user', 'dbo', 'table', 'Fun_PayApplyAttach', 'column', 'AttachType'
go

alter table dbo.Fun_PayApplyAttach
   add constraint PK_FUN_PAYAPPLYATTACH primary key (PayApplyAttachId)
go


/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachGet    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachLoad    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachInsert    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachUpdate    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachUpdateStatus    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyAttachUpdateStatus    Script Date: 2015年3月13日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PayApplyAttachUpdateStatus
// 存储过程功能描述：更新Fun_PayApplyAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PayApplyAttach'

set @str = 'update [dbo].[Fun_PayApplyAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PayApplyAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PayApplyAttachGoBack
// 存储过程功能描述：撤返Fun_PayApplyAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PayApplyAttach'

set @str = 'update [dbo].[Fun_PayApplyAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PayApplyAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PayApplyAttachGet
// 存储过程功能描述：查询指定Fun_PayApplyAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachGet
    /*
	@PayApplyAttachId int
    */
    @id int
AS

SELECT
	[PayApplyAttachId],
	[AttachId],
	[PayApplyId],
	[AttachType]
FROM
	[dbo].[Fun_PayApplyAttach]
WHERE
	[PayApplyAttachId] = @id

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
// 存储过程名：[dbo].Fun_PayApplyAttachLoad
// 存储过程功能描述：查询所有Fun_PayApplyAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachLoad
AS

SELECT
	[PayApplyAttachId],
	[AttachId],
	[PayApplyId],
	[AttachType]
FROM
	[dbo].[Fun_PayApplyAttach]

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
// 存储过程名：[dbo].Fun_PayApplyAttachInsert
// 存储过程功能描述：新增一条Fun_PayApplyAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachInsert
	@AttachId int =NULL ,
	@PayApplyId int =NULL ,
	@AttachType int =NULL ,
	@PayApplyAttachId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PayApplyAttach] (
	[AttachId],
	[PayApplyId],
	[AttachType]
) VALUES (
	@AttachId,
	@PayApplyId,
	@AttachType
)


SET @PayApplyAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_PayApplyAttachUpdate
// 存储过程功能描述：更新Fun_PayApplyAttach
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyAttachUpdate
    @PayApplyAttachId int,
@AttachId int = NULL,
@PayApplyId int = NULL,
@AttachType int = NULL
AS

UPDATE [dbo].[Fun_PayApplyAttach] SET
	[AttachId] = @AttachId,
	[PayApplyId] = @PayApplyId,
	[AttachType] = @AttachType
WHERE
	[PayApplyAttachId] = @PayApplyAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



