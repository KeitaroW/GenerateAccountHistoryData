USE [game]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 22.10.2018 16:51:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[Accounts]

CREATE TABLE [dbo].[Accounts](
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


