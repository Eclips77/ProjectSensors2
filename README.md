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

A new on-screen countdown shows how long you have to pick a sensor each turn. Use the **How To Play** option in the main menu for a quick overview of controls and tips.

## Database Setup

Run the `mysql_setup.sql` script in your MySQL server (for example using phpMyAdmin) to create the default `game` database and the required tables. Each player is stored in the `players` table and individual game sessions are recorded in `game_history`.

### Upgrading existing databases

If you upgraded from an older version and encounter errors like `Unknown column 'username'` or `Unknown column 'score'`, your existing `game_history` table is missing these fields. Run the provided `mysql_upgrade.sql` script to add the required columns and create the foreign key linking `game_history.username` to the `players` table.

When starting the application you will be prompted for a username. After logging in you can play, view your history or log out to switch users. A special user named `admin` can view the history of all players.
