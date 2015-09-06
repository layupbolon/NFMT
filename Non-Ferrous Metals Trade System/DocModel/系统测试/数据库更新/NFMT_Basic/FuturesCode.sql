alter table dbo.FuturesCode
   drop constraint PK_FUTURESCODE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FuturesCode')
            and   type = 'U')
   drop table dbo.FuturesCode
go

/*==============================================================*/
/* Table: FuturesCode                                           */
/*==============================================================*/
create table dbo.FuturesCode (
   FuturesCodeId        int                  identity,
   ExchageId            int                  null,
   CodeSize             numeric(19,4)        null,
   FirstTradeDate       datetime             null,
   LastTradeDate        datetime             null,
   MUId                 int                  null,
   CurrencyId           int                  null,
   AssetId              int                  null,
   FuturesCodeStatus    int                  null,
   TradeCode            varchar(80)          null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '期货合约',
   'user', 'dbo', 'table', 'FuturesCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '期货合约序号',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'FuturesCodeId'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易所序号',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'ExchageId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约规模',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'CodeSize'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易起始日期',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'FirstTradeDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易终止日期',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'LastTradeDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约单位',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约状态',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'FuturesCodeStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易代码',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'TradeCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'FuturesCode', 'column', 'LastModifyTime'
go

alter table dbo.FuturesCode
   add constraint PK_FUTURESCODE primary key (FuturesCodeId)
go


/****** Object:  Stored Procedure [dbo].FuturesCodeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeGet]
GO

/****** Object:  Stored Procedure [dbo].FuturesCodeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeLoad]
GO

/****** Object:  Stored Procedure [dbo].FuturesCodeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeInsert]
GO

/****** Object:  Stored Procedure [dbo].FuturesCodeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeUpdate]
GO

/****** Object:  Stored Procedure [dbo].FuturesCodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].FuturesCodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesCodeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesCodeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesCodeUpdateStatus
// 存储过程功能描述：更新FuturesCode中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.FuturesCode'

set @str = 'update [dbo].[FuturesCode] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FuturesCodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].FuturesCodeGoBack
// 存储过程功能描述：撤返FuturesCode，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.FuturesCode'

set @str = 'update [dbo].[FuturesCode] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FuturesCodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].FuturesCodeGet
// 存储过程功能描述：查询指定FuturesCode的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeGet
    /*
	@FuturesCodeId int
    */
    @id int
AS

SELECT
	[FuturesCodeId],
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesCode]
WHERE
	[FuturesCodeId] = @id

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
// 存储过程名：[dbo].FuturesCodeLoad
// 存储过程功能描述：查询所有FuturesCode记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeLoad
AS

SELECT
	[FuturesCodeId],
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesCode]

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
// 存储过程名：[dbo].FuturesCodeInsert
// 存储过程功能描述：新增一条FuturesCode记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeInsert
	@ExchageId int =NULL ,
	@CodeSize numeric(19, 4) =NULL ,
	@FirstTradeDate datetime =NULL ,
	@LastTradeDate datetime =NULL ,
	@MUId int =NULL ,
	@CurrencyId int =NULL ,
	@AssetId int =NULL ,
	@FuturesCodeStatus int =NULL ,
	@TradeCode varchar(80) =NULL ,
	@CreatorId int =NULL ,
	@FuturesCodeId int OUTPUT
AS

INSERT INTO [dbo].[FuturesCode] (
	[ExchageId],
	[CodeSize],
	[FirstTradeDate],
	[LastTradeDate],
	[MUId],
	[CurrencyId],
	[AssetId],
	[FuturesCodeStatus],
	[TradeCode],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ExchageId,
	@CodeSize,
	@FirstTradeDate,
	@LastTradeDate,
	@MUId,
	@CurrencyId,
	@AssetId,
	@FuturesCodeStatus,
	@TradeCode,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @FuturesCodeId = @@IDENTITY

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
// 存储过程名：[dbo].FuturesCodeUpdate
// 存储过程功能描述：更新FuturesCode
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesCodeUpdate
    @FuturesCodeId int,
@ExchageId int = NULL,
@CodeSize numeric(19, 4) = NULL,
@FirstTradeDate datetime = NULL,
@LastTradeDate datetime = NULL,
@MUId int = NULL,
@CurrencyId int = NULL,
@AssetId int = NULL,
@FuturesCodeStatus int = NULL,
@TradeCode varchar(80) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[FuturesCode] SET
	[ExchageId] = @ExchageId,
	[CodeSize] = @CodeSize,
	[FirstTradeDate] = @FirstTradeDate,
	[LastTradeDate] = @LastTradeDate,
	[MUId] = @MUId,
	[CurrencyId] = @CurrencyId,
	[AssetId] = @AssetId,
	[FuturesCodeStatus] = @FuturesCodeStatus,
	[TradeCode] = @TradeCode,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[FuturesCodeId] = @FuturesCodeId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



