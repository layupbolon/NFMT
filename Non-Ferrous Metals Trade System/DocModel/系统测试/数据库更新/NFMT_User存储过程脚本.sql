USE [NFMT_User]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefGet]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefInsert]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefLoad]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailEmp_RefUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionGet]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionInsert]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionLoad]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthOptionUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailGet]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailInsert]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailLoad]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupDetailUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupGet]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupInsert]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupLoad]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AuthGroupUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[AccountGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountGet]
GO
/****** Object:  StoredProcedure [dbo].[AccountGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountGoBack]
GO
/****** Object:  StoredProcedure [dbo].[AccountInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountInsert]
GO
/****** Object:  StoredProcedure [dbo].[AccountLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountLoad]
GO
/****** Object:  StoredProcedure [dbo].[AccountUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountUpdate]
GO
/****** Object:  StoredProcedure [dbo].[AccountUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AccountUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[BlocGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocGet]
GO
/****** Object:  StoredProcedure [dbo].[BlocGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocGoBack]
GO
/****** Object:  StoredProcedure [dbo].[BlocInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocInsert]
GO
/****** Object:  StoredProcedure [dbo].[BlocLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocLoad]
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocUpdate]
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlocUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[ContactGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactGet]
GO
/****** Object:  StoredProcedure [dbo].[ContactGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactGoBack]
GO
/****** Object:  StoredProcedure [dbo].[ContactInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactInsert]
GO
/****** Object:  StoredProcedure [dbo].[ContactLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactLoad]
GO
/****** Object:  StoredProcedure [dbo].[ContactUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactUpdate]
GO
/****** Object:  StoredProcedure [dbo].[ContactUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ContactUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptGet]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptGoBack]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptInsert]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptLoad]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptUpdate]
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorpDeptUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[CorporationGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationGet]
GO
/****** Object:  StoredProcedure [dbo].[CorporationGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationGoBack]
GO
/****** Object:  StoredProcedure [dbo].[CorporationInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationInsert]
GO
/****** Object:  StoredProcedure [dbo].[CorporationLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationLoad]
GO
/****** Object:  StoredProcedure [dbo].[CorporationUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationUpdate]
GO
/****** Object:  StoredProcedure [dbo].[CorporationUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CorporationUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentGet]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentGoBack]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentInsert]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentLoad]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentUpdate]
GO
/****** Object:  StoredProcedure [dbo].[DepartmentUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DepartmentUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpGet]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpGoBack]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpInsert]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpLoad]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpUpdate]
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeptEmpUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupGet]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupGoBack]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupInsert]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupLoad]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupUpdate]
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpAuthGroupUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactGet]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactGoBack]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactInsert]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactLoad]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactUpdate]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeContactUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeGet]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeGoBack]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeInsert]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeLoad]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeUpdate]
GO
/****** Object:  StoredProcedure [dbo].[EmployeeUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmployeeUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleGet]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleGoBack]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleInsert]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleLoad]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleUpdate]
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EmpRoleUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[MenuGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuGet]
GO
/****** Object:  StoredProcedure [dbo].[MenuGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuGoBack]
GO
/****** Object:  StoredProcedure [dbo].[MenuInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuInsert]
GO
/****** Object:  StoredProcedure [dbo].[MenuLoad]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuLoad]
GO
/****** Object:  StoredProcedure [dbo].[MenuUpdate]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuUpdate]
GO
/****** Object:  StoredProcedure [dbo].[MenuUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MenuUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[RoleGet]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleGet]
GO
/****** Object:  StoredProcedure [dbo].[RoleGoBack]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleGoBack]
GO
/****** Object:  StoredProcedure [dbo].[RoleInsert]    Script Date: 10/22/2014 11:03:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleInsert]
GO
/****** Object:  StoredProcedure [dbo].[RoleLoad]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleLoad]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuGet]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuGet]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuGoBack]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuGoBack]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuInsert]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuInsert]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuLoad]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuLoad]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateGet]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateGet]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateGoBack]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateGoBack]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateInsert]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateInsert]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateLoad]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateLoad]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateUpdate]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateUpdate]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuOperateUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuUpdate]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuUpdate]
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleMenuUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[RoleUpdate]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleUpdate]
GO
/****** Object:  StoredProcedure [dbo].[RoleUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RoleUpdateStatus]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:03:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectPager]
GO
/****** Object:  StoredProcedure [dbo].[SelectPager]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectPager]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE proc [dbo].[SelectPager]
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
/****** Object:  StoredProcedure [dbo].[RoleUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleUpdateStatus
// 存储过程功能描述：更新Role中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Role''

set @str = ''update [dbo].[Role] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RoleId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleUpdate]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleUpdate
// 存储过程功能描述：更新Role
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleUpdate]
    @RoleId int,
@RoleName varchar(80),
@RoleStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Role] SET
	[RoleName] = @RoleName,
	[RoleStatus] = @RoleStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RoleId] = @RoleId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuUpdateStatus
// 存储过程功能描述：更新RoleMenu中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.RoleMenu''

set @str = ''update [dbo].[RoleMenu] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RoleMenuID = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuUpdate]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuUpdate
// 存储过程功能描述：更新RoleMenu
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuUpdate]
    @RoleMenuID int,
@RoleId int,
@MenuId int,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[RoleMenu] SET
	[RoleId] = @RoleId,
	[MenuId] = @MenuId,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RoleMenuID] = @RoleMenuID

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateUpdateStatus]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateUpdateStatus
// 存储过程功能描述：更新RoleMenuOperate中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.RoleMenuOperate''

set @str = ''update [dbo].[RoleMenuOperate] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefID = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateUpdate]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateUpdate
// 存储过程功能描述：更新RoleMenuOperate
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateUpdate]
    @RefID int,
@RoleId int = NULL,
@MenuId int = NULL,
@RefStatus int = NULL,
@OperateId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[RoleMenuOperate] SET
	[RoleId] = @RoleId,
	[MenuId] = @MenuId,
	[RefStatus] = @RefStatus,
	[OperateId] = @OperateId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RefID] = @RefID

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateLoad]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateLoad
// 存储过程功能描述：查询所有RoleMenuOperate记录
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateLoad]
AS

SELECT
	[RefID],
	[RoleId],
	[MenuId],
	[RefStatus],
	[OperateId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[RoleMenuOperate]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateInsert]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateInsert
// 存储过程功能描述：新增一条RoleMenuOperate记录
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateInsert]
	@RoleId int =NULL ,
	@MenuId int =NULL ,
	@RefStatus int =NULL ,
	@OperateId int =NULL ,
	@CreatorId int =NULL ,
	@RefID int OUTPUT
AS

INSERT INTO [dbo].[RoleMenuOperate] (
	[RoleId],
	[MenuId],
	[RefStatus],
	[OperateId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@RoleId,
	@MenuId,
	@RefStatus,
	@OperateId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RefID = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateGoBack]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateGoBack
// 存储过程功能描述：撤返RoleMenuOperate，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.RoleMenuOperate''

set @str = ''update [dbo].[RoleMenuOperate] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefID = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuOperateGet]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuOperateGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuOperateGet
// 存储过程功能描述：查询指定RoleMenuOperate的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuOperateGet]
    /*
	@RefID int
    */
    @id int
