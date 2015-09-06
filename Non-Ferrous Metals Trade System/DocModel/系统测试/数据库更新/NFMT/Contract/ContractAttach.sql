alter table dbo.Con_ContractAttach
   drop constraint PK_CON_CONTRACTATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractAttach')
            and   type = 'U')
   drop table dbo.Con_ContractAttach
go

/*==============================================================*/
/* Table: Con_ContractAttach                                    */
/*==============================================================*/
create table dbo.Con_ContractAttach (
   ContractAttachId     int                  identity,
   ContractId           int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约附件',
   'user', 'dbo', 'table', 'Con_ContractAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约附件序号',
   'user', 'dbo', 'table', 'Con_ContractAttach', 'column', 'ContractAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractAttach', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件主表序号',
   'user', 'dbo', 'table', 'Con_ContractAttach', 'column', 'AttachId'
go

alter table dbo.Con_ContractAttach
   add constraint PK_CON_CONTRACTATTACH primary key (ContractAttachId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractAttachUpdateStatus
// 存储过程功能描述：更新Con_ContractAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractAttach'

set @str = 'update [dbo].[Con_ContractAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ContractAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractAttachGoBack
// 存储过程功能描述：撤返Con_ContractAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractAttach'

set @str = 'update [dbo].[Con_ContractAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ContractAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractAttachGet
// 存储过程功能描述：查询指定Con_ContractAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachGet
    /*
	@ContractAttachId int
    */
    @id int
AS

SELECT
	[ContractAttachId],
	[ContractId],
	[AttachId]
FROM
	[dbo].[Con_ContractAttach]
WHERE
	[ContractAttachId] = @id

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
// 存储过程名：[dbo].Con_ContractAttachLoad
// 存储过程功能描述：查询所有Con_ContractAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachLoad
AS

SELECT
	[ContractAttachId],
	[ContractId],
	[AttachId]
FROM
	[dbo].[Con_ContractAttach]

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
// 存储过程名：[dbo].Con_ContractAttachInsert
// 存储过程功能描述：新增一条Con_ContractAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachInsert
	@ContractId int =NULL ,
	@AttachId int =NULL ,
	@ContractAttachId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractAttach] (
	[ContractId],
	[AttachId]
) VALUES (
	@ContractId,
	@AttachId
)


SET @ContractAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractAttachUpdate
// 存储过程功能描述：更新Con_ContractAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractAttachUpdate
    @ContractAttachId int,
@ContractId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Con_ContractAttach] SET
	[ContractId] = @ContractId,
	[AttachId] = @AttachId
WHERE
	[ContractAttachId] = @ContractAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



