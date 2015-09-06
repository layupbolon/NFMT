alter table dbo.Brand
   drop constraint PK_BRAND
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Brand')
            and   type = 'U')
   drop table dbo.Brand
go

/*==============================================================*/
/* Table: Brand                                                 */
/*==============================================================*/
create table dbo.Brand (
   BrandId              int                  identity,
   ProducerId           int                  null,
   BrandName            varchar(80)          null,
   BrandFullName        varchar(400)         null,
   BrandInfo            varchar(800)         null,
   BrandStatus          int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', 'dbo', 'table', 'Brand'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌序号',
   'user', 'dbo', 'table', 'Brand', 'column', 'BrandId'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种名称生产商序号',
   'user', 'dbo', 'table', 'Brand', 'column', 'ProducerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌名称',
   'user', 'dbo', 'table', 'Brand', 'column', 'BrandName'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌全称',
   'user', 'dbo', 'table', 'Brand', 'column', 'BrandFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌备注',
   'user', 'dbo', 'table', 'Brand', 'column', 'BrandInfo'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌状态',
   'user', 'dbo', 'table', 'Brand', 'column', 'BrandStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Brand', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Brand', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Brand', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Brand', 'column', 'LastModifyTime'
go

alter table dbo.Brand
   add constraint PK_BRAND primary key (BrandId)
go


/****** Object:  Stored Procedure [dbo].BrandGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandGet]
GO

/****** Object:  Stored Procedure [dbo].BrandLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandLoad]
GO

/****** Object:  Stored Procedure [dbo].BrandInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandInsert]
GO

/****** Object:  Stored Procedure [dbo].BrandUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandUpdate]
GO

/****** Object:  Stored Procedure [dbo].BrandUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BrandUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BrandGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BrandGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BrandUpdateStatus
// 存储过程功能描述：更新Brand中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Brand'

set @str = 'update [dbo].[Brand] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BrandId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BrandGoBack
// 存储过程功能描述：撤返Brand，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Brand'

set @str = 'update [dbo].[Brand] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BrandId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BrandGet
// 存储过程功能描述：查询指定Brand的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandGet
    /*
	@BrandId int
    */
    @id int
AS

SELECT
	[BrandId],
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Brand]
WHERE
	[BrandId] = @id

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
// 存储过程名：[dbo].BrandLoad
// 存储过程功能描述：查询所有Brand记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandLoad
AS

SELECT
	[BrandId],
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Brand]

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
// 存储过程名：[dbo].BrandInsert
// 存储过程功能描述：新增一条Brand记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandInsert
	@ProducerId int =NULL ,
	@BrandName varchar(80) =NULL ,
	@BrandFullName varchar(400) =NULL ,
	@BrandInfo varchar(800) =NULL ,
	@BrandStatus int =NULL ,
	@CreatorId int =NULL ,
	@BrandId int OUTPUT
AS

INSERT INTO [dbo].[Brand] (
	[ProducerId],
	[BrandName],
	[BrandFullName],
	[BrandInfo],
	[BrandStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ProducerId,
	@BrandName,
	@BrandFullName,
	@BrandInfo,
	@BrandStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BrandId = @@IDENTITY

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
// 存储过程名：[dbo].BrandUpdate
// 存储过程功能描述：更新Brand
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BrandUpdate
    @BrandId int,
@ProducerId int = NULL,
@BrandName varchar(80) = NULL,
@BrandFullName varchar(400) = NULL,
@BrandInfo varchar(800) = NULL,
@BrandStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Brand] SET
	[ProducerId] = @ProducerId,
	[BrandName] = @BrandName,
	[BrandFullName] = @BrandFullName,
	[BrandInfo] = @BrandInfo,
	[BrandStatus] = @BrandStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BrandId] = @BrandId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