AS

SELECT
	[RefID],
	[RoleId],
	[MenuId],
	[RefStatus],
	[OperateId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[RoleMenuOperate]
WHERE
	[RefID] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuLoad]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuLoad
// 存储过程功能描述：查询所有RoleMenu记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuLoad]
AS

SELECT
	[RoleMenuID],
	[RoleId],
	[MenuId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[RoleMenu]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuInsert]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuInsert
// 存储过程功能描述：新增一条RoleMenu记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuInsert]
	@RoleId int ,
	@MenuId int ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@RoleMenuID int OUTPUT
AS

INSERT INTO [dbo].[RoleMenu] (
	[RoleId],
	[MenuId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@RoleId,
	@MenuId,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RoleMenuID = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuGoBack]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuGoBack
// 存储过程功能描述：撤返RoleMenu，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.RoleMenu''

set @str = ''update [dbo].[RoleMenu] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RoleMenuID = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleMenuGet]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleMenuGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleMenuGet
// 存储过程功能描述：查询指定RoleMenu的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleMenuGet]
    /*
	@RoleMenuID int
    */
    @id int
AS

SELECT
	[RoleMenuID],
	[RoleId],
	[MenuId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[RoleMenu]
WHERE
	[RoleMenuID] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleLoad]    Script Date: 10/22/2014 11:03:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleLoad
// 存储过程功能描述：查询所有Role记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleLoad]
AS

SELECT
	[RoleId],
	[RoleName],
	[RoleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Role]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleInsert
// 存储过程功能描述：新增一条Role记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleInsert]
	@RoleName varchar(80) ,
	@RoleStatus int =NULL ,
	@CreatorId int ,
	@RoleId int OUTPUT
AS

INSERT INTO [dbo].[Role] (
	[RoleName],
	[RoleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@RoleName,
	@RoleStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RoleId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleGoBack
// 存储过程功能描述：撤返Role，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Role''

set @str = ''update [dbo].[Role] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RoleId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[RoleGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].RoleGet
// 存储过程功能描述：查询指定Role的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[RoleGet]
    /*
	@RoleId int
    */
    @id int
AS

SELECT
	[RoleId],
	[RoleName],
	[RoleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Role]
WHERE
	[RoleId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuUpdateStatus
// 存储过程功能描述：更新Menu中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Menu''

set @str = ''update [dbo].[Menu] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MenuId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuUpdate
// 存储过程功能描述：更新Menu
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuUpdate]
    @MenuId int,
@MenuName varchar(80) = NULL,
@MenuDesc varchar(400) = NULL,
@ParentId int = NULL,
@FirstId int = NULL,
@Url varchar(400) = NULL,
@MenuStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Menu] SET
	[MenuName] = @MenuName,
	[MenuDesc] = @MenuDesc,
	[ParentId] = @ParentId,
	[FirstId] = @FirstId,
	[Url] = @Url,
	[MenuStatus] = @MenuStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MenuId] = @MenuId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuLoad
// 存储过程功能描述：查询所有Menu记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuLoad]
AS

