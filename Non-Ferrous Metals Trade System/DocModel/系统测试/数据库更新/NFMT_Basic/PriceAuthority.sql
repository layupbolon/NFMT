alter table dbo.PriceAuthority
   drop constraint PK_PRICEAUTHORITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.PriceAuthority')
            and   type = 'U')
   drop table dbo.PriceAuthority
go

/*==============================================================*/
/* Table: PriceAuthority                                        */
/*==============================================================*/
create table dbo.PriceAuthority (
   PAId                 int                  identity,
   PAName               varchar(80)          null,
   PAMobile             varchar(80)          null,
   PAPhone              varchar(80)          null,
   PAStatus             int                  null,
   CompanyId            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '点价权',
   'user', 'dbo', 'table', 'PriceAuthority'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价权序号',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'PAId'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价权人名称',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'PAName'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价权手机',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'PAMobile'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价权座机',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'PAPhone'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价权状态',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'PAStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属公司',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'CompanyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'PriceAuthority', 'column', 'LastModifyTime'
go

alter table dbo.PriceAuthority
   add constraint PK_PRICEAUTHORITY primary key (PAId)
go


/****** Object:  Stored Procedure [dbo].PriceAuthorityGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityGet]
GO

/****** Object:  Stored Procedure [dbo].PriceAuthorityLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityLoad]
GO

/****** Object:  Stored Procedure [dbo].PriceAuthorityInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityInsert]
GO

/****** Object:  Stored Procedure [dbo].PriceAuthorityUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityUpdate]
GO

/****** Object:  Stored Procedure [dbo].PriceAuthorityUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].PriceAuthorityUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceAuthorityGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PriceAuthorityGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].PriceAuthorityUpdateStatus
// 存储过程功能描述：更新PriceAuthority中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.PriceAuthority'

set @str = 'update [dbo].[PriceAuthority] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PAId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].PriceAuthorityGoBack
// 存储过程功能描述：撤返PriceAuthority，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.PriceAuthority'

set @str = 'update [dbo].[PriceAuthority] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PAId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].PriceAuthorityGet
// 存储过程功能描述：查询指定PriceAuthority的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityGet
    /*
	@PAId int
    */
    @id int
AS

SELECT
	[PAId],
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[PriceAuthority]
WHERE
	[PAId] = @id

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
// 存储过程名：[dbo].PriceAuthorityLoad
// 存储过程功能描述：查询所有PriceAuthority记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityLoad
AS

SELECT
	[PAId],
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[PriceAuthority]

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
// 存储过程名：[dbo].PriceAuthorityInsert
// 存储过程功能描述：新增一条PriceAuthority记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityInsert
	@PAName varchar(80) ,
	@PAMobile varchar(80) =NULL ,
	@PAPhone varchar(80) =NULL ,
	@PAStatus int ,
	@CompanyId int ,
	@CreatorId int ,
	@PAId int OUTPUT
AS

INSERT INTO [dbo].[PriceAuthority] (
	[PAName],
	[PAMobile],
	[PAPhone],
	[PAStatus],
	[CompanyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PAName,
	@PAMobile,
	@PAPhone,
	@PAStatus,
	@CompanyId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PAId = @@IDENTITY

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
// 存储过程名：[dbo].PriceAuthorityUpdate
// 存储过程功能描述：更新PriceAuthority
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].PriceAuthorityUpdate
    @PAId int,
@PAName varchar(80),
@PAMobile varchar(80) = NULL,
@PAPhone varchar(80) = NULL,
@PAStatus int,
@CompanyId int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[PriceAuthority] SET
	[PAName] = @PAName,
	[PAMobile] = @PAMobile,
	[PAPhone] = @PAPhone,
	[PAStatus] = @PAStatus,
	[CompanyId] = @CompanyId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PAId] = @PAId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



