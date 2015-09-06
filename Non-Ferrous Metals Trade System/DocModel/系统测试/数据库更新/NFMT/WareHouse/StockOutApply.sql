alter table dbo.St_StockOutApply
   drop constraint PK_ST_STOCKOUTAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockOutApply')
            and   type = 'U')
   drop table dbo.St_StockOutApply
go

/*==============================================================*/
/* Table: St_StockOutApply                                      */
/*==============================================================*/
create table dbo.St_StockOutApply (
   StockOutApplyId      int                  identity,
   ApplyId              int                  null,
   ContractId           int                  null,
   SubContractId        int                  null,
   GrossAmount          decimal(18,4)        null,
   NetAmount            decimal(18,4)        null,
   Bundles              int                  null,
   UnitId               int                  null,
   BuyCorpId            int                  null,
   CreateFrom           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出库申请',
   'user', 'dbo', 'table', 'St_StockOutApply'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'StockOutApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'ApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'SubContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请总毛重',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请总净重',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请总捆数',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'Bundles'
go

execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'UnitId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收货公司',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'BuyCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建来源',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'CreateFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_StockOutApply', 'column', 'LastModifyTime'
go

alter table dbo.St_StockOutApply
   add constraint PK_ST_STOCKOUTAPPLY primary key (StockOutApplyId)
go


/****** Object:  Stored Procedure [dbo].St_StockOutApplyGet    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyLoad    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyInsert    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyUpdate    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyUpdateStatus    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutApplyUpdateStatus    Script Date: 2015年1月27日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockOutApplyUpdateStatus
// 存储过程功能描述：更新St_StockOutApply中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutApply'

set @str = 'update [dbo].[St_StockOutApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockOutApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutApplyGoBack
// 存储过程功能描述：撤返St_StockOutApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutApply'

set @str = 'update [dbo].[St_StockOutApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockOutApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutApplyGet
// 存储过程功能描述：查询指定St_StockOutApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyGet
    /*
	@StockOutApplyId int
    */
    @id int
AS

SELECT
	[StockOutApplyId],
	[ApplyId],
	[ContractId],
	[SubContractId],
	[GrossAmount],
	[NetAmount],
	[Bundles],
	[UnitId],
	[BuyCorpId],
	[CreateFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockOutApply]
WHERE
	[StockOutApplyId] = @id

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
// 存储过程名：[dbo].St_StockOutApplyLoad
// 存储过程功能描述：查询所有St_StockOutApply记录
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyLoad
AS

SELECT
	[StockOutApplyId],
	[ApplyId],
	[ContractId],
	[SubContractId],
	[GrossAmount],
	[NetAmount],
	[Bundles],
	[UnitId],
	[BuyCorpId],
	[CreateFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockOutApply]

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
// 存储过程名：[dbo].St_StockOutApplyInsert
// 存储过程功能描述：新增一条St_StockOutApply记录
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyInsert
	@ApplyId int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@GrossAmount decimal(18, 4) =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@Bundles int =NULL ,
	@UnitId int =NULL ,
	@BuyCorpId int =NULL ,
	@CreateFrom int =NULL ,
	@CreatorId int =NULL ,
	@StockOutApplyId int OUTPUT
AS

INSERT INTO [dbo].[St_StockOutApply] (
	[ApplyId],
	[ContractId],
	[SubContractId],
	[GrossAmount],
	[NetAmount],
	[Bundles],
	[UnitId],
	[BuyCorpId],
	[CreateFrom],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@ContractId,
	@SubContractId,
	@GrossAmount,
	@NetAmount,
	@Bundles,
	@UnitId,
	@BuyCorpId,
	@CreateFrom,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StockOutApplyId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockOutApplyUpdate
// 存储过程功能描述：更新St_StockOutApply
// 创建人：CodeSmith
// 创建时间： 2015年1月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutApplyUpdate
    @StockOutApplyId int,
@ApplyId int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@GrossAmount decimal(18, 4) = NULL,
@NetAmount decimal(18, 4) = NULL,
@Bundles int = NULL,
@UnitId int = NULL,
@BuyCorpId int = NULL,
@CreateFrom int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockOutApply] SET
	[ApplyId] = @ApplyId,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[Bundles] = @Bundles,
	[UnitId] = @UnitId,
	[BuyCorpId] = @BuyCorpId,
	[CreateFrom] = @CreateFrom,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StockOutApplyId] = @StockOutApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



