alter table dbo.ClauseContract_Ref
   drop constraint PK_CLAUSECONTRACT_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ClauseContract_Ref')
            and   type = 'U')
   drop table dbo.ClauseContract_Ref
go

/*==============================================================*/
/* Table: ClauseContract_Ref                                    */
/*==============================================================*/
create table dbo.ClauseContract_Ref (
   RefId                int                  identity,
   MasterId             int                  null,
   ClauseId             int                  null,
   Sort                 int                  null,
   IsChose              bit                  null,
   RefStatus            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '模板条款关联表',
   'user', 'dbo', 'table', 'ClauseContract_Ref'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联序号序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'RefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约模板序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'MasterId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约条款序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'ClauseId'
go

execute sp_addextendedproperty 'MS_Description', 
   '排序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'Sort'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否选中',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'IsChose'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'RefStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'ClauseContract_Ref', 'column', 'LastModifyTime'
go

alter table dbo.ClauseContract_Ref
   add constraint PK_CLAUSECONTRACT_REF primary key (RefId)
go

/****** Object:  Stored Procedure [dbo].ClauseContract_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefGet]
GO

/****** Object:  Stored Procedure [dbo].ClauseContract_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].ClauseContract_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].ClauseContract_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].ClauseContract_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].ClauseContract_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ClauseContract_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ClauseContract_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ClauseContract_RefUpdateStatus
// 存储过程功能描述：更新ClauseContract_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ClauseContract_Ref'

set @str = 'update [dbo].[ClauseContract_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ClauseContract_RefGoBack
// 存储过程功能描述：撤返ClauseContract_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.ClauseContract_Ref'

set @str = 'update [dbo].[ClauseContract_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].ClauseContract_RefGet
// 存储过程功能描述：查询指定ClauseContract_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ClauseContract_Ref]
WHERE
	[RefId] = @id

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
// 存储过程名：[dbo].ClauseContract_RefLoad
// 存储过程功能描述：查询所有ClauseContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefLoad
AS

SELECT
	[RefId],
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[ClauseContract_Ref]

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
// 存储过程名：[dbo].ClauseContract_RefInsert
// 存储过程功能描述：新增一条ClauseContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefInsert
	@MasterId int =NULL ,
	@ClauseId int =NULL ,
	@Sort int =NULL ,
	@IsChose bit =NULL ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[ClauseContract_Ref] (
	[MasterId],
	[ClauseId],
	[Sort],
	[IsChose],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterId,
	@ClauseId,
	@Sort,
	@IsChose,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RefId = @@IDENTITY

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
// 存储过程名：[dbo].ClauseContract_RefUpdate
// 存储过程功能描述：更新ClauseContract_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].ClauseContract_RefUpdate
    @RefId int,
@MasterId int = NULL,
@ClauseId int = NULL,
@Sort int = NULL,
@IsChose bit = NULL,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[ClauseContract_Ref] SET
	[MasterId] = @MasterId,
	[ClauseId] = @ClauseId,
	[Sort] = @Sort,
	[IsChose] = @IsChose,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



