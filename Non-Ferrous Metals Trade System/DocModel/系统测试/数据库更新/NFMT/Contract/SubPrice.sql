alter table dbo.Con_SubPrice
   drop constraint PK_CON_SUBPRICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_SubPrice')
            and   type = 'U')
   drop table dbo.Con_SubPrice
go

/*==============================================================*/
/* Table: Con_SubPrice                                          */
/*==============================================================*/
create table dbo.Con_SubPrice (
   SubPriceId           int                  identity,
   SubId                int                  null,
   FixedPrice           numeric(18,6)        null,
   FixedPriceMemo       varchar(4000)        null,
   WhoDoPrice           int                  null,
   AlmostPrice          decimal(18,6)        null,
   DoPriceBeginDate     datetime             null,
   DoPriceEndDate       datetime             null,
   IsQP                 bit                  null,
   PriceFrom            int                  null,
   PriceStyle1          int                  null,
   PriceStyle2          int                  null,
   MarginMode           int                  null,
   MarginAmount         numeric(18,4)        null,
   MarginMemo           varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '子合约价格明细',
   'user', 'dbo', 'table', 'Con_SubPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约价格序号',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'SubPriceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价价格',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'FixedPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价价格备注',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'FixedPriceMemo'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价方',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'WhoDoPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价估价',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'AlmostPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价起始日',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'DoPriceBeginDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价最终日',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'DoPriceEndDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '可否QP延期',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'IsQP'
go

execute sp_addextendedproperty 'MS_Description', 
   '裸价来源(交易所列表)',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'PriceFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '作价方式2',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'PriceStyle2'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金方式',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'MarginMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金数量',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'MarginAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金备注',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'MarginMemo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_SubPrice', 'column', 'LastModifyTime'
go

alter table dbo.Con_SubPrice
   add constraint PK_CON_SUBPRICE primary key (SubPriceId)
go

/****** Object:  Stored Procedure [dbo].Con_SubPriceGet    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceGet]
GO

/****** Object:  Stored Procedure [dbo].Con_SubPriceLoad    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_SubPriceInsert    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_SubPriceUpdate    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_SubPriceUpdateStatus    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_SubPriceUpdateStatus    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubPriceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubPriceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_SubPriceUpdateStatus
// 存储过程功能描述：更新Con_SubPrice中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubPrice'

set @str = 'update [dbo].[Con_SubPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubPriceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_SubPriceGoBack
// 存储过程功能描述：撤返Con_SubPrice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubPrice'

set @str = 'update [dbo].[Con_SubPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubPriceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_SubPriceGet
// 存储过程功能描述：查询指定Con_SubPrice的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceGet
    /*
	@SubPriceId int
    */
    @id int
AS

SELECT
	[SubPriceId],
	[SubId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_SubPrice]
WHERE
	[SubPriceId] = @id

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
// 存储过程名：[dbo].Con_SubPriceLoad
// 存储过程功能描述：查询所有Con_SubPrice记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceLoad
AS

SELECT
	[SubPriceId],
	[SubId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_SubPrice]

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
// 存储过程名：[dbo].Con_SubPriceInsert
// 存储过程功能描述：新增一条Con_SubPrice记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceInsert
	@SubId int =NULL ,
	@FixedPrice numeric(18, 6) =NULL ,
	@FixedPriceMemo varchar(4000) =NULL ,
	@WhoDoPrice int =NULL ,
	@AlmostPrice decimal(18, 6) =NULL ,
	@DoPriceBeginDate datetime =NULL ,
	@DoPriceEndDate datetime =NULL ,
	@IsQP bit =NULL ,
	@PriceFrom int =NULL ,
	@PriceStyle1 int =NULL ,
	@PriceStyle2 int =NULL ,
	@MarginMode int =NULL ,
	@MarginAmount numeric(18, 4) =NULL ,
	@MarginMemo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@SubPriceId int OUTPUT
AS

INSERT INTO [dbo].[Con_SubPrice] (
	[SubId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SubId,
	@FixedPrice,
	@FixedPriceMemo,
	@WhoDoPrice,
	@AlmostPrice,
	@DoPriceBeginDate,
	@DoPriceEndDate,
	@IsQP,
	@PriceFrom,
	@PriceStyle1,
	@PriceStyle2,
	@MarginMode,
	@MarginAmount,
	@MarginMemo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @SubPriceId = @@IDENTITY

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
// 存储过程名：[dbo].Con_SubPriceUpdate
// 存储过程功能描述：更新Con_SubPrice
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubPriceUpdate
    @SubPriceId int,
@SubId int = NULL,
@FixedPrice numeric(18, 6) = NULL,
@FixedPriceMemo varchar(4000) = NULL,
@WhoDoPrice int = NULL,
@AlmostPrice decimal(18, 6) = NULL,
@DoPriceBeginDate datetime = NULL,
@DoPriceEndDate datetime = NULL,
@IsQP bit = NULL,
@PriceFrom int = NULL,
@PriceStyle1 int = NULL,
@PriceStyle2 int = NULL,
@MarginMode int = NULL,
@MarginAmount numeric(18, 4) = NULL,
@MarginMemo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_SubPrice] SET
	[SubId] = @SubId,
	[FixedPrice] = @FixedPrice,
	[FixedPriceMemo] = @FixedPriceMemo,
	[WhoDoPrice] = @WhoDoPrice,
	[AlmostPrice] = @AlmostPrice,
	[DoPriceBeginDate] = @DoPriceBeginDate,
	[DoPriceEndDate] = @DoPriceEndDate,
	[IsQP] = @IsQP,
	[PriceFrom] = @PriceFrom,
	[PriceStyle1] = @PriceStyle1,
	[PriceStyle2] = @PriceStyle2,
	[MarginMode] = @MarginMode,
	[MarginAmount] = @MarginAmount,
	[MarginMemo] = @MarginMemo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[SubPriceId] = @SubPriceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



