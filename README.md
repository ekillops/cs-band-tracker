# _Band Tracker_

#### _C# Sql Practice 12/16/2016_

#### By _**Erik Killops**_

## Description

_A simple web app to track the venues a band has played at or what bands have played at a venue._

#### Specs

| BEHAVIOR                                       | INPUT                                           | OUTPUT                                    |
|------------------------------------------------|-------------------------------------------------|-------------------------------------------|
| Add a venue                                    | Add 'Doug Fir'                                  | 'Doug Fir' added                          |
| Update venue properties                        | Change 'Doug Fir' size from 'small' to 'medium' | 'Doug Fir' size: 'medium'                 |
| To add a band                                  | Add 'The Chameleons'                            | 'The Chameleons' added                    |
| To add genres                                  | Add genre 'Post Punk'                           | 'Post Punk' added                         |
| Add genre to band                              | Add 'Post Punk' to 'The Chameleons'             | 'Post Punk' genre add to 'The Chameleons' |
| Add genre to venue                             | Add 'Rock' to 'Doug Fir'                        | 'Rock' genre added to 'Doug Fir'          |
| Create performance                             | Add 'Foghorn Stringband' at 'Doug Fir'          | Performance added                         |
| Show list of bands that have played at a venue | Select 'Doug Fir'                               | List of bands                             |
| Show list of venues a band has played at       | Select 'The Chameleons'                         | List of venues                            |
| Show list of genres for band or venue          | 'Doug Fir' genres                               | List of genres                            |


## Setup/Installation Requirements

_Requires Windows, .Net, SMSS, and SQL SERVER_

1. Clone repository.
2. In SSMS, open band_tracker.sql
3. Click execute.
4. Repeat steps 2-5 for band_tracker_test.
5. In Powershell,  run ">dnx kestrel" and visit "localhost:5004".

Alternatively, open the .sql files in a text editor and run the commands one at a time in a SQL command line.

A image of the schema is included for reference.

## Known Bugs



## Technologies Used

HTML, C#, Nancy, Razor, Xunit, SQL.

### License

*GPL*

Copyright (c) 2016 **_Erik Killops, Epicodus_**
