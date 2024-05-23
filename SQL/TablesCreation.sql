use DBChampionship;

CREATE TABLE Team 
(
	name varchar(30),
	nickname varchar(3) UNIQUE not null,
	creationDate Date not null,
	CONSTRAINT pkTeam primary key (name)
);

CREATE TABLE Championship
(
	name varchar(30),
	season varchar(7),
	startDate Date not null,
	endDate Date null
	CONSTRAINT pkChampionship primary key (name, season)
);

CREATE TABLE Stats 
(
	teamName varchar(30) not null,
	championshipName varchar(30) not null,
	season varchar(7) not null,
	pontuation int null,
	scoredGoals int null,
	sufferedGoals int null,
	CONSTRAINT fkStats_Championship FOREIGN KEY (championshipName, season) REFERENCES Championship(name, season),
	CONSTRAINT fkStats_Team FOREIGN KEY (teamName) REFERENCES Team(name)
);

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