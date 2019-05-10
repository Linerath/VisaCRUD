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

DROP TABLE IF EXISTS [dbo].[ServiceTypes]
GO

DROP TABLE IF EXISTS [dbo].[AppUsersRoles]
GO

DROP TABLE IF EXISTS [dbo].[AppUsers]
GO

DROP TABLE IF EXISTS [dbo].[AppRoles]
GO

CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO

CREATE TABLE [dbo].[ServiceTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.ServiceTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
)
GO

CREATE TABLE [dbo].[Visas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country_Id] [int] NOT NULL,
	[ServiceType_Id] [int],
	[Terms] [nvarchar](100),
	[Validity] [nvarchar](100),
	[Period] [nvarchar](100),
	[Price] [DECIMAL](6, 2),
	[WebSite] [nvarchar](50),
	[AdditionalDocs] [nvarchar](256),
 CONSTRAINT [PK_dbo.Visas] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.Visas_dbo.Countries_Country_Id]
	FOREIGN KEY([Country_Id])
	REFERENCES [dbo].[Countries]([Id])
	ON DELETE CASCADE,
 CONSTRAINT [FK_dbo.Visas_dbo.ServiceTypes_ServiceType_Id]
	FOREIGN KEY([ServiceType_Id])
	REFERENCES [dbo].[ServiceTypes]([Id])
	ON DELETE CASCADE,
)
GO

CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256),
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


CREATE TABLE [dbo].[AppUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_dbo.AppUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [Unique.AppUsers] UNIQUE([Login])
)
GO

CREATE TABLE [dbo].[AppRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NameRus] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.AppRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [Unique.AppRoles] UNIQUE([Name])
)
GO

CREATE TABLE [dbo].[AppUsersRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AppUsersRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.AppUsersRoles_dbo.AppUsers_User_Id]
	FOREIGN KEY([User_Id])
	REFERENCES [dbo].[AppUsers]([Id])
	ON DELETE CASCADE,
 CONSTRAINT [FK_dbo.AppUsersRoles_dbo.AppRoles_Role_Id]
	FOREIGN KEY([Role_Id])
	REFERENCES [dbo].[AppRoles]([Id])
	ON DELETE CASCADE
)
GO