SELECT
	[MenuId],
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Menu]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuInsert
// 存储过程功能描述：新增一条Menu记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuInsert]
	@MenuName varchar(80) =NULL ,
	@MenuDesc varchar(400) =NULL ,
	@ParentId int =NULL ,
	@FirstId int =NULL ,
	@Url varchar(400) =NULL ,
	@MenuStatus int =NULL ,
	@CreatorId int =NULL ,
	@MenuId int OUTPUT
AS

INSERT INTO [dbo].[Menu] (
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MenuName,
	@MenuDesc,
	@ParentId,
	@FirstId,
	@Url,
	@MenuStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MenuId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuGoBack
// 存储过程功能描述：撤返Menu，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Menu''

set @str = ''update [dbo].[Menu] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where MenuId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[MenuGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuGet
// 存储过程功能描述：查询指定Menu的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[MenuGet]
    /*
	@MenuId int
    */
    @id int
AS

SELECT
	[MenuId],
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Menu]
WHERE
	[MenuId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleUpdateStatus
// 存储过程功能描述：更新EmpRole中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.EmpRole''

set @str = ''update [dbo].[EmpRole] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpRoleId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleUpdate
// 存储过程功能描述：更新EmpRole
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleUpdate]
    @EmpRoleId int,
@EmpId int,
@RoleId int,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[EmpRole] SET
	[EmpId] = @EmpId,
	[RoleId] = @RoleId,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[EmpRoleId] = @EmpRoleId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleLoad
// 存储过程功能描述：查询所有EmpRole记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleLoad]
AS

SELECT
	[EmpRoleId],
	[EmpId],
	[RoleId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[EmpRole]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleInsert
// 存储过程功能描述：新增一条EmpRole记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleInsert]
	@EmpId int ,
	@RoleId int ,
	@RefStatus int =NULL ,
	@CreatorId int ,
	@EmpRoleId int OUTPUT
AS

INSERT INTO [dbo].[EmpRole] (
	[EmpId],
	[RoleId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@EmpId,
	@RoleId,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @EmpRoleId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleGoBack
// 存储过程功能描述：撤返EmpRole，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.EmpRole''

set @str = ''update [dbo].[EmpRole] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpRoleId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpRoleGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpRoleGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpRoleGet
// 存储过程功能描述：查询指定EmpRole的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpRoleGet]
    /*
	@EmpRoleId int
    */
    @id int
AS

SELECT
	[EmpRoleId],
	[EmpId],
	[RoleId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[EmpRole]
WHERE
	[EmpRoleId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeUpdateStatus
// 存储过程功能描述：更新Employee中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Employee''

set @str = ''update [dbo].[Employee] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeUpdate
// 存储过程功能描述：更新Employee
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeUpdate]
    @EmpId int,
@DeptId int,
@EmpCode varchar(20) = NULL,
@Name varchar(20) = NULL,
@Sex bit = NULL,
@BirthDay datetime = NULL,
@Telephone varchar(20) = NULL,
@Phone varchar(20) = NULL,
@WorkStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Employee] SET
	[DeptId] = @DeptId,
	[EmpCode] = @EmpCode,
	[Name] = @Name,
	[Sex] = @Sex,
	[BirthDay] = @BirthDay,
	[Telephone] = @Telephone,
	[Phone] = @Phone,
	[WorkStatus] = @WorkStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[EmpId] = @EmpId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeLoad
// 存储过程功能描述：查询所有Employee记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeLoad]
AS

SELECT
	[EmpId],
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Employee]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeInsert
// 存储过程功能描述：新增一条Employee记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeInsert]
	@DeptId int ,
	@EmpCode varchar(20) =NULL ,
	@Name varchar(20) =NULL ,
	@Sex bit =NULL ,
	@BirthDay datetime =NULL ,
	@Telephone varchar(20) =NULL ,
	@Phone varchar(20) =NULL ,
	@WorkStatus int =NULL ,
	@CreatorId int =NULL ,
	@EmpId int OUTPUT
AS

INSERT INTO [dbo].[Employee] (
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DeptId,
	@EmpCode,
	@Name,
	@Sex,
	@BirthDay,
	@Telephone,
	@Phone,
	@WorkStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @EmpId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeGoBack
// 存储过程功能描述：撤返Employee，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Employee''

set @str = ''update [dbo].[Employee] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeGet
// 存储过程功能描述：查询指定Employee的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeGet]
    /*
	@EmpId int
    */
    @id int
AS

SELECT
	[EmpId],
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Employee]
WHERE
	[EmpId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactUpdateStatus
// 存储过程功能描述：更新EmployeeContact中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.EmployeeContact''

set @str = ''update [dbo].[EmployeeContact] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ECId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactUpdate
// 存储过程功能描述：更新EmployeeContact
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactUpdate]
    @ECId int,
@ContactId int,
@EmpId int,
@RefStatus int = NULL
AS

UPDATE [dbo].[EmployeeContact] SET
	[ContactId] = @ContactId,
	[EmpId] = @EmpId,
	[RefStatus] = @RefStatus
WHERE
	[ECId] = @ECId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactLoad
// 存储过程功能描述：查询所有EmployeeContact记录
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactLoad]
AS

SELECT
	[ECId],
	[ContactId],
	[EmpId],
	[RefStatus]
FROM
	[dbo].[EmployeeContact]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactInsert
// 存储过程功能描述：新增一条EmployeeContact记录
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactInsert]
	@ContactId int ,
	@EmpId int ,
	@RefStatus int =NULL ,
	@ECId int OUTPUT
AS

INSERT INTO [dbo].[EmployeeContact] (
	[ContactId],
	[EmpId],
	[RefStatus]
) VALUES (
	@ContactId,
	@EmpId,
	@RefStatus
)


SET @ECId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactGoBack
// 存储过程功能描述：撤返EmployeeContact，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.EmployeeContact''

set @str = ''update [dbo].[EmployeeContact] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where ECId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmployeeContactGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeContactGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactGet
// 存储过程功能描述：查询指定EmployeeContact的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmployeeContactGet]
    /*
	@ECId int
    */
    @id int
AS

SELECT
	[ECId],
	[ContactId],
	[EmpId],
	[RefStatus]
FROM
	[dbo].[EmployeeContact]
WHERE
	[ECId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupUpdateStatus
// 存储过程功能描述：更新EmpAuthGroup中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.EmpAuthGroup''

set @str = ''update [dbo].[EmpAuthGroup] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpAuthGroupId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupUpdate
// 存储过程功能描述：更新EmpAuthGroup
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupUpdate]
    @EmpAuthGroupId int,
@AuthGroupId int,
@EmpId int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[EmpAuthGroup] SET
	[AuthGroupId] = @AuthGroupId,
	[EmpId] = @EmpId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[EmpAuthGroupId] = @EmpAuthGroupId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupLoad
// 存储过程功能描述：查询所有EmpAuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupLoad]
AS

SELECT
	[EmpAuthGroupId],
	[AuthGroupId],
	[EmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[EmpAuthGroup]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupInsert
// 存储过程功能描述：新增一条EmpAuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupInsert]
	@AuthGroupId int ,
	@EmpId int ,
	@CreatorId int =NULL ,
	@EmpAuthGroupId int OUTPUT
AS

INSERT INTO [dbo].[EmpAuthGroup] (
	[AuthGroupId],
	[EmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuthGroupId,
	@EmpId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @EmpAuthGroupId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupGoBack
// 存储过程功能描述：撤返EmpAuthGroup，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.EmpAuthGroup''

set @str = ''update [dbo].[EmpAuthGroup] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where EmpAuthGroupId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[EmpAuthGroupGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpAuthGroupGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmpAuthGroupGet
// 存储过程功能描述：查询指定EmpAuthGroup的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[EmpAuthGroupGet]
    /*
	@EmpAuthGroupId int
    */
    @id int
AS

SELECT
	[EmpAuthGroupId],
	[AuthGroupId],
	[EmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[EmpAuthGroup]
WHERE
	[EmpAuthGroupId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpUpdateStatus
// 存储过程功能描述：更新DeptEmp中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.DeptEmp''

set @str = ''update [dbo].[DeptEmp] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DeptEmpId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpUpdate
// 存储过程功能描述：更新DeptEmp
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpUpdate]
    @DeptEmpId int,
@DeptId int,
@EmpId int,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[DeptEmp] SET
	[DeptId] = @DeptId,
	[EmpId] = @EmpId,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DeptEmpId] = @DeptEmpId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpLoad
// 存储过程功能描述：查询所有DeptEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpLoad]
AS

SELECT
	[DeptEmpId],
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeptEmp]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpInsert
// 存储过程功能描述：新增一条DeptEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpInsert]
	@DeptId int ,
	@EmpId int ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@DeptEmpId int OUTPUT
AS

INSERT INTO [dbo].[DeptEmp] (
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DeptId,
	@EmpId,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DeptEmpId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpGoBack
// 存储过程功能描述：撤返DeptEmp，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.DeptEmp''

set @str = ''update [dbo].[DeptEmp] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DeptEmpId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DeptEmpGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptEmpGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpGet
// 存储过程功能描述：查询指定DeptEmp的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DeptEmpGet]
    /*
	@DeptEmpId int
    */
    @id int
AS

SELECT
	[DeptEmpId],
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeptEmp]
WHERE
	[DeptEmpId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentUpdateStatus
// 存储过程功能描述：更新Department中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Department''

set @str = ''update [dbo].[Department] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DeptId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentUpdate
// 存储过程功能描述：更新Department
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentUpdate]
    @DeptId int,
@CorpId int,
@DeptCode varchar(80) = NULL,
@DeptName varchar(80),
@DeptFullName varchar(80) = NULL,
@DeptShort varchar(80) = NULL,
@DeptType int = NULL,
@ParentLeve int = NULL,
@DeptStatus int,
@DeptLevel int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Department] SET
	[CorpId] = @CorpId,
	[DeptCode] = @DeptCode,
	[DeptName] = @DeptName,
	[DeptFullName] = @DeptFullName,
	[DeptShort] = @DeptShort,
	[DeptType] = @DeptType,
	[ParentLeve] = @ParentLeve,
	[DeptStatus] = @DeptStatus,
	[DeptLevel] = @DeptLevel,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DeptId] = @DeptId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentLoad
// 存储过程功能描述：查询所有Department记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentLoad]
AS

SELECT
	[DeptId],
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Department]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentInsert
// 存储过程功能描述：新增一条Department记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentInsert]
	@CorpId int ,
	@DeptCode varchar(80) =NULL ,
	@DeptName varchar(80) ,
	@DeptFullName varchar(80) =NULL ,
	@DeptShort varchar(80) =NULL ,
	@DeptType int =NULL ,
	@ParentLeve int =NULL ,
	@DeptStatus int ,
	@DeptLevel int =NULL ,
	@CreatorId int ,
	@DeptId int OUTPUT
AS

INSERT INTO [dbo].[Department] (
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CorpId,
	@DeptCode,
	@DeptName,
	@DeptFullName,
	@DeptShort,
	@DeptType,
	@ParentLeve,
	@DeptStatus,
	@DeptLevel,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DeptId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentGoBack
// 存储过程功能描述：撤返Department，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Department''

set @str = ''update [dbo].[Department] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DeptId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DepartmentGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentGet
// 存储过程功能描述：查询指定Department的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[DepartmentGet]
    /*
	@DeptId int
    */
    @id int
AS

SELECT
	[DeptId],
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Department]
WHERE
	[DeptId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationUpdateStatus
// 存储过程功能描述：更新Corporation中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Corporation''

set @str = ''update [dbo].[Corporation] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where CorpId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationUpdate
// 存储过程功能描述：更新Corporation
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationUpdate]
    @CorpId int,
@ParentId int = NULL,
@CorpCode varchar(80) = NULL,
@CorpName varchar(40),
@CorpEName varchar(80) = NULL,
@TaxPayerId varchar(80) = NULL,
@CorpFullName varchar(80) = NULL,
@CorpFullEName varchar(200) = NULL,
@CorpAddress varchar(400) = NULL,
@CorpEAddress varchar(800) = NULL,
@CorpTel varchar(40) = NULL,
@CorpFax varchar(40) = NULL,
@CorpZip varchar(20) = NULL,
@CorpType int = NULL,
@IsSelf bit = NULL,
@CorpStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Corporation] SET
	[ParentId] = @ParentId,
	[CorpCode] = @CorpCode,
	[CorpName] = @CorpName,
	[CorpEName] = @CorpEName,
	[TaxPayerId] = @TaxPayerId,
	[CorpFullName] = @CorpFullName,
	[CorpFullEName] = @CorpFullEName,
	[CorpAddress] = @CorpAddress,
	[CorpEAddress] = @CorpEAddress,
	[CorpTel] = @CorpTel,
	[CorpFax] = @CorpFax,
	[CorpZip] = @CorpZip,
	[CorpType] = @CorpType,
	[IsSelf] = @IsSelf,
	[CorpStatus] = @CorpStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CorpId] = @CorpId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationLoad
