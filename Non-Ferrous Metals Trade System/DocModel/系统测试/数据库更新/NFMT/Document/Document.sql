alter table dbo.Doc_Document
   drop constraint PK_DOC_DOCUMENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_Document')
            and   type = 'U')
   drop table dbo.Doc_Document
go

/*==============================================================*/
/* Table: Doc_Document                                          */
/*==============================================================*/
create table dbo.Doc_Document (
   DocumentId           int                  identity,
   OrderId              int                  null,
   DocumentDate         datetime             null,
   DocEmpId             int                  null,
   PresentDate          datetime             null,
   Presenter            int                  null,
   AcceptanceDate       datetime             null,
   Acceptancer          int                  null,
   Meno                 varchar(4000)        null,
   DocumentStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单',
   'user', 'dbo', 'table', 'Doc_Document'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单序号',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'DocumentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单指令序号',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单日期',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'DocumentDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单人',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'DocEmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '交单时间',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'PresentDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '承兑确认时间',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'AcceptanceDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单状态',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'DocumentStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_Document', 'column', 'LastModifyTime'
go

alter table dbo.Doc_Document
   add constraint PK_DOC_DOCUMENT primary key (DocumentId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentUpdateStatus
// 存储过程功能描述：更新Doc_Document中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_Document'

set @str = 'update [dbo].[Doc_Document] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DocumentId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Doc_DocumentGoBack
// 存储过程功能描述：撤返Doc_Document，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_Document'

set @str = 'update [dbo].[Doc_Document] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DocumentId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Doc_DocumentGet
// 存储过程功能描述：查询指定Doc_Document的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentGet
    /*
	@DocumentId int
    */
    @id int
AS

SELECT
	[DocumentId],
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_Document]
WHERE
	[DocumentId] = @id

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
// 存储过程名：[dbo].Doc_DocumentLoad
// 存储过程功能描述：查询所有Doc_Document记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentLoad
AS

SELECT
	[DocumentId],
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_Document]

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
// 存储过程名：[dbo].Doc_DocumentInsert
// 存储过程功能描述：新增一条Doc_Document记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInsert
	@OrderId int =NULL ,
	@DocumentDate datetime =NULL ,
	@DocEmpId int =NULL ,
	@PresentDate datetime =NULL ,
	@Presenter int =NULL ,
	@AcceptanceDate datetime =NULL ,
	@Acceptancer int =NULL ,
	@Meno varchar(4000) =NULL ,
	@DocumentStatus int =NULL ,
	@CreatorId int =NULL ,
	@DocumentId int OUTPUT
AS

INSERT INTO [dbo].[Doc_Document] (
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@DocumentDate,
	@DocEmpId,
	@PresentDate,
	@Presenter,
	@AcceptanceDate,
	@Acceptancer,
	@Meno,
	@DocumentStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DocumentId = @@IDENTITY

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
// 存储过程名：[dbo].Doc_DocumentUpdate
// 存储过程功能描述：更新Doc_Document
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentUpdate
    @DocumentId int,
@OrderId int = NULL,
@DocumentDate datetime = NULL,
@DocEmpId int = NULL,
@PresentDate datetime = NULL,
@Presenter int = NULL,
@AcceptanceDate datetime = NULL,
@Acceptancer int = NULL,
@Meno varchar(4000) = NULL,
@DocumentStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_Document] SET
	[OrderId] = @OrderId,
	[DocumentDate] = @DocumentDate,
	[DocEmpId] = @DocEmpId,
	[PresentDate] = @PresentDate,
	[Presenter] = @Presenter,
	[AcceptanceDate] = @AcceptanceDate,
	[Acceptancer] = @Acceptancer,
	[Meno] = @Meno,
	[DocumentStatus] = @DocumentStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DocumentId] = @DocumentId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



