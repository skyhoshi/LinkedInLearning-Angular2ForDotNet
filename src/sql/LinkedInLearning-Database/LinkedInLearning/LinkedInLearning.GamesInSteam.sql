CREATE TABLE [LinkedInLearning].[GamesInSteam]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT  newsequentialid() , 
    [Name] VARCHAR(MAX) NOT NULL, 
    [StoreId] NVARCHAR(100) NULL, 
    [StoreUri] NVARCHAR(MAX) NULL, 
    [SteamAppJson] NVARCHAR(MAX) NOT NULL DEFAULT '{}', 
    CONSTRAINT [PK_GamesInSteam] PRIMARY KEY ([Id]), 
    CONSTRAINT [AppJson should be formmated as JSON] Check (ISJSON(SteamAppJson)=1)
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Key of Game In Steam Table',
    @level0type = N'SCHEMA',
    @level0name = N'LinkedInLearning',
    @level1type = N'TABLE',
    @level1name = N'GamesInSteam',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of Game',
    @level0type = N'SCHEMA',
    @level0name = N'LinkedInLearning',
    @level1type = N'TABLE',
    @level1name = N'GamesInSteam',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Steam Store ID : AKA AppId',
    @level0type = N'SCHEMA',
    @level0name = N'LinkedInLearning',
    @level1type = N'TABLE',
    @level1name = N'GamesInSteam',
    @level2type = N'COLUMN',
    @level2name = N'StoreId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'URL to Steam Store Page',
    @level0type = N'SCHEMA',
    @level0name = N'LinkedInLearning',
    @level1type = N'TABLE',
    @level1name = N'GamesInSteam',
    @level2type = N'COLUMN',
    @level2name = N'StoreUri'