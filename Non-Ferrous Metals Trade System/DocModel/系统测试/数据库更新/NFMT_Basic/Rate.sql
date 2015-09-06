alter table dbo.Rate
   drop constraint PK_RATE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Rate')
            and   type = 'U')
   drop table dbo.Rate
go

/*==============================================================*/
/* Table: Rate                                                  */
/*==============================================================*/
create table dbo.Rate (
   RateId               int                  not null,
   FromCurrencyId       int                  null,
   ToCurrencyId         int                  null,
   RateValue            numeric(18,4)        null,
   RateStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '汇率',
   'user', 'dbo', 'table', 'Rate'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇率序号',
   'user', 'dbo', 'table', 'Rate', 'column', 'RateId'
go

execute sp_addextendedproperty 'MS_Description', 
   '兑换币种序号',
   'user', 'dbo', 'table', 'Rate', 'column', 'FromCurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '换至币种序号',
   'user', 'dbo', 'table', 'Rate', 'column', 'ToCurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇率',
   'user', 'dbo', 'table', 'Rate', 'column', 'RateValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇率状态',
   'user', 'dbo', 'table', 'Rate', 'column', 'RateStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Rate', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Rate', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Rate', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Rate', 'column', 'LastModifyTime'
go

alter table dbo.Rate
   add constraint PK_RATE primary key (RateId)
go


/****** Object:  Stored Procedure [dbo].RateGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateGet]
GO

/****** Object:  Stored Procedure [dbo].RateLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateLoad]
GO

/****** Object:  Stored Procedure [dbo].RateInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateInsert]
GO

/****** Object:  Stored Procedure [dbo].RateUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateUpdate]
GO

/****** Object:  Stored Procedure [dbo].RateUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].RateUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RateGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RateGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RateUpdateStatus
// 存储过程功能描述：更新Rate中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Rate'

set @str = 'update [dbo].[Rate] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RateId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].RateGoBack
// 存储过程功能描述：撤返Rate，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Rate'

set @str = 'update [dbo].[Rate] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RateId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].RateGet
// 存储过程功能描述：查询指定Rate的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateGet
    /*
	@RateId int
    */
    @id int
AS

SELECT
	[RateId],
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Rate]
WHERE
	[RateId] = @id

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
// 存储过程名：[dbo].RateLoad
// 存储过程功能描述：查询所有Rate记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateLoad
AS

SELECT
	[RateId],
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Rate]

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
// 存储过程名：[dbo].RateInsert
// 存储过程功能描述：新增一条Rate记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateInsert
	@FromCurrencyId int =NULL ,
	@ToCurrencyId int =NULL ,
	@RateValue numeric(18, 4) =NULL ,
	@RateStatus int =NULL ,
	@CreatorId int =NULL ,
	@RateId int OUTPUT
AS

INSERT INTO [dbo].[Rate] (
	[FromCurrencyId],
	[ToCurrencyId],
	[RateValue],
	[RateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@FromCurrencyId,
	@ToCurrencyId,
	@RateValue,
	@RateStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RateId = @@IDENTITY

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
// 存储过程名：[dbo].RateUpdate
// 存储过程功能描述：更新Rate
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].RateUpdate
    @RateId int,
@FromCurrencyId int = NULL,
@ToCurrencyId int = NULL,
@RateValue numeric(18, 4) = NULL,
@RateStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Rate] SET
	[FromCurrencyId] = @FromCurrencyId,
	[ToCurrencyId] = @ToCurrencyId,
	[RateValue] = @RateValue,
	[RateStatus] = @RateStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RateId] = @RateId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



