/****** Script for SelectTopNRows command from SSMS  ******/

SELECT TOP (1000) [Id]
      ,[Email]
      ,[Name]
  FROM [AlwaysEncrypted].[dbo].[Users]


CREATE DATABASE AlwaysEncrypted
use AlwaysEncrypted
CREATE COLUMN MASTER KEY LocalColumnMasterKey  
WITH (  
    KEY_STORE_PROVIDER_NAME = N'APP_SETTINGS_KEY_VAULT',  
    KEY_PATH = N'Some path -_-'
);

select * from sys.column_master_keys

--Master key: 1C92F1DAE3CC5E54DF576296C3CCFD8BD347DC52DE5153F108C7110EA71DD5F9
--Master key b64: HJLx2uPMXlTfV2KWw8z9i9NH3FLeUVPxCMcRDqcd1fk=
--Encryption key: A981CC4D0A55129009276AA37C774DC222F7A55F7D8C6C740504393F787A5DFA
--Encryption key b64: qYHMTQpVEpAJJ2qjfHdNwiL3pV99jGx0BQQ5P3h6Xfo=
--Enc Encryption key: B4FEC438EB4C742562FFF69BC502BD39F07737F45692A6A95B5DDAD0E6FC9C18
--Enc Encryption key b64: tP7EOOtMdCVi//abxQK9OfB3N/RWkqapW13a0Ob8nBg=

create COLUMN ENCRYPTION KEY LocalColumnEncKey   
WITH VALUES  
  (  
    COLUMN_MASTER_KEY = LocalColumnMasterKey,   
    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',   
    ENCRYPTED_VALUE = 0xB4FEC438EB4C742562FFF69BC502BD39F07737F45692A6A95B5DDAD0E6FC9C18
  )   
  
select * from sys.column_encryption_key_values
select * from sys.column_encryption_keys

DROP TABLE [dbo].[Users]

create TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nchar](4000) ENCRYPTED WITH (ENCRYPTION_TYPE = Randomized , ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = LocalColumnEncKey) NULL,
	[Name] [nchar](4000) NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Users] ([Email], [Name])
values ('test', 'test')
 

drop TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nchar](4000) NULL,
	[Name] [nchar](4000) NULL
) ON [PRIMARY]
GO

