IF (Exists( Select * FROM [LinkedInLearning].[GamesInSteam] Where [Name] = N'The Stanley Parable'))
BEGIN
INSERT INTO [LinkedInLearning].[GamesInSteam] ([Id], [Name], [StoreId], [StoreUri], [SteamAppJson]) VALUES (N'bd7d5564-1096-ea11-969e-00155d00aa09', N'The Stanley Parable', N'221910', NULL, N'{
    "appid": 221910,
    "name": "The Stanley Parable",
    "logo": "https:\/\/steamcdn-a.akamaihd.net\/steamcommunity\/public\/images\/apps\/221910\/80de64fedc906c4d81123e7ddc1d61d39183ab2d.jpg",
    "friendlyURL": 221910,
    "availStatLinks": {
        "achievements": true,
        "global_achievements": true,
        "stats": false,
        "gcpd": false,
        "leaderboards": false,
        "global_leaderboards": false
    },
    "hours_forever": "5.4",
    "last_played": 1487287118
}')
END
GO
