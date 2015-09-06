alter table dbo.Inv_SI
   drop constraint PK_INV_SI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_SI')
            and   type = 'U')
   drop table dbo.Inv_SI
go

/*==============================================================*/
/* Table: Inv_SI                                                */
/*==============================================================*/
create table dbo.Inv_SI (
   SIId                 int                  identity,
   InvoiceId            int                  null,
   ChangeCurrencyId     int                  null,
   ChangeRate           numeric(18,4)        null,
   ChangeBala           numeric(18,4)        null,
   PayDept              int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '价外票',
   'user', 'dbo', 'table', 'Inv_SI'
go

execute sp_addextendedproperty 'MS_Description', 
   '价外票序号',
   'user', 'dbo', 'table', 'Inv_SI', 'column', 'SIId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'Inv_SI', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '折算币种',
   'user', 'dbo', 'table', 'Inv_SI', 'column', 'ChangeCurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇率',
   'user', 'dbo', 'table', 'Inv_SI', 'column', 'ChangeRate'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本部门',
   'user', 'dbo', 'table', 'Inv_SI', 'column', 'PayDept'
go

alter table dbo.Inv_SI
   add constraint PK_INV_SI primary key (SIId)
go


/****** Object:  Stored Procedure [dbo].Inv_SIGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_SILoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SILoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SILoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_SIUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_SIGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_SIGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_SIUpdateStatus
// 存储过程功能描述：更新Inv_SI中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_SI'

set @str = 'update [dbo].[Inv_SI] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SIId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_SIGoBack
// 存储过程功能描述：撤返Inv_SI，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_SI'

set @str = 'update [dbo].[Inv_SI] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SIId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_SIGet
// 存储过程功能描述：查询指定Inv_SI的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIGet
    /*
	@SIId int
    */
    @id int
AS

SELECT
	[SIId],
	[InvoiceId],
	[ChangeCurrencyId],
	[ChangeRate],
	[ChangeBala],
	[PayDept]
FROM
	[dbo].[Inv_SI]
WHERE
	[SIId] = @id

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
// 存储过程名：[dbo].Inv_SILoad
// 存储过程功能描述：查询所有Inv_SI记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SILoad
AS

SELECT
	[SIId],
	[InvoiceId],
	[ChangeCurrencyId],
	[ChangeRate],
	[ChangeBala],
	[PayDept]
FROM
	[dbo].[Inv_SI]

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
// 存储过程名：[dbo].Inv_SIInsert
// 存储过程功能描述：新增一条Inv_SI记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIInsert
	@InvoiceId int =NULL ,
	@ChangeCurrencyId int =NULL ,
	@ChangeRate numeric(18, 4) =NULL ,
	@ChangeBala numeric(18, 4) =NULL ,
	@PayDept int =NULL ,
	@SIId int OUTPUT
AS

INSERT INTO [dbo].[Inv_SI] (
	[InvoiceId],
	[ChangeCurrencyId],
	[ChangeRate],
	[ChangeBala],
	[PayDept]
) VALUES (
	@InvoiceId,
	@ChangeCurrencyId,
	@ChangeRate,
	@ChangeBala,
	@PayDept
)


SET @SIId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_SIUpdate
// 存储过程功能描述：更新Inv_SI
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_SIUpdate
    @SIId int,
@InvoiceId int = NULL,
@ChangeCurrencyId int = NULL,
@ChangeRate numeric(18, 4) = NULL,
@ChangeBala numeric(18, 4) = NULL,
@PayDept int = NULL
AS

UPDATE [dbo].[Inv_SI] SET
	[InvoiceId] = @InvoiceId,
	[ChangeCurrencyId] = @ChangeCurrencyId,
	[ChangeRate] = @ChangeRate,
	[ChangeBala] = @ChangeBala,
	[PayDept] = @PayDept
WHERE
	[SIId] = @SIId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



