alter table dbo.Fun_Funds
   drop constraint PK_FUN_FUNDS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_Funds')
            and   type = 'U')
   drop table dbo.Fun_Funds
go

/*==============================================================*/
/* Table: Fun_Funds                                             */
/*==============================================================*/
create table dbo.Fun_Funds (
   FundsId              int                  identity,
   FundsLimitd          int                  null,
   BlocId               int                  null,
   CorpId               int                  null,
   FundsValue           numeric(18,4)        null,
   CurrencyId           int                  null,
   FundsType            int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '资金',
   'user', 'dbo', 'table', 'Fun_Funds'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金序号',
   'user', 'dbo', 'table', 'Fun_Funds', 'column', 'FundsId'
go

execute sp_addextendedproperty 'MS_Description', 
   '集团序号',
   'user', 'dbo', 'table', 'Fun_Funds', 'column', 'BlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'Fun_Funds', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'Fun_Funds', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金类型
   现金、本票、支票、信用证',
   'user', 'dbo', 'table', 'Fun_Funds', 'column', 'FundsType'
go

alter table dbo.Fun_Funds
   add constraint PK_FUN_FUNDS primary key (FundsId)
go


/****** Object:  Stored Procedure [dbo].Fun_FundsGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_FundsUpdateStatus
// 存储过程功能描述：更新Fun_Funds中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_Funds'

set @str = 'update [dbo].[Fun_Funds] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FundsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsGoBack
// 存储过程功能描述：撤返Fun_Funds，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_Funds'

set @str = 'update [dbo].[Fun_Funds] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FundsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsGet
// 存储过程功能描述：查询指定Fun_Funds的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsGet
    /*
	@FundsId int
    */
    @id int
AS

SELECT
	[FundsId],
	[FundsLimitd],
	[BlocId],
	[CorpId],
	[FundsValue],
	[CurrencyId],
	[FundsType]
FROM
	[dbo].[Fun_Funds]
WHERE
	[FundsId] = @id

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
// 存储过程名：[dbo].Fun_FundsLoad
// 存储过程功能描述：查询所有Fun_Funds记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLoad
AS

SELECT
	[FundsId],
	[FundsLimitd],
	[BlocId],
	[CorpId],
	[FundsValue],
	[CurrencyId],
	[FundsType]
FROM
	[dbo].[Fun_Funds]

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
// 存储过程名：[dbo].Fun_FundsInsert
// 存储过程功能描述：新增一条Fun_Funds记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsInsert
	@FundsLimitd int =NULL ,
	@BlocId int =NULL ,
	@CorpId int =NULL ,
	@FundsValue numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@FundsType int =NULL ,
	@FundsId int OUTPUT
AS

INSERT INTO [dbo].[Fun_Funds] (
	[FundsLimitd],
	[BlocId],
	[CorpId],
	[FundsValue],
	[CurrencyId],
	[FundsType]
) VALUES (
	@FundsLimitd,
	@BlocId,
	@CorpId,
	@FundsValue,
	@CurrencyId,
	@FundsType
)


SET @FundsId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_FundsUpdate
// 存储过程功能描述：更新Fun_Funds
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsUpdate
    @FundsId int,
@FundsLimitd int = NULL,
@BlocId int = NULL,
@CorpId int = NULL,
@FundsValue numeric(18, 4) = NULL,
@CurrencyId int = NULL,
@FundsType int = NULL
AS

UPDATE [dbo].[Fun_Funds] SET
	[FundsLimitd] = @FundsLimitd,
	[BlocId] = @BlocId,
	[CorpId] = @CorpId,
	[FundsValue] = @FundsValue,
	[CurrencyId] = @CurrencyId,
	[FundsType] = @FundsType
WHERE
	[FundsId] = @FundsId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