// 存储过程功能描述：查询所有Corporation记录
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationLoad]
AS

SELECT
	[CorpId],
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Corporation]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationInsert
// 存储过程功能描述：新增一条Corporation记录
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationInsert]
	@ParentId int =NULL ,
	@CorpCode varchar(80) =NULL ,
	@CorpName varchar(40) ,
	@CorpEName varchar(80) =NULL ,
	@TaxPayerId varchar(80) =NULL ,
	@CorpFullName varchar(80) =NULL ,
	@CorpFullEName varchar(200) =NULL ,
	@CorpAddress varchar(400) =NULL ,
	@CorpEAddress varchar(800) =NULL ,
	@CorpTel varchar(40) =NULL ,
	@CorpFax varchar(40) =NULL ,
	@CorpZip varchar(20) =NULL ,
	@CorpType int =NULL ,
	@IsSelf bit =NULL ,
	@CorpStatus int ,
	@CreatorId int ,
	@CorpId int OUTPUT
AS

INSERT INTO [dbo].[Corporation] (
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ParentId,
	@CorpCode,
	@CorpName,
	@CorpEName,
	@TaxPayerId,
	@CorpFullName,
	@CorpFullEName,
	@CorpAddress,
	@CorpEAddress,
	@CorpTel,
	@CorpFax,
	@CorpZip,
	@CorpType,
	@IsSelf,
	@CorpStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CorpId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationGoBack
// 存储过程功能描述：撤返Corporation，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Corporation''

set @str = ''update [dbo].[Corporation] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where CorpId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorporationGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporationGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorporationGet
// 存储过程功能描述：查询指定Corporation的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorporationGet]
    /*
	@CorpId int
    */
    @id int
AS

SELECT
	[CorpId],
	[ParentId],
	[CorpCode],
	[CorpName],
	[CorpEName],
	[TaxPayerId],
	[CorpFullName],
	[CorpFullEName],
	[CorpAddress],
	[CorpEAddress],
	[CorpTel],
	[CorpFax],
	[CorpZip],
	[CorpType],
	[IsSelf],
	[CorpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Corporation]
WHERE
	[CorpId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptUpdateStatus
// 存储过程功能描述：更新CorpDept中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.CorpDept''

set @str = ''update [dbo].[CorpDept] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where CorpEmpId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptUpdate
// 存储过程功能描述：更新CorpDept
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptUpdate]
    @CorpEmpId int,
@DeptId int = NULL,
@CorpId int = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[CorpDept] SET
	[DeptId] = @DeptId,
	[CorpId] = @CorpId,
	[RefStatus] = @RefStatus
WHERE
	[CorpEmpId] = @CorpEmpId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptLoad
// 存储过程功能描述：查询所有CorpDept记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptLoad]
AS

SELECT
	[CorpEmpId],
	[DeptId],
	[CorpId],
	[RefStatus]
FROM
	[dbo].[CorpDept]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptInsert
// 存储过程功能描述：新增一条CorpDept记录
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptInsert]
	@DeptId int =NULL ,
	@CorpId int =NULL ,
	@RefStatus int =NULL ,
	@CorpEmpId int OUTPUT
