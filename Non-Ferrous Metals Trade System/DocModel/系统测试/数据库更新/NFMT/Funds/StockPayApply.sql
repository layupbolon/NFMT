alter table dbo.Fun_StockPayApply_Ref
   drop constraint PK_FUN_STOCKPAYAPPLY_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_StockPayApply_Ref')
            and   type = 'U')
   drop table dbo.Fun_StockPayApply_Ref
go

/*==============================================================*/
/* Table: Fun_StockPayApply_Ref                                 */
/*==============================================================*/
create table dbo.Fun_StockPayApply_Ref (
   RefId                int                  identity,
   PayApplyId           int                  null,
   ContractRefId        int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   ContractId           int                  null,
   SubId                int                  null,
   ApplyBala            decimal(18,4)        null,
   RefStatus            int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '库存付款申请',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存付款申请序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'RefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约申请序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'ContractRefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请金额',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'ApplyBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Fun_StockPayApply_Ref', 'column', 'RefStatus'
go

alter table dbo.Fun_StockPayApply_Ref
   add constraint PK_FUN_STOCKPAYAPPLY_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_StockPayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_StockPayApply_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_StockPayApply_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_StockPayApply_RefUpdateStatus
// 存储过程功能描述：更新Fun_StockPayApply_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_StockPayApply_Ref'

set @str = 'update [dbo].[Fun_StockPayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_StockPayApply_RefGoBack
// 存储过程功能描述：撤返Fun_StockPayApply_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_StockPayApply_Ref'

set @str = 'update [dbo].[Fun_StockPayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_StockPayApply_RefGet
// 存储过程功能描述：查询指定Fun_StockPayApply_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[PayApplyId],
	[ContractRefId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[ApplyBala],
	[RefStatus]
FROM
	[dbo].[Fun_StockPayApply_Ref]
WHERE
	[RefId] = @id

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
// 存储过程名：[dbo].Fun_StockPayApply_RefLoad
// 存储过程功能描述：查询所有Fun_StockPayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefLoad
AS

SELECT
	[RefId],
	[PayApplyId],
	[ContractRefId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[ApplyBala],
	[RefStatus]
FROM
	[dbo].[Fun_StockPayApply_Ref]

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
// 存储过程名：[dbo].Fun_StockPayApply_RefInsert
// 存储过程功能描述：新增一条Fun_StockPayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefInsert
	@PayApplyId int =NULL ,
	@ContractRefId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@ContractId int =NULL ,
	@SubId int =NULL ,
	@ApplyBala decimal(18, 4) =NULL ,
	@RefStatus int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_StockPayApply_Ref] (
	[PayApplyId],
	[ContractRefId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[ApplyBala],
	[RefStatus]
) VALUES (
	@PayApplyId,
	@ContractRefId,
	@StockId,
	@StockLogId,
	@ContractId,
	@SubId,
	@ApplyBala,
	@RefStatus
)


SET @RefId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_StockPayApply_RefUpdate
// 存储过程功能描述：更新Fun_StockPayApply_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_StockPayApply_RefUpdate
    @RefId int,
@PayApplyId int = NULL,
@ContractRefId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@ContractId int = NULL,
@SubId int = NULL,
@ApplyBala decimal(18, 4) = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[Fun_StockPayApply_Ref] SET
	[PayApplyId] = @PayApplyId,
	[ContractRefId] = @ContractRefId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[ContractId] = @ContractId,
	[SubId] = @SubId,
	[ApplyBala] = @ApplyBala,
	[RefStatus] = @RefStatus
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



