USE [NFMT_Sms]
GO

SET IDENTITY_INSERT [dbo].[Sm_SmsType] ON
INSERT [dbo].[Sm_SmsType] ([SmsTypeId], [TypeName], [ListUrl], [ViewUrl], [SmsTypeStatus], [SourceBaseName], [SourceTableName]) VALUES (1, N'任务提醒', N'../WorkFlow/TaskList.aspx', N'../WorkFlow/TaskDetail.aspx?id=', 50, N'NFMT_WorkFlow', N'dbo.Wf_Task')
SET IDENTITY_INSERT [dbo].[Sm_SmsType] OFF
