alter table Inv_FinBusInvAllot
   drop constraint PK_INV_FINBUSINVALLOT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Inv_FinBusInvAllot')
            and   type = 'U')
   drop table Inv_FinBusInvAllot
go

/*==============================================================*/
/* Table: Inv_FinBusInvAllot                                    */
/*==============================================================*/
create table Inv_FinBusInvAllot (
   AllotId              int                  identity,
   AllotBala            numeric(18,4)        null,
   CurrencyId           int                  null,
   Alloter              int                  null,
   AllotDate            DateTime             null,
   AllotStatus          int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '业务发票财务发票分配',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'AllotId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '金额',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'AllotBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配人',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'Alloter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配时间',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'AllotDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配状态',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'AllotStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllot', 'column', 'LastModifyTime'
go

alter table Inv_FinBusInvAllot
   add constraint PK_INV_FINBUSINVALLOT primary key (AllotId)
go


/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_FinBusInvAllotUpdateStatus
// 存储过程功能描述：更新Inv_FinBusInvAllot中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinBusInvAllot'

set @str = 'update [dbo].[Inv_FinBusInvAllot] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AllotId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_FinBusInvAllotGoBack
// 存储过程功能描述：撤返Inv_FinBusInvAllot，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinBusInvAllot'

set @str = 'update [dbo].[Inv_FinBusInvAllot] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AllotId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_FinBusInvAllotGet
// 存储过程功能描述：查询指定Inv_FinBusInvAllot的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotGet
    /*
	@AllotId int
    */
    @id int
AS

SELECT
	[AllotId],
	[AllotBala],
	[CurrencyId],
	[Alloter],
	[AllotDate],
	[AllotStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_FinBusInvAllot]
WHERE
	[AllotId] = @id

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
// 存储过程名：[dbo].Inv_FinBusInvAllotLoad
// 存储过程功能描述：查询所有Inv_FinBusInvAllot记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotLoad
AS

SELECT
	[AllotId],
	[AllotBala],
	[CurrencyId],
	[Alloter],
	[AllotDate],
	[AllotStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_FinBusInvAllot]

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
// 存储过程名：[dbo].Inv_FinBusInvAllotInsert
// 存储过程功能描述：新增一条Inv_FinBusInvAllot记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotInsert
	@AllotBala numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@Alloter int =NULL ,
	@AllotDate datetime =NULL ,
	@AllotStatus int =NULL ,
	@CreatorId int =NULL ,
	@AllotId int OUTPUT
AS

INSERT INTO [dbo].[Inv_FinBusInvAllot] (
	[AllotBala],
	[CurrencyId],
	[Alloter],
	[AllotDate],
	[AllotStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AllotBala,
	@CurrencyId,
	@Alloter,
	@AllotDate,
	@AllotStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AllotId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_FinBusInvAllotUpdate
// 存储过程功能描述：更新Inv_FinBusInvAllot
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotUpdate
    @AllotId int,
@AllotBala numeric(18, 4) = NULL,
@CurrencyId int = NULL,
@Alloter int = NULL,
@AllotDate datetime = NULL,
@AllotStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Inv_FinBusInvAllot] SET
	[AllotBala] = @AllotBala,
	[CurrencyId] = @CurrencyId,
	[Alloter] = @Alloter,
	[AllotDate] = @AllotDate,
	[AllotStatus] = @AllotStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AllotId] = @AllotId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



