alter table dbo.Con_ContractSubAttach
   drop constraint PK_CON_CONTRACTSUBATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractSubAttach')
            and   type = 'U')
   drop table dbo.Con_ContractSubAttach
go

/*==============================================================*/
/* Table: Con_ContractSubAttach                                 */
/*==============================================================*/
create table dbo.Con_ContractSubAttach (
   SubAttachId          int                  identity,
   SubId                int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '子合约附件',
   'user', 'dbo', 'table', 'Con_ContractSubAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约附件序号',
   'user', 'dbo', 'table', 'Con_ContractSubAttach', 'column', 'SubAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Con_ContractSubAttach', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Con_ContractSubAttach', 'column', 'AttachId'
go

alter table dbo.Con_ContractSubAttach
   add constraint PK_CON_CONTRACTSUBATTACH primary key (SubAttachId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractSubAttachUpdateStatus
// 存储过程功能描述：更新Con_ContractSubAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSubAttach'

set @str = 'update [dbo].[Con_ContractSubAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SubAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubAttachGoBack
// 存储过程功能描述：撤返Con_ContractSubAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSubAttach'

set @str = 'update [dbo].[Con_ContractSubAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SubAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubAttachGet
// 存储过程功能描述：查询指定Con_ContractSubAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachGet
    /*
	@SubAttachId int
    */
    @id int
AS

SELECT
	[SubAttachId],
	[SubId],
	[AttachId]
FROM
	[dbo].[Con_ContractSubAttach]
WHERE
	[SubAttachId] = @id

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
// 存储过程名：[dbo].Con_ContractSubAttachLoad
// 存储过程功能描述：查询所有Con_ContractSubAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachLoad
AS

SELECT
	[SubAttachId],
	[SubId],
	[AttachId]
FROM
	[dbo].[Con_ContractSubAttach]

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
// 存储过程名：[dbo].Con_ContractSubAttachInsert
// 存储过程功能描述：新增一条Con_ContractSubAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachInsert
	@SubId int =NULL ,
	@AttachId int =NULL ,
	@SubAttachId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractSubAttach] (
	[SubId],
	[AttachId]
) VALUES (
	@SubId,
	@AttachId
)


SET @SubAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractSubAttachUpdate
// 存储过程功能描述：更新Con_ContractSubAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubAttachUpdate
    @SubAttachId int,
@SubId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Con_ContractSubAttach] SET
	[SubId] = @SubId,
	[AttachId] = @AttachId
WHERE
	[SubAttachId] = @SubAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



