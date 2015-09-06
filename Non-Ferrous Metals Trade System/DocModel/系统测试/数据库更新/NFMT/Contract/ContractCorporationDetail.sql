alter table dbo.Con_ContractCorporationDetail
   drop constraint PK_CON_CONTRACTCORPORATIONDETA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractCorporationDetail')
            and   type = 'U')
   drop table dbo.Con_ContractCorporationDetail
go

/*==============================================================*/
/* Table: Con_ContractCorporationDetail                         */
/*==============================================================*/
create table dbo.Con_ContractCorporationDetail (
   DetailId             int                  identity,
   ContractId           int                  null,
   CorpId               int                  null,
   CorpName             varchar(200)         null,
   DeptId               int                  null,
   DeptName             varchar(80)          null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null,
   IsDefaultCorp        bit                  null,
   IsInnerCorp          bit                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约抬头明细',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司名称',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'CorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本中心序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本中心名称',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'DeptName'
go

execute sp_addextendedproperty 'MS_Description', 
   '抬头状态',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'LastModifyTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否默认公司',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'IsDefaultCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否内部公司',
   'user', 'dbo', 'table', 'Con_ContractCorporationDetail', 'column', 'IsInnerCorp'
go

alter table dbo.Con_ContractCorporationDetail
   add constraint PK_CON_CONTRACTCORPORATIONDETA primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractCorporationDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractCorporationDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractCorporationDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractCorporationDetailUpdateStatus
// 存储过程功能描述：更新Con_ContractCorporationDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractCorporationDetail'

set @str = 'update [dbo].[Con_ContractCorporationDetail] '+
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
// 存储过程名：[dbo].Con_ContractCorporationDetailGoBack
// 存储过程功能描述：撤返Con_ContractCorporationDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractCorporationDetail'

set @str = 'update [dbo].[Con_ContractCorporationDetail] '+
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
// 存储过程名：[dbo].Con_ContractCorporationDetailGet
// 存储过程功能描述：查询指定Con_ContractCorporationDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractId],
	[CorpId],
	[CorpName],
	[DeptId],
	[DeptName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[IsDefaultCorp],
	[IsInnerCorp]
FROM
	[dbo].[Con_ContractCorporationDetail]
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
// 存储过程名：[dbo].Con_ContractCorporationDetailLoad
// 存储过程功能描述：查询所有Con_ContractCorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailLoad
AS

SELECT
	[DetailId],
	[ContractId],
	[CorpId],
	[CorpName],
	[DeptId],
	[DeptName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[IsDefaultCorp],
	[IsInnerCorp]
FROM
	[dbo].[Con_ContractCorporationDetail]

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
// 存储过程名：[dbo].Con_ContractCorporationDetailInsert
// 存储过程功能描述：新增一条Con_ContractCorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailInsert
	@ContractId int =NULL ,
	@CorpId int =NULL ,
	@CorpName varchar(200) =NULL ,
	@DeptId int =NULL ,
	@DeptName varchar(80) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@IsDefaultCorp bit =NULL ,
	@IsInnerCorp bit =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractCorporationDetail] (
	[ContractId],
	[CorpId],
	[CorpName],
	[DeptId],
	[DeptName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[IsDefaultCorp],
	[IsInnerCorp]
) VALUES (
	@ContractId,
	@CorpId,
	@CorpName,
	@DeptId,
	@DeptName,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()
,
	@IsDefaultCorp,
	@IsInnerCorp
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
// 存储过程名：[dbo].Con_ContractCorporationDetailUpdate
// 存储过程功能描述：更新Con_ContractCorporationDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractCorporationDetailUpdate
    @DetailId int,
@ContractId int = NULL,
@CorpId int = NULL,
@CorpName varchar(200) = NULL,
@DeptId int = NULL,
@DeptName varchar(80) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL,
@IsDefaultCorp bit = NULL,
@IsInnerCorp bit = NULL
AS

UPDATE [dbo].[Con_ContractCorporationDetail] SET
	[ContractId] = @ContractId,
	[CorpId] = @CorpId,
	[CorpName] = @CorpName,
	[DeptId] = @DeptId,
	[DeptName] = @DeptName,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()
,
	[IsDefaultCorp] = @IsDefaultCorp,
	[IsInnerCorp] = @IsInnerCorp
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



