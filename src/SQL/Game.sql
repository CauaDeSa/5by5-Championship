use DBChampionship;

CREATE TABLE Game 
(
	championship varchar(30),
	season varchar(7),
	visitor varchar(3),
	home varchar(3),
	homeGoals int null,
	visitorGoals int null,
	CONSTRAINT fkGame_Championship FOREIGN KEY (championship, season) REFERENCES Championship(name, season),
	CONSTRAINT fkGame_Visitor FOREIGN KEY (visitor) REFERENCES Team(nickname),
	CONSTRAINT fkGame_Home FOREIGN KEY (home) REFERENCES Team(nickname)
);

GO

CREATE PROCEDURE spInitializeGame
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Game' AND xtype='U')
    BEGIN
        CREATE TABLE Game 
        (
            championship varchar(30),
            season varchar(7),
            visitor varchar(3),
            home varchar(3),
            homeGoals int null,
            visitorGoals int null,
            CONSTRAINT fkGame_Championship FOREIGN KEY (championship, season) REFERENCES Championship(name, season),
            CONSTRAINT fkGame_Visitor FOREIGN KEY (visitor) REFERENCES Team(nickname),
            CONSTRAINT fkGame_Home FOREIGN KEY (home) REFERENCES Team(nickname)
        );
    END
END

GO

CREATE OR ALTER PROCEDURE spCreateGame 
    @championship varchar(30),
    @season varchar(7),
    @visitor varchar(3),
    @home varchar(3),
    @homeGoals int,
    @visitorGoals int,
    @bool int OUTPUT
AS
BEGIN
    IF EXISTS (SELECT * FROM Game WHERE championship = @championship AND season = @season AND visitor = @visitor AND home = @home)
    BEGIN
        SET @bool = 0;
        RETURN;
    END

    INSERT INTO Game (championship, season, visitor, home, homeGoals, visitorGoals)
    VALUES (@championship, @season, @visitor, @home, @homeGoals, @visitorGoals)
    SET @bool = 1;
    RETURN;
END

GO 

CREATE PROCEDURE spRetrieveGame
    @championship varchar(30),
    @season varchar(7),
    @visitor varchar(3),
    @home varchar(3)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Game WHERE championship = @championship AND season = @season AND visitor = @visitor AND home = @home) RETURN 0;

    SELECT * FROM Game WHERE championship = @championship AND season = @season AND visitor = @visitor AND home = @home
END

GO

INSERT INTO Game (championship, season, visitor, home, homeGoals, visitorGoals)
VALUES
    ('EsaChampionship', '2023/2', 'ATR', 'AWP', 0, 1),
    ('EsaChampionship', '2023/2', 'ATR', 'DXL', 2, 0),
    ('EsaChampionship', '2023/2', 'AWP', 'ATR', 1, 1),
    ('EsaChampionship', '2023/2', 'AWP', 'DXL', 1, 2),
    ('EsaChampionship', '2023/2', 'DXL', 'ATR', 3, 1),
    ('EsaChampionship', '2023/2', 'DXL', 'AWP', 4, 3),
    ('AmanChampionship', '2024/1', 'MGN', 'ATR', NULL, NULL),
    ('AmanChampionship', '2024/1', 'MGN', 'DXL', 7, 1),
    ('AmanChampionship', '2024/1', 'MGN', 'AWP', NULL, NULL),
    ('AmanChampionship', '2024/1', 'ATR', 'DXL', 2, 5),
    ('AmanChampionship', '2024/1', 'ATR', 'AWP', NULL, NULL),
    ('AmanChampionship', '2024/1', 'ATR', 'MGN', NULL, NULL),
    ('AmanChampionship', '2024/1', 'DXL', 'ATR', NULL, NULL),
    ('AmanChampionship', '2024/1', 'DXL', 'AWP', 0, 4),
    ('AmanChampionship', '2024/1', 'DXL', 'MGN', 2, 0),
    ('AmanChampionship', '2024/1', 'AWP', 'MGN', 0, 2),
    ('AmanChampionship', '2024/1', 'AWP', 'ATR', NULL, NULL),
    ('AmanChampionship', '2024/1', 'AWP', 'DXL', NULL, NULL);