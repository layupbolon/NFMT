USE [NFMT_Sms]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigGet]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigGet]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigGoBack]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigInsert]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigLoad]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigUpdate]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigUpdateStatus]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsConfigUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailGet]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailGoBack]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailInsert]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailLoad]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailUpdate]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsGet]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsGet]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsGoBack]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsInsert]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsLoad]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterGet]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterGet]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterGoBack]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterInsert]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterLoad]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterUpdate]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsParameterUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadGoBack]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeGet]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeGet]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeGoBack]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeInsert]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeLoad]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeUpdate]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsTypeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsUpdate]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadInsert]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadInsert]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadLoad]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadLoad]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadUpdate]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadUpdate]
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadGet]    Script Date: 10/22/2014 11:02:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Sm_SmsReadGet]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectPager]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:02:24 ******/
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
    @columnName varchar(4000)=''*'', --列名
    @tableName varchar(8000)='''',   --表名
    @orderStr varchar(1000), --排序列，
    @whereStr varchar(2000)='''', --条件
    @rowCount int output     --总记录条数
as
begin
declare @beginRow int,--起始行
        @endRow int,  --结束行
        @sql varchar(max) ,
        @sqlfrom varchar(8000), 
        @sqlWhere varchar(2000),
        @sqlCount nvarchar(max)
       
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
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadGet]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadGet
// 存储过程功能描述：查询指定Sm_SmsRead的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadGet]
	@SmsReadId int
AS

SELECT
	[SmsReadId],
	[SmsId],
	[EmpId],
	[LastReadTime],
	[ReadStatus]
FROM
	[dbo].[Sm_SmsRead]
WHERE
	[SmsReadId] = @SmsReadId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadUpdate]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadUpdate
// 存储过程功能描述：更新Sm_SmsRead
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadUpdate]
    @SmsReadId int,
@SmsId int = NULL,
@EmpId int = NULL,
@LastReadTime datetime = NULL,
@ReadStatus int = NULL
AS

UPDATE [dbo].[Sm_SmsRead] SET
	[SmsId] = @SmsId,
	[EmpId] = @EmpId,
	[LastReadTime] = @LastReadTime,
	[ReadStatus] = @ReadStatus
WHERE
	[SmsReadId] = @SmsReadId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadLoad]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadLoad
// 存储过程功能描述：查询所有Sm_SmsRead记录
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadLoad]
AS

SELECT
	[SmsReadId],
	[SmsId],
	[EmpId],
	[LastReadTime],
	[ReadStatus]
FROM
	[dbo].[Sm_SmsRead]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadInsert]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadInsert
// 存储过程功能描述：新增一条Sm_SmsRead记录
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadInsert]
	@SmsId int =NULL ,
	@EmpId int =NULL ,
	@LastReadTime datetime =NULL ,
	@ReadStatus int =NULL ,
	@SmsReadId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsRead] (
	[SmsId],
	[EmpId],
	[LastReadTime],
	[ReadStatus]
) VALUES (
	@SmsId,
	@EmpId,
	@LastReadTime,
	@ReadStatus
)


SET @SmsReadId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsUpdateStatus
// 存储过程功能描述：更新Sm_Sms中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_Sms''

set @str = ''update [dbo].[Sm_Sms] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where SmsId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsUpdate]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsUpdate
// 存储过程功能描述：更新Sm_Sms
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsUpdate]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeUpdateStatus
// 存储过程功能描述：更新Sm_SmsType中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsType''

set @str = ''update [dbo].[Sm_SmsType] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsTypeId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeUpdate]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeUpdate
// 存储过程功能描述：更新Sm_SmsType
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeUpdate]
    @SmsTypeId int,
@TypeName varchar(200) = NULL,
@ListUrl varchar(200) = NULL,
@ViewUrl varchar(200) = NULL,
@SmsTypeStatus int = NULL,
@SourceBaseName varchar(50) = NULL,
@SourceTableName varchar(50) = NULL
AS

