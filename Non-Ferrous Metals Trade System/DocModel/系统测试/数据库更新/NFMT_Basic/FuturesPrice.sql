alter table dbo.FuturesPrice
   drop constraint PK_FUTURESPRICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FuturesPrice')
            and   type = 'U')
   drop table dbo.FuturesPrice
go

/*==============================================================*/
/* Table: FuturesPrice                                          */
/*==============================================================*/
create table dbo.FuturesPrice (
   FPId                 int                  identity,
   TradeDate            datetime             null,
   TradeCode            varchar(80)          null,
   DeliverDate          datetime             null,
   SettlePrice          numeric(19,4)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '期货价格表',
   'user', 'dbo', 'table', 'FuturesPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '期货价格序号',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'FPId'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易日',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'TradeDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '交易代码',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'TradeCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '交割日',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'DeliverDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '结算价',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'SettlePrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'FuturesPrice', 'column', 'LastModifyTime'
go

alter table dbo.FuturesPrice
   add constraint PK_FUTURESPRICE primary key (FPId)
go


/****** Object:  Stored Procedure [dbo].FuturesPriceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceGet]
GO

/****** Object:  Stored Procedure [dbo].FuturesPriceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceLoad]
GO

/****** Object:  Stored Procedure [dbo].FuturesPriceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceInsert]
GO

/****** Object:  Stored Procedure [dbo].FuturesPriceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceUpdate]
GO

/****** Object:  Stored Procedure [dbo].FuturesPriceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].FuturesPriceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FuturesPriceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[FuturesPriceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].FuturesPriceUpdateStatus
// 存储过程功能描述：更新FuturesPrice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.FuturesPrice'

set @str = 'update [dbo].[FuturesPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].FuturesPriceGoBack
// 存储过程功能描述：撤返FuturesPrice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.FuturesPrice'

set @str = 'update [dbo].[FuturesPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].FuturesPriceGet
// 存储过程功能描述：查询指定FuturesPrice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceGet
    /*
	@FPId int
    */
    @id int
AS

SELECT
	[FPId],
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesPrice]
WHERE
	[FPId] = @id

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
// 存储过程名：[dbo].FuturesPriceLoad
// 存储过程功能描述：查询所有FuturesPrice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceLoad
AS

SELECT
	[FPId],
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[FuturesPrice]

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
// 存储过程名：[dbo].FuturesPriceInsert
// 存储过程功能描述：新增一条FuturesPrice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceInsert
	@TradeDate datetime ,
	@TradeCode varchar(80) ,
	@DeliverDate datetime ,
	@SettlePrice numeric(19, 4) =NULL ,
	@CreatorId int ,
	@FPId int OUTPUT
AS

INSERT INTO [dbo].[FuturesPrice] (
	[TradeDate],
	[TradeCode],
	[DeliverDate],
	[SettlePrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@TradeDate,
	@TradeCode,
	@DeliverDate,
	@SettlePrice,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @FPId = @@IDENTITY

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
// 存储过程名：[dbo].FuturesPriceUpdate
// 存储过程功能描述：更新FuturesPrice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].FuturesPriceUpdate
    @FPId int,
@TradeDate datetime,
@TradeCode varchar(80),
@DeliverDate datetime,
@SettlePrice numeric(19, 4) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[FuturesPrice] SET
	[TradeDate] = @TradeDate,
	[TradeCode] = @TradeCode,
	[DeliverDate] = @DeliverDate,
	[SettlePrice] = @SettlePrice,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[FPId] = @FPId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



