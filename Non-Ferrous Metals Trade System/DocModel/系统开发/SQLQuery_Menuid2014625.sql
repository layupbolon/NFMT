--------------------------------------Menu���в˵�ά��
--insert into Menu values('ϵͳ����','','','','','','');
--insert into Menu values('Ȩ�޼�������','','','','','','');
--insert into Menu values('�û�Ȩ��','','','','','','');
--insert into Menu values('���Ź���','','','','','','');
--insert into Menu values('��ҵ����','','','','','','');
--insert into Menu values('���Ź���','','','','','','');
--insert into Menu values('Ա������','','','','','','');
--insert into Menu values('��ɫ����','','','','','','');
--insert into Menu values('��ϵ�˹���','','','','','','');
--insert into Menu values('����ת��','','','','','','');
--insert into Menu values('��������','','','','','','');
--insert into Menu values('�����б�','','','','','','');
--insert into Menu values('��������','','','','','','');
--insert into Menu values('������λ����','','','','','','');
--insert into Menu values('Ʒ�ֹ���','','','','','','');
--insert into Menu values('���ֹ���','','','','','','');
--insert into Menu values('���ʹ���','','','','','','');
--insert into Menu values('�����˻�����','','','','','','');
--insert into Menu values('�������','','','','','','');
--insert into Menu values('�����ع���','','','','','','');
--insert into Menu values('�����̹���','','','','','','');
--insert into Menu values('Ʒ�ƹ���','','','','','','');
--insert into Menu values('����������','','','','','','');
--insert into Menu values('�ڻ���Լ����','','','','','','');
--insert into Menu values('�ڻ�����۹���','','','','','','');
--insert into Menu values('��Լ�������','','','','','','');
--insert into Menu values('��Լ','','','','','','');
--insert into Menu values('��Լ����','','','','','','');
--insert into Menu values('�Ӻ�Լ����','','','','','','');
--insert into Menu values('�Ӻ�Լ������','','','','','','');
--insert into Menu values('�Ƶ�','','','','','','');
--insert into Menu values('�Ƶ�����','','','','','','');
--insert into Menu values('�ִ�','','','','','','');
--insert into Menu values('���Ǽ�','','','','','','');
--insert into Menu values('������','','','','','','');
--insert into Menu values('��������','','','','','','');
--insert into Menu values('����','','','','','','');
--insert into Menu values('�ƿ�����','','','','','','');
--insert into Menu values('�ƿ�','','','','','','');
--insert into Menu values('��Ѻ����','','','','','','');
--insert into Menu values('��Ѻ','','','','','','');
--insert into Menu values('�ع�����','','','','','','');
--insert into Menu values('�ع�','','','','','','');
--insert into Menu values('���鿴','','','','','','');
--insert into Menu values('�ո���','','','','','','');
--insert into Menu values('��������','','','','','','');
--insert into Menu values('���񸶿�','','','','','','');
--insert into Menu values('�����ո���','','','','','','');
--insert into Menu values('�տ�Ǽ�','','','','','','');
--insert into Menu values('��˾�տ����','','','','','','');
--insert into Menu values('��Լ�տ����','','','','','','');
--insert into Menu values('����տ����','','','','','','');
--insert into Menu values('���','','','','','','');
--insert into Menu values('��۵�����','','','','','','');
--insert into Menu values('��۵�','','','','','','');
--insert into Menu values('��Ʊ','','','','','','');
--insert into Menu values('��Ʊ','','','','','','');
--insert into Menu values('ֱ����Ʊ','','','','','','');
--insert into Menu values('������Ʊ','','','','','','');
--insert into Menu values('������Ʊ','','','','','','');
--insert into Menu values('����Ʊ','','','','','','');
--insert into Menu values('����Ʊ','','','','','','');
--insert into Menu values('��Ʊ����','','','','','','');
--insert into Menu values('��Ʊ�ո������','','','','','','');
--insert into Menu values('��Ʊ������','','','','','','');
--insert into Menu values('Ԥ������','','','','','','');
--insert into Menu values('��ʷ���������','','','','','','');
--insert into Menu values('��ʷ��¼�鿴','','','','','','');
--insert into Menu values('��ʷ�����鿴','','','','','','');
--insert into Menu values('ͳ�Ʊ���','','','','','','');
--insert into Menu values('ʹ�ð���','','','','','','');
--insert into Menu values('�û��ֲ�','','','','','','');
--insert into Menu values('����������','','','','','','');
--insert into Menu values('�汾��Ϣ','','','','');


---------------------------------------���˵��ڵ㼶���Լ����ڵ������ϸ
select * from dbo.Menu

update dbo.Menu set ParentId=40,MenuDesc='ϵͳ����' where MenuName='Ȩ�޼�������'

update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='���Ź���'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='��ҵ����'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='���Ź���'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='Ա������'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='��ɫ����'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='��ϵ�˹���'
update dbo.Menu set ParentId=42,MenuDesc='�û�Ȩ��' where MenuName='����ת��'

update dbo.Menu set ParentId=50,MenuDesc='��������' where MenuName='�����б�'

update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='������λ����'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='Ʒ�ֹ���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='���ֹ���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='���ʹ���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='���й���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�����˻�����'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�������'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�����ع���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�����̹���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='Ʒ�ƹ���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='����������'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�ڻ���Լ����'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='�ڻ�����۹���'
update dbo.Menu set ParentId=52,MenuDesc='��������' where MenuName='��Լ�������'


update dbo.Menu set ParentId=66,MenuDesc='��Լ' where MenuName='��Լ����'
update dbo.Menu set ParentId=66,MenuDesc='��Լ' where MenuName='�Ӻ�Լ����'
update dbo.Menu set ParentId=66,MenuDesc='��Լ' where MenuName='�Ӻ�Լ������'

update dbo.Menu set ParentId=70,MenuDesc='�Ƶ�' where MenuName='�Ƶ�����'


update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='���Ǽ�'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='������'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='��������'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='����'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='�ƿ�����'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='�ƿ�'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='��Ѻ����'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='��Ѻ'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='�ع�����'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='�ع�'
update dbo.Menu set ParentId=72,MenuDesc='�ִ�' where MenuName='���鿴'

update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='��������'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='���񸶿�'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='�����ո���'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='�տ�Ǽ�'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='��˾�տ����'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='��Լ�տ����'
update dbo.Menu set ParentId=84,MenuDesc='�ո���' where MenuName='����տ����'

update dbo.Menu set ParentId=92,MenuDesc='���' where MenuName='��۵�����'
update dbo.Menu set ParentId=92,MenuDesc='���' where MenuName='��۵�'

update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='��Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='ֱ����Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='������Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='������Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='����Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='����Ʊ'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='��Ʊ����'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='��Ʊ�ո������'
update dbo.Menu set ParentId=95,MenuDesc='��Ʊ' where MenuName='��Ʊ������'

update dbo.Menu set ParentId=106,MenuDesc='��ʷ���������' where MenuName='��ʷ��¼�鿴'
update dbo.Menu set ParentId=106,MenuDesc='��ʷ���������' where MenuName='��ʷ�����鿴'

update dbo.Menu set ParentId=110,MenuDesc='ʹ�ð���' where MenuName='�û��ֲ�'
update dbo.Menu set ParentId=110,MenuDesc='ʹ�ð���' where MenuName='����������'
update dbo.Menu set ParentId=110,MenuDesc='ʹ�ð���' where MenuName='�汾��Ϣ'







