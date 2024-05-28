use DBChampionship;

CREATE TABLE Team 
(
	name varchar(30),
	nickname varchar(3) UNIQUE not null,
	creationDate Date not null,
    isActive int not null,
	CONSTRAINT pkTeam primary key (name)
);

GO

CREATE PROCEDURE spInitializeTeam
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Team' AND xtype='U')
    BEGIN
        CREATE TABLE Team 
        (
            name varchar(30),
            nickname varchar(3) UNIQUE not null,
            creationDate Date not null,
            isActive int not null,
            CONSTRAINT pkTeam primary key (name)
        );
    END
END

GO

CREATE OR ALTER PROCEDURE spCreateTeam 
    @bool int OUTPUT,
    @name varchar(30),
    @nickname varchar(3),
    @creationDate Date
AS
BEGIN
    IF EXISTS (SELECT * FROM Team WHERE name = @name OR nickname = @nickname)
    BEGIN
        SET @bool = 0;
        RETURN;
    END

    INSERT INTO Team (name, nickname, creationDate, isActive)
    VALUES (@name, @nickname, @creationDate, 1)
    SET @bool = 1;
END

GO

CREATE OR ALTER PROCEDURE spNameIsUsed
    @bool int OUTPUT,
    @name varchar(30)
AS
BEGIN
    IF EXISTS (SELECT * FROM Team WHERE name = @name) SET @bool = 1;
    ELSE SET @bool = 0;
END

GO 

CREATE OR ALTER PROCEDURE spNicknameIsUsed
    @bool int OUTPUT,
    @nickname varchar(3)
AS
BEGIN
    IF EXISTS (SELECT * FROM Team WHERE nickname = @nickname) SET @bool = 1;
    ELSE SET @bool = 0;
END

GO

CREATE OR ALTER PROCEDURE spRetrieveTeam
    @name varchar(30)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Team WHERE name = @name) RETURN;
    IF EXISTS (SELECT * FROM Team WHERE name = @name AND isActive = 0) RETURN;

    SELECT * FROM Team WHERE name = @name
END

GO

CREATE PROCEDURE spRetrieveAllTeams
AS
BEGIN
    SELECT * FROM Team WHERE isActive = 1;
END

GO

INSERT INTO Team (name, nickname, creationDate, isActive)
VALUES
    ('Atiradores FC', 'ATR', '2004-01-20', 1),
    ('AWPocalipse FC', 'AWP', '2006-05-12', 1),
    ('DotXL FC', 'DXL', '1999-12-09', 1),
    ('Magnum FC', 'MGN', '2010-10-23', 1);