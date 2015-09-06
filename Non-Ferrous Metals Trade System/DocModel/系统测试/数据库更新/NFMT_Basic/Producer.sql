alter table dbo.Producer
   drop constraint PK_PRODUCER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Producer')
            and   type = 'U')
   drop table dbo.Producer
go

/*==============================================================*/
/* Table: Producer                                              */
/*==============================================================*/
create table dbo.Producer (
   ProducerId           int                  identity,
   ProducerName         varchar(80)          null,
   ProducerFullName     varchar(400)         null,
   ProducerShort        varchar(80)          null,
   ProducerArea         int                  null,
   ProducerStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '生产商',
   'user', 'dbo', 'table', 'Producer'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商序号',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商名称',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerName'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商全称',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商简称',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerShort'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商地区',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerArea'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商状态',
   'user', 'dbo', 'table', 'Producer', 'column', 'ProducerStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Producer', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Producer', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Producer', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Producer', 'column', 'LastModifyTime'
go

alter table dbo.Producer
   add constraint PK_PRODUCER primary key (ProducerId)
go


/****** Object:  Stored Procedure [dbo].ProducerGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerGet]
GO

/****** Object:  Stored Procedure [dbo].ProducerLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerLoad]
GO

/****** Object:  Stored Procedure [dbo].ProducerInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerInsert]
GO

/****** Object:  Stored Procedure [dbo].ProducerUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerUpdate]
GO

/****** Object:  Stored Procedure [dbo].ProducerUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ProducerUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProducerGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProducerGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ProducerUpdateStatus
// 存储过程功能描述：更新Producer中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Producer'

set @str = 'update [dbo].[Producer] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ProducerId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ProducerGoBack
// 存储过程功能描述：撤返Producer，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Producer'

set @str = 'update [dbo].[Producer] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ProducerId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ProducerGet
// 存储过程功能描述：查询指定Producer的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerGet
    /*
	@ProducerId int
    */
    @id int
AS

SELECT
	[ProducerId],
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Producer]
WHERE
	[ProducerId] = @id

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
// 存储过程名：[dbo].ProducerLoad
// 存储过程功能描述：查询所有Producer记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerLoad
AS

SELECT
	[ProducerId],
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Producer]

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
// 存储过程名：[dbo].ProducerInsert
// 存储过程功能描述：新增一条Producer记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerInsert
	@ProducerName varchar(80) =NULL ,
	@ProducerFullName varchar(400) =NULL ,
	@ProducerShort varchar(80) =NULL ,
	@ProducerArea int =NULL ,
	@ProducerStatus int =NULL ,
	@CreatorId int =NULL ,
	@ProducerId int OUTPUT
AS

INSERT INTO [dbo].[Producer] (
	[ProducerName],
	[ProducerFullName],
	[ProducerShort],
	[ProducerArea],
	[ProducerStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ProducerName,
	@ProducerFullName,
	@ProducerShort,
	@ProducerArea,
	@ProducerStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ProducerId = @@IDENTITY

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
// 存储过程名：[dbo].ProducerUpdate
// 存储过程功能描述：更新Producer
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ProducerUpdate
    @ProducerId int,
@ProducerName varchar(80) = NULL,
@ProducerFullName varchar(400) = NULL,
@ProducerShort varchar(80) = NULL,
@ProducerArea int = NULL,
@ProducerStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Producer] SET
	[ProducerName] = @ProducerName,
	[ProducerFullName] = @ProducerFullName,
	[ProducerShort] = @ProducerShort,
	[ProducerArea] = @ProducerArea,
	[ProducerStatus] = @ProducerStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ProducerId] = @ProducerId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