AS

INSERT INTO [dbo].[CorpDept] (
	[DeptId],
	[CorpId],
	[RefStatus]
) VALUES (
	@DeptId,
	@CorpId,
	@RefStatus
)


SET @CorpEmpId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptGoBack
// 存储过程功能描述：撤返CorpDept，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.CorpDept''

set @str = ''update [dbo].[CorpDept] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   
           +'' where CorpEmpId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[CorpDeptGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpDeptGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptGet
// 存储过程功能描述：查询指定CorpDept的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[CorpDeptGet]
    /*
	@CorpEmpId int
    */
    @id int
AS

SELECT
	[CorpEmpId],
	[DeptId],
	[CorpId],
	[RefStatus]
FROM
	[dbo].[CorpDept]
WHERE
	[CorpEmpId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactUpdateStatus
// 存储过程功能描述：更新Contact中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Contact''

set @str = ''update [dbo].[Contact] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ContactId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactUpdate
// 存储过程功能描述：更新Contact
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactUpdate]
    @ContactId int,
@ContactName varchar(80),
@ContactCode varchar(80) = NULL,
@ContactTel varchar(80) = NULL,
@ContactFax varchar(80) = NULL,
@ContactAddress varchar(400) = NULL,
@CompanyId int = NULL,
@ContactStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Contact] SET
	[ContactName] = @ContactName,
	[ContactCode] = @ContactCode,
	[ContactTel] = @ContactTel,
	[ContactFax] = @ContactFax,
	[ContactAddress] = @ContactAddress,
	[CompanyId] = @CompanyId,
	[ContactStatus] = @ContactStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ContactId] = @ContactId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactLoad
