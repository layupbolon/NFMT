alter table dbo.CorporationDetail
   drop constraint PK_CORPORATIONDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.CorporationDetail')
            and   type = 'U')
   drop table dbo.CorporationDetail
go

/*==============================================================*/
/* Table: CorporationDetail                                     */
/*==============================================================*/
create table dbo.CorporationDetail (
   DetailId             int                  identity,
   CorpId               int                  null,
   BusinessLicenseCode  varchar(80)          null,
   RegisteredCapital    decimal(18,4)        null,
   CurrencyId           int                  null,
   RegisteredDate       datetime             null,
   CorpProperty         varchar(200)         null,
   BusinessScope        varchar(500)         null,
   TaxRegisteredCode    varchar(200)         null,
   OrganizationCode     varchar(200)         null,
   IsChildCorp          bit                  null,
   CorpZip              varchar(20)          null,
   CorpType             int                  null,
   IsSelf               bit                  null,
   Memo                 varchar(4000)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '客户明细',
   'user', 'dbo', 'table', 'CorporationDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属集团',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '营业执照注册号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'BusinessLicenseCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '注册资本',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'RegisteredCapital'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '注册时间',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'RegisteredDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司性质',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CorpProperty'
go

execute sp_addextendedproperty 'MS_Description', 
   '经营范围',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'BusinessScope'
go

execute sp_addextendedproperty 'MS_Description', 
   '税务注册号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'TaxRegisteredCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '组织机构注册号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'OrganizationCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否子公司',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'IsChildCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司邮编',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CorpZip'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司类型',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CorpType'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否己方公司',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'IsSelf'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'CorporationDetail', 'column', 'LastModifyTime'
go

alter table dbo.CorporationDetail
   add constraint PK_CORPORATIONDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].CorporationDetailGet    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailGet]
GO

/****** Object:  Stored Procedure [dbo].CorporationDetailLoad    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].CorporationDetailInsert    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].CorporationDetailUpdate    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].CorporationDetailUpdateStatus    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].CorporationDetailUpdateStatus    Script Date: 2015年1月22日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorporationDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorporationDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationDetailUpdateStatus
// 存储过程功能描述：更新CorporationDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorporationDetail'

set @str = 'update [dbo].[CorporationDetail] '+
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
// 存储过程名：[dbo].CorporationDetailGoBack
// 存储过程功能描述：撤返CorporationDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorporationDetail'

set @str = 'update [dbo].[CorporationDetail] '+
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
// 存储过程名：[dbo].CorporationDetailGet
// 存储过程功能描述：查询指定CorporationDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[CorpId],
	[BusinessLicenseCode],
	[RegisteredCapital],
	[CurrencyId],
	[RegisteredDate],
	[CorpProperty],
	[BusinessScope],
	[TaxRegisteredCode],
	[OrganizationCode],
	[IsChildCorp],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[Memo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[CorporationDetail]
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
// 存储过程名：[dbo].CorporationDetailLoad
// 存储过程功能描述：查询所有CorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailLoad
AS

SELECT
	[DetailId],
	[CorpId],
	[BusinessLicenseCode],
	[RegisteredCapital],
	[CurrencyId],
	[RegisteredDate],
	[CorpProperty],
	[BusinessScope],
	[TaxRegisteredCode],
	[OrganizationCode],
	[IsChildCorp],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[Memo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[CorporationDetail]

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
// 存储过程名：[dbo].CorporationDetailInsert
// 存储过程功能描述：新增一条CorporationDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailInsert
	@CorpId int =NULL ,
	@BusinessLicenseCode varchar(80) =NULL ,
	@RegisteredCapital decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@RegisteredDate datetime =NULL ,
	@CorpProperty varchar(200) =NULL ,
	@BusinessScope varchar(500) =NULL ,
	@TaxRegisteredCode varchar(200) =NULL ,
	@OrganizationCode varchar(200) =NULL ,
	@IsChildCorp bit =NULL ,
	@CorpZip varchar(20) =NULL ,
	@CorpType int =NULL ,
	@IsSelf bit =NULL ,
	@Memo varchar(4000) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[CorporationDetail] (
	[CorpId],
	[BusinessLicenseCode],
	[RegisteredCapital],
	[CurrencyId],
	[RegisteredDate],
	[CorpProperty],
	[BusinessScope],
	[TaxRegisteredCode],
	[OrganizationCode],
	[IsChildCorp],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[Memo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CorpId,
	@BusinessLicenseCode,
	@RegisteredCapital,
	@CurrencyId,
	@RegisteredDate,
	@CorpProperty,
	@BusinessScope,
	@TaxRegisteredCode,
	@OrganizationCode,
	@IsChildCorp,
	@CorpZip,
	@CorpType,
	@IsSelf,
	@Memo,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

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
// 存储过程名：[dbo].CorporationDetailUpdate
// 存储过程功能描述：更新CorporationDetail
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorporationDetailUpdate
    @DetailId int,
@CorpId int = NULL,
@BusinessLicenseCode varchar(80) = NULL,
@RegisteredCapital decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@RegisteredDate datetime = NULL,
@CorpProperty varchar(200) = NULL,
@BusinessScope varchar(500) = NULL,
@TaxRegisteredCode varchar(200) = NULL,
@OrganizationCode varchar(200) = NULL,
@IsChildCorp bit = NULL,
@CorpZip varchar(20) = NULL,
@CorpType int = NULL,
@IsSelf bit = NULL,
@Memo varchar(4000) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[CorporationDetail] SET
	[CorpId] = @CorpId,
	[BusinessLicenseCode] = @BusinessLicenseCode,
	[RegisteredCapital] = @RegisteredCapital,
	[CurrencyId] = @CurrencyId,
	[RegisteredDate] = @RegisteredDate,
	[CorpProperty] = @CorpProperty,
	[BusinessScope] = @BusinessScope,
	[TaxRegisteredCode] = @TaxRegisteredCode,
	[OrganizationCode] = @OrganizationCode,
	[IsChildCorp] = @IsChildCorp,
	[CorpZip] = @CorpZip,
	[CorpType] = @CorpType,
	[IsSelf] = @IsSelf,
	[Memo] = @Memo,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



