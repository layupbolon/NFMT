select * from dbo.Menu
--truncate table dbo.Menu
--------------------------------根菜单
insert into Menu(MenuName,MenuDesc,ParentId,FirstId)values('系统配置','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('用户权限','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('工作任务','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('基础数据','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('合约','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('制单','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('仓储','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('收付款','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('点价','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('发票','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('预警提醒','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('历史访问操作','',0,1);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('使用帮助','',0,1);
---------------------------子菜单
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('权限级别配置','系统配置',1,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('集团管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('企业管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('部门管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('员工管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('角色管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('联系人管理','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('数据转移','用户权限',2,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('任务列表','工作任务',3,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('计量单位管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('品种管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('币种管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('汇率管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('银行管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('银行账户管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('区域管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('交货地管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('生产商管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('品牌管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('交易所管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('期货合约管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('期货结算价管理','基础数据',4,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('合约条款管理','基础数据',4,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('合约管理','合约',5,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('子合约管理','合约',5,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('子合约库存分配','合约',5,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('制单管理','制单',6,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('入库登记','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('入库分配','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('出库申请','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('出库','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('移库申请','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('移库','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('质押申请','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('质押','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('回购申请','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('回购','仓储',7,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('库存查看','仓储',7,2);


insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('付款申请','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('财务付款','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('虚拟收付款','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('收款登记','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('公司收款分配','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('合约收款分配','收付款',8,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('库存收款分配','收付款',8,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('点价单申请','点价',9,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('点价单','点价',9,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('临票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('直接终票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('替临终票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('补零终票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('价外票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('财务发票','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('发票关联','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('发票收付款关联','发票',10,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('发票库存关联','发票',10,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('历史登录查看','历史访问与操作',12,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('历史操作查看','历史访问与操作',12,2);

insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('用户手册','使用帮助',13,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('常见问题解答','使用帮助',13,2);
insert into Menu(MenuName,MenuDesc,ParentId,FirstId) values('版本信息','使用帮助',13,2);


----------------------------------------------角色

select * from dbo.Role

insert into dbo.Role(RoleName,CreatorId,CreateTime)values('超级管理员',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('总裁',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('副总裁',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('总监',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('总经理',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('经理',0,GETDATE())
insert into dbo.Role(RoleName,CreatorId,CreateTime)values('员工',0,GETDATE())


-------------------------------------------公司

select * from Corporation
insert into Corporation(CorpName,CorpStatus,CreatorId,CreateTime)values('西安迈科金属有限公司',4,1,GETDATE());
insert into Corporation(CorpName,CorpStatus,CreatorId,CreateTime)values('香港裕明贸易有限公司',4,1,GETDATE());
insert into Corporation(CorpName,CorpStatus,CreatorId,CreateTime)values('香港迈迪信投资有限公司',4,1,GETDATE());
insert into Corporation(CorpName,CorpStatus,CreatorId,CreateTime)values('深圳迈科资源有限公司',4,1,GETDATE());

---------------------------------------------部门
select * from Department
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'采购部',4,1,GETDATE())
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'外贸铜营销部',4,1,GETDATE())
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'内贸铜营销部',4,1,GETDATE())
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'价格中心',4,1,GETDATE())
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'上海分公司财务部',4,1,GETDATE())
insert into dbo.Department(CorpId,DeptName,DeptStatus,CreatorId,CreateTime)values(2,'仓储部',4,1,GETDATE())

--------------------------------------------

