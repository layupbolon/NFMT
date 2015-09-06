alter table Fin_PledgeApply
   drop constraint PK_FIN_PLEDGEAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fin_PledgeApply')
            and   type = 'U')
   drop table Fin_PledgeApply
go

/*==============================================================*/
/* Table: Fin_PledgeApply                                       */
/*==============================================================*/
create table Fin_PledgeApply (
   PledgeApplyId        int                  identity,
   PledgeApplyNo        varchar(20)          null,
   DeptId               int                  null,
   ApplyTime            datetime             null,
   FinancingBankId      int                  null,
   FinancingAccountId   int                  null,
   AssetId              int                  null,
   SwitchBack           bit                  null,
   ExchangeId           int                  null,
   SumNetAmount         numeric(18,4)        null,
   SumHands             int                  null,
   PledgeApplyStatus    int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '融资头寸质押申请单',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请单号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'PledgeApplyNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'DeptId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日期',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'ApplyTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '融资银行',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'FinancingBankId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '融资银行账号序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'FinancingAccountId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '融资货物序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'AssetId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头寸是否转回',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'SwitchBack'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交易所',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'ExchangeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '净重合计',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'SumNetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手数合计',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'SumHands'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'PledgeApplyStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Fin_PledgeApply', 'column', 'LastModifyTime'
go

alter table Fin_PledgeApply
   add constraint PK_FIN_PLEDGEAPPLY primary key (PledgeApplyId)
go


/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyGet    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyLoad    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyInsert    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyUpdate    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyUpdateStatus    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyUpdateStatus    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fin_PledgeApplyUpdateStatus
// 存储过程功能描述：更新Fin_PledgeApply中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApply'

set @str = 'update [dbo].[Fin_PledgeApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PledgeApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyGoBack
// 存储过程功能描述：撤返Fin_PledgeApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApply'

set @str = 'update [dbo].[Fin_PledgeApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PledgeApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyGet
// 存储过程功能描述：查询指定Fin_PledgeApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyGet
    /*
	@PledgeApplyId int
    */
    @id int
AS

SELECT
	[PledgeApplyId],
	[PledgeApplyNo],
	[DeptId],
	[ApplyTime],
	[FinancingBankId],
	[FinancingAccountId],
	[AssetId],
	[SwitchBack],
	[ExchangeId],
	[SumNetAmount],
	[SumHands],
	[PledgeApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fin_PledgeApply]
WHERE
	[PledgeApplyId] = @id

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
// 存储过程名：[dbo].Fin_PledgeApplyLoad
// 存储过程功能描述：查询所有Fin_PledgeApply记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyLoad
AS

SELECT
	[PledgeApplyId],
	[PledgeApplyNo],
	[DeptId],
	[ApplyTime],
	[FinancingBankId],
	[FinancingAccountId],
	[AssetId],
	[SwitchBack],
	[ExchangeId],
	[SumNetAmount],
	[SumHands],
	[PledgeApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fin_PledgeApply]

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
// 存储过程名：[dbo].Fin_PledgeApplyInsert
// 存储过程功能描述：新增一条Fin_PledgeApply记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyInsert
	@PledgeApplyNo varchar(20) =NULL ,
	@DeptId int =NULL ,
	@ApplyTime datetime =NULL ,
	@FinancingBankId int =NULL ,
	@FinancingAccountId int =NULL ,
	@AssetId int =NULL ,
	@SwitchBack bit =NULL ,
	@ExchangeId int =NULL ,
	@SumNetAmount numeric(18, 4) =NULL ,
	@SumHands int =NULL ,
	@PledgeApplyStatus int =NULL ,
	@CreatorId int =NULL ,
	@PledgeApplyId int OUTPUT
AS

INSERT INTO [dbo].[Fin_PledgeApply] (
	[PledgeApplyNo],
	[DeptId],
	[ApplyTime],
	[FinancingBankId],
	[FinancingAccountId],
	[AssetId],
	[SwitchBack],
	[ExchangeId],
	[SumNetAmount],
	[SumHands],
	[PledgeApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PledgeApplyNo,
	@DeptId,
	@ApplyTime,
	@FinancingBankId,
	@FinancingAccountId,
	@AssetId,
	@SwitchBack,
	@ExchangeId,
	@SumNetAmount,
	@SumHands,
	@PledgeApplyStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PledgeApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Fin_PledgeApplyUpdate
// 存储过程功能描述：更新Fin_PledgeApply
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyUpdate
    @PledgeApplyId int,
@PledgeApplyNo varchar(20) = NULL,
@DeptId int = NULL,
@ApplyTime datetime = NULL,
@FinancingBankId int = NULL,
@FinancingAccountId int = NULL,
@AssetId int = NULL,
@SwitchBack bit = NULL,
@ExchangeId int = NULL,
@SumNetAmount numeric(18, 4) = NULL,
@SumHands int = NULL,
@PledgeApplyStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fin_PledgeApply] SET
	[PledgeApplyNo] = @PledgeApplyNo,
	[DeptId] = @DeptId,
	[ApplyTime] = @ApplyTime,
	[FinancingBankId] = @FinancingBankId,
	[FinancingAccountId] = @FinancingAccountId,
	[AssetId] = @AssetId,
	[SwitchBack] = @SwitchBack,
	[ExchangeId] = @ExchangeId,
	[SumNetAmount] = @SumNetAmount,
	[SumHands] = @SumHands,
	[PledgeApplyStatus] = @PledgeApplyStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PledgeApplyId] = @PledgeApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



