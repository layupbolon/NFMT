alter table dbo.Contact
   drop constraint PK_CONTACT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Contact')
            and   type = 'U')
   drop table dbo.Contact
go

/*==============================================================*/
/* Table: Contact                                               */
/*==============================================================*/
create table dbo.Contact (
   ContactId            int                  identity,
   ContactName          varchar(80)          null,
   ContactCode          varchar(80)          null,
   ContactTel           varchar(80)          null,
   ContactFax           varchar(80)          null,
   ContactAddress       varchar(400)         null,
   CompanyId            int                  null,
   ContactStatus        int                  null,
   OwnerId              int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '联系人',
   'user', 'dbo', 'table', 'Contact'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人序号',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactId'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人姓名',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactName'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人身份证号',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人电话',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactTel'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人传真',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactFax'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人地址',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人公司',
   'user', 'dbo', 'table', 'Contact', 'column', 'CompanyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '联系人状态',
   'user', 'dbo', 'table', 'Contact', 'column', 'ContactStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '归属人',
   'user', 'dbo', 'table', 'Contact', 'column', 'OwnerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Contact', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Contact', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Contact', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Contact', 'column', 'LastModifyTime'
go

alter table dbo.Contact
   add constraint PK_CONTACT primary key (ContactId)
go


/****** Object:  Stored Procedure [dbo].ContactGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactGet]
GO

/****** Object:  Stored Procedure [dbo].ContactLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactLoad]
GO

/****** Object:  Stored Procedure [dbo].ContactInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactInsert]
GO

/****** Object:  Stored Procedure [dbo].ContactUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactUpdate]
GO

/****** Object:  Stored Procedure [dbo].ContactUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ContactUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContactGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContactGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactUpdateStatus
// 存储过程功能描述：更新Contact中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Contact'

set @str = 'update [dbo].[Contact] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContactId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContactGoBack
// 存储过程功能描述：撤返Contact，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Contact'

set @str = 'update [dbo].[Contact] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContactId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContactGet
// 存储过程功能描述：查询指定Contact的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactGet
    /*
	@ContactId int
    */
    @id int
AS

SELECT
	[ContactId],
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Contact]
WHERE
	[ContactId] = @id

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
// 存储过程名：[dbo].ContactLoad
// 存储过程功能描述：查询所有Contact记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactLoad
AS

SELECT
	[ContactId],
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Contact]

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
// 存储过程名：[dbo].ContactInsert
// 存储过程功能描述：新增一条Contact记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactInsert
	@ContactName varchar(80) ,
	@ContactCode varchar(80) =NULL ,
	@ContactTel varchar(80) =NULL ,
	@ContactFax varchar(80) =NULL ,
	@ContactAddress varchar(400) =NULL ,
	@CompanyId int =NULL ,
	@ContactStatus int =NULL ,
	@CreatorId int ,
	@ContactId int OUTPUT
AS

INSERT INTO [dbo].[Contact] (
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContactName,
	@ContactCode,
	@ContactTel,
	@ContactFax,
	@ContactAddress,
	@CompanyId,
	@ContactStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ContactId = @@IDENTITY

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
// 存储过程名：[dbo].ContactUpdate
// 存储过程功能描述：更新Contact
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContactUpdate
    @ContactId int,
@ContactName varchar(80),
@ContactCode varchar(80) = NULL,
@ContactTel varchar(80) = NULL,
@ContactFax varchar(80) = NULL,
@ContactAddress varchar(400) = NULL,
@CompanyId int = NULL,
@ContactStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Contact] SET
	[ContactName] = @ContactName,
	[ContactCode] = @ContactCode,
	[ContactTel] = @ContactTel,
	[ContactFax] = @ContactFax,
	[ContactAddress] = @ContactAddress,
	[CompanyId] = @CompanyId,
	[ContactStatus] = @ContactStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ContactId] = @ContactId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