// 存储过程功能描述：查询所有Contact记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactLoad]
AS

SELECT
	[ContactId],
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Contact]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactInsert
// 存储过程功能描述：新增一条Contact记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactInsert]
	@ContactName varchar(80) ,
	@ContactCode varchar(80) =NULL ,
	@ContactTel varchar(80) =NULL ,
	@ContactFax varchar(80) =NULL ,
	@ContactAddress varchar(400) =NULL ,
	@CompanyId int =NULL ,
	@ContactStatus int =NULL ,
	@CreatorId int ,
	@ContactId int OUTPUT
AS

INSERT INTO [dbo].[Contact] (
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContactName,
	@ContactCode,
	@ContactTel,
	@ContactFax,
	@ContactAddress,
	@CompanyId,
	@ContactStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ContactId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactGoBack
// 存储过程功能描述：撤返Contact，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Contact''

set @str = ''update [dbo].[Contact] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where ContactId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ContactGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].ContactGet
// 存储过程功能描述：查询指定Contact的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[ContactGet]
    /*
	@ContactId int
    */
    @id int
AS

SELECT
	[ContactId],
	[ContactName],
	[ContactCode],
	[ContactTel],
	[ContactFax],
	[ContactAddress],
	[CompanyId],
	[ContactStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Contact]
WHERE
	[ContactId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
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

CREATE PROCEDURE [dbo].[BlocUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Bloc''

set @str = ''update [dbo].[Bloc] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BlocId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocUpdate
// 存储过程功能描述：更新Bloc
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BlocUpdate]
    @BlocId int,
@BlocName varchar(200),
@BlocFullName varchar(400) = NULL,
@BlocEname varchar(400) = NULL,
@BlocStatus int = NULL,
@IsSelf bit = null,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Bloc] SET
	IsSelf = @IsSelf,
	[BlocName] = @BlocName,
	[BlocFullName] = @BlocFullName,
	[BlocEname] = @BlocEname,
	[BlocStatus] = @BlocStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BlocId] = @BlocId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocLoad
// 存储过程功能描述：查询所有Bloc记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BlocLoad]
AS

SELECT
	[BlocId],
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[IsSelf],
	[BlocStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bloc]


' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocInsert
// 存储过程功能描述：新增一条Bloc记录
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BlocInsert]
	@BlocName varchar(200) ,
	@BlocFullName varchar(400) =NULL ,
	@BlocEname varchar(400) =NULL ,
	@BlocStatus int =NULL ,
	@IsSelf bit = null,
	@CreatorId int ,
	@BlocId int OUTPUT
AS

