# Pop Movies
Simple MovieDB explorer - gets the list of the most popular movies from Movie DB-  developed in Xamarin with usage of the MvvmCross and SQLite.

Repo contains:

* `CdlMovieCore` project - base layer for the application, handles: Movie DB calls and SQLite usage
* `CdlMovieApp` project - the iOS applicatio 
* `CdlMovieCore.UnitTests` - the Unit Test project - currently covers with tests only own REST client located in `CdlMovieCore` project

**Caution:**

Before running project please change the Movie DB API key located in the `Const.cs` file on your own.