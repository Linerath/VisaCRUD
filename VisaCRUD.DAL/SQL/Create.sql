USE [VisaCRUD]
GO

DROP TABLE IF EXISTS [dbo].[VisasDocuments]
GO

DROP TABLE IF EXISTS [dbo].[Documents]
GO

DROP TABLE IF EXISTS [dbo].[Visas]
GO

DROP TABLE IF EXISTS [dbo].[Countries]
GO

CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO

CREATE TABLE [dbo].[Visas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country_Id] [int] NOT NULL,
	[ServiceType] [nvarchar](max),
	[Terms] [nvarchar](max),
	[Validity] [nvarchar](max),
	[Period] [nvarchar](max),
	[Number] [nvarchar](max),
	[WebSite] [nvarchar](max),
CONSTRAINT [PK_dbo.Visas] PRIMARY KEY CLUSTERED ([Id] ASC),
CONSTRAINT [FK_dbo.Visas_dbo.Countries_Country_Id]
	FOREIGN KEY([Country_Id])
	REFERENCES [dbo].[Countries]([Id])
	ON DELETE CASCADE,
)
GO

CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
CONSTRAINT [PK_dbo.Documents] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO

CREATE TABLE [dbo].[VisasDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Visa_Id] [int] NOT NULL,
	[Document_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.VisasDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.VisasDocuments_dbo.Visas_Visa_Id]
	FOREIGN KEY([Visa_Id])
	REFERENCES [dbo].[Visas]([Id])
	ON DELETE CASCADE,
 CONSTRAINT [FK_dbo.VisasDocuments_dbo.Documents_Document_Id]
	FOREIGN KEY([Document_Id])
	REFERENCES [dbo].[Documents]([Id])
	ON DELETE CASCADE
 )
GO