INSERT INTO [dbo].[Bloc] (
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[BlocStatus],
	IsSelf,
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BlocName,
	@BlocFullName,
	@BlocEname,
	@BlocStatus,
	@IsSelf,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BlocId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocGoBack
// 存储过程功能描述：撤返Bloc，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BlocGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Bloc''

set @str = ''update [dbo].[Bloc] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where BlocId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[BlocGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlocGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocGet
// 存储过程功能描述：查询指定Bloc的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[BlocGet]
    /*
	@BlocId int
    */
    @id int
AS

SELECT
	[BlocId],
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[IsSelf],
	[BlocStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bloc]
WHERE
	[BlocId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountUpdateStatus
// 存储过程功能描述：更新Account中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.Account''

set @str = ''update [dbo].[Account] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AccId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountUpdate
// 存储过程功能描述：更新Account
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountUpdate]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountLoad
// 存储过程功能描述：查询所有Account记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountLoad]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountInsert
// 存储过程功能描述：新增一条Account记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountInsert]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountGoBack
// 存储过程功能描述：撤返Account，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.Account''

set @str = ''update [dbo].[Account] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AccId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AccountGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AccountGet
// 存储过程功能描述：查询指定Account的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AccountGet]
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupUpdateStatus
// 存储过程功能描述：更新AuthGroup中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthGroup''

set @str = ''update [dbo].[AuthGroup] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AuthGroupId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupUpdate
// 存储过程功能描述：更新AuthGroup
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupUpdate]
    @AuthGroupId int,
@AssetId int = NULL,
@TradeDirection int = NULL,
@TradeBorder int = NULL,
@ContractInOut int = NULL,
@ContractLimit int = NULL,
@CorpId int = NULL,
@AuthGroupStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthGroup] SET
	[AssetId] = @AssetId,
	[TradeDirection] = @TradeDirection,
	[TradeBorder] = @TradeBorder,
	[ContractInOut] = @ContractInOut,
	[ContractLimit] = @ContractLimit,
	[CorpId] = @CorpId,
	[AuthGroupStatus] = @AuthGroupStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AuthGroupId] = @AuthGroupId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupLoad
// 存储过程功能描述：查询所有AuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupLoad]
AS

SELECT
	[AuthGroupId],
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroup]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupInsert
// 存储过程功能描述：新增一条AuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupInsert]
	@AssetId int =NULL ,
	@TradeDirection int =NULL ,
	@TradeBorder int =NULL ,
	@ContractInOut int =NULL ,
	@ContractLimit int =NULL ,
	@CorpId int =NULL ,
	@AuthGroupStatus int =NULL ,
	@CreatorId int =NULL ,
	@AuthGroupId int OUTPUT
AS

INSERT INTO [dbo].[AuthGroup] (
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AssetId,
	@TradeDirection,
	@TradeBorder,
	@ContractInOut,
	@ContractLimit,
	@CorpId,
	@AuthGroupStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AuthGroupId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupGoBack
// 存储过程功能描述：撤返AuthGroup，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthGroup''

set @str = ''update [dbo].[AuthGroup] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AuthGroupId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupGet
// 存储过程功能描述：查询指定AuthGroup的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupGet]
    /*
	@AuthGroupId int
    */
    @id int
AS

SELECT
	[AuthGroupId],
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroup]
WHERE
	[AuthGroupId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailUpdateStatus
// 存储过程功能描述：更新AuthGroupDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthGroupDetail''

set @str = ''update [dbo].[AuthGroupDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailUpdate
// 存储过程功能描述：更新AuthGroupDetail
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailUpdate]
    @DetailId int,
@AuthGroupId int = NULL,
@EmpId int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthGroupDetail] SET
	[AuthGroupId] = @AuthGroupId,
	[EmpId] = @EmpId,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailLoad
// 存储过程功能描述：查询所有AuthGroupDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailLoad]
AS

SELECT
	[DetailId],
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroupDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailInsert
// 存储过程功能描述：新增一条AuthGroupDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailInsert]
	@AuthGroupId int =NULL ,
	@EmpId int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[AuthGroupDetail] (
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuthGroupId,
	@EmpId,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailGoBack
// 存储过程功能描述：撤返AuthGroupDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthGroupDetail''

set @str = ''update [dbo].[AuthGroupDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''''''+@dbName+'''''' and TableName = ''''''+@tableName+'''''' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthGroupDetailGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthGroupDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupDetailGet
// 存储过程功能描述：查询指定AuthGroupDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthGroupDetailGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[AuthGroupId],
	[EmpId],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroupDetail]
WHERE
	[DetailId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionUpdateStatus
// 存储过程功能描述：更新AuthOption中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.AuthOption''

set @str = ''update [dbo].[AuthOption] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AuthOptionId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionUpdate
// 存储过程功能描述：更新AuthOption
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionUpdate]
    @AuthOptionId int,
@OptionCode varchar(50) = NULL,
@OptionName varchar(50) = NULL,
@DataBaseName varchar(50) = NULL,
@TableName varchar(50) = NULL,
@RowId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthOption] SET
	[OptionCode] = @OptionCode,
	[OptionName] = @OptionName,
	[DataBaseName] = @DataBaseName,
	[TableName] = @TableName,
	[RowId] = @RowId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AuthOptionId] = @AuthOptionId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionLoad
// 存储过程功能描述：查询所有AuthOption记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionLoad]
AS

SELECT
	[AuthOptionId],
	[OptionCode],
	[OptionName],
	[DataBaseName],
	[TableName],
	[RowId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOption]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionInsert
