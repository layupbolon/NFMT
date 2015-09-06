alter table dbo.Con_SubCorporationDetail
   drop constraint PK_CON_SUBCORPORATIONDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_SubCorporationDetail')
            and   type = 'U')
   drop table dbo.Con_SubCorporationDetail
go

/*==============================================================*/
/* Table: Con_SubCorporationDetail                              */
/*==============================================================*/
create table dbo.Con_SubCorporationDetail (
   DetailId             int                  identity,
   ContractId           int                  null,
   SubId                int                  null,
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
   '子合约抬头明细',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司名称',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'CorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本中心序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '成本中心名称',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'DeptName'
go

execute sp_addextendedproperty 'MS_Description', 
   '抬头状态',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'LastModifyTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否默认公司',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'IsDefaultCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否内部公司',
   'user', 'dbo', 'table', 'Con_SubCorporationDetail', 'column', 'IsInnerCorp'
go

alter table dbo.Con_SubCorporationDetail
   add constraint PK_CON_SUBCORPORATIONDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_SubCorporationDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubCorporationDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubCorporationDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_SubCorporationDetailUpdateStatus
// 存储过程功能描述：更新Con_SubCorporationDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubCorporationDetail'

set @str = 'update [dbo].[Con_SubCorporationDetail] '+
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
// 存储过程名：[dbo].Con_SubCorporationDetailGoBack
// 存储过程功能描述：撤返Con_SubCorporationDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubCorporationDetail'

set @str = 'update [dbo].[Con_SubCorporationDetail] '+
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
// 存储过程名：[dbo].Con_SubCorporationDetailGet
// 存储过程功能描述：查询指定Con_SubCorporationDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractId],
	[SubId],
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
	[dbo].[Con_SubCorporationDetail]
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
// 存储过程名：[dbo].Con_SubCorporationDetailLoad
// 存储过程功能描述：查询所有Con_SubCorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailLoad
AS

SELECT
	[DetailId],
	[ContractId],
	[SubId],
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
	[dbo].[Con_SubCorporationDetail]

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
// 存储过程名：[dbo].Con_SubCorporationDetailInsert
// 存储过程功能描述：新增一条Con_SubCorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailInsert
	@ContractId int =NULL ,
	@SubId int =NULL ,
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

INSERT INTO [dbo].[Con_SubCorporationDetail] (
	[ContractId],
	[SubId],
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
	@SubId,
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
// 存储过程名：[dbo].Con_SubCorporationDetailUpdate
// 存储过程功能描述：更新Con_SubCorporationDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubCorporationDetailUpdate
    @DetailId int,
@ContractId int = NULL,
@SubId int = NULL,
@CorpId int = NULL,
@CorpName varchar(200) = NULL,
@DeptId int = NULL,
@DeptName varchar(80) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL,
@IsDefaultCorp bit = NULL,
@IsInnerCorp bit = NULL
AS

UPDATE [dbo].[Con_SubCorporationDetail] SET
	[ContractId] = @ContractId,
	[SubId] = @SubId,
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



