# Investigation Game

This console application simulates interrogations of captured agents using different sensors.

## Building

The project targets **.NET Framework 4.7.2**. To build:

```sh
dotnet build ProjectSensors.sln
```

Or using `msbuild`/`xbuild` if `dotnet` is unavailable.

## Running

After building, execute the compiled `ProjectSensors.exe` from the `bin` directory.

The game records sessions to a MySQL database using the connection string you provide when creating the DAL classes.

## Database Setup

Run the `mysql_setup.sql` script in your MySQL server (for example using phpMyAdmin) to create the default `game` database and the required tables. Each player is stored in the `players` table and individual game sessions are recorded in `game_history`.

When starting the application you will be prompted for a username. History and scores are tracked separately for each player.
