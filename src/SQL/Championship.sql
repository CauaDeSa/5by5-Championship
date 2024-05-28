use DBChampionship;

CREATE TABLE Championship
(
	name varchar(30),
	season varchar(7),
	startDate Date not null,
	endDate Date null
	CONSTRAINT pkChampionship primary key (name, season)
);

GO

CREATE PROCEDURE spInitializeChampionship
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Championship' AND xtype='U')
    BEGIN
        CREATE TABLE Championship
        (
            name varchar(30),
            season varchar(7),
            startDate Date not null,
            endDate Date null
            CONSTRAINT pkChampionship primary key (name, season)
        );
    END
END

GO

CREATE PROCEDURE spHasChampionship
    @bool int OUTPUT,
    @name varchar(30),
    @season varchar(7)
AS
BEGIN
    IF EXISTS (SELECT * FROM Championship WHERE name = @name AND season = @season) SET @bool = 1;
    ELSE SET @bool = 0;
END

GO

CREATE OR ALTER PROCEDURE spCreateNewChampionship
    @bool int OUTPUT,
    @name varchar(30),
    @season varchar(7),
    @startDate Date
AS
BEGIN
    IF EXISTS (SELECT * FROM Championship WHERE name = @name AND season = @season) SET @bool = 0;

    ELSE INSERT INTO Championship (name, season, startDate) VALUES (@name, @season, @startDate)
    SET @bool = 1;
END

GO

CREATE PROCEDURE spRetrieveChampionship
    @name varchar(30),
    @season varchar(7)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Championship WHERE name = @name AND season = @season) RETURN 0;

    SELECT * FROM Championship WHERE name = @name AND season = @season
END

GO

CREATE PROCEDURE spRetrieveAllChampionships
AS
BEGIN
    SELECT * FROM Championship
END

GO

CREATE OR ALTER PROCEDURE spEndChampionship
    @bool int OUTPUT,
    @name varchar(30),
    @season varchar(7),
    @endDate Date
AS
BEGIN
    UPDATE Championship
    SET endDate = @endDate
    WHERE name = @name AND season = @season

    SET @bool = 1
    RETURN;
END

GO

INSERT INTO Championship (name, season, startDate, endDate)
VALUES
    ('EsaChampionship', '2023/2', '2023-08-20', '2023-08-30'),
	('AmanChampionship', '2024/1', '2024-01-14', NULL);