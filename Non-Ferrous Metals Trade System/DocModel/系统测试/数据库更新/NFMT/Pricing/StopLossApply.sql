alter table Pri_StopLossApply
   drop constraint PK_PRI_STOPLOSSAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_StopLossApply')
            and   type = 'U')
   drop table Pri_StopLossApply
go

/*==============================================================*/
/* Table: Pri_StopLossApply                                     */
/*==============================================================*/
create table Pri_StopLossApply (
   StopLossApplyId      int                  identity,
   ApplyId              int                  null,
   PricingId            int                  null,
   PricingDirection     int                  null,
   SubContractId        int                  null,
   ContractId           int                  null,
   AssertId             int                  null,
   StopLossPrice        decimal(18,4)        null,
   CurrencyId           int                  null,
   StopLossWeight       decimal(18,4)        null,
   MUId                 int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '止损申请',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'StopLossApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'PricingId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价方向',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'PricingDirection'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损品种',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'AssertId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损价格',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'StopLossPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格币种',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损重量',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'StopLossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', @CurrentUser, 'table', 'Pri_StopLossApply', 'column', 'MUId'
go

alter table Pri_StopLossApply
   add constraint PK_PRI_STOPLOSSAPPLY primary key (StopLossApplyId)
go


/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_StopLossApplyUpdateStatus
// 存储过程功能描述：更新Pri_StopLossApply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLossApply'

set @str = 'update [dbo].[Pri_StopLossApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StopLossApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossApplyGoBack
// 存储过程功能描述：撤返Pri_StopLossApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLossApply'

set @str = 'update [dbo].[Pri_StopLossApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StopLossApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossApplyGet
// 存储过程功能描述：查询指定Pri_StopLossApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyGet
    /*
	@StopLossApplyId int
    */
    @id int
AS

SELECT
	[StopLossApplyId],
	[ApplyId],
	[PricingId],
	[PricingDirection],
	[SubContractId],
	[ContractId],
	[AssertId],
	[StopLossPrice],
	[CurrencyId],
	[StopLossWeight],
	[MUId]
FROM
	[dbo].[Pri_StopLossApply]
WHERE
	[StopLossApplyId] = @id

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
// 存储过程名：[dbo].Pri_StopLossApplyLoad
// 存储过程功能描述：查询所有Pri_StopLossApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyLoad
AS

SELECT
	[StopLossApplyId],
	[ApplyId],
	[PricingId],
	[PricingDirection],
	[SubContractId],
	[ContractId],
	[AssertId],
	[StopLossPrice],
	[CurrencyId],
	[StopLossWeight],
	[MUId]
FROM
	[dbo].[Pri_StopLossApply]

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
// 存储过程名：[dbo].Pri_StopLossApplyInsert
// 存储过程功能描述：新增一条Pri_StopLossApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyInsert
	@ApplyId int =NULL ,
	@PricingId int =NULL ,
	@PricingDirection int =NULL ,
	@SubContractId int =NULL ,
	@ContractId int =NULL ,
	@AssertId int =NULL ,
	@StopLossPrice decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@StopLossWeight decimal(18, 4) =NULL ,
	@MUId int =NULL ,
	@StopLossApplyId int OUTPUT
AS

INSERT INTO [dbo].[Pri_StopLossApply] (
	[ApplyId],
	[PricingId],
	[PricingDirection],
	[SubContractId],
	[ContractId],
	[AssertId],
	[StopLossPrice],
	[CurrencyId],
	[StopLossWeight],
	[MUId]
) VALUES (
	@ApplyId,
	@PricingId,
	@PricingDirection,
	@SubContractId,
	@ContractId,
	@AssertId,
	@StopLossPrice,
	@CurrencyId,
	@StopLossWeight,
	@MUId
)


SET @StopLossApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_StopLossApplyUpdate
// 存储过程功能描述：更新Pri_StopLossApply
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossApplyUpdate
    @StopLossApplyId int,
@ApplyId int = NULL,
@PricingId int = NULL,
@PricingDirection int = NULL,
@SubContractId int = NULL,
@ContractId int = NULL,
@AssertId int = NULL,
@StopLossPrice decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@StopLossWeight decimal(18, 4) = NULL,
@MUId int = NULL
AS

UPDATE [dbo].[Pri_StopLossApply] SET
	[ApplyId] = @ApplyId,
	[PricingId] = @PricingId,
	[PricingDirection] = @PricingDirection,
	[SubContractId] = @SubContractId,
	[ContractId] = @ContractId,
	[AssertId] = @AssertId,
	[StopLossPrice] = @StopLossPrice,
	[CurrencyId] = @CurrencyId,
	[StopLossWeight] = @StopLossWeight,
	[MUId] = @MUId
WHERE
	[StopLossApplyId] = @StopLossApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



