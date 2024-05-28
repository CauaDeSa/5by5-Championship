use DBChampionship;

CREATE TABLE Stats 
(
	teamName varchar(30) not null,
	championshipName varchar(30) not null,
	season varchar(7) not null,
	pontuation int not null,
	scoredGoals int not null,
	sufferedGoals int not null,
	CONSTRAINT fkStats_Championship FOREIGN KEY (championshipName, season) REFERENCES Championship(name, season),
	CONSTRAINT fkStats_Team FOREIGN KEY (teamName) REFERENCES Team(name)
);

GO

CREATE PROCEDURE spInitializeStats
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Stats' AND xtype='U')
    BEGIN
        CREATE TABLE Stats 
        (
            teamName varchar(30) not null,
            championshipName varchar(30) not null,
            season varchar(7) not null,
            pontuation int not null,
            scoredGoals int not null,
            sufferedGoals int not null,
            CONSTRAINT fkStats_Championship FOREIGN KEY (championshipName, season) REFERENCES Championship(name, season),
            CONSTRAINT fkStats_Team FOREIGN KEY (teamName) REFERENCES Team(name)
        );
    END
END

GO

CREATE OR ALTER PROCEDURE spCreateStats 
    @teamName varchar(30),
    @championshipName varchar(30),
    @season varchar(7),
    @pontuation int,
    @scoredGoals int,
    @sufferedGoals int,
    @bool int OUTPUT
AS
BEGIN
    IF EXISTS (SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season)
    BEGIN
        SET @bool = 0
    END

    INSERT INTO Stats (teamName, championshipName, season, pontuation, scoredGoals, sufferedGoals)
    VALUES (@teamName, @championshipName, @season, @pontuation, @scoredGoals, @sufferedGoals)
    SET @bool = 1;
END

GO

CREATE PROCEDURE spRetrieveByChampionshipAndSeason
	@championship varchar(30),
    @season varchar(7)
AS
BEGIN
    SELECT * FROM Stats WHERE championshipName = @championship AND season = @season
END

GO

CREATE OR ALTER PROCEDURE spUpdateTeamStats
    @bool int OUTPUT,
    @name varchar(30),
    @nickname varchar(3),
    @championshipName varchar(30),
    @season varchar(7)
AS
BEGIN
    DECLARE @scoredGoals int,
            @sufferedGoals int,
            @pontuation int;

    BEGIN
        SET @scoredGoals = (SELECT SUM(homeGoals) FROM Game WHERE home = @nickname AND championship = @championshipName AND season = @season)
        SET @scoredGoals += (SELECT SUM(visitorGoals) FROM Game WHERE visitor = @nickname AND championship = @championshipName AND season = @season);

        SET @sufferedGoals = (SELECT SUM(homeGoals) FROM Game WHERE visitor = @nickname AND championship = @championshipName AND season = @season)
        SET @sufferedGoals += (SELECT SUM(visitorGoals) FROM Game WHERE home = @nickname AND championship = @championshipName AND season = @season);

        SET @scoredGoals = IIF(@scoredGoals IS NULL, 0, @scoredGoals);
        SET @sufferedGoals = IIF(@sufferedGoals IS NULL, 0, @sufferedGoals);
        
        SET @pontuation = (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (home = @nickname) AND homeGoals > visitorGoals) * 3 +
                          (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (visitor = @nickname) AND visitorGoals > homeGoals) * 5 +
                          (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (home = @nickname OR visitor = @nickname) AND homeGoals = visitorGoals) * 1;

        UPDATE Stats SET scoredGoals = @scoredGoals WHERE teamName = @name AND championshipName = @championshipName AND season = @season;
        UPDATE Stats SET sufferedGoals = @sufferedGoals WHERE teamName = @name AND championshipName = @championshipName AND season = @season;
        UPDATE Stats SET pontuation = @pontuation WHERE teamName = @name AND championshipName = @championshipName AND season = @season;
        SET @bool = 1;
    END
END

GO

INSERT INTO Stats (teamName, championshipName, season, pontuation, scoredGoals, sufferedGoals)
VALUES
    ('Atiradores FC', 'EsaChampionship', '2023/2', 4, 4, 5),
    ('AWPocalipse FC', 'EsaChampionship', '2023/2', 1, 6, 7),
    ('DotXL FC', 'EsaChampionship', '2023/2', 11, 9, 7),
    ('Magnum FC', 'AmanChampionship', '2024/1', 8, 9, 3),
    ('Atiradores FC', 'AmanChampionship', '2024/1', 0, 2, 5),
    ('AWPocalipse FC', 'AmanChampionship', '2024/1', 5, 4, 2),
    ('DotXL FC', 'AmanChampionship', '2024/1', 8, 8, 13);