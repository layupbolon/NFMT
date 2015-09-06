------------------------品种
select * from Asset
--truncate table Asset
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('铜CU',0,0,0,GETDATE())
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('锌ZN',0,0,0,GETDATE())
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('铝AL',0,0,0,GETDATE())

------------------------币种
select * from Currency
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('美元USD',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('人名币CNY',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('港币HKD',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('欧元EUR',0,0,GETDATE())

-------------------------银行
select * from Bank
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('中国银行','BOC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('中国工商银行','ICBC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('中国农业银行','ABC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('中国建设银行','CCB',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('交通银行','BCM',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('华夏银行','Huaxia Bank',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('标准银行','SCB',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('星展银行','DBS',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('招商银行','CMB',0,0,GETDATE())

------------------------交易所
select * from Exchange
insert into Exchange(ExchangeName,ExchangeCode,ExchangeStatus,CreatorId,CreateTime)values('伦敦金属交易所','LME',0,0,GETDATE())
insert into Exchange(ExchangeName,ExchangeCode,ExchangeStatus,CreatorId,CreateTime)values('上海期货交易所','SHFE',0,0,GETDATE())

