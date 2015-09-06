alter table dbo.Currency
   drop constraint PK_CURRENCY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Currency')
            and   type = 'U')
   drop table dbo.Currency
go

/*==============================================================*/
/* Table: Currency                                              */
/*==============================================================*/
create table dbo.Currency (
   CurrencyId           int                  identity,
   CurrencyName         varchar(20)          null,
   CurrencyStatus       int                  null,
   CurrencyFullName     varchar(20)          null,
   CurencyShort         varchar(20)          null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '币种表',
   'user', 'dbo', 'table', 'Currency'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种序号',
   'user', 'dbo', 'table', 'Currency', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种名称',
   'user', 'dbo', 'table', 'Currency', 'column', 'CurrencyName'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种状态',
   'user', 'dbo', 'table', 'Currency', 'column', 'CurrencyStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种全称',
   'user', 'dbo', 'table', 'Currency', 'column', 'CurrencyFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种缩写',
   'user', 'dbo', 'table', 'Currency', 'column', 'CurencyShort'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Currency', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Currency', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Currency', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Currency', 'column', 'LastModifyTime'
go

alter table dbo.Currency
   add constraint PK_CURRENCY primary key (CurrencyId)
go


/****** Object:  Stored Procedure [dbo].CurrencyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyGet]
GO

/****** Object:  Stored Procedure [dbo].CurrencyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyLoad]
GO

/****** Object:  Stored Procedure [dbo].CurrencyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyInsert]
GO

/****** Object:  Stored Procedure [dbo].CurrencyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyUpdate]
GO

/****** Object:  Stored Procedure [dbo].CurrencyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].CurrencyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CurrencyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CurrencyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CurrencyUpdateStatus
// 存储过程功能描述：更新Currency中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Currency'

set @str = 'update [dbo].[Currency] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CurrencyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CurrencyGoBack
// 存储过程功能描述：撤返Currency，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Currency'

set @str = 'update [dbo].[Currency] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CurrencyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CurrencyGet
// 存储过程功能描述：查询指定Currency的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyGet
    /*
	@CurrencyId int
    */
    @id int
AS

SELECT
	[CurrencyId],
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Currency]
WHERE
	[CurrencyId] = @id

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
// 存储过程名：[dbo].CurrencyLoad
// 存储过程功能描述：查询所有Currency记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyLoad
AS

SELECT
	[CurrencyId],
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Currency]

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
// 存储过程名：[dbo].CurrencyInsert
// 存储过程功能描述：新增一条Currency记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyInsert
	@CurrencyName varchar(20) ,
	@CurrencyStatus int ,
	@CurrencyFullName varchar(20) =NULL ,
	@CurencyShort varchar(20) =NULL ,
	@CreatorId int ,
	@CurrencyId int OUTPUT
AS

INSERT INTO [dbo].[Currency] (
	[CurrencyName],
	[CurrencyStatus],
	[CurrencyFullName],
	[CurencyShort],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CurrencyName,
	@CurrencyStatus,
	@CurrencyFullName,
	@CurencyShort,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CurrencyId = @@IDENTITY

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
// 存储过程名：[dbo].CurrencyUpdate
// 存储过程功能描述：更新Currency
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CurrencyUpdate
    @CurrencyId int,
@CurrencyName varchar(20),
@CurrencyStatus int,
@CurrencyFullName varchar(20) = NULL,
@CurencyShort varchar(20) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Currency] SET
	[CurrencyName] = @CurrencyName,
	[CurrencyStatus] = @CurrencyStatus,
	[CurrencyFullName] = @CurrencyFullName,
	[CurencyShort] = @CurencyShort,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CurrencyId] = @CurrencyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



