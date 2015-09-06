alter table dbo.Con_SubDetail
   drop constraint PK_CON_SUBDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_SubDetail')
            and   type = 'U')
   drop table dbo.Con_SubDetail
go

/*==============================================================*/
/* Table: Con_SubDetail                                         */
/*==============================================================*/
create table dbo.Con_SubDetail (
   SubDetailId          int                  identity,
   SubId                int                  null,
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
   '子合约明细',
   'user', 'dbo', 'table', 'Con_SubDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约明细序号',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'SubDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现利率基准',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'DiscountBase'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现规则(按比例/金额)',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'DiscountType'
go

execute sp_addextendedproperty 'MS_Description', 
   '贴现利率',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'DiscountRate'
go

execute sp_addextendedproperty 'MS_Description', 
   '延期规则(按比例/金额)',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'DelayType'
go

execute sp_addextendedproperty 'MS_Description', 
   '延期费/率',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'DelayRate'
go

execute sp_addextendedproperty 'MS_Description', 
   '溢短装',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'MoreOrLess'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_SubDetail', 'column', 'LastModifyTime'
go

alter table dbo.Con_SubDetail
   add constraint PK_CON_SUBDETAIL primary key (SubDetailId)
go


/****** Object:  Stored Procedure [dbo].Con_SubDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Con_SubDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_SubDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_SubDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_SubDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_SubDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_SubDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_SubDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_SubDetailUpdateStatus
// 存储过程功能描述：更新Con_SubDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubDetail'

set @str = 'update [dbo].[Con_SubDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_SubDetailGoBack
// 存储过程功能描述：撤返Con_SubDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_SubDetail'

set @str = 'update [dbo].[Con_SubDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_SubDetailGet
// 存储过程功能描述：查询指定Con_SubDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailGet
    /*
	@SubDetailId int
    */
    @id int
AS

SELECT
	[SubDetailId],
	[SubId],
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
	[dbo].[Con_SubDetail]
WHERE
	[SubDetailId] = @id

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
// 存储过程名：[dbo].Con_SubDetailLoad
// 存储过程功能描述：查询所有Con_SubDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailLoad
AS

SELECT
	[SubDetailId],
	[SubId],
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
	[dbo].[Con_SubDetail]

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
// 存储过程名：[dbo].Con_SubDetailInsert
// 存储过程功能描述：新增一条Con_SubDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailInsert
	@SubId int =NULL ,
	@DiscountBase int =NULL ,
	@DiscountType int =NULL ,
	@DiscountRate numeric(18, 8) =NULL ,
	@DelayType int =NULL ,
	@DelayRate numeric(18, 8) =NULL ,
	@MoreOrLess numeric(18, 8) =NULL ,
	@CreatorId int =NULL ,
	@SubDetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_SubDetail] (
	[SubId],
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
	@SubId,
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


SET @SubDetailId = @@IDENTITY

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
// 存储过程名：[dbo].Con_SubDetailUpdate
// 存储过程功能描述：更新Con_SubDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_SubDetailUpdate
    @SubDetailId int,
@SubId int = NULL,
@DiscountBase int = NULL,
@DiscountType int = NULL,
@DiscountRate numeric(18, 8) = NULL,
@DelayType int = NULL,
@DelayRate numeric(18, 8) = NULL,
@MoreOrLess numeric(18, 8) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_SubDetail] SET
	[SubId] = @SubId,
	[DiscountBase] = @DiscountBase,
	[DiscountType] = @DiscountType,
	[DiscountRate] = @DiscountRate,
	[DelayType] = @DelayType,
	[DelayRate] = @DelayRate,
	[MoreOrLess] = @MoreOrLess,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[SubDetailId] = @SubDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



