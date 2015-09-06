alter table dbo.Con_ContractClause_Ref
   drop constraint PK_CON_CONTRACTCLAUSE_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractClause_Ref')
            and   type = 'U')
   drop table dbo.Con_ContractClause_Ref
go

/*==============================================================*/
/* Table: Con_ContractClause_Ref                                */
/*==============================================================*/
create table dbo.Con_ContractClause_Ref (
   RefId                int                  not null,
   ContractId           int                  null,
   MasterId             int                  null,
   ClauseId             int                  null,
   RefStatus            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约条款关联',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'RefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '条款模版序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'MasterId'
go

execute sp_addextendedproperty 'MS_Description', 
   '条款序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'ClauseId'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'RefStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractClause_Ref', 'column', 'LastModifyTime'
go

alter table dbo.Con_ContractClause_Ref
   add constraint PK_CON_CONTRACTCLAUSE_REF primary key (RefId)
go

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefGet    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefLoad    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefInsert    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefUpdate    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefUpdateStatus    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractClause_RefUpdateStatus    Script Date: 2015年2月3日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractClause_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractClause_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractClause_RefUpdateStatus
// 存储过程功能描述：更新Con_ContractClause_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractClause_Ref'

set @str = 'update [dbo].[Con_ContractClause_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractClause_RefGoBack
// 存储过程功能描述：撤返Con_ContractClause_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractClause_Ref'

set @str = 'update [dbo].[Con_ContractClause_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractClause_RefGet
// 存储过程功能描述：查询指定Con_ContractClause_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[ContractId],
	[MasterId],
	[ClauseId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractClause_Ref]
WHERE
	[RefId] = @id

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
// 存储过程名：[dbo].Con_ContractClause_RefLoad
// 存储过程功能描述：查询所有Con_ContractClause_Ref记录
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefLoad
AS

SELECT
	[RefId],
	[ContractId],
	[MasterId],
	[ClauseId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractClause_Ref]

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
// 存储过程名：[dbo].Con_ContractClause_RefInsert
// 存储过程功能描述：新增一条Con_ContractClause_Ref记录
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefInsert
	@ContractId int =NULL ,
	@MasterId int =NULL ,
	@ClauseId int =NULL ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractClause_Ref] (
	[ContractId],
	[MasterId],
	[ClauseId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@MasterId,
	@ClauseId,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RefId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractClause_RefUpdate
// 存储过程功能描述：更新Con_ContractClause_Ref
// 创建人：CodeSmith
// 创建时间： 2015年2月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractClause_RefUpdate
    @RefId int,
@ContractId int = NULL,
@MasterId int = NULL,
@ClauseId int = NULL,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_ContractClause_Ref] SET
	[ContractId] = @ContractId,
	[MasterId] = @MasterId,
	[ClauseId] = @ClauseId,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