// 存储过程功能描述：新增一条AuthOption记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionInsert]
	@OptionCode varchar(50) =NULL ,
	@OptionName varchar(50) =NULL ,
	@DataBaseName varchar(50) =NULL ,
	@TableName varchar(50) =NULL ,
	@RowId int =NULL ,
	@CreatorId int =NULL ,
	@AuthOptionId int OUTPUT
AS

INSERT INTO [dbo].[AuthOption] (
	[OptionCode],
	[OptionName],
	[DataBaseName],
	[TableName],
	[RowId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OptionCode,
	@OptionName,
	@DataBaseName,
	@TableName,
	@RowId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AuthOptionId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionGoBack
// 存储过程功能描述：撤返AuthOption，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthOption''

set @str = ''update [dbo].[AuthOption] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where AuthOptionId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionGet
// 存储过程功能描述：查询指定AuthOption的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionGet]
    /*
	@AuthOptionId int
    */
    @id int
AS

SELECT
	[AuthOptionId],
	[OptionCode],
	[OptionName],
	[DataBaseName],
	[TableName],
	[RowId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOption]
WHERE
	[AuthOptionId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailUpdateStatus
// 存储过程功能描述：更新AuthOptionDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.AuthOptionDetail''

set @str = ''update [dbo].[AuthOptionDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailUpdate
// 存储过程功能描述：更新AuthOptionDetail
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailUpdate]
    @DetailId int,
@AuthOptionId int = NULL,
@DetailCode varchar(50) = NULL,
@DetailName varchar(50) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthOptionDetail] SET
	[AuthOptionId] = @AuthOptionId,
	[DetailCode] = @DetailCode,
	[DetailName] = @DetailName,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailLoad
// 存储过程功能描述：查询所有AuthOptionDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailLoad]
AS

SELECT
	[DetailId],
	[AuthOptionId],
	[DetailCode],
	[DetailName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOptionDetail]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailInsert
// 存储过程功能描述：新增一条AuthOptionDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailInsert]
	@AuthOptionId int =NULL ,
	@DetailCode varchar(50) =NULL ,
	@DetailName varchar(50) =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[AuthOptionDetail] (
	[AuthOptionId],
	[DetailCode],
	[DetailName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuthOptionId,
	@DetailCode,
	@DetailName,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailGoBack
// 存储过程功能描述：撤返AuthOptionDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthOptionDetail''

set @str = ''update [dbo].[AuthOptionDetail] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where DetailId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailGet
// 存储过程功能描述：查询指定AuthOptionDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[AuthOptionId],
	[DetailCode],
	[DetailName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOptionDetail]
WHERE
	[DetailId] = @id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefUpdateStatus]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefUpdateStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefUpdateStatus
// 存储过程功能描述：更新AuthOptionDetailEmp_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from dbo.BDStatus where TableName = ''dbo.AuthOptionDetailEmp_Ref''

set @str = ''update [dbo].[AuthOptionDetailEmp_Ref] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefId = ''+ Convert(varchar,@id) 
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefUpdate]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefUpdate
// 存储过程功能描述：更新AuthOptionDetailEmp_Ref
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefUpdate]
    @RefId int,
@EmpId int = NULL,
@DetailId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthOptionDetailEmp_Ref] SET
	[EmpId] = @EmpId,
	[DetailId] = @DetailId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RefId] = @RefId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefLoad]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefLoad]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefLoad
// 存储过程功能描述：查询所有AuthOptionDetailEmp_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefLoad]
AS

SELECT
	[RefId],
	[EmpId],
	[DetailId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOptionDetailEmp_Ref]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefInsert]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefInsert
// 存储过程功能描述：新增一条AuthOptionDetailEmp_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年7月3日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefInsert]
	@EmpId int =NULL ,
	@DetailId int =NULL ,
	@CreatorId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[AuthOptionDetailEmp_Ref] (
	[EmpId],
	[DetailId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@EmpId,
	@DetailId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RefId = @@IDENTITY

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefGoBack]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefGoBack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefGoBack
// 存储过程功能描述：撤返AuthOptionDetailEmp_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年7月29日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = ''dbo.AuthOptionDetailEmp_Ref''

set @str = ''update [dbo].[AuthOptionDetailEmp_Ref] ''+
           '' set '' + @StatusNameCode + '' = '' + Convert(varchar,@status)   +'', LastModifyId = '' + Convert(varchar,@lastModifyId)  + '' , LastModifyTime = getdate()''
           +'' where RefId = ''+ Convert(varchar,@id) 
set @str = @str + '' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''''已作废'''' and StatusId = 1) where DataBaseName = ''+@dbName+'' and TableName = ''+@tableName+'' and RowId = '' + Convert(varchar,@id)
exec(@str)

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AuthOptionDetailEmp_RefGet]    Script Date: 10/22/2014 11:03:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthOptionDetailEmp_RefGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOptionDetailEmp_RefGet
// 存储过程功能描述：查询指定AuthOptionDetailEmp_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[AuthOptionDetailEmp_RefGet]
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[EmpId],
	[DetailId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOptionDetailEmp_Ref]
WHERE
	[RefId] = @id

' 
END
GO
