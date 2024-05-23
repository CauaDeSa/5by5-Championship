use DBChampionship;

INSERT INTO Team (name, nickname, creationDate)
VALUES
    ('Atiradores FC', 'ATR', '2004-01-20'),
    ('AWPocalipse FC', 'AWP', '2006-05-12'),
    ('DotXL FC', 'DXL', '1999-12-09'),
    ('Magnum FC', 'MGN', '2010-10-23');

INSERT INTO Championship (name, season, startDate, endDate)
VALUES
    ('EsaChampionship', '2023/2', '2023-08-20', '2023-08-30'),
	('AmanChampionship', '2024/1', '2024-01-14', NULL);


INSERT INTO Stats (teamName, championshipName, season, pontuation, scoredGoals, sufferedGoals)
VALUES
    ('Atiradores FC', 'EsaChampionship', '2023/2', 4, 4, 5),
    ('AWPocalipse FC', 'EsaChampionship', '2023/2', 1, 6, 7),
    ('DotXL FC', 'EsaChampionship', '2023/2', 11, 9, 7),
    ('Magnum FC', 'AmanChampionship', '2024/1', 8, 9, 3),
    ('Atiradores FC', 'AmanChampionship', '2024/1', 0, 2, 5),
    ('AWPocalipse FC', 'AmanChampionship', '2024/1', 5, 4, 2),
    ('DotXL FC', 'AmanChampionship', '2024/1', 8, 8, 13);


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