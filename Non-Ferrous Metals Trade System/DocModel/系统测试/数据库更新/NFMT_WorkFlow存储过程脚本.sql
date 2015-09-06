USE [NFMT_WorkFlow]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_AuditEmpUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_DataSourceUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeConditionUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_NodeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_FlowMasterUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskAttachGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskAttachGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskAttachGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskNodeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogGet]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogGet]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogGoBack]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogInsert]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogInsert]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogLoad]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogLoad]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskOperateLogUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskUpdate]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Wf_TaskUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectPager]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


create proc [dbo].[SelectPager]
    @pageIndex int=1, --当前页
    @pageSize int=10, --一页显示条数
    @columnName nvarchar(800)=''*'', --列名
    @tableName nvarchar(500)='''',   --表名
    @orderStr nvarchar(500), --排序列，
    @whereStr varchar(1000)='''', --条件
    @rowCount int output     --总记录条数
as
begin
declare @beginRow int,--起始行
        @endRow int,  --结束行
        @sql nvarchar(3000) ,
        @sqlfrom nvarchar(510), 
        @sqlWhere nvarchar(1010),
        @sqlCount nvarchar(1520)
       
        set @beginRow =(@pageIndex-1)*@pageSize+1
        set @endRow = @pageIndex*@pageSize

        set @sql =N'' select row_number() over(order by ''+@orderStr+'') as rowSerial, ''+@columnName
        set @sqlfrom ='' from ''+@tableName+''''

    --是否在条件
    if len(@whereStr)>0
        set @sqlWhere='' where ''+@whereStr
    else
        set @sqlWhere='' ''

        --总记录条数
        set @sqlCount=''select @count=count(0) ''+@sqlfrom+'' ''+@sqlWhere
        exec sp_executesql @sqlCount, N''@count bigint output'',@count=@rowCount output


        set @sql =@sql+@sqlfrom+@sqlWhere
		
		set @sql=''select * from ( ''+@sql+'') as t where rowSerial between ''+convert(varchar,@beginRow) +'' and ''+convert(varchar,@endRow)
        
        print @sql
        exec (@sql)
end





' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocUpdateStatus
// 存储过程功能描述：更新Bloc中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

create PROCEDURE [dbo].[BlocUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Bloc''

set @str = ''update NFMT_User.[dbo].[Bloc] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BlocId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskUpdateStatus
// 存储过程功能描述：更新Wf_Task中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_Task''

set @str = ''update [dbo].[Wf_Task] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskUpdate
// 存储过程功能描述：更新Wf_Task
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskUpdate]
    @TaskId int,
@MasterId int = NULL,
@TaskName varchar(200) = NULL,
@TaskConnext varchar(max) = NULL,
@TaskStatus int = NULL,
@DataSourceId int = NULL,
@TaskType int = NULL
AS

UPDATE [dbo].[Wf_Task] SET
	[MasterId] = @MasterId,
	[TaskName] = @TaskName,
	[TaskConnext] = @TaskConnext,
	[TaskStatus] = @TaskStatus,
	[DataSourceId] = @DataSourceId,
	[TaskType] = @TaskType
WHERE
	[TaskId] = @TaskId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogUpdateStatus
// 存储过程功能描述：更新Wf_TaskOperateLog中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_TaskOperateLog''

set @str = ''update [dbo].[Wf_TaskOperateLog] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where LogId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogUpdate
// 存储过程功能描述：更新Wf_TaskOperateLog
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogUpdate]
    @LogId int,
@TaskNodeId int,
@EmpId int,
@Memo varchar(4000) = NULL,
@LogTime datetime = NULL,
@LogResult varchar(400) = NULL
AS

UPDATE [dbo].[Wf_TaskOperateLog] SET
	[TaskNodeId] = @TaskNodeId,
	[EmpId] = @EmpId,
	[Memo] = @Memo,
	[LogTime] = @LogTime,
	[LogResult] = @LogResult
WHERE
	[LogId] = @LogId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogLoad
// 存储过程功能描述：查询所有Wf_TaskOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogLoad]
AS

SELECT
	[LogId],
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
FROM
	[dbo].[Wf_TaskOperateLog]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogInsert
// 存储过程功能描述：新增一条Wf_TaskOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogInsert]
	@TaskNodeId int ,
	@EmpId int ,
	@Memo varchar(4000) =NULL ,
	@LogTime datetime =NULL ,
	@LogResult varchar(400) =NULL ,
	@LogId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskOperateLog] (
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
) VALUES (
	@TaskNodeId,
	@EmpId,
	@Memo,
	@LogTime,
	@LogResult
)


SET @LogId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogGoBack
// 存储过程功能描述：撤返Wf_TaskOperateLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_TaskOperateLog''

set @str = ''update [dbo].[Wf_TaskOperateLog] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where LogId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskOperateLogGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskOperateLogGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogGet
// 存储过程功能描述：查询指定Wf_TaskOperateLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskOperateLogGet]
	@LogId int
AS

SELECT
	[LogId],
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
FROM
	[dbo].[Wf_TaskOperateLog]
WHERE
	[LogId] = @LogId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Wf_TaskNodeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_TaskNode''

set @str = ''update [dbo].[Wf_TaskNode] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskNodeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeUpdate
// 存储过程功能描述：更新Wf_TaskNode
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskNodeUpdate]
    @TaskNodeId int,
@NodeId int,
@TaskId int,
@NodeLevel int = NULL,
@NodeStatus int = NULL,
@EmpId int = NULL,
@AuditTime datetime = NULL
AS

UPDATE [dbo].[Wf_TaskNode] SET
	[NodeId] = @NodeId,
	[TaskId] = @TaskId,
	[NodeLevel] = @NodeLevel,
	[NodeStatus] = @NodeStatus,
	[EmpId] = @EmpId,
	[AuditTime] = @AuditTime
WHERE
	[TaskNodeId] = @TaskNodeId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeLoad
// 存储过程功能描述：查询所有Wf_TaskNode记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskNodeLoad]
AS

SELECT
	[TaskNodeId],
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
FROM
	[dbo].[Wf_TaskNode]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeInsert
// 存储过程功能描述：新增一条Wf_TaskNode记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskNodeInsert]
	@NodeId int ,
	@TaskId int ,
	@NodeLevel int =NULL ,
	@NodeStatus int =NULL ,
	@EmpId int =NULL ,
	@AuditTime datetime =NULL ,
	@TaskNodeId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskNode] (
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
) VALUES (
	@NodeId,
	@TaskId,
	@NodeLevel,
	@NodeStatus,
	@EmpId,
	@AuditTime
)


SET @TaskNodeId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeGoBack
// 存储过程功能描述：撤返Wf_TaskNode，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskNodeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_TaskNode''

set @str = ''update [dbo].[Wf_TaskNode] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskNodeId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskNodeGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskNodeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeGet
// 存储过程功能描述：查询指定Wf_TaskNode的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskNodeGet]
	@id int
AS

SELECT
	[TaskNodeId],
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
FROM
	[dbo].[Wf_TaskNode]
WHERE
	[TaskNodeId] = @id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskLoad
// 存储过程功能描述：查询所有Wf_Task记录
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskLoad]
AS

SELECT
	[TaskId],
	[MasterId],
	[TaskName],
	[TaskConnext],
	[TaskStatus],
	[DataSourceId],
	[TaskType]
FROM
	[dbo].[Wf_Task]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskInsert
// 存储过程功能描述：新增一条Wf_Task记录
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskInsert]
	@MasterId int =NULL ,
	@TaskName varchar(200) =NULL ,
	@TaskConnext varchar(max) =NULL ,
	@TaskStatus int =NULL ,
	@DataSourceId int =NULL ,
	@TaskType int =NULL ,
	@TaskId int OUTPUT
AS

INSERT INTO [dbo].[Wf_Task] (
	[MasterId],
	[TaskName],
	[TaskConnext],
	[TaskStatus],
	[DataSourceId],
	[TaskType]
) VALUES (
	@MasterId,
	@TaskName,
	@TaskConnext,
	@TaskStatus,
	@DataSourceId,
	@TaskType
)


SET @TaskId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskGoBack
// 存储过程功能描述：撤返Wf_Task，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_Task''

set @str = ''update [dbo].[Wf_Task] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskGet
// 存储过程功能描述：查询指定Wf_Task的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskGet]
	@id int
AS

declare @str varchar(2000)
set @str = '' ''						 
select @str = @str + CONVERT(varchar, t.TaskNodeId) + '': '' + e.Name + ''在 '' + CONVERT(varchar,t.AuditTime,120) + '' '' + bd.StatusName + ''，''
from dbo.Wf_TaskNode t left join NFMT_User.dbo.Employee e on t.EmpId = e.EmpId
					   left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = t.NodeStatus

where TaskId = @id

if(LEN(@str)>1)
	set @str = SUBSTRING(@str,1,len(@str)-1)
	
SELECT
	t.[TaskId],
	t.[MasterId],
	t.[TaskName],
	t.[TaskStatus],
	t.[DataSourceId],
	t.[TaskType],
	t.[TaskConnext],
	ds.ApplyMemo,
	ds.ApplyTime,
	ds.ViewUrl,
	e.Name as EmpName,
	@str as FlowDescribtion
FROM
	[dbo].[Wf_Task] t left join dbo.Wf_DataSource ds on ds.SourceId = t.DataSourceId
				      left join NFMT_User.dbo.Employee e on ds.EmpId = e.EmpId
				      left join NFMT_Basic.dbo.BDStatusDetail bd on bd.DetailId = t.TaskStatus
WHERE
	t.[TaskId] = @id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_TaskAttachGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_TaskAttachGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskAttachGoBack
// 存储过程功能描述：撤返Wf_TaskAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_TaskAttachGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_TaskAttach''

set @str = ''update [dbo].[Wf_TaskAttach] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where TaskAttachId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterUpdateStatus
// 存储过程功能描述：更新Wf_FlowMaster中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_FlowMaster''

set @str = ''update [dbo].[Wf_FlowMaster] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MasterId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterUpdate
// 存储过程功能描述：更新Wf_FlowMaster
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterUpdate]
    @MasterId int,
@MasterName varchar(200),
@MasterStatus int,
@ViewUrl varchar(200) = NULL,
@ConditionUrl varchar(200) = NULL,
@SuccessUrl varchar(200) = NULL,
@RefusalUrl varchar(200) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Wf_FlowMaster] SET
	[MasterName] = @MasterName,
	[MasterStatus] = @MasterStatus,
	[ViewUrl] = @ViewUrl,
	[ConditionUrl] = @ConditionUrl,
	[SuccessUrl] = @SuccessUrl,
	[RefusalUrl] = @RefusalUrl,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MasterId] = @MasterId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterLoad
// 存储过程功能描述：查询所有Wf_FlowMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterLoad]
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_FlowMaster]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterInsert
// 存储过程功能描述：新增一条Wf_FlowMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterInsert]
	@MasterName varchar(200) ,
	@MasterStatus int ,
	@ViewUrl varchar(200) =NULL ,
	@ConditionUrl varchar(200) =NULL ,
	@SuccessUrl varchar(200) =NULL ,
	@RefusalUrl varchar(200) =NULL ,
	@CreatorId int =NULL ,
	@MasterId int OUTPUT
AS

INSERT INTO [dbo].[Wf_FlowMaster] (
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterName,
	@MasterStatus,
	@ViewUrl,
	@ConditionUrl,
	@SuccessUrl,
	@RefusalUrl,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MasterId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterGoBack
// 存储过程功能描述：撤返Wf_FlowMaster，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_FlowMaster''

set @str = ''update [dbo].[Wf_FlowMaster] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MasterId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_FlowMasterGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_FlowMasterGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterGet
// 存储过程功能描述：查询指定Wf_FlowMaster的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_FlowMasterGet]
	@id int
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_FlowMaster]
WHERE
	[MasterId] = @id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeUpdateStatus
// 存储过程功能描述：更新Wf_Node中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_Node''

set @str = ''update [dbo].[Wf_Node] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where NodeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeUpdate
// 存储过程功能描述：更新Wf_Node
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeUpdate]
    @NodeId int,
@MasterId int,
@NodeStatus int,
@NodeName varchar(20),
@NodeType int = NULL,
@IsFirst bit = NULL,
@IsLast bit = NULL,
@PreNodeId int = NULL,
@RoleId int = NULL,
@AuthGroupId int = NULL,
@NodeLevel int = NULL
AS

UPDATE [dbo].[Wf_Node] SET
	[MasterId] = @MasterId,
	[NodeStatus] = @NodeStatus,
	[NodeName] = @NodeName,
	[NodeType] = @NodeType,
	[IsFirst] = @IsFirst,
	[IsLast] = @IsLast,
	[PreNodeId] = @PreNodeId,
	[RoleId] = @RoleId,
	[AuthGroupId] = @AuthGroupId,
	[NodeLevel] = @NodeLevel
WHERE
	[NodeId] = @NodeId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeLoad
// 存储过程功能描述：查询所有Wf_Node记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeLoad]
AS

SELECT
	[NodeId],
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[RoleId],
	[AuthGroupId],
	[NodeLevel]
FROM
	[dbo].[Wf_Node]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeInsert
// 存储过程功能描述：新增一条Wf_Node记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeInsert]
	@MasterId int ,
	@NodeStatus int ,
	@NodeName varchar(20) ,
	@NodeType int =NULL ,
	@IsFirst bit =NULL ,
	@IsLast bit =NULL ,
	@PreNodeId int =NULL ,
	@RoleId int =NULL ,
	@AuthGroupId int =NULL ,
	@NodeLevel int =NULL ,
	@NodeId int OUTPUT
AS

INSERT INTO [dbo].[Wf_Node] (
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[RoleId],
	[AuthGroupId],
	[NodeLevel]
) VALUES (
	@MasterId,
	@NodeStatus,
	@NodeName,
	@NodeType,
	@IsFirst,
	@IsLast,
	@PreNodeId,
	@RoleId,
	@AuthGroupId,
	@NodeLevel
)


SET @NodeId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeGoBack
// 存储过程功能描述：撤返Wf_Node，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_Node''

set @str = ''update [dbo].[Wf_Node] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where NodeId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeGet
// 存储过程功能描述：查询指定Wf_Node的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeGet]
	@id int
AS

SELECT
	[NodeId],
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[RoleId],
	[AuthGroupId],
	[NodeLevel]
FROM
	[dbo].[Wf_Node]
WHERE
	[NodeId] = @id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionUpdateStatus
// 存储过程功能描述：更新Wf_NodeCondition中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_NodeCondition''

set @str = ''update [dbo].[Wf_NodeCondition] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ConditionId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionUpdate
// 存储过程功能描述：更新Wf_NodeCondition
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionUpdate]
    @ConditionId int,
@ConditionStatus int,
@NodeId int,
@FieldName varchar(50) = NULL,
@FieldValue varchar(50) = NULL,
@ConditionType int = NULL,
@LogicType int = NULL
AS

UPDATE [dbo].[Wf_NodeCondition] SET
	[ConditionStatus] = @ConditionStatus,
	[NodeId] = @NodeId,
	[FieldName] = @FieldName,
	[FieldValue] = @FieldValue,
	[ConditionType] = @ConditionType,
	[LogicType] = @LogicType
WHERE
	[ConditionId] = @ConditionId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionLoad
// 存储过程功能描述：查询所有Wf_NodeCondition记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionLoad]
AS

SELECT
	[ConditionId],
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
FROM
	[dbo].[Wf_NodeCondition]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionInsert
// 存储过程功能描述：新增一条Wf_NodeCondition记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionInsert]
	@ConditionStatus int ,
	@NodeId int ,
	@FieldName varchar(50) =NULL ,
	@FieldValue varchar(50) =NULL ,
	@ConditionType int =NULL ,
	@LogicType int =NULL ,
	@ConditionId int OUTPUT
AS

INSERT INTO [dbo].[Wf_NodeCondition] (
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
) VALUES (
	@ConditionStatus,
	@NodeId,
	@FieldName,
	@FieldValue,
	@ConditionType,
	@LogicType
)


SET @ConditionId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionGoBack
// 存储过程功能描述：撤返Wf_NodeCondition，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_NodeCondition''

set @str = ''update [dbo].[Wf_NodeCondition] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ConditionId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_NodeConditionGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_NodeConditionGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionGet
// 存储过程功能描述：查询指定Wf_NodeCondition的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_NodeConditionGet]
	@ConditionId int
AS

SELECT
	[ConditionId],
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
FROM
	[dbo].[Wf_NodeCondition]
WHERE
	[ConditionId] = @ConditionId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceUpdateStatus
// 存储过程功能描述：更新Wf_DataSource中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_DataSource''

set @str = ''update [dbo].[Wf_DataSource] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SourceId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceUpdate
// 存储过程功能描述：更新Wf_DataSource
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceUpdate]
    @SourceId int,
@BaseName varchar(200) = NULL,
@TableCode varchar(50) = NULL,
@DataStatus int = NULL,
@RowId int = NULL,
@DalName varchar(80) = NULL,
@AssName varchar(50) = NULL,
@ViewUrl varchar(400) = NULL,
@RefusalUrl varchar(800) = NULL,
@SuccessUrl varchar(800) = NULL,
@ConditionUrl varchar(800) = NULL,
@EmpId int = NULL,
@ApplyTime datetime = NULL,
@ApplyTitle varchar(400) = NULL,
@ApplyMemo varchar(4000) = NULL,
@ApplyInfo varchar(4000) = NULL
AS

UPDATE [dbo].[Wf_DataSource] SET
	[BaseName] = @BaseName,
	[TableCode] = @TableCode,
	[DataStatus] = @DataStatus,
	[RowId] = @RowId,
	[DalName] = @DalName,
	[AssName] = @AssName,
	[ViewUrl] = @ViewUrl,
	[RefusalUrl] = @RefusalUrl,
	[SuccessUrl] = @SuccessUrl,
	[ConditionUrl] = @ConditionUrl,
	[EmpId] = @EmpId,
	[ApplyTime] = @ApplyTime,
	[ApplyTitle] = @ApplyTitle,
	[ApplyMemo] = @ApplyMemo,
	[ApplyInfo] = @ApplyInfo
WHERE
	[SourceId] = @SourceId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceLoad
// 存储过程功能描述：查询所有Wf_DataSource记录
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceLoad]
AS

SELECT
	[SourceId],
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
FROM
	[dbo].[Wf_DataSource]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceInsert
// 存储过程功能描述：新增一条Wf_DataSource记录
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceInsert]
	@BaseName varchar(200) =NULL ,
	@TableCode varchar(50) =NULL ,
	@DataStatus int =NULL ,
	@RowId int =NULL ,
	@DalName varchar(80) =NULL ,
	@AssName varchar(50) =NULL ,
	@ViewUrl varchar(400) =NULL ,
	@RefusalUrl varchar(800) =NULL ,
	@SuccessUrl varchar(800) =NULL ,
	@ConditionUrl varchar(800) =NULL ,
	@EmpId int =NULL ,
	@ApplyTime datetime =NULL ,
	@ApplyTitle varchar(400) =NULL ,
	@ApplyMemo varchar(4000) =NULL ,
	@ApplyInfo varchar(4000) =NULL ,
	@SourceId int OUTPUT
AS

INSERT INTO [dbo].[Wf_DataSource] (
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
) VALUES (
	@BaseName,
	@TableCode,
	@DataStatus,
	@RowId,
	@DalName,
	@AssName,
	@ViewUrl,
	@RefusalUrl,
	@SuccessUrl,
	@ConditionUrl,
	@EmpId,
	@ApplyTime,
	@ApplyTitle,
	@ApplyMemo,
	@ApplyInfo
)


SET @SourceId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceGoBack
// 存储过程功能描述：撤返Wf_DataSource，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_DataSource''

set @str = ''update [dbo].[Wf_DataSource] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SourceId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_DataSourceGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_DataSourceGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceGet
// 存储过程功能描述：查询指定Wf_DataSource的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_DataSourceGet]
	@id int
AS

SELECT
	[SourceId],
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
FROM
	[dbo].[Wf_DataSource]
WHERE
	[SourceId] = @id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpUpdateStatus]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpUpdateStatus
// 存储过程功能描述：更新Wf_AuditEmp中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_AuditEmp''

set @str = ''update [dbo].[Wf_AuditEmp] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where AuditEmpId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpUpdate]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpUpdate
// 存储过程功能描述：更新Wf_AuditEmp
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpUpdate]
    @AuditEmpId int,
@TaskNodeId int,
@EmpId int
AS

UPDATE [dbo].[Wf_AuditEmp] SET
	[TaskNodeId] = @TaskNodeId,
	[EmpId] = @EmpId
WHERE
	[AuditEmpId] = @AuditEmpId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpLoad]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpLoad
// 存储过程功能描述：查询所有Wf_AuditEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpLoad]
AS

SELECT
	[AuditEmpId],
	[TaskNodeId],
	[EmpId]
FROM
	[dbo].[Wf_AuditEmp]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpInsert]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpInsert
// 存储过程功能描述：新增一条Wf_AuditEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpInsert]
	@TaskNodeId int ,
	@EmpId int ,
	@AuditEmpId int OUTPUT
AS

INSERT INTO [dbo].[Wf_AuditEmp] (
	[TaskNodeId],
	[EmpId]
) VALUES (
	@TaskNodeId,
	@EmpId
)


SET @AuditEmpId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpGoBack]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpGoBack
// 存储过程功能描述：撤返Wf_AuditEmp，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Wf_AuditEmp''

set @str = ''update [dbo].[Wf_AuditEmp] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where AuditEmpId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Wf_AuditEmpGet]    Script Date: 10/22/2014 11:04:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Wf_AuditEmpGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpGet
// 存储过程功能描述：查询指定Wf_AuditEmp的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Wf_AuditEmpGet]
	@AuditEmpId int
AS

SELECT
	[AuditEmpId],
	[TaskNodeId],
	[EmpId]
FROM
	[dbo].[Wf_AuditEmp]
WHERE
	[AuditEmpId] = @AuditEmpId

' 
END
GO
