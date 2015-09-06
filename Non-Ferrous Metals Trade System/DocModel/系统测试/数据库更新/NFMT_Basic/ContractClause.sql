alter table dbo.ContractClause
   drop constraint PK_CONTRACTCLAUSE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ContractClause')
            and   type = 'U')
   drop table dbo.ContractClause
go

/*==============================================================*/
/* Table: ContractClause                                        */
/*==============================================================*/
create table dbo.ContractClause (
   ClauseId             int                  identity,
   ClauseText           varchar(max)         null,
   ClauseEnText         varchar(max)         null,
   ClauseStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约条款',
   'user', 'dbo', 'table', 'ContractClause'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约条款序号',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'ClauseId'
go

execute sp_addextendedproperty 'MS_Description', 
   '条款内容',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'ClauseText'
go

execute sp_addextendedproperty 'MS_Description', 
   '条款英文内容',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'ClauseEnText'
go

execute sp_addextendedproperty 'MS_Description', 
   '条款状态',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'ClauseStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'ContractClause', 'column', 'LastModifyTime'
go

alter table dbo.ContractClause
   add constraint PK_CONTRACTCLAUSE primary key (ClauseId)
go


/****** Object:  Stored Procedure [dbo].ContractClauseGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseGet]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseLoad]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseInsert]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseUpdate]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ContractClauseUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ContractClauseGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ContractClauseGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContractClauseUpdateStatus
// 存储过程功能描述：更新ContractClause中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractClause'

set @str = 'update [dbo].[ContractClause] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ClauseId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractClauseGoBack
// 存储过程功能描述：撤返ContractClause，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ContractClause'

set @str = 'update [dbo].[ContractClause] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ClauseId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ContractClauseGet
// 存储过程功能描述：查询指定ContractClause的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseGet
    /*
	@ClauseId int
    */
    @id int
AS

SELECT
	[ClauseId],
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClause]
WHERE
	[ClauseId] = @id

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
// 存储过程名：[dbo].ContractClauseLoad
// 存储过程功能描述：查询所有ContractClause记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseLoad
AS

SELECT
	[ClauseId],
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ContractClause]

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
// 存储过程名：[dbo].ContractClauseInsert
// 存储过程功能描述：新增一条ContractClause记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseInsert
	@ClauseText varchar(max) =NULL ,
	@ClauseEnText varchar(max) =NULL ,
	@ClauseStatus int =NULL ,
	@CreatorId int =NULL ,
	@ClauseId int OUTPUT
AS

INSERT INTO [dbo].[ContractClause] (
	[ClauseText],
	[ClauseEnText],
	[ClauseStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ClauseText,
	@ClauseEnText,
	@ClauseStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ClauseId = @@IDENTITY

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
// 存储过程名：[dbo].ContractClauseUpdate
// 存储过程功能描述：更新ContractClause
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ContractClauseUpdate
    @ClauseId int,
@ClauseText varchar(max) = NULL,
@ClauseEnText varchar(max) = NULL,
@ClauseStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ContractClause] SET
	[ClauseText] = @ClauseText,
	[ClauseEnText] = @ClauseEnText,
	[ClauseStatus] = @ClauseStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ClauseId] = @ClauseId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



