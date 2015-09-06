alter table dbo.Fun_CashInAllot
   drop constraint PK_FUN_CASHINALLOT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_CashInAllot')
            and   type = 'U')
   drop table dbo.Fun_CashInAllot
go

/*==============================================================*/
/* Table: Fun_CashInAllot                                       */
/*==============================================================*/
create table dbo.Fun_CashInAllot (
   AllotId              int                  identity,
   AllotBala            numeric(18,4)        null,
   AllotType            int                  null,
   CurrencyId           int                  null,
   AllotDesc            varchar(400)         null,
   Alloter              int                  null,
   AllotTime            datetime             null,
   AllotStatus          int                  null,
   AllotFrom            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '收款分配',
   'user', 'dbo', 'table', 'Fun_CashInAllot'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款分配序号',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotId'
go

execute sp_addextendedproperty 'MS_Description', 
   '分配金额',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '分配类型',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotType'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '分配人',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'Alloter'
go

execute sp_addextendedproperty 'MS_Description', 
   '分配时间',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款分配状态',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '分配来源',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'AllotFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Fun_CashInAllot', 'column', 'LastModifyTime'
go

alter table dbo.Fun_CashInAllot
   add constraint PK_FUN_CASHINALLOT primary key (AllotId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInAllotGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAllotLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAllotInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAllotUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAllotUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInAllotUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInAllotGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInAllotGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInAllotUpdateStatus
// 存储过程功能描述：更新Fun_CashInAllot中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInAllot'

set @str = 'update [dbo].[Fun_CashInAllot] '+
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
// 存储过程名：[dbo].Fun_CashInAllotGoBack
// 存储过程功能描述：撤返Fun_CashInAllot，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInAllot'

set @str = 'update [dbo].[Fun_CashInAllot] '+
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
// 存储过程名：[dbo].Fun_CashInAllotGet
// 存储过程功能描述：查询指定Fun_CashInAllot的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotGet
    /*
	@AllotId int
    */
    @id int
AS

SELECT
	[AllotId],
	[AllotBala],
	[AllotType],
	[CurrencyId],
	[AllotDesc],
	[Alloter],
	[AllotTime],
	[AllotStatus],
	[AllotFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_CashInAllot]
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
// 存储过程名：[dbo].Fun_CashInAllotLoad
// 存储过程功能描述：查询所有Fun_CashInAllot记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotLoad
AS

SELECT
	[AllotId],
	[AllotBala],
	[AllotType],
	[CurrencyId],
	[AllotDesc],
	[Alloter],
	[AllotTime],
	[AllotStatus],
	[AllotFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_CashInAllot]

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
// 存储过程名：[dbo].Fun_CashInAllotInsert
// 存储过程功能描述：新增一条Fun_CashInAllot记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotInsert
	@AllotBala numeric(18, 4) =NULL ,
	@AllotType int =NULL ,
	@CurrencyId int =NULL ,
	@AllotDesc varchar(400) =NULL ,
	@Alloter int =NULL ,
	@AllotTime datetime =NULL ,
	@AllotStatus int =NULL ,
	@AllotFrom int =NULL ,
	@CreatorId int =NULL ,
	@AllotId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInAllot] (
	[AllotBala],
	[AllotType],
	[CurrencyId],
	[AllotDesc],
	[Alloter],
	[AllotTime],
	[AllotStatus],
	[AllotFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AllotBala,
	@AllotType,
	@CurrencyId,
	@AllotDesc,
	@Alloter,
	@AllotTime,
	@AllotStatus,
	@AllotFrom,
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
// 存储过程名：[dbo].Fun_CashInAllotUpdate
// 存储过程功能描述：更新Fun_CashInAllot
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInAllotUpdate
    @AllotId int,
@AllotBala numeric(18, 4) = NULL,
@AllotType int = NULL,
@CurrencyId int = NULL,
@AllotDesc varchar(400) = NULL,
@Alloter int = NULL,
@AllotTime datetime = NULL,
@AllotStatus int = NULL,
@AllotFrom int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fun_CashInAllot] SET
	[AllotBala] = @AllotBala,
	[AllotType] = @AllotType,
	[CurrencyId] = @CurrencyId,
	[AllotDesc] = @AllotDesc,
	[Alloter] = @Alloter,
	[AllotTime] = @AllotTime,
	[AllotStatus] = @AllotStatus,
	[AllotFrom] = @AllotFrom,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AllotId] = @AllotId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



