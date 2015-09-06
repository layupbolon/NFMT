alter table dbo.Fun_PaymentVirtual
   drop constraint PK_FUN_PAYMENTVIRTUAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PaymentVirtual')
            and   type = 'U')
   drop table dbo.Fun_PaymentVirtual
go

/*==============================================================*/
/* Table: Fun_PaymentVirtual                                    */
/*==============================================================*/
create table dbo.Fun_PaymentVirtual (
   VirtualId            int                  identity,
   PaymentId            int                  not null,
   PayApplyId           int                  null,
   PayBala              numeric(18,4)        null,
   DetailStatus         int                  null,
   IsConfirm            bit                  null,
   FundsLogId           int                  null,
   Memo                 varchar(4000)        null,
   ConfirmMemo          varchar(4000)        null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '虚拟收付款',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟收款序号',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'VirtualId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'PaymentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'PayBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否确认',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'IsConfirm'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'FundsLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟收付款备注',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟收付款确认备注',
   'user', 'dbo', 'table', 'Fun_PaymentVirtual', 'column', 'ConfirmMemo'
go

alter table dbo.Fun_PaymentVirtual
   add constraint PK_FUN_PAYMENTVIRTUAL primary key (VirtualId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentVirtualUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentVirtualGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentVirtualGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentVirtualUpdateStatus
// 存储过程功能描述：更新Fun_PaymentVirtual中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentVirtual'

set @str = 'update [dbo].[Fun_PaymentVirtual] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where VirtualId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentVirtualGoBack
// 存储过程功能描述：撤返Fun_PaymentVirtual，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentVirtual'

set @str = 'update [dbo].[Fun_PaymentVirtual] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where VirtualId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentVirtualGet
// 存储过程功能描述：查询指定Fun_PaymentVirtual的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualGet
    /*
	@VirtualId int
    */
    @id int
AS

SELECT
	[VirtualId],
	[PaymentId],
	[PayApplyId],
	[PayBala],
	[DetailStatus],
	[IsConfirm],
	[Memo],
	[ConfirmMemo],
	[FundsLogId]
FROM
	[dbo].[Fun_PaymentVirtual]
WHERE
	[VirtualId] = @id

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
// 存储过程名：[dbo].Fun_PaymentVirtualLoad
// 存储过程功能描述：查询所有Fun_PaymentVirtual记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualLoad
AS

SELECT
	[VirtualId],
	[PaymentId],
	[PayApplyId],
	[PayBala],
	[DetailStatus],
	[IsConfirm],
	[Memo],
	[ConfirmMemo],
	[FundsLogId]
FROM
	[dbo].[Fun_PaymentVirtual]

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
// 存储过程名：[dbo].Fun_PaymentVirtualInsert
// 存储过程功能描述：新增一条Fun_PaymentVirtual记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualInsert
	@PaymentId int ,
	@PayApplyId int =NULL ,
	@PayBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@IsConfirm bit =NULL ,
	@Memo varchar(4000) =NULL ,
	@ConfirmMemo varchar(4000) =NULL ,
	@FundsLogId int =NULL ,
	@VirtualId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PaymentVirtual] (
	[PaymentId],
	[PayApplyId],
	[PayBala],
	[DetailStatus],
	[IsConfirm],
	[Memo],
	[ConfirmMemo],
	[FundsLogId]
) VALUES (
	@PaymentId,
	@PayApplyId,
	@PayBala,
	@DetailStatus,
	@IsConfirm,
	@Memo,
	@ConfirmMemo,
	@FundsLogId
)


SET @VirtualId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_PaymentVirtualUpdate
// 存储过程功能描述：更新Fun_PaymentVirtual
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentVirtualUpdate
    @VirtualId int,
@PaymentId int,
@PayApplyId int = NULL,
@PayBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@IsConfirm bit = NULL,
@Memo varchar(4000) = NULL,
@ConfirmMemo varchar(4000) = NULL,
@FundsLogId int = NULL
AS

UPDATE [dbo].[Fun_PaymentVirtual] SET
	[PaymentId] = @PaymentId,
	[PayApplyId] = @PayApplyId,
	[PayBala] = @PayBala,
	[DetailStatus] = @DetailStatus,
	[IsConfirm] = @IsConfirm,
	[Memo] = @Memo,
	[ConfirmMemo] = @ConfirmMemo,
	[FundsLogId] = @FundsLogId
WHERE
	[VirtualId] = @VirtualId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



