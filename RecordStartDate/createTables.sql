if not exists
(
	select TABLE_NAME
	FROM game.INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'AccountHistory'
)


CREATE TABLE AccountHistory(
	[Id] [int] NOT NULL,
	[Loginname] [char](30) NOT NULL,
	[Password] [char](20) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[Charactername] [char](30) NOT NULL,
	[Nation] [char](3) NOT NULL,
	[Geartype] [char](1) NOT NULL,
	[Level] [int] NOT NULL,
	[Levelpercentage] [decimal](4, 2) NOT NULL,
	[Spi] [int] NOT NULL,
	[Credits] [int] NOT NULL,
	[Fame] [int] NOT NULL,
	[Brigade] [nchar](20) NULL,
	[Attack] [decimal](3, 0) NOT NULL,
	[Defence] [decimal](3, 0) NOT NULL,
	[Evasion] [decimal](3, 0) NOT NULL,
	[Fuel] [decimal](3, 0) NOT NULL,
	[Spirit] [decimal](3, 0) NOT NULL,
	[Shield] [decimal](3, 0) NOT NULL,
	[UnusedStatpoints] [decimal](3, 0) NULL,
	RecordStartDate Date,
	RecordEndDate Date,
	CONSTRAINT PK_ACCOUNT_HISTORY PRIMARY KEY (Id, RecordStartDate)
)

if not exists
(
	select TABLE_NAME
	FROM game.INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'AccountTmp'
)

CREATE TABLE AccountsTmp(
	[Id] [int] NOT NULL,
	[Loginname] [char](30) NOT NULL,
	[Password] [char](20) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[Charactername] [char](30) NOT NULL,
	[Nation] [char](3) NOT NULL,
	[Geartype] [char](1) NOT NULL,
	[Level] [int] NOT NULL,
	[Levelpercentage] [decimal](4, 2) NOT NULL,
	[Spi] [int] NOT NULL,
	[Credits] [int] NOT NULL,
	[Fame] [int] NOT NULL,
	[Brigade] [nchar](20) NULL,
	[Attack] [decimal](3, 0) NOT NULL,
	[Defence] [decimal](3, 0) NOT NULL,
	[Evasion] [decimal](3, 0) NOT NULL,
	[Fuel] [decimal](3, 0) NOT NULL,
	[Spirit] [decimal](3, 0) NOT NULL,
	[Shield] [decimal](3, 0) NOT NULL,
	[UnusedStatpoints] [decimal](3, 0) NULL,
	FileDate Date,
	CONSTRAINT PK_ACCOUNT_TMP PRIMARY KEY (Id)
)