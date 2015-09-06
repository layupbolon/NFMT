alter table dbo.InvoiceAttach
   drop constraint PK_INVOICEATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.InvoiceAttach')
            and   type = 'U')
   drop table dbo.InvoiceAttach
go

/*==============================================================*/
/* Table: InvoiceAttach                                         */
/*==============================================================*/
create table dbo.InvoiceAttach (
   InvoiceAttachId      int                  identity,
   AttachId             int                  null,
   InvoiceId            int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '发票附件',
   'user', 'dbo', 'table', 'InvoiceAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票附件序号',
   'user', 'dbo', 'table', 'InvoiceAttach', 'column', 'InvoiceAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'InvoiceAttach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'InvoiceAttach', 'column', 'InvoiceId'
go

alter table dbo.InvoiceAttach
   add constraint PK_INVOICEATTACH primary key (InvoiceAttachId)
go


/****** Object:  Stored Procedure [dbo].InvoiceAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachGet]
GO

/****** Object:  Stored Procedure [dbo].InvoiceAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].InvoiceAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].InvoiceAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].InvoiceAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].InvoiceAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].InvoiceAttachUpdateStatus
// 存储过程功能描述：更新InvoiceAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.InvoiceAttach'

set @str = 'update [dbo].[InvoiceAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where InvoiceAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].InvoiceAttachGoBack
// 存储过程功能描述：撤返InvoiceAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.InvoiceAttach'

set @str = 'update [dbo].[InvoiceAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where InvoiceAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].InvoiceAttachGet
// 存储过程功能描述：查询指定InvoiceAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachGet
    /*
	@InvoiceAttachId int
    */
    @id int
AS

SELECT
	[InvoiceAttachId],
	[AttachId],
	[InvoiceId]
FROM
	[dbo].[InvoiceAttach]
WHERE
	[InvoiceAttachId] = @id

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
// 存储过程名：[dbo].InvoiceAttachLoad
// 存储过程功能描述：查询所有InvoiceAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachLoad
AS

SELECT
	[InvoiceAttachId],
	[AttachId],
	[InvoiceId]
FROM
	[dbo].[InvoiceAttach]

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
// 存储过程名：[dbo].InvoiceAttachInsert
// 存储过程功能描述：新增一条InvoiceAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachInsert
	@AttachId int =NULL ,
	@InvoiceId int =NULL ,
	@InvoiceAttachId int OUTPUT
AS

INSERT INTO [dbo].[InvoiceAttach] (
	[AttachId],
	[InvoiceId]
) VALUES (
	@AttachId,
	@InvoiceId
)


SET @InvoiceAttachId = @@IDENTITY

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
// 存储过程名：[dbo].InvoiceAttachUpdate
// 存储过程功能描述：更新InvoiceAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceAttachUpdate
    @InvoiceAttachId int,
@AttachId int = NULL,
@InvoiceId int = NULL
AS

UPDATE [dbo].[InvoiceAttach] SET
	[AttachId] = @AttachId,
	[InvoiceId] = @InvoiceId
WHERE
	[InvoiceAttachId] = @InvoiceAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



