/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT TOP (1000) [Id]
--      ,[Email]
--      ,[Name]
--  FROM [AlwaysEncrypted].[dbo].[Users]


-- creates master key 
-- Key must be AES-256
CREATE COLUMN MASTER KEY LocalColumnMasterKey  
WITH (  
    KEY_STORE_PROVIDER_NAME = N'APP_SETTINGS_KEY_VAULT',  
    KEY_PATH = N'https://myvault.vault.azure.net:443/keys/MyCMK/4c05f1a41b12488f9cba2ea964b6a700'
);

select * from sys.column_master_keys

-- AES-256
-- original key S1s5q04VxYRaOAeoL7FDX2F4Vci5rHOMsHG2Q+sQbL+JUD2gOpxRTys+gIBvZcyh

CREATE COLUMN ENCRYPTION KEY LocalColumnEncKey   
WITH VALUES  
  (  
    COLUMN_MASTER_KEY = LocalColumnMasterKey,   
    ALGORITHM = 'RSA_OAEP',   
    ENCRYPTED_VALUE = 0x00000000000000000000000000000000000000000000000000001 
  )   
  
select * from sys.column_encryption_key_values
select * from sys.column_encryption_keys

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nchar](255) ENCRYPTED WITH (ENCRYPTION_TYPE = Randomized , ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256', COLUMN_ENCRYPTION_KEY = LocalColumnEncKey) NULL,
	[Name] [nchar](255) NULL
) ON [PRIMARY]
GO

