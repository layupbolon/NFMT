--------------------------------------Menu所有菜单维护
--insert into Menu values('系统配置','','','','','','');
--insert into Menu values('权限级别配置','','','','','','');
--insert into Menu values('用户权限','','','','','','');
--insert into Menu values('集团管理','','','','','','');
--insert into Menu values('企业管理','','','','','','');
--insert into Menu values('部门管理','','','','','','');
--insert into Menu values('员工管理','','','','','','');
--insert into Menu values('角色管理','','','','','','');
--insert into Menu values('联系人管理','','','','','','');
--insert into Menu values('数据转移','','','','','','');
--insert into Menu values('工作任务','','','','','','');
--insert into Menu values('任务列表','','','','','','');
--insert into Menu values('基础数据','','','','','','');
--insert into Menu values('计量单位管理','','','','','','');
--insert into Menu values('品种管理','','','','','','');
--insert into Menu values('币种管理','','','','','','');
--insert into Menu values('汇率管理','','','','','','');
--insert into Menu values('银行账户管理','','','','','','');
--insert into Menu values('区域管理','','','','','','');
--insert into Menu values('交货地管理','','','','','','');
--insert into Menu values('生产商管理','','','','','','');
--insert into Menu values('品牌管理','','','','','','');
--insert into Menu values('交易所管理','','','','','','');
--insert into Menu values('期货合约管理','','','','','','');
--insert into Menu values('期货结算价管理','','','','','','');
--insert into Menu values('合约条款管理','','','','','','');
--insert into Menu values('合约','','','','','','');
--insert into Menu values('合约管理','','','','','','');
--insert into Menu values('子合约管理','','','','','','');
--insert into Menu values('子合约库存分配','','','','','','');
--insert into Menu values('制单','','','','','','');
--insert into Menu values('制单管理','','','','','','');
--insert into Menu values('仓储','','','','','','');
--insert into Menu values('入库登记','','','','','','');
--insert into Menu values('入库分配','','','','','','');
--insert into Menu values('出库申请','','','','','','');
--insert into Menu values('出库','','','','','','');
--insert into Menu values('移库申请','','','','','','');
--insert into Menu values('移库','','','','','','');
--insert into Menu values('质押申请','','','','','','');
--insert into Menu values('质押','','','','','','');
--insert into Menu values('回购申请','','','','','','');
--insert into Menu values('回购','','','','','','');
--insert into Menu values('库存查看','','','','','','');
--insert into Menu values('收付款','','','','','','');
--insert into Menu values('付款申请','','','','','','');
--insert into Menu values('财务付款','','','','','','');
--insert into Menu values('虚拟收付款','','','','','','');
--insert into Menu values('收款登记','','','','','','');
--insert into Menu values('公司收款分配','','','','','','');
--insert into Menu values('合约收款分配','','','','','','');
--insert into Menu values('库存收款分配','','','','','','');
--insert into Menu values('点价','','','','','','');
--insert into Menu values('点价单申请','','','','','','');
--insert into Menu values('点价单','','','','','','');
--insert into Menu values('发票','','','','','','');
--insert into Menu values('临票','','','','','','');
--insert into Menu values('直接终票','','','','','','');
--insert into Menu values('替临终票','','','','','','');
--insert into Menu values('补零终票','','','','','','');
--insert into Menu values('价外票','','','','','','');
--insert into Menu values('财务发票','','','','','','');
--insert into Menu values('发票关联','','','','','','');
--insert into Menu values('发票收付款关联','','','','','','');
--insert into Menu values('发票库存关联','','','','','','');
--insert into Menu values('预警提醒','','','','','','');
--insert into Menu values('历史访问与操作','','','','','','');
--insert into Menu values('历史登录查看','','','','','','');
--insert into Menu values('历史操作查看','','','','','','');
--insert into Menu values('统计报表','','','','','','');
--insert into Menu values('使用帮助','','','','','','');
--insert into Menu values('用户手册','','','','','','');
--insert into Menu values('常见问题解答','','','','','','');
--insert into Menu values('版本信息','','','','');


---------------------------------------各菜单节点级别以及父节点关联明细
select * from dbo.Menu

update dbo.Menu set ParentId=40,MenuDesc='系统配置' where MenuName='权限级别配置'

update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='集团管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='企业管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='部门管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='员工管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='角色管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='联系人管理'
update dbo.Menu set ParentId=42,MenuDesc='用户权限' where MenuName='数据转移'

update dbo.Menu set ParentId=50,MenuDesc='工作任务' where MenuName='任务列表'

update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='计量单位管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='品种管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='币种管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='汇率管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='银行管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='银行账户管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='区域管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='交货地管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='生产商管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='品牌管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='交易所管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='期货合约管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='期货结算价管理'
update dbo.Menu set ParentId=52,MenuDesc='基础数据' where MenuName='合约条款管理'


update dbo.Menu set ParentId=66,MenuDesc='合约' where MenuName='合约管理'
update dbo.Menu set ParentId=66,MenuDesc='合约' where MenuName='子合约管理'
update dbo.Menu set ParentId=66,MenuDesc='合约' where MenuName='子合约库存分配'

update dbo.Menu set ParentId=70,MenuDesc='制单' where MenuName='制单管理'


update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='入库登记'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='入库分配'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='出库申请'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='出库'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='移库申请'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='移库'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='质押申请'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='质押'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='回购申请'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='回购'
update dbo.Menu set ParentId=72,MenuDesc='仓储' where MenuName='库存查看'

update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='付款申请'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='财务付款'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='虚拟收付款'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='收款登记'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='公司收款分配'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='合约收款分配'
update dbo.Menu set ParentId=84,MenuDesc='收付款' where MenuName='库存收款分配'

update dbo.Menu set ParentId=92,MenuDesc='点价' where MenuName='点价单申请'
update dbo.Menu set ParentId=92,MenuDesc='点价' where MenuName='点价单'

update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='临票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='直接终票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='替临终票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='补零终票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='价外票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='财务发票'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='发票关联'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='发票收付款关联'
update dbo.Menu set ParentId=95,MenuDesc='发票' where MenuName='发票库存关联'

update dbo.Menu set ParentId=106,MenuDesc='历史访问与操作' where MenuName='历史登录查看'
update dbo.Menu set ParentId=106,MenuDesc='历史访问与操作' where MenuName='历史操作查看'

update dbo.Menu set ParentId=110,MenuDesc='使用帮助' where MenuName='用户手册'
update dbo.Menu set ParentId=110,MenuDesc='使用帮助' where MenuName='常见问题解答'
update dbo.Menu set ParentId=110,MenuDesc='使用帮助' where MenuName='版本信息'







