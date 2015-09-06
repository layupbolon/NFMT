alter table dbo.Corporation
   drop constraint PK_CORPORATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Corporation')
            and   type = 'U')
   drop table dbo.Corporation
go

/*==============================================================*/
/* Table: Corporation                                           */
/*==============================================================*/
create table dbo.Corporation (
   CorpId               int                  identity,
   ParentId             int                  null,
   CorpCode             varchar(80)          null,
   CorpName             varchar(40)          null,
   CorpEName            varchar(80)          null,
   TaxPayerId           varchar(80)          null,
   CorpFullName         varchar(80)          null,
   CorpFullEName        varchar(200)         null,
   CorpAddress          varchar(400)         null,
   CorpEAddress         varchar(800)         null,
   CorpTel              varchar(40)          null,
   CorpFax              varchar(40)          null,
   CorpZip              varchar(20)          null,
   CorpType             int                  null,
   IsSelf               bit                  null,
   CorpStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '公司',
   'user', 'dbo', 'table', 'Corporation'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属集团',
   'user', 'dbo', 'table', 'Corporation', 'column', 'ParentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司代码',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司名称',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司英文名称',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpEName'
go

execute sp_addextendedproperty 'MS_Description', 
   '纳税人识别号',
   'user', 'dbo', 'table', 'Corporation', 'column', 'TaxPayerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司全称',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司英文全称',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpFullEName'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司地址',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司地址(英文)',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpEAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司电话',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpTel'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司传真',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpFax'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司邮编',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpZip'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司类型',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpType'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否己方公司',
   'user', 'dbo', 'table', 'Corporation', 'column', 'IsSelf'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司状态',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CorpStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Corporation', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Corporation', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Corporation', 'column', 'LastModifyTime'
go

alter table dbo.Corporation
   add constraint PK_CORPORATION primary key (CorpId)
go

/****** Object:  Stored Procedure [dbo].CorporationGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationGet]
GO

/****** Object:  Stored Procedure [dbo].CorporationLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationLoad]
GO

/****** Object:  Stored Procedure [dbo].CorporationInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationInsert]
GO

/****** Object:  Stored Procedure [dbo].CorporationUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationUpdate]
GO

/****** Object:  Stored Procedure [dbo].CorporationUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].CorporationUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationUpdateStatus
// 存储过程功能描述：更新Corporation中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Corporation'

set @str = 'update [dbo].[Corporation] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CorpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorporationGoBack
// 存储过程功能描述：撤返Corporation，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Corporation'

set @str = 'update [dbo].[Corporation] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CorpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorporationGet
// 存储过程功能描述：查询指定Corporation的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationGet
    /*
	@CorpId int
    */
    @id int
AS

SELECT
	[CorpId],
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Corporation]
WHERE
	[CorpId] = @id

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
// 存储过程名：[dbo].CorporationLoad
// 存储过程功能描述：查询所有Corporation记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationLoad
AS

SELECT
	[CorpId],
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Corporation]

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
// 存储过程名：[dbo].CorporationInsert
// 存储过程功能描述：新增一条Corporation记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationInsert
	@ParentId int =NULL ,
	@CorpCode varchar(80) =NULL ,
	@CorpName varchar(40) ,
	@CorpEName varchar(80) =NULL ,
	@TaxPayerId varchar(80) =NULL ,
	@CorpFullName varchar(80) =NULL ,
	@CorpFullEName varchar(200) =NULL ,
	@CorpAddress varchar(400) =NULL ,
	@CorpEAddress varchar(800) =NULL ,
	@CorpTel varchar(40) =NULL ,
	@CorpFax varchar(40) =NULL ,
	@CorpZip varchar(20) =NULL ,
	@CorpType int =NULL ,
	@IsSelf bit =NULL ,
	@CorpStatus int ,
	@CreatorId int ,
	@CorpId int OUTPUT
AS

INSERT INTO [dbo].[Corporation] (
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ParentId,
	@CorpCode,
	@CorpName,
	@CorpEName,
	@TaxPayerId,
	@CorpFullName,
	@CorpFullEName,
	@CorpAddress,
	@CorpEAddress,
	@CorpTel,
	@CorpFax,
	@CorpZip,
	@CorpType,
	@IsSelf,
	@CorpStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CorpId = @@IDENTITY

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
// 存储过程名：[dbo].CorporationUpdate
// 存储过程功能描述：更新Corporation
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationUpdate
    @CorpId int,
@ParentId int = NULL,
@CorpCode varchar(80) = NULL,
@CorpName varchar(40),
@CorpEName varchar(80) = NULL,
@TaxPayerId varchar(80) = NULL,
@CorpFullName varchar(80) = NULL,
@CorpFullEName varchar(200) = NULL,
@CorpAddress varchar(400) = NULL,
@CorpEAddress varchar(800) = NULL,
@CorpTel varchar(40) = NULL,
@CorpFax varchar(40) = NULL,
@CorpZip varchar(20) = NULL,
@CorpType int = NULL,
@IsSelf bit = NULL,
@CorpStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Corporation] SET
	[ParentId] = @ParentId,
	[CorpCode] = @CorpCode,
	[CorpName] = @CorpName,
	[CorpEName] = @CorpEName,
	[TaxPayerId] = @TaxPayerId,
	[CorpFullName] = @CorpFullName,
	[CorpFullEName] = @CorpFullEName,
	[CorpAddress] = @CorpAddress,
	[CorpEAddress] = @CorpEAddress,
	[CorpTel] = @CorpTel,
	[CorpFax] = @CorpFax,
	[CorpZip] = @CorpZip,
	[CorpType] = @CorpType,
	[IsSelf] = @IsSelf,
	[CorpStatus] = @CorpStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CorpId] = @CorpId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



