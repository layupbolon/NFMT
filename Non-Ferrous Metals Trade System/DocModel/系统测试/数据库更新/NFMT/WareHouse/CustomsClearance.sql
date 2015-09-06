alter table St_CustomsClearance
   drop constraint PK_ST_CUSTOMSCLEARANCE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsClearance')
            and   type = 'U')
   drop table St_CustomsClearance
go

/*==============================================================*/
/* Table: St_CustomsClearance                                   */
/*==============================================================*/
create table St_CustomsClearance (
   CustomsId            int                  identity,
   CustomsApplyId       int                  null,
   Customser            int                  null,
   CustomsCorpId        int                  null,
   CustomsDate          datetime             null,
   CustomsName          int                  null,
   GrossWeight          decimal(18,4)        null,
   NetWeight            decimal(18,4)        null,
   CurrencyId           int                  null,
   CustomsPrice         decimal(18,4)        null,
   TariffRate           decimal(18,6)        null,
   AddedValueRate       decimal(18,6)        null,
   OtherFees            decimal(18,4)        null,
   Memo                 varchar(4000)        null,
   CustomsStatus        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关',
   'user', @CurrentUser, 'table', 'St_CustomsClearance'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关人',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'Customser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实际报关公司',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关时间',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关海关',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关总毛重',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'GrossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关总净重',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'NetWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关单价',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关税税率',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'TariffRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '增值税率',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'AddedValueRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '检验检疫费',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'OtherFees'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关状态',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CustomsStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_CustomsClearance', 'column', 'LastModifyTime'
go

alter table St_CustomsClearance
   add constraint PK_ST_CUSTOMSCLEARANCE primary key (CustomsId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsClearanceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsClearanceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsClearanceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsClearanceUpdateStatus
// 存储过程功能描述：更新St_CustomsClearance中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsClearance'

set @str = 'update [dbo].[St_CustomsClearance] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CustomsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsClearanceGoBack
// 存储过程功能描述：撤返St_CustomsClearance，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsClearance'

set @str = 'update [dbo].[St_CustomsClearance] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CustomsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsClearanceGet
// 存储过程功能描述：查询指定St_CustomsClearance的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceGet
    /*
	@CustomsId int
    */
    @id int
AS

SELECT
	[CustomsId],
	[CustomsApplyId],
	[Customser],
	[CustomsCorpId],
	[CustomsDate],
	[CustomsName],
	[GrossWeight],
	[NetWeight],
	[CurrencyId],
	[CustomsPrice],
	[TariffRate],
	[AddedValueRate],
	[OtherFees],
	[Memo],
	[CustomsStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_CustomsClearance]
WHERE
	[CustomsId] = @id

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
// 存储过程名：[dbo].St_CustomsClearanceLoad
// 存储过程功能描述：查询所有St_CustomsClearance记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceLoad
AS

SELECT
	[CustomsId],
	[CustomsApplyId],
	[Customser],
	[CustomsCorpId],
	[CustomsDate],
	[CustomsName],
	[GrossWeight],
	[NetWeight],
	[CurrencyId],
	[CustomsPrice],
	[TariffRate],
	[AddedValueRate],
	[OtherFees],
	[Memo],
	[CustomsStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_CustomsClearance]

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
// 存储过程名：[dbo].St_CustomsClearanceInsert
// 存储过程功能描述：新增一条St_CustomsClearance记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceInsert
	@CustomsApplyId int =NULL ,
	@Customser int =NULL ,
	@CustomsCorpId int =NULL ,
	@CustomsDate datetime =NULL ,
	@CustomsName int =NULL ,
	@GrossWeight decimal(18, 4) =NULL ,
	@NetWeight decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@CustomsPrice decimal(18, 4) =NULL ,
	@TariffRate decimal(18, 6) =NULL ,
	@AddedValueRate decimal(18, 6) =NULL ,
	@OtherFees decimal(18, 4) =NULL ,
	@Memo varchar(4000) =NULL ,
	@CustomsStatus int =NULL ,
	@CreatorId int =NULL ,
	@CustomsId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsClearance] (
	[CustomsApplyId],
	[Customser],
	[CustomsCorpId],
	[CustomsDate],
	[CustomsName],
	[GrossWeight],
	[NetWeight],
	[CurrencyId],
	[CustomsPrice],
	[TariffRate],
	[AddedValueRate],
	[OtherFees],
	[Memo],
	[CustomsStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CustomsApplyId,
	@Customser,
	@CustomsCorpId,
	@CustomsDate,
	@CustomsName,
	@GrossWeight,
	@NetWeight,
	@CurrencyId,
	@CustomsPrice,
	@TariffRate,
	@AddedValueRate,
	@OtherFees,
	@Memo,
	@CustomsStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CustomsId = @@IDENTITY

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
// 存储过程名：[dbo].St_CustomsClearanceUpdate
// 存储过程功能描述：更新St_CustomsClearance
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsClearanceUpdate
    @CustomsId int,
@CustomsApplyId int = NULL,
@Customser int = NULL,
@CustomsCorpId int = NULL,
@CustomsDate datetime = NULL,
@CustomsName int = NULL,
@GrossWeight decimal(18, 4) = NULL,
@NetWeight decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@CustomsPrice decimal(18, 4) = NULL,
@TariffRate decimal(18, 6) = NULL,
@AddedValueRate decimal(18, 6) = NULL,
@OtherFees decimal(18, 4) = NULL,
@Memo varchar(4000) = NULL,
@CustomsStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_CustomsClearance] SET
	[CustomsApplyId] = @CustomsApplyId,
	[Customser] = @Customser,
	[CustomsCorpId] = @CustomsCorpId,
	[CustomsDate] = @CustomsDate,
	[CustomsName] = @CustomsName,
	[GrossWeight] = @GrossWeight,
	[NetWeight] = @NetWeight,
	[CurrencyId] = @CurrencyId,
	[CustomsPrice] = @CustomsPrice,
	[TariffRate] = @TariffRate,
	[AddedValueRate] = @AddedValueRate,
	[OtherFees] = @OtherFees,
	[Memo] = @Memo,
	[CustomsStatus] = @CustomsStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CustomsId] = @CustomsId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



