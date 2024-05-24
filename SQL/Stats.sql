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

CREATE PROCEDURE spCreateStats 
    @teamName varchar(30),
    @championshipName varchar(30),
    @season varchar(7),
    @pontuation int,
    @scoredGoals int,
    @sufferedGoals int
AS
BEGIN
    IF EXISTS (SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season) RETURN 0;

    INSERT INTO Stats (teamName, championshipName, season, pontuation, scoredGoals, sufferedGoals)
    VALUES (@teamName, @championshipName, @season, @pontuation, @scoredGoals, @sufferedGoals)
    RETURN 1;
END

GO

CREATE PROCEDURE spRetrieveStats
    @teamName varchar(30),
    @championshipName varchar(30),
    @season varchar(7)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season) RETURN 0;

    SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season
END

GO

CREATE PROCEDURE spRetrieveAllStats
AS
BEGIN
    SELECT * FROM Stats
END

GO

CREATE PROCEDURE spUpdateTeamStats
    @teamName varchar(30),
    @championshipName varchar(30),
    @season varchar(7)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season) RETURN 0;

    DECLARE @scoredGoals int,
            @sufferedGoals int,
            @pontuation int;

    BEGIN
        SET @scoredGoals = (SELECT SUM(homeGoals) FROM Game WHERE home = @teamName AND championship = @championshipName AND season = @season)
        SET @scoredGoals += (SELECT SUM(visitorGoals) FROM Game WHERE visitor = @teamName AND championship = @championshipName AND season = @season);

        SET @sufferedGoals = (SELECT SUM(homeGoals) FROM Game WHERE visitor = @teamName AND championship = @championshipName AND season = @season)
        SET @sufferedGoals += (SELECT SUM(visitorGoals) FROM Game WHERE home = @teamName AND championship = @championshipName AND season = @season);

        SET @scoredGoals = IIF(@scoredGoals IS NULL, 0, @scoredGoals);
        SET @sufferedGoals = IIF(@sufferedGoals IS NULL, 0, @sufferedGoals);
        
        SET @pontuation = (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (home = @teamName) AND homeGoals > visitorGoals) * 3 +
                          (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (visitor = @teamName) AND visitorGoals > homeGoals) * 5 +
                          (SELECT COUNT(*) FROM Game WHERE championship = @championshipName AND season = @season AND (home = @teamName OR visitor = @teamName) AND homeGoals = visitorGoals) * 1;

        UPDATE Stats SET scoredGoals = @scoredGoals, sufferedGoals = @sufferedGoals, pontuation = @pontuation WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season;
    END
END

GO

CREATE PROCEDURE spDeleteStats
    @teamName varchar(30),
    @championshipName varchar(30),
    @season varchar(7)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Stats WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season) RETURN 0;

    DELETE FROM Stats
    WHERE teamName = @teamName AND championshipName = @championshipName AND season = @season
    RETURN 1;
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