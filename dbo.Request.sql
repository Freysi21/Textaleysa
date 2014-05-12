CREATE TABLE [dbo].[Request] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [userName]   NVARCHAR (MAX) NULL,
    [date]       DATETIME       NOT NULL,
    [mediaType]  NVARCHAR (MAX) NULL,
    [mediaTitle] NVARCHAR (MAX) NULL,
    [language]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Request] PRIMARY KEY CLUSTERED ([ID])
);

