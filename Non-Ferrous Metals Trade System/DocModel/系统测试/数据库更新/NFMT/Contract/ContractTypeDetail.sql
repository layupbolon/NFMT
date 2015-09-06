alter table dbo.Con_ContractTypeDetail
   drop constraint PK_CON_CONTRACTTYPEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractTypeDetail')
            and   type = 'U')
   drop table dbo.Con_ContractTypeDetail
go

/*==============================================================*/
/* Table: Con_ContractTypeDetail                                */
/*==============================================================*/
create table dbo.Con_ContractTypeDetail (
   DetailId             int                  identity,
   ContractId           int                  null,
   ContractType         int                  null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约类型明细',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约类型',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'ContractType'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractTypeDetail', 'column', 'LastModifyTime'
go

alter table dbo.Con_ContractTypeDetail
   add constraint PK_CON_CONTRACTTYPEDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailGet    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailLoad    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailInsert    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailUpdate    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailUpdateStatus    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractTypeDetailUpdateStatus    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractTypeDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractTypeDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractTypeDetailUpdateStatus
// 存储过程功能描述：更新Con_ContractTypeDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractTypeDetail'

set @str = 'update [dbo].[Con_ContractTypeDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractTypeDetailGoBack
// 存储过程功能描述：撤返Con_ContractTypeDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractTypeDetail'

set @str = 'update [dbo].[Con_ContractTypeDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractTypeDetailGet
// 存储过程功能描述：查询指定Con_ContractTypeDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractTypeDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].Con_ContractTypeDetailLoad
// 存储过程功能描述：查询所有Con_ContractTypeDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailLoad
AS

SELECT
	[DetailId],
	[ContractId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractTypeDetail]

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
// 存储过程名：[dbo].Con_ContractTypeDetailInsert
// 存储过程功能描述：新增一条Con_ContractTypeDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailInsert
	@ContractId int =NULL ,
	@ContractType int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractTypeDetail] (
	[ContractId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@ContractType,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractTypeDetailUpdate
// 存储过程功能描述：更新Con_ContractTypeDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractTypeDetailUpdate
    @DetailId int,
@ContractId int = NULL,
@ContractType int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_ContractTypeDetail] SET
	[ContractId] = @ContractId,
	[ContractType] = @ContractType,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



