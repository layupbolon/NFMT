alter table dbo.Sm_Sms
   drop constraint PK_SM_SMS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Sm_Sms')
            and   type = 'U')
   drop table dbo.Sm_Sms
go

/*==============================================================*/
/* Table: Sm_Sms                                                */
/*==============================================================*/
create table dbo.Sm_Sms (
   SmsId                int                  identity,
   SmsTypeId            int                  null,
   SmsHead              varchar(80)          null,
   SmsBody              varchar(200)         null,
   SmsRelTime           datetime             null,
   SmsStatus            int                  null,
   SmsLevel             int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   SourceId             int                  null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '消息',
   'user', 'dbo', 'table', 'Sm_Sms'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息序号',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsId'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息类别',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsTypeId'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息标题',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsHead'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息内容',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsBody'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息发布时间',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsRelTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息状态（0=无效消息，1=待处理消息，2=已处理消息）',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息优先级',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SmsLevel'
go

execute sp_addextendedproperty 'MS_Description', 
   '发起人序号',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发起时间',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '数据序号',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'SourceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Sm_Sms', 'column', 'LastModifyTime'
go

alter table dbo.Sm_Sms
   add constraint PK_SM_SMS primary key (SmsId)
go


/****** Object:  Stored Procedure [dbo].Sm_SmsGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsGet]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsLoad]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsInsert]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsUpdate]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsUpdateStatus
// 存储过程功能描述：更新Sm_Sms中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_Sms'

set @str = 'update [dbo].[Sm_Sms] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SmsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Sm_SmsGoBack
// 存储过程功能描述：撤返Sm_Sms，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_Sms'

set @str = 'update [dbo].[Sm_Sms] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SmsId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Sm_SmsGet
// 存储过程功能描述：查询指定Sm_Sms的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsGet
    /*
	@SmsId int
    */
    @id int
AS

SELECT
	[SmsId],
	[SmsTypeId],
	[SmsHead],
	[SmsBody],
	[SmsRelTime],
	[SmsStatus],
	[SmsLevel],
	[CreatorId],
	[CreateTime],
	[SourceId],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Sm_Sms]
WHERE
	[SmsId] = @id

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
// 存储过程名：[dbo].Sm_SmsLoad
// 存储过程功能描述：查询所有Sm_Sms记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsLoad
AS

SELECT
	[SmsId],
	[SmsTypeId],
	[SmsHead],
	[SmsBody],
	[SmsRelTime],
	[SmsStatus],
	[SmsLevel],
	[CreatorId],
	[CreateTime],
	[SourceId],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Sm_Sms]

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
// 存储过程名：[dbo].Sm_SmsInsert
// 存储过程功能描述：新增一条Sm_Sms记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsInsert
	@SmsTypeId int =NULL ,
	@SmsHead varchar(80) =NULL ,
	@SmsBody varchar(200) =NULL ,
	@SmsRelTime datetime =NULL ,
	@SmsStatus int =NULL ,
	@SmsLevel int =NULL ,
	@CreatorId int =NULL ,
	@SourceId int =NULL ,
	@SmsId int OUTPUT
AS

INSERT INTO [dbo].[Sm_Sms] (
	[SmsTypeId],
	[SmsHead],
	[SmsBody],
	[SmsRelTime],
	[SmsStatus],
	[SmsLevel],
	[CreatorId],
	[CreateTime],
	[SourceId],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SmsTypeId,
	@SmsHead,
	@SmsBody,
	@SmsRelTime,
	@SmsStatus,
	@SmsLevel,
	@CreatorId,
        getdate()
,
	@SourceId,
       @CreatorId
,
       getdate()

)


SET @SmsId = @@IDENTITY

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
// 存储过程名：[dbo].Sm_SmsUpdate
// 存储过程功能描述：更新Sm_Sms
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsUpdate
    @SmsId int,
@SmsTypeId int = NULL,
@SmsHead varchar(80) = NULL,
@SmsBody varchar(200) = NULL,
@SmsRelTime datetime = NULL,
@SmsStatus int = NULL,
@SmsLevel int = NULL,
@SourceId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Sm_Sms] SET
	[SmsTypeId] = @SmsTypeId,
	[SmsHead] = @SmsHead,
	[SmsBody] = @SmsBody,
	[SmsRelTime] = @SmsRelTime,
	[SmsStatus] = @SmsStatus,
	[SmsLevel] = @SmsLevel,
	[SourceId] = @SourceId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[SmsId] = @SmsId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