UPDATE [dbo].[Sm_SmsType] SET
	[TypeName] = @TypeName,
	[ListUrl] = @ListUrl,
	[ViewUrl] = @ViewUrl,
	[SmsTypeStatus] = @SmsTypeStatus,
	[SourceBaseName] = @SourceBaseName,
	[SourceTableName] = @SourceTableName
WHERE
	[SmsTypeId] = @SmsTypeId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeLoad]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeLoad
// 存储过程功能描述：查询所有Sm_SmsType记录
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeLoad]
AS

SELECT
	[SmsTypeId],
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
FROM
	[dbo].[Sm_SmsType]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeInsert]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeInsert
// 存储过程功能描述：新增一条Sm_SmsType记录
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeInsert]
	@TypeName varchar(200) =NULL ,
	@ListUrl varchar(200) =NULL ,
	@ViewUrl varchar(200) =NULL ,
	@SmsTypeStatus int =NULL ,
	@SourceBaseName varchar(50) =NULL ,
	@SourceTableName varchar(50) =NULL ,
	@SmsTypeId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsType] (
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
) VALUES (
	@TypeName,
	@ListUrl,
	@ViewUrl,
	@SmsTypeStatus,
	@SourceBaseName,
	@SourceTableName
)


SET @SmsTypeId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeGoBack]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeGoBack
// 存储过程功能描述：撤返Sm_SmsType，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsType''

set @str = ''update [dbo].[Sm_SmsType] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsTypeId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsTypeGet]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsTypeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeGet
// 存储过程功能描述：查询指定Sm_SmsType的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsTypeGet]
    /*
	@SmsTypeId int
    */
    @id int
AS

SELECT
	[SmsTypeId],
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
FROM
	[dbo].[Sm_SmsType]
WHERE
	[SmsTypeId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadUpdateStatus
// 存储过程功能描述：更新Sm_SmsRead中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsRead''

set @str = ''update [dbo].[Sm_SmsRead] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsReadId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsReadGoBack]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsReadGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsReadGoBack
// 存储过程功能描述：撤返Sm_SmsRead，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsReadGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsRead''

set @str = ''update [dbo].[Sm_SmsRead] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsReadId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterUpdateStatus
// 存储过程功能描述：更新Sm_SmsParameter中的状态
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Sm_SmsParameter''

set @str = ''update [dbo].[Sm_SmsParameter] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ParameterId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterUpdate]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterUpdate
// 存储过程功能描述：更新Sm_SmsParameter
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterUpdate]
    @ParameterId int,
@SmsTypeId int = NULL,
@ParameterType varchar(20) = NULL,
@ParamterValue varchar(50) = NULL,
@ParameterStatus int = NULL,
@IsType bit = NULL
AS

UPDATE [dbo].[Sm_SmsParameter] SET
	[SmsTypeId] = @SmsTypeId,
	[ParameterType] = @ParameterType,
	[ParamterValue] = @ParamterValue,
	[ParameterStatus] = @ParameterStatus,
	[IsType] = @IsType
WHERE
	[ParameterId] = @ParameterId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterLoad]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterLoad
// 存储过程功能描述：查询所有Sm_SmsParameter记录
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterLoad]
AS

SELECT
	[ParameterId],
	[SmsTypeId],
	[ParameterType],
	[ParamterValue],
	[ParameterStatus],
	[IsType]
FROM
	[dbo].[Sm_SmsParameter]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterInsert]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterInsert
// 存储过程功能描述：新增一条Sm_SmsParameter记录
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterInsert]
	@SmsTypeId int =NULL ,
	@ParameterType varchar(20) =NULL ,
	@ParamterValue varchar(50) =NULL ,
	@ParameterStatus int =NULL ,
	@IsType bit =NULL ,
	@ParameterId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsParameter] (
	[SmsTypeId],
	[ParameterType],
	[ParamterValue],
	[ParameterStatus],
	[IsType]
) VALUES (
	@SmsTypeId,
	@ParameterType,
	@ParamterValue,
	@ParameterStatus,
	@IsType
)


SET @ParameterId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterGoBack]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterGoBack
// 存储过程功能描述：撤返Sm_SmsParameter，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsParameter''

