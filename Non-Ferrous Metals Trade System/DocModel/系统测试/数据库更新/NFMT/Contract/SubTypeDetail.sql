alter table dbo.Con_SubTypeDetail
   drop constraint PK_CON_SUBTYPEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_SubTypeDetail')
            and   type = 'U')
   drop table dbo.Con_SubTypeDetail
go

/*==============================================================*/
/* Table: Con_SubTypeDetail                                     */
/*==============================================================*/
create table dbo.Con_SubTypeDetail (
   DetailId             int                  identity,
   ContractId           int                  null,
   SubId                int                  null,
   ContractType         int                  null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '子合约类型明细',
   'user', 'dbo', 'table', 'Con_SubTypeDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约类型',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'ContractType'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_SubTypeDetail', 'column', 'LastModifyTime'
go

alter table dbo.Con_SubTypeDetail
   add constraint PK_CON_SUBTYPEDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailGet    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailLoad    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailInsert    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailUpdate    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailUpdateStatus    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_SubTypeDetailUpdateStatus    Script Date: 2015年3月12日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubTypeDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubTypeDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_SubTypeDetailUpdateStatus
// 存储过程功能描述：更新Con_SubTypeDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubTypeDetail'

set @str = 'update [dbo].[Con_SubTypeDetail] '+
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
// 存储过程名：[dbo].Con_SubTypeDetailGoBack
// 存储过程功能描述：撤返Con_SubTypeDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubTypeDetail'

set @str = 'update [dbo].[Con_SubTypeDetail] '+
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
// 存储过程名：[dbo].Con_SubTypeDetailGet
// 存储过程功能描述：查询指定Con_SubTypeDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractId],
	[SubId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_SubTypeDetail]
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
// 存储过程名：[dbo].Con_SubTypeDetailLoad
// 存储过程功能描述：查询所有Con_SubTypeDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailLoad
AS

SELECT
	[DetailId],
	[ContractId],
	[SubId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_SubTypeDetail]

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
// 存储过程名：[dbo].Con_SubTypeDetailInsert
// 存储过程功能描述：新增一条Con_SubTypeDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailInsert
	@ContractId int =NULL ,
	@SubId int =NULL ,
	@ContractType int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_SubTypeDetail] (
	[ContractId],
	[SubId],
	[ContractType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@SubId,
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
// 存储过程名：[dbo].Con_SubTypeDetailUpdate
// 存储过程功能描述：更新Con_SubTypeDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubTypeDetailUpdate
    @DetailId int,
@ContractId int = NULL,
@SubId int = NULL,
@ContractType int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_SubTypeDetail] SET
	[ContractId] = @ContractId,
	[SubId] = @SubId,
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



