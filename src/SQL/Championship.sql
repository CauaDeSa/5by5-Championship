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

CREATE PROCEDURE spCreateNewChampionship
    @name varchar(30),
    @season varchar(7),
    @startDate Date
AS
BEGIN
    IF EXISTS (SELECT * FROM Championship WHERE name = @name AND season = @season) RETURN 0;

    INSERT INTO Championship (name, season, startDate) VALUES (@name, @season, @startDate)
    RETURN 1;
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

CREATE PROCEDURE spEndChampionship
    @name varchar(30),
    @season varchar(7),
    @endDate Date
AS
BEGIN
    Declare @Tcount int,
            @Gcount int;

    SET @Tcount = (SELECT COUNT(*) FROM Stats WHERE championshipName = @name AND season = @season);
    SET @Gcount = (SELECT COUNT(*) FROM Game WHERE championship = @name AND season = @season);

    IF @Tcount > @Gcount RETURN 0;

    UPDATE Championship
    SET endDate = @endDate
    WHERE name = @name AND season = @season
    RETURN 1;
END

GO

INSERT INTO Championship (name, season, startDate, endDate)
VALUES
    ('EsaChampionship', '2023/2', '2023-08-20', '2023-08-30'),
	('AmanChampionship', '2024/1', '2024-01-14', NULL);