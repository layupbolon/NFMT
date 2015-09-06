alter table dbo.Account
   drop constraint PK_ACCOUNT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Account')
            and   type = 'U')
   drop table dbo.Account
go

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table dbo.Account (
   AccId                int                  identity,
   AccountName          varchar(20)          null,
   PassWord             varchar(20)          null,
   AccStatus            int                  null,
   EmpId                int                  null,
   IsValid              bit                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '账户表',
   'user', 'dbo', 'table', 'Account'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户序号',
   'user', 'dbo', 'table', 'Account', 'column', 'AccId'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户名称',
   'user', 'dbo', 'table', 'Account', 'column', 'AccountName'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户密码',
   'user', 'dbo', 'table', 'Account', 'column', 'PassWord'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户状态',
   'user', 'dbo', 'table', 'Account', 'column', 'AccStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联员工序号',
   'user', 'dbo', 'table', 'Account', 'column', 'EmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否有效',
   'user', 'dbo', 'table', 'Account', 'column', 'IsValid'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Account', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Account', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Account', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Account', 'column', 'LastModifyTime'
go

alter table dbo.Account
   add constraint PK_ACCOUNT primary key (AccId)
go


/****** Object:  Stored Procedure [dbo].AccountGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountGet]
GO

/****** Object:  Stored Procedure [dbo].AccountLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountLoad]
GO

/****** Object:  Stored Procedure [dbo].AccountInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountInsert]
GO

/****** Object:  Stored Procedure [dbo].AccountUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountUpdate]
GO

/****** Object:  Stored Procedure [dbo].AccountUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AccountUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AccountGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AccountGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountUpdateStatus
// 存储过程功能描述：更新Account中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Account'

set @str = 'update [dbo].[Account] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AccId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AccountGoBack
// 存储过程功能描述：撤返Account，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Account'

set @str = 'update [dbo].[Account] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AccId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AccountGet
// 存储过程功能描述：查询指定Account的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountGet
    /*
	@AccId int
    */
    @id int
AS

SELECT
	[AccId],
	[AccountName],
	[PassWord],
	[AccStatus],
	[EmpId],
	[IsValid],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Account]
WHERE
	[AccId] = @id

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
// 存储过程名：[dbo].AccountLoad
// 存储过程功能描述：查询所有Account记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountLoad
AS

SELECT
	[AccId],
	[AccountName],
	[PassWord],
	[AccStatus],
	[EmpId],
	[IsValid],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Account]

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
// 存储过程名：[dbo].AccountInsert
// 存储过程功能描述：新增一条Account记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountInsert
	@AccountName varchar(20) ,
	@PassWord varchar(20) ,
	@AccStatus int =NULL ,
	@EmpId int ,
	@IsValid bit ,
	@CreatorId int ,
	@AccId int OUTPUT
AS

INSERT INTO [dbo].[Account] (
	[AccountName],
	[PassWord],
	[AccStatus],
	[EmpId],
	[IsValid],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AccountName,
	@PassWord,
	@AccStatus,
	@EmpId,
	@IsValid,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AccId = @@IDENTITY

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
// 存储过程名：[dbo].AccountUpdate
// 存储过程功能描述：更新Account
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AccountUpdate
    @AccId int,
@AccountName varchar(20),
@PassWord varchar(20),
@AccStatus int = NULL,
@EmpId int,
@IsValid bit,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Account] SET
	[AccountName] = @AccountName,
	[PassWord] = @PassWord,
	[AccStatus] = @AccStatus,
	[EmpId] = @EmpId,
	[IsValid] = @IsValid,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AccId] = @AccId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



