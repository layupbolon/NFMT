alter table dbo.ContractMaster
   drop constraint PK_CONTRACTMASTER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ContractMaster')
            and   type = 'U')
   drop table dbo.ContractMaster
go

/*==============================================================*/
/* Table: ContractMaster                                        */
/*==============================================================*/
create table dbo.ContractMaster (
   MasterId             int                  identity,
   MasterName           varchar(200)         null,
   MasterEname          varchar(200)         null,
   MasterStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约模板',
   'user', 'dbo', 'table', 'ContractMaster'
go

execute sp_addextendedproperty 'MS_Description', 
   '模板序号',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'MasterId'
go

execute sp_addextendedproperty 'MS_Description', 
   '模板名称',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'MasterName'
go

execute sp_addextendedproperty 'MS_Description', 
   '模板英文名称',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'MasterEname'
go

execute sp_addextendedproperty 'MS_Description', 
   '模板状态',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'MasterStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'ContractMaster', 'column', 'LastModifyTime'
go

alter table dbo.ContractMaster
   add constraint PK_CONTRACTMASTER primary key (MasterId)
go


/****** Object:  Stored Procedure [dbo].ContractMasterGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterGet]
GO

/****** Object:  Stored Procedure [dbo].ContractMasterLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterLoad]
GO

/****** Object:  Stored Procedure [dbo].ContractMasterInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterInsert]
GO

/****** Object:  Stored Procedure [dbo].ContractMasterUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterUpdate]
GO

/****** Object:  Stored Procedure [dbo].ContractMasterUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ContractMasterUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractMasterGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractMasterGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractMasterUpdateStatus
// 存储过程功能描述：更新ContractMaster中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractMaster'

set @str = 'update [dbo].[ContractMaster] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MasterId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractMasterGoBack
// 存储过程功能描述：撤返ContractMaster，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractMaster'

set @str = 'update [dbo].[ContractMaster] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MasterId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractMasterGet
// 存储过程功能描述：查询指定ContractMaster的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterGet
    /*
	@MasterId int
    */
    @id int
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractMaster]
WHERE
	[MasterId] = @id

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
// 存储过程名：[dbo].ContractMasterLoad
// 存储过程功能描述：查询所有ContractMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterLoad
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractMaster]

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
// 存储过程名：[dbo].ContractMasterInsert
// 存储过程功能描述：新增一条ContractMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterInsert
	@MasterName varchar(200) =NULL ,
	@MasterEname varchar(200) =NULL ,
	@MasterStatus int =NULL ,
	@CreatorId int =NULL ,
	@MasterId int OUTPUT
AS

INSERT INTO [dbo].[ContractMaster] (
	[MasterName],
	[MasterEname],
	[MasterStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterName,
	@MasterEname,
	@MasterStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MasterId = @@IDENTITY

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
// 存储过程名：[dbo].ContractMasterUpdate
// 存储过程功能描述：更新ContractMaster
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractMasterUpdate
    @MasterId int,
@MasterName varchar(200) = NULL,
@MasterEname varchar(200) = NULL,
@MasterStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractMaster] SET
	[MasterName] = @MasterName,
	[MasterEname] = @MasterEname,
	[MasterStatus] = @MasterStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MasterId] = @MasterId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



