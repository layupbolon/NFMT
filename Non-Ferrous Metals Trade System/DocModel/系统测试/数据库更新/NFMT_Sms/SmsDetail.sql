alter table dbo.Sm_SmsDetail
   drop constraint PK_SM_SMSDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Sm_SmsDetail')
            and   type = 'U')
   drop table dbo.Sm_SmsDetail
go

/*==============================================================*/
/* Table: Sm_SmsDetail                                          */
/*==============================================================*/
create table dbo.Sm_SmsDetail (
   DetailId             int                  identity,
   SmsId                int                  null,
   EmpId                int                  null,
   ReadTime             datetime             null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '消息明细表',
   'user', 'dbo', 'table', 'Sm_SmsDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '已读序号',
   'user', 'dbo', 'table', 'Sm_SmsDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息序号',
   'user', 'dbo', 'table', 'Sm_SmsDetail', 'column', 'SmsId'
go

execute sp_addextendedproperty 'MS_Description', 
   '员工人',
   'user', 'dbo', 'table', 'Sm_SmsDetail', 'column', 'EmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '读取时间',
   'user', 'dbo', 'table', 'Sm_SmsDetail', 'column', 'ReadTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Sm_SmsDetail', 'column', 'DetailStatus'
go

alter table dbo.Sm_SmsDetail
   add constraint PK_SM_SMSDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Sm_SmsDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailUpdateStatus
// 存储过程功能描述：更新Sm_SmsDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_SmsDetail'

set @str = 'update [dbo].[Sm_SmsDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].Sm_SmsDetailGoBack
// 存储过程功能描述：撤返Sm_SmsDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_SmsDetail'

set @str = 'update [dbo].[Sm_SmsDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].Sm_SmsDetailGet
// 存储过程功能描述：查询指定Sm_SmsDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[SmsId],
	[EmpId],
	[ReadTime],
	[DetailStatus]
FROM
	[dbo].[Sm_SmsDetail]
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
// 存储过程名：[dbo].Sm_SmsDetailLoad
// 存储过程功能描述：查询所有Sm_SmsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailLoad
AS

SELECT
	[DetailId],
	[SmsId],
	[EmpId],
	[ReadTime],
	[DetailStatus]
FROM
	[dbo].[Sm_SmsDetail]

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
// 存储过程名：[dbo].Sm_SmsDetailInsert
// 存储过程功能描述：新增一条Sm_SmsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailInsert
	@SmsId int =NULL ,
	@EmpId int =NULL ,
	@ReadTime datetime =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsDetail] (
	[SmsId],
	[EmpId],
	[ReadTime],
	[DetailStatus]
) VALUES (
	@SmsId,
	@EmpId,
	@ReadTime,
	@DetailStatus
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
// 存储过程名：[dbo].Sm_SmsDetailUpdate
// 存储过程功能描述：更新Sm_SmsDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsDetailUpdate
    @DetailId int,
@SmsId int = NULL,
@EmpId int = NULL,
@ReadTime datetime = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Sm_SmsDetail] SET
	[SmsId] = @SmsId,
	[EmpId] = @EmpId,
	[ReadTime] = @ReadTime,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



