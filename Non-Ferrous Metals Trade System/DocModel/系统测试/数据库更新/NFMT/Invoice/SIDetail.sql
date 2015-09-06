alter table dbo.Inv_SIDetail
   drop constraint PK_INV_SIDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_SIDetail')
            and   type = 'U')
   drop table dbo.Inv_SIDetail
go

/*==============================================================*/
/* Table: Inv_SIDetail                                          */
/*==============================================================*/
create table dbo.Inv_SIDetail (
   SIDetailId           int                  identity,
   SIId                 int                  null,
   PayDept              int                  null,
   FeeType              int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   ContractId           int                  null,
   ContractSubId        int                  null,
   DetailBala           numeric(18,4)        null,
   DetailStatus         int                  null,
   Memo                 varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '价外票明细',
   'user', 'dbo', 'table', 'Inv_SIDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'SIDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '价外票序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'SIId'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本部门',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'PayDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票内容',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'FeeType'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '采购合约序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '采购子合约序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'ContractSubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细金额',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'DetailBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Inv_SIDetail', 'column', 'LastModifyTime'
go

alter table dbo.Inv_SIDetail
   add constraint PK_INV_SIDETAIL primary key (SIDetailId)
go


/****** Object:  Stored Procedure [dbo].Inv_SIDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_SIDetailUpdateStatus
// 存储过程功能描述：更新Inv_SIDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_SIDetail'

set @str = 'update [dbo].[Inv_SIDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SIDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_SIDetailGoBack
// 存储过程功能描述：撤返Inv_SIDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_SIDetail'

set @str = 'update [dbo].[Inv_SIDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SIDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_SIDetailGet
// 存储过程功能描述：查询指定Inv_SIDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailGet
    /*
	@SIDetailId int
    */
    @id int
AS

SELECT
	[SIDetailId],
	[SIId],
	[PayDept],
	[FeeType],
	[StockId],
	[StockLogId],
	[ContractId],
	[ContractSubId],
	[DetailBala],
	[DetailStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_SIDetail]
WHERE
	[SIDetailId] = @id

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
// 存储过程名：[dbo].Inv_SIDetailLoad
// 存储过程功能描述：查询所有Inv_SIDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailLoad
AS

SELECT
	[SIDetailId],
	[SIId],
	[PayDept],
	[FeeType],
	[StockId],
	[StockLogId],
	[ContractId],
	[ContractSubId],
	[DetailBala],
	[DetailStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_SIDetail]

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
// 存储过程名：[dbo].Inv_SIDetailInsert
// 存储过程功能描述：新增一条Inv_SIDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailInsert
	@SIId int =NULL ,
	@PayDept int =NULL ,
	@FeeType int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@DetailBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@Memo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@SIDetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_SIDetail] (
	[SIId],
	[PayDept],
	[FeeType],
	[StockId],
	[StockLogId],
	[ContractId],
	[ContractSubId],
	[DetailBala],
	[DetailStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SIId,
	@PayDept,
	@FeeType,
	@StockId,
	@StockLogId,
	@ContractId,
	@ContractSubId,
	@DetailBala,
	@DetailStatus,
	@Memo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @SIDetailId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_SIDetailUpdate
// 存储过程功能描述：更新Inv_SIDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIDetailUpdate
    @SIDetailId int,
@SIId int = NULL,
@PayDept int = NULL,
@FeeType int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@ContractId int = NULL,
@ContractSubId int = NULL,
@DetailBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@Memo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Inv_SIDetail] SET
	[SIId] = @SIId,
	[PayDept] = @PayDept,
	[FeeType] = @FeeType,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[DetailBala] = @DetailBala,
	[DetailStatus] = @DetailStatus,
	[Memo] = @Memo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[SIDetailId] = @SIDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



