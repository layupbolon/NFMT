alter table dbo.BDStyleDetail
   drop constraint PK_BDSTYLEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BDStyleDetail')
            and   type = 'U')
   drop table dbo.BDStyleDetail
go

/*==============================================================*/
/* Table: BDStyleDetail                                         */
/*==============================================================*/
create table dbo.BDStyleDetail (
   StyleDetailId        int                  identity,
   BDStyleId            int                  null,
   DetailCode           varchar(80)          null,
   DetailName           varchar(80)          null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '基础类型编码明细表',
   'user', 'dbo', 'table', 'BDStyleDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'StyleDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '主表序号',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'BDStyleId'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细编号',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'DetailCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细名称',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'DetailName'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'BDStyleDetail', 'column', 'LastModifyTime'
go

alter table dbo.BDStyleDetail
   add constraint PK_BDSTYLEDETAIL primary key (StyleDetailId)
go


/****** Object:  Stored Procedure [dbo].BDStyleDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailGet]
GO

/****** Object:  Stored Procedure [dbo].BDStyleDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].BDStyleDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].BDStyleDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].BDStyleDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BDStyleDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleDetailUpdateStatus
// 存储过程功能描述：更新BDStyleDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStyleDetail'

set @str = 'update [dbo].[BDStyleDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StyleDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStyleDetailGoBack
// 存储过程功能描述：撤返BDStyleDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStyleDetail'

set @str = 'update [dbo].[BDStyleDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StyleDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStyleDetailGet
// 存储过程功能描述：查询指定BDStyleDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailGet
    /*
	@StyleDetailId int
    */
    @id int
AS

SELECT
	[StyleDetailId],
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyleDetail]
WHERE
	[StyleDetailId] = @id

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
// 存储过程名：[dbo].BDStyleDetailLoad
// 存储过程功能描述：查询所有BDStyleDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailLoad
AS

SELECT
	[StyleDetailId],
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyleDetail]

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
// 存储过程名：[dbo].BDStyleDetailInsert
// 存储过程功能描述：新增一条BDStyleDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailInsert
	@BDStyleId int ,
	@DetailCode varchar(80) ,
	@DetailName varchar(80) ,
	@DetailStatus int ,
	@CreatorId int ,
	@StyleDetailId int OUTPUT
AS

INSERT INTO [dbo].[BDStyleDetail] (
	[BDStyleId],
	[DetailCode],
	[DetailName],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BDStyleId,
	@DetailCode,
	@DetailName,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StyleDetailId = @@IDENTITY

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
// 存储过程名：[dbo].BDStyleDetailUpdate
// 存储过程功能描述：更新BDStyleDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleDetailUpdate
    @StyleDetailId int,
@BDStyleId int,
@DetailCode varchar(80),
@DetailName varchar(80),
@DetailStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStyleDetail] SET
	[BDStyleId] = @BDStyleId,
	[DetailCode] = @DetailCode,
	[DetailName] = @DetailName,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StyleDetailId] = @StyleDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



