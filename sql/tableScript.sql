

CREATE DATABASE NYS;
GO

USE NYS;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Order_Name] [varchar](max) NOT NULL,
	[Order_Date] [datetime] NOT NULL,
	[Due_Date] [datetime] NOT NULL,
	[Design] [varchar](50) NOT NULL,
	[Cutting] [varchar](50) NOT NULL,
	[Printing] [varchar](50) NOT NULL,
	[Sewing] [varchar](50) NOT NULL,
	[QC] [varchar](50) NOT NULL,
	[Packaging] [varchar](50) NOT NULL,
	[Created_At] [datetime] NOT NULL,
	[Updated_At] [datetime] NULL,
	[IsDelete] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [PK_NewTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DEFAULT_Order_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
