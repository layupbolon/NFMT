------------------------Ʒ��
select * from Asset
--truncate table Asset
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('ͭCU',0,0,0,GETDATE())
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('пZN',0,0,0,GETDATE())
insert into dbo.Asset(AssetName,MUId,AssetStatus,CreatorId,CreateTime)values('��AL',0,0,0,GETDATE())

------------------------����
select * from Currency
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('��ԪUSD',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('������CNY',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('�۱�HKD',0,0,GETDATE())
insert into Currency(CurrencyName,CurrencyStatus,CreatorId,CreateTime)values('ŷԪEUR',0,0,GETDATE())

-------------------------����
select * from Bank
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('�й�����','BOC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('�й���������','ICBC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('�й�ũҵ����','ABC',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('�й���������','CCB',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('��ͨ����','BCM',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('��������','Huaxia Bank',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('��׼����','SCB',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('��չ����','DBS',0,0,GETDATE())
insert into Bank(BankName,BankEname,BankStatus,CreatorId,CreateTime)values('��������','CMB',0,0,GETDATE())

------------------------������
select * from Exchange
insert into Exchange(ExchangeName,ExchangeCode,ExchangeStatus,CreatorId,CreateTime)values('�׶ؽ���������','LME',0,0,GETDATE())
insert into Exchange(ExchangeName,ExchangeCode,ExchangeStatus,CreatorId,CreateTime)values('�Ϻ��ڻ�������','SHFE',0,0,GETDATE())

