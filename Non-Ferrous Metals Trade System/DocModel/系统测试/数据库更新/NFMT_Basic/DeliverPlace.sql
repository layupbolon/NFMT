alter table dbo.DeliverPlace
   drop constraint PK_DELIVERPLACE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.DeliverPlace')
            and   type = 'U')
   drop table dbo.DeliverPlace
go

/*==============================================================*/
/* Table: DeliverPlace                                          */
/*==============================================================*/
create table dbo.DeliverPlace (
   DPId                 int                  identity,
   DPType               int                  null,
   DPArea               int                  null,
   DPCompany            int                  null,
   DPName               varchar(80)          null,
   DPFullName           varchar(400)         null,
   DPStatus             int                  null,
   DPAddress            varchar(400)         null,
   DPEAddress           varchar(400)         null,
   DPTel                varchar(80)          null,
   DPContact            varchar(80)          null,
   DPFax                varchar(80)          null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', 'dbo', 'table', 'DeliverPlace'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地序号',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPId'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地类型(港/库)',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPType'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地地区',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPArea'
go

execute sp_addextendedproperty 'MS_Description', 
   '仓储/码头公司',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPCompany'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地名称',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPName'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地全称',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地状态',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地地址',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地地址(英文)',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPEAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地电话',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPTel'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地联系人',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPContact'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地传真',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'DPFax'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'DeliverPlace', 'column', 'LastModifyTime'
go

alter table dbo.DeliverPlace
   add constraint PK_DELIVERPLACE primary key (DPId)
go


/****** Object:  Stored Procedure [dbo].DeliverPlaceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceGet]
GO

/****** Object:  Stored Procedure [dbo].DeliverPlaceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceLoad]
GO

/****** Object:  Stored Procedure [dbo].DeliverPlaceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceInsert]
GO

/****** Object:  Stored Procedure [dbo].DeliverPlaceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceUpdate]
GO

/****** Object:  Stored Procedure [dbo].DeliverPlaceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].DeliverPlaceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliverPlaceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeliverPlaceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeliverPlaceUpdateStatus
// 存储过程功能描述：更新DeliverPlace中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.DeliverPlace'

set @str = 'update [dbo].[DeliverPlace] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DeliverPlaceGoBack
// 存储过程功能描述：撤返DeliverPlace，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.DeliverPlace'

set @str = 'update [dbo].[DeliverPlace] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DeliverPlaceGet
// 存储过程功能描述：查询指定DeliverPlace的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceGet
    /*
	@DPId int
    */
    @id int
AS

SELECT
	[DPId],
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeliverPlace]
WHERE
	[DPId] = @id

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
// 存储过程名：[dbo].DeliverPlaceLoad
// 存储过程功能描述：查询所有DeliverPlace记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceLoad
AS

SELECT
	[DPId],
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeliverPlace]

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
// 存储过程名：[dbo].DeliverPlaceInsert
// 存储过程功能描述：新增一条DeliverPlace记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceInsert
	@DPType int ,
	@DPArea int ,
	@DPCompany int =NULL ,
	@DPName varchar(80) ,
	@DPFullName varchar(400) =NULL ,
	@DPStatus int ,
	@DPAddress varchar(400) ,
	@DPEAddress varchar(400) =NULL ,
	@DPTel varchar(80) =NULL ,
	@DPContact varchar(80) =NULL ,
	@DPFax varchar(80) =NULL ,
	@CreatorId int ,
	@DPId int OUTPUT
AS

INSERT INTO [dbo].[DeliverPlace] (
	[DPType],
	[DPArea],
	[DPCompany],
	[DPName],
	[DPFullName],
	[DPStatus],
	[DPAddress],
	[DPEAddress],
	[DPTel],
	[DPContact],
	[DPFax],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DPType,
	@DPArea,
	@DPCompany,
	@DPName,
	@DPFullName,
	@DPStatus,
	@DPAddress,
	@DPEAddress,
	@DPTel,
	@DPContact,
	@DPFax,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DPId = @@IDENTITY

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
// 存储过程名：[dbo].DeliverPlaceUpdate
// 存储过程功能描述：更新DeliverPlace
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeliverPlaceUpdate
    @DPId int,
@DPType int,
@DPArea int,
@DPCompany int = NULL,
@DPName varchar(80),
@DPFullName varchar(400) = NULL,
@DPStatus int,
@DPAddress varchar(400),
@DPEAddress varchar(400) = NULL,
@DPTel varchar(80) = NULL,
@DPContact varchar(80) = NULL,
@DPFax varchar(80) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[DeliverPlace] SET
	[DPType] = @DPType,
	[DPArea] = @DPArea,
	[DPCompany] = @DPCompany,
	[DPName] = @DPName,
	[DPFullName] = @DPFullName,
	[DPStatus] = @DPStatus,
	[DPAddress] = @DPAddress,
	[DPEAddress] = @DPEAddress,
	[DPTel] = @DPTel,
	[DPContact] = @DPContact,
	[DPFax] = @DPFax,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DPId] = @DPId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



