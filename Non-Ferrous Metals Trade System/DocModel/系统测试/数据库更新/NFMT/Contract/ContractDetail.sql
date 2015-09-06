alter table dbo.Con_ContractDetail
   drop constraint PK_CON_CONTRACTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractDetail')
            and   type = 'U')
   drop table dbo.Con_ContractDetail
go

/*==============================================================*/
/* Table: Con_ContractDetail                                    */
/*==============================================================*/
create table dbo.Con_ContractDetail (
   ContractDetailId     int                  identity,
   ContractId           int                  null,
   DiscountBase         int                  null,
   DiscountType         int                  null,
   DiscountRate         numeric(18,8)        null,
   DelayType            int                  null,
   DelayRate            numeric(18,8)        null,
   MoreOrLess           numeric(18,8)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约明细',
   'user', 'dbo', 'table', 'Con_ContractDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约明细序号',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'ContractDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现利率基准',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'DiscountBase'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现规则(按比例/金额)',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'DiscountType'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现利率',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'DiscountRate'
go

execute sp_addextendedproperty 'MS_Description', 
   '延期规则(按比例/金额)',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'DelayType'
go

execute sp_addextendedproperty 'MS_Description', 
   '延期费/率',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'DelayRate'
go

execute sp_addextendedproperty 'MS_Description', 
   '溢短装',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'MoreOrLess'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractDetail', 'column', 'LastModifyTime'
go

alter table dbo.Con_ContractDetail
   add constraint PK_CON_CONTRACTDETAIL primary key (ContractDetailId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractDetailUpdateStatus
// 存储过程功能描述：更新Con_ContractDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractDetail'

set @str = 'update [dbo].[Con_ContractDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractDetailGoBack
// 存储过程功能描述：撤返Con_ContractDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractDetail'

set @str = 'update [dbo].[Con_ContractDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractDetailGet
// 存储过程功能描述：查询指定Con_ContractDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailGet
    /*
	@ContractDetailId int
    */
    @id int
AS

SELECT
	[ContractDetailId],
	[ContractId],
	[DiscountBase],
	[DiscountType],
	[DiscountRate],
	[DelayType],
	[DelayRate],
	[MoreOrLess],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractDetail]
WHERE
	[ContractDetailId] = @id

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
// 存储过程名：[dbo].Con_ContractDetailLoad
// 存储过程功能描述：查询所有Con_ContractDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailLoad
AS

SELECT
	[ContractDetailId],
	[ContractId],
	[DiscountBase],
	[DiscountType],
	[DiscountRate],
	[DelayType],
	[DelayRate],
	[MoreOrLess],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractDetail]

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
// 存储过程名：[dbo].Con_ContractDetailInsert
// 存储过程功能描述：新增一条Con_ContractDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailInsert
	@ContractId int =NULL ,
	@DiscountBase int =NULL ,
	@DiscountType int =NULL ,
	@DiscountRate numeric(18, 8) =NULL ,
	@DelayType int =NULL ,
	@DelayRate numeric(18, 8) =NULL ,
	@MoreOrLess numeric(18, 8) =NULL ,
	@CreatorId int =NULL ,
	@ContractDetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractDetail] (
	[ContractId],
	[DiscountBase],
	[DiscountType],
	[DiscountRate],
	[DelayType],
	[DelayRate],
	[MoreOrLess],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@DiscountBase,
	@DiscountType,
	@DiscountRate,
	@DelayType,
	@DelayRate,
	@MoreOrLess,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ContractDetailId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractDetailUpdate
// 存储过程功能描述：更新Con_ContractDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDetailUpdate
    @ContractDetailId int,
@ContractId int = NULL,
@DiscountBase int = NULL,
@DiscountType int = NULL,
@DiscountRate numeric(18, 8) = NULL,
@DelayType int = NULL,
@DelayRate numeric(18, 8) = NULL,
@MoreOrLess numeric(18, 8) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_ContractDetail] SET
	[ContractId] = @ContractId,
	[DiscountBase] = @DiscountBase,
	[DiscountType] = @DiscountType,
	[DiscountRate] = @DiscountRate,
	[DelayType] = @DelayType,
	[DelayRate] = @DelayRate,
	[MoreOrLess] = @MoreOrLess,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ContractDetailId] = @ContractDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



