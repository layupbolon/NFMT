alter table dbo.Fun_PaymentAttach
   drop constraint PK_FUN_PAYMENTATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PaymentAttach')
            and   type = 'U')
   drop table dbo.Fun_PaymentAttach
go

/*==============================================================*/
/* Table: Fun_PaymentAttach                                     */
/*==============================================================*/
create table dbo.Fun_PaymentAttach (
   PaymentAttachId      int                  identity,
   AttachId             int                  null,
   PaymentId            int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '财务付款附件',
   'user', 'dbo', 'table', 'Fun_PaymentAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款附件序号',
   'user', 'dbo', 'table', 'Fun_PaymentAttach', 'column', 'PaymentAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Fun_PaymentAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentAttach', 'column', 'PaymentId'
go

alter table dbo.Fun_PaymentAttach
   add constraint PK_FUN_PAYMENTATTACH primary key (PaymentAttachId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentAttachUpdateStatus
// 存储过程功能描述：更新Fun_PaymentAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentAttach'

set @str = 'update [dbo].[Fun_PaymentAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PaymentAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentAttachGoBack
// 存储过程功能描述：撤返Fun_PaymentAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentAttach'

set @str = 'update [dbo].[Fun_PaymentAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where PaymentAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentAttachGet
// 存储过程功能描述：查询指定Fun_PaymentAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachGet
    /*
	@PaymentAttachId int
    */
    @id int
AS

SELECT
	[PaymentAttachId],
	[AttachId],
	[PaymentId]
FROM
	[dbo].[Fun_PaymentAttach]
WHERE
	[PaymentAttachId] = @id

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
// 存储过程名：[dbo].Fun_PaymentAttachLoad
// 存储过程功能描述：查询所有Fun_PaymentAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachLoad
AS

SELECT
	[PaymentAttachId],
	[AttachId],
	[PaymentId]
FROM
	[dbo].[Fun_PaymentAttach]

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
// 存储过程名：[dbo].Fun_PaymentAttachInsert
// 存储过程功能描述：新增一条Fun_PaymentAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachInsert
	@AttachId int =NULL ,
	@PaymentId int =NULL ,
	@PaymentAttachId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PaymentAttach] (
	[AttachId],
	[PaymentId]
) VALUES (
	@AttachId,
	@PaymentId
)


SET @PaymentAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_PaymentAttachUpdate
// 存储过程功能描述：更新Fun_PaymentAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentAttachUpdate
    @PaymentAttachId int,
@AttachId int = NULL,
@PaymentId int = NULL
AS

UPDATE [dbo].[Fun_PaymentAttach] SET
	[AttachId] = @AttachId,
	[PaymentId] = @PaymentId
WHERE
	[PaymentAttachId] = @PaymentAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



