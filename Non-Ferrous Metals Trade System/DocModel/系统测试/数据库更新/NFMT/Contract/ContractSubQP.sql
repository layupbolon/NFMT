alter table dbo.Con_ContractSubQP
   drop constraint PK_CON_CONTRACTSUBQP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractSubQP')
            and   type = 'U')
   drop table dbo.Con_ContractSubQP
go

/*==============================================================*/
/* Table: Con_ContractSubQP                                     */
/*==============================================================*/
create table dbo.Con_ContractSubQP (
   QPId                 int                  identity,
   SubId                int                  null,
   CurQP                int                  null,
   InitAmount           numeric(19,4)        not null default 0,
   ChgedAmount          numeric(19,4)        not null default 0,
   QPChgDate            int                  not null default convert(int,convert(varchar(8),getdate(),112)),
   QPFrom               int                  not null default -1,
   ThisQPFeeBala        numeric(19,2)        not null default 0,
   QPMemo               varchar(200)         not null,
   TotalInitFeeBala     numeric(19,4)        not null default 0,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '子合约QP',
   'user', 'dbo', 'table', 'Con_ContractSubQP'
go

execute sp_addextendedproperty 'MS_Description', 
   'QP序号',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'QPId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约内序号',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '当前QP',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'CurQP'
go

execute sp_addextendedproperty 'MS_Description', 
   '初数量',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'InitAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '被延期数量',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'ChgedAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '延期日',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'QPChgDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '前QP序号',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'QPFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '本次延期费用金额',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'ThisQPFeeBala'
go

execute sp_addextendedproperty 'MS_Description', 
   'QPMemo',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'QPMemo'
go

execute sp_addextendedproperty 'MS_Description', 
   '从初始QP至今累积金额',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'TotalInitFeeBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractSubQP', 'column', 'LastModifyTime'
go

alter table dbo.Con_ContractSubQP
   add constraint PK_CON_CONTRACTSUBQP primary key (QPId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractSubQPGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubQPLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubQPInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubQPUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubQPUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubQPUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubQPGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubQPGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractSubQPUpdateStatus
// 存储过程功能描述：更新Con_ContractSubQP中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSubQP'

set @str = 'update [dbo].[Con_ContractSubQP] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where QPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubQPGoBack
// 存储过程功能描述：撤返Con_ContractSubQP，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSubQP'

set @str = 'update [dbo].[Con_ContractSubQP] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where QPId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubQPGet
// 存储过程功能描述：查询指定Con_ContractSubQP的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPGet
    /*
	@QPId int
    */
    @id int
AS

SELECT
	[QPId],
	[SubId],
	[CurQP],
	[InitAmount],
	[ChgedAmount],
	[QPChgDate],
	[QPFrom],
	[ThisQPFeeBala],
	[QPMemo],
	[TotalInitFeeBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractSubQP]
WHERE
	[QPId] = @id

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
// 存储过程名：[dbo].Con_ContractSubQPLoad
// 存储过程功能描述：查询所有Con_ContractSubQP记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPLoad
AS

SELECT
	[QPId],
	[SubId],
	[CurQP],
	[InitAmount],
	[ChgedAmount],
	[QPChgDate],
	[QPFrom],
	[ThisQPFeeBala],
	[QPMemo],
	[TotalInitFeeBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractSubQP]

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
// 存储过程名：[dbo].Con_ContractSubQPInsert
// 存储过程功能描述：新增一条Con_ContractSubQP记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPInsert
	@SubId int =NULL ,
	@CurQP int =NULL ,
	@InitAmount numeric(19, 4) ,
	@ChgedAmount numeric(19, 4) ,
	@QPChgDate int ,
	@QPFrom int ,
	@ThisQPFeeBala numeric(19, 2) ,
	@QPMemo varchar(200) ,
	@TotalInitFeeBala numeric(19, 4) ,
	@CreatorId int =NULL ,
	@QPId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractSubQP] (
	[SubId],
	[CurQP],
	[InitAmount],
	[ChgedAmount],
	[QPChgDate],
	[QPFrom],
	[ThisQPFeeBala],
	[QPMemo],
	[TotalInitFeeBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SubId,
	@CurQP,
	@InitAmount,
	@ChgedAmount,
	@QPChgDate,
	@QPFrom,
	@ThisQPFeeBala,
	@QPMemo,
	@TotalInitFeeBala,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @QPId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractSubQPUpdate
// 存储过程功能描述：更新Con_ContractSubQP
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubQPUpdate
    @QPId int,
@SubId int = NULL,
@CurQP int = NULL,
@InitAmount numeric(19, 4),
@ChgedAmount numeric(19, 4),
@QPChgDate int,
@QPFrom int,
@ThisQPFeeBala numeric(19, 2),
@QPMemo varchar(200),
@TotalInitFeeBala numeric(19, 4),
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_ContractSubQP] SET
	[SubId] = @SubId,
	[CurQP] = @CurQP,
	[InitAmount] = @InitAmount,
	[ChgedAmount] = @ChgedAmount,
	[QPChgDate] = @QPChgDate,
	[QPFrom] = @QPFrom,
	[ThisQPFeeBala] = @ThisQPFeeBala,
	[QPMemo] = @QPMemo,
	[TotalInitFeeBala] = @TotalInitFeeBala,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[QPId] = @QPId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



