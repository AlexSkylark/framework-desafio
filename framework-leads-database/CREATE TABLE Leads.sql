USE [framework-leads]
GO

/****** Object:  Table [dbo].[Leads]    Script Date: 22/09/2022 05:06:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Leads]') AND type in (N'U'))
DROP TABLE [dbo].[Leads]
GO

/****** Object:  Table [dbo].[Leads]    Script Date: 22/09/2022 05:06:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Leads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CONTACT_FIRSTNAME] [varchar](200) NOT NULL,
	[CONTACT_LASTNAME] [varchar](200) NOT NULL,
	[CONTACT_PHONE] [varchar](20) NOT NULL,
	[CONTACT_EMAIL] [varchar](100) NOT NULL,
	[DATE_CREATED] [datetime] NOT NULL,
	[SUBURB] [varchar](200) NOT NULL,
	[CATEGORY] [varchar](100) NOT NULL,
	[STATUS] [int] NOT NULL,
	[DESCRIPTION] [varchar](2000) NOT NULL,
	[PRICE] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Leads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
