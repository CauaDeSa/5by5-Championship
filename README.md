# MODELLING EXERCISE / DATA MAPPING / IMPLEMENTATION

[Requirements Document in portuguese](https://github.com/CauaDeSa/5by5-Championship/files/15424928/campeonato_futebol.pdf).

<br>

In this project, students will have to individually prepare the database to control the registration of 
football teams from an amateur league.  

Each team must have the following information in its registration: Name, Surname and Date of 
Creation;  

The Championship should initially have 3 teams, and up to 5 teams can be registered. 
Therefore, the games that should take place will be between Visitor and Home and can be up to 20 ((N - 1) * N xbecause each team doesn't play against itself).

We will have the following scorecard: 

* 1 point for each team when DRAW
* 5 points for the winner that hit VISITORS WIN (AS VISITOR TEAM) 
* 3 points for the winner that hit HOME WIN (AS HOME TEAM)

What will my bank/app have to show me? 

* Who is the champion at the end of the championship? 
* How will we check the top 5 teams in the championship? 
* How will we calculate the score of the teams according to the games that have taken place? 
* What would it look like with a trigger or how would it look with a stored procedure? 
* Who is the team that scored the most goals in the league?  
* Who has conceded the most goals in the league? 
* Which game had the most goals? 
* What is the most goals each team has scored in a single game? 

<br>

## Entity Relation Model

<img src="https://github.com/CauaDeSa/5by5-Championship/assets/127906505/c0aa419e-6077-4efa-875d-c273a1b9a245" width="750px">

<br><br>

## Relational Model

 <b>Team</b> (name, nickname, creationDate)

 <br>
 
 <b>Championship</b> (name, season, startDate, endDate)

 <br>
 
 <b>Stats</b> (Tname, Cname, season, pontuation, scoredGoals, sufferedGoals)
   *  FK Tname referencia Team (name)
   *  FK Cname referencia Championship (name)
   *  FK season referencia Championship (season)

 <br>
   
 <b>Game</b> (championship, season, visitor, home, homeGoals, visitorGoals)
   *  FK championship referencia Championship (name)
   *  FK season referencia Championship (season)
   *  FK visitor referencia Team (name)
   *  FK home referencia Team (name)

  <br>

   ## Tables Model

   <img src="https://github.com/CauaDeSa/5by5-Championship/assets/127906505/0a36b573-6716-4032-ba1d-29ca99e72a1d" width="750px">
   
   <br><br>
    
   <img src="https://github.com/CauaDeSa/5by5-Championship/assets/127906505/ac47b24b-6243-4712-83ef-b70b6e5298aa" width="750px">

   <br><br>
   
   <img src="https://github.com/CauaDeSa/5by5-Championship/assets/127906505/e1d93295-0532-4ee5-a4be-02d80d635022" width="750px">

   <br><br>
   
   <img src="https://github.com/CauaDeSa/5by5-Championship/assets/127906505/b767be5a-b323-4580-bff8-0b6442706207" width="750px">
