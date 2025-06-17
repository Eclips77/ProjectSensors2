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
