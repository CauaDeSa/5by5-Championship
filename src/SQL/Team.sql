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

CREATE PROCEDURE spCreateTeam 
    @name varchar(30),
    @nickname varchar(3),
    @creationDate Date
AS
BEGIN
    IF EXISTS (SELECT * FROM Team WHERE name = @name) RETURN 0;

    INSERT INTO Team (name, nickname, creationDate, active)
    VALUES (@name, @nickname, @creationDate, 1)
    RETURN 1;
END

GO

CREATE PROCEDURE spRetrieveTeam
    @name varchar(30)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Team WHERE name = @name) RETURN 0;

    SELECT * FROM Team WHERE name = @name
END

GO

CREATE PROCEDURE spRetrieveAllTeams
AS
BEGIN
    SELECT * FROM Team WHERE isActive = 1;
END

GO 

CREATE PROCEDURE spUpdateTeam
    @name varchar(30),
    @nickname varchar(3),
    @creationDate Date
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Team WHERE name = @name)
    EXEC spCreateTeam @name, @nickname, @creationDate;

    UPDATE Team
    SET nickname = @nickname, creationDate = @creationDate
    WHERE name = @name
END

GO

CREATE PROCEDURE spChangeSituation
    @name varchar(30)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Team WHERE name = @name) RETURN 0;

    UPDATE Team SET isActive = IIF(isActive = 1, 0, 1) WHERE name = @name
END

GO

INSERT INTO Team (name, nickname, creationDate, isActive)
VALUES
    ('Atiradores FC', 'ATR', '2004-01-20', 1),
    ('AWPocalipse FC', 'AWP', '2006-05-12', 1),
    ('DotXL FC', 'DXL', '1999-12-09', 1),
    ('Magnum FC', 'MGN', '2010-10-23', 1);