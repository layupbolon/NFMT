alter table dbo.Fun_ContractPayApply_Ref
   drop constraint PK_FUN_CONTRACTPAYAPPLY_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_ContractPayApply_Ref')
            and   type = 'U')
   drop table dbo.Fun_ContractPayApply_Ref
go

/*==============================================================*/
/* Table: Fun_ContractPayApply_Ref                              */
/*==============================================================*/
create table dbo.Fun_ContractPayApply_Ref (
   RefId                int                  identity,
   PayApplyId           int                  null,
   ContractId           int                  null,
   ContractSubId        int                  null,
   ApplyBala            numeric(18,4)        null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约付款申请',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约付款申请序号',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref', 'column', 'RefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref', 'column', 'ContractSubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请金额',
   'user', 'dbo', 'table', 'Fun_ContractPayApply_Ref', 'column', 'ApplyBala'
go

alter table dbo.Fun_ContractPayApply_Ref
   add constraint PK_FUN_CONTRACTPAYAPPLY_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_ContractPayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_ContractPayApply_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_ContractPayApply_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_ContractPayApply_RefUpdateStatus
// 存储过程功能描述：更新Fun_ContractPayApply_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_ContractPayApply_Ref'

set @str = 'update [dbo].[Fun_ContractPayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].Fun_ContractPayApply_RefGoBack
// 存储过程功能描述：撤返Fun_ContractPayApply_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_ContractPayApply_Ref'

set @str = 'update [dbo].[Fun_ContractPayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].Fun_ContractPayApply_RefGet
// 存储过程功能描述：查询指定Fun_ContractPayApply_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[PayApplyId],
	[ContractId],
	[ContractSubId],
	[ApplyBala]
FROM
	[dbo].[Fun_ContractPayApply_Ref]
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
// 存储过程名：[dbo].Fun_ContractPayApply_RefLoad
// 存储过程功能描述：查询所有Fun_ContractPayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefLoad
AS

SELECT
	[RefId],
	[PayApplyId],
	[ContractId],
	[ContractSubId],
	[ApplyBala]
FROM
	[dbo].[Fun_ContractPayApply_Ref]

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
// 存储过程名：[dbo].Fun_ContractPayApply_RefInsert
// 存储过程功能描述：新增一条Fun_ContractPayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefInsert
	@PayApplyId int =NULL ,
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@ApplyBala numeric(18, 4) =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_ContractPayApply_Ref] (
	[PayApplyId],
	[ContractId],
	[ContractSubId],
	[ApplyBala]
) VALUES (
	@PayApplyId,
	@ContractId,
	@ContractSubId,
	@ApplyBala
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
// 存储过程名：[dbo].Fun_ContractPayApply_RefUpdate
// 存储过程功能描述：更新Fun_ContractPayApply_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_ContractPayApply_RefUpdate
    @RefId int,
@PayApplyId int = NULL,
@ContractId int = NULL,
@ContractSubId int = NULL,
@ApplyBala numeric(18, 4) = NULL
AS

UPDATE [dbo].[Fun_ContractPayApply_Ref] SET
	[PayApplyId] = @PayApplyId,
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[ApplyBala] = @ApplyBala
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



