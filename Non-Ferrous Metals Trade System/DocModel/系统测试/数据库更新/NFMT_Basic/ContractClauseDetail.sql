alter table dbo.ContractClauseDetail
   drop constraint PK_CONTRACTCLAUSEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ContractClauseDetail')
            and   type = 'U')
   drop table dbo.ContractClauseDetail
go

/*==============================================================*/
/* Table: ContractClauseDetail                                  */
/*==============================================================*/
create table dbo.ContractClauseDetail (
   ClauseDetailId       int                  identity,
   ClauseId             int                  null,
   DetailDisplayType    int                  null,
   DetailDataType       int                  null,
   FormatSerial         int                  null,
   DetailValue          varchar(200)         null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约条款明细',
   'user', 'dbo', 'table', 'ContractClauseDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约条款序号',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'ClauseDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细显示类型',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'DetailDisplayType'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细数据类型',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'DetailDataType'
go

execute sp_addextendedproperty 'MS_Description', 
   '格式序号',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'FormatSerial'
go

execute sp_addextendedproperty 'MS_Description', 
   '显示数据',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'DetailValue'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'ContractClauseDetail', 'column', 'LastModifyTime'
go

alter table dbo.ContractClauseDetail
   add constraint PK_CONTRACTCLAUSEDETAIL primary key (ClauseDetailId)
go


/****** Object:  Stored Procedure [dbo].ContractClauseDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailGet]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseDetailUpdateStatus
// 存储过程功能描述：更新ContractClauseDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractClauseDetail'

set @str = 'update [dbo].[ContractClauseDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ClauseDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractClauseDetailGoBack
// 存储过程功能描述：撤返ContractClauseDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractClauseDetail'

set @str = 'update [dbo].[ContractClauseDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ClauseDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractClauseDetailGet
// 存储过程功能描述：查询指定ContractClauseDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailGet
    /*
	@ClauseDetailId int
    */
    @id int
AS

SELECT
	[ClauseDetailId],
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClauseDetail]
WHERE
	[ClauseDetailId] = @id

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
// 存储过程名：[dbo].ContractClauseDetailLoad
// 存储过程功能描述：查询所有ContractClauseDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailLoad
AS

SELECT
	[ClauseDetailId],
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClauseDetail]

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
// 存储过程名：[dbo].ContractClauseDetailInsert
// 存储过程功能描述：新增一条ContractClauseDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailInsert
	@ClauseId int =NULL ,
	@DetailDisplayType int =NULL ,
	@DetailDataType int =NULL ,
	@FormatSerial int =NULL ,
	@DetailValue varchar(200) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@ClauseDetailId int OUTPUT
AS

INSERT INTO [dbo].[ContractClauseDetail] (
	[ClauseId],
	[DetailDisplayType],
	[DetailDataType],
	[FormatSerial],
	[DetailValue],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ClauseId,
	@DetailDisplayType,
	@DetailDataType,
	@FormatSerial,
	@DetailValue,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ClauseDetailId = @@IDENTITY

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
// 存储过程名：[dbo].ContractClauseDetailUpdate
// 存储过程功能描述：更新ContractClauseDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseDetailUpdate
    @ClauseDetailId int,
@ClauseId int = NULL,
@DetailDisplayType int = NULL,
@DetailDataType int = NULL,
@FormatSerial int = NULL,
@DetailValue varchar(200) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractClauseDetail] SET
	[ClauseId] = @ClauseId,
	[DetailDisplayType] = @DetailDisplayType,
	[DetailDataType] = @DetailDataType,
	[FormatSerial] = @FormatSerial,
	[DetailValue] = @DetailValue,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ClauseDetailId] = @ClauseDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



