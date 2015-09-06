alter table dbo.St_StockOutApplyDetail
   drop constraint PK_ST_STOCKOUTAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockOutApplyDetail')
            and   type = 'U')
   drop table dbo.St_StockOutApplyDetail
go

/*==============================================================*/
/* Table: St_StockOutApplyDetail                                */
/*==============================================================*/
create table dbo.St_StockOutApplyDetail (
   DetailId             int                  identity,
   StockOutApplyId      int                  null,
   StockId              int                  null,
   DetailStatus         int                  null,
   ContractId           int                  null,
   SubContractId        int                  null,
   NetAmount            decimal(18,4)        null,
   GrossAmount          decimal(18,4)        null,
   Bundles              int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出库申请明细',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库申请序号',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'StockOutApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库申请明细状态',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'SubContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请净量',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请毛重',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', 'dbo', 'table', 'St_StockOutApplyDetail', 'column', 'Bundles'
go

alter table dbo.St_StockOutApplyDetail
   add constraint PK_ST_STOCKOUTAPPLYDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockOutApplyDetailUpdateStatus
// 存储过程功能描述：更新St_StockOutApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutApplyDetail'

set @str = 'update [dbo].[St_StockOutApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutApplyDetailGoBack
// 存储过程功能描述：撤返St_StockOutApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutApplyDetail'

set @str = 'update [dbo].[St_StockOutApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutApplyDetailGet
// 存储过程功能描述：查询指定St_StockOutApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StockOutApplyId],
	[StockId],
	[DetailStatus],
	[ContractId],
	[SubContractId],
	[NetAmount],
	[GrossAmount],
	[Bundles]
FROM
	[dbo].[St_StockOutApplyDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].St_StockOutApplyDetailLoad
// 存储过程功能描述：查询所有St_StockOutApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailLoad
AS

SELECT
	[DetailId],
	[StockOutApplyId],
	[StockId],
	[DetailStatus],
	[ContractId],
	[SubContractId],
	[NetAmount],
	[GrossAmount],
	[Bundles]
FROM
	[dbo].[St_StockOutApplyDetail]

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
// 存储过程名：[dbo].St_StockOutApplyDetailInsert
// 存储过程功能描述：新增一条St_StockOutApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailInsert
	@StockOutApplyId int =NULL ,
	@StockId int =NULL ,
	@DetailStatus int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@GrossAmount decimal(18, 4) =NULL ,
	@Bundles int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_StockOutApplyDetail] (
	[StockOutApplyId],
	[StockId],
	[DetailStatus],
	[ContractId],
	[SubContractId],
	[NetAmount],
	[GrossAmount],
	[Bundles]
) VALUES (
	@StockOutApplyId,
	@StockId,
	@DetailStatus,
	@ContractId,
	@SubContractId,
	@NetAmount,
	@GrossAmount,
	@Bundles
)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockOutApplyDetailUpdate
// 存储过程功能描述：更新St_StockOutApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyDetailUpdate
    @DetailId int,
@StockOutApplyId int = NULL,
@StockId int = NULL,
@DetailStatus int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@NetAmount decimal(18, 4) = NULL,
@GrossAmount decimal(18, 4) = NULL,
@Bundles int = NULL
AS

UPDATE [dbo].[St_StockOutApplyDetail] SET
	[StockOutApplyId] = @StockOutApplyId,
	[StockId] = @StockId,
	[DetailStatus] = @DetailStatus,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[NetAmount] = @NetAmount,
	[GrossAmount] = @GrossAmount,
	[Bundles] = @Bundles
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