set @str = ''update [dbo].[Sm_SmsParameter] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ParameterId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsParameterGet]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsParameterGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsParameterGet
// 存储过程功能描述：查询指定Sm_SmsParameter的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsParameterGet]
    /*
	@ParameterId int
    */
    @id int
AS

SELECT
	[ParameterId],
	[SmsTypeId],
	[ParameterType],
	[ParamterValue],
	[ParameterStatus],
	[IsType]
FROM
	[dbo].[Sm_SmsParameter]
WHERE
	[ParameterId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsLoad]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsLoad
// 存储过程功能描述：查询所有Sm_Sms记录
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsLoad]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsInsert]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsInsert
// 存储过程功能描述：新增一条Sm_Sms记录
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsInsert]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsGoBack]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsGoBack
// 存储过程功能描述：撤返Sm_Sms，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_Sms''

set @str = ''update [dbo].[Sm_Sms] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where SmsId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsGet]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsGet
// 存储过程功能描述：查询指定Sm_Sms的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsGet]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailUpdateStatus]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailUpdateStatus
// 存储过程功能描述：更新Sm_SmsDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsDetail''

set @str = ''update [dbo].[Sm_SmsDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where DetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailUpdate]    Script Date: 10/22/2014 11:02:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailUpdate
// 存储过程功能描述：更新Sm_SmsDetail
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailUpdate]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailLoad]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailLoad
// 存储过程功能描述：查询所有Sm_SmsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailLoad]
AS

SELECT
	[DetailId],
	[SmsId],
	[EmpId],
	[ReadTime],
	[DetailStatus]
FROM
	[dbo].[Sm_SmsDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailInsert]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailInsert
// 存储过程功能描述：新增一条Sm_SmsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailInsert]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailGoBack]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailGoBack
// 存储过程功能描述：撤返Sm_SmsDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsDetail''

set @str = ''update [dbo].[Sm_SmsDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where DetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsDetailGet]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsDetailGet
// 存储过程功能描述：查询指定Sm_SmsDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsDetailGet]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigUpdateStatus]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigUpdateStatus
// 存储过程功能描述：更新Sm_SmsConfig中的状态
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Sm_SmsConfig''

set @str = ''update [dbo].[Sm_SmsConfig] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsConfigId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigUpdate]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigUpdate
// 存储过程功能描述：更新Sm_SmsConfig
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigUpdate]
    @SmsConfigId int,
@EmpId varchar(80) = NULL,
@ConfigStatus varchar(80) = NULL
AS

UPDATE [dbo].[Sm_SmsConfig] SET
	[EmpId] = @EmpId,
	[ConfigStatus] = @ConfigStatus
WHERE
	[SmsConfigId] = @SmsConfigId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigLoad]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigLoad
// 存储过程功能描述：查询所有Sm_SmsConfig记录
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigLoad]
AS

SELECT
	[SmsConfigId],
	[EmpId],
	[ConfigStatus]
FROM
	[dbo].[Sm_SmsConfig]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigInsert]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigInsert
// 存储过程功能描述：新增一条Sm_SmsConfig记录
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigInsert]
	@EmpId varchar(80) =NULL ,
	@ConfigStatus varchar(80) =NULL ,
	@SmsConfigId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsConfig] (
	[EmpId],
	[ConfigStatus]
) VALUES (
	@EmpId,
	@ConfigStatus
)


SET @SmsConfigId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigGoBack]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigGoBack
// 存储过程功能描述：撤返Sm_SmsConfig，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Sm_SmsConfig''

set @str = ''update [dbo].[Sm_SmsConfig] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where SmsConfigId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Sm_SmsConfigGet]    Script Date: 10/22/2014 11:02:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sm_SmsConfigGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsConfigGet
// 存储过程功能描述：查询指定Sm_SmsConfig的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Sm_SmsConfigGet]
    /*
	@SmsConfigId int
    */
    @id int
AS

SELECT
	[SmsConfigId],
	[EmpId],
	[ConfigStatus]
FROM
	[dbo].[Sm_SmsConfig]
WHERE
	[SmsConfigId] = @id

' 
END
GO
