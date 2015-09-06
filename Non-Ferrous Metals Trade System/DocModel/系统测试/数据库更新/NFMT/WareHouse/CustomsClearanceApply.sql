alter table St_CustomsClearanceApply
   drop constraint PK_ST_CUSTOMSCLEARANCEAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsClearanceApply')
            and   type = 'U')
   drop table St_CustomsClearanceApply
go

/*==============================================================*/
/* Table: St_CustomsClearanceApply                              */
/*==============================================================*/
create table St_CustomsClearanceApply (
   CustomsApplyId       int                  identity,
   ApplyId              int                  null,
   AssetId              int                  null,
   GrossWeight          decimal(18,4)        null,
   NetWeight            decimal(18,4)        null,
   UnitId               int                  null,
   OutCorpId            int                  null,
   InCorpId             int                  null,
   CustomsCorpId        int                  null,
   CustomsPrice         decimal(18,4)        null,
   CurrencyId           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关申请',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CustomsApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'AssetId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请总毛重',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'GrossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请总净重',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'NetWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'UnitId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关外公司',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'OutCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关内公司',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'InCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关公司',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CustomsCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关单价',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CustomsPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_CustomsClearanceApply', 'column', 'LastModifyTime'
go

alter table St_CustomsClearanceApply
   add constraint PK_ST_CUSTOMSCLEARANCEAPPLY primary key (CustomsApplyId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsClearanceApplyUpdateStatus
// 存储过程功能描述：更新St_CustomsClearanceApply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsClearanceApply'

set @str = 'update [dbo].[St_CustomsClearanceApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CustomsApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsClearanceApplyGoBack
// 存储过程功能描述：撤返St_CustomsClearanceApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsClearanceApply'

set @str = 'update [dbo].[St_CustomsClearanceApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CustomsApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsClearanceApplyGet
// 存储过程功能描述：查询指定St_CustomsClearanceApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyGet
    /*
	@CustomsApplyId int
    */
    @id int
AS

SELECT
	[CustomsApplyId],
	[ApplyId],
	[AssetId],
	[GrossWeight],
	[NetWeight],
	[UnitId],
	[OutCorpId],
	[InCorpId],
	[CustomsCorpId],
	[CustomsPrice],
	[CurrencyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_CustomsClearanceApply]
WHERE
	[CustomsApplyId] = @id

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
// 存储过程名：[dbo].St_CustomsClearanceApplyLoad
// 存储过程功能描述：查询所有St_CustomsClearanceApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyLoad
AS

SELECT
	[CustomsApplyId],
	[ApplyId],
	[AssetId],
	[GrossWeight],
	[NetWeight],
	[UnitId],
	[OutCorpId],
	[InCorpId],
	[CustomsCorpId],
	[CustomsPrice],
	[CurrencyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_CustomsClearanceApply]

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
// 存储过程名：[dbo].St_CustomsClearanceApplyInsert
// 存储过程功能描述：新增一条St_CustomsClearanceApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyInsert
	@ApplyId int =NULL ,
	@AssetId int =NULL ,
	@GrossWeight decimal(18, 4) =NULL ,
	@NetWeight decimal(18, 4) =NULL ,
	@UnitId int =NULL ,
	@OutCorpId int =NULL ,
	@InCorpId int =NULL ,
	@CustomsCorpId int =NULL ,
	@CustomsPrice decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@CreatorId int =NULL ,
	@CustomsApplyId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsClearanceApply] (
	[ApplyId],
	[AssetId],
	[GrossWeight],
	[NetWeight],
	[UnitId],
	[OutCorpId],
	[InCorpId],
	[CustomsCorpId],
	[CustomsPrice],
	[CurrencyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@AssetId,
	@GrossWeight,
	@NetWeight,
	@UnitId,
	@OutCorpId,
	@InCorpId,
	@CustomsCorpId,
	@CustomsPrice,
	@CurrencyId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CustomsApplyId = @@IDENTITY

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
// 存储过程名：[dbo].St_CustomsClearanceApplyUpdate
// 存储过程功能描述：更新St_CustomsClearanceApply
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceApplyUpdate
    @CustomsApplyId int,
@ApplyId int = NULL,
@AssetId int = NULL,
@GrossWeight decimal(18, 4) = NULL,
@NetWeight decimal(18, 4) = NULL,
@UnitId int = NULL,
@OutCorpId int = NULL,
@InCorpId int = NULL,
@CustomsCorpId int = NULL,
@CustomsPrice decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_CustomsClearanceApply] SET
	[ApplyId] = @ApplyId,
	[AssetId] = @AssetId,
	[GrossWeight] = @GrossWeight,
	[NetWeight] = @NetWeight,
	[UnitId] = @UnitId,
	[OutCorpId] = @OutCorpId,
	[InCorpId] = @InCorpId,
	[CustomsCorpId] = @CustomsCorpId,
	[CustomsPrice] = @CustomsPrice,
	[CurrencyId] = @CurrencyId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CustomsApplyId] = @CustomsApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



