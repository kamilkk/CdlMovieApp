using SQLite;

using CdlMovieCore.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CdlMovieCore
{
    public class Repository
    {
        private readonly SQLiteAsyncConnection connection;

        public string StatusMessage { get; private set; }

        public Repository(string dbPath)
        {
            this.connection = new SQLiteAsyncConnection(dbPath);
            this.connection.CreateTableAsync<Movie>().Wait();
        }

        public void ResetStatus()
        {
            StatusMessage = string.Empty;
        }

        public async Task<bool> AddMovie(Movie movie)
        {
            try
            {
                var result = await this.connection.InsertAsync(movie).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Movie: {movie.Title})";
                return result > 0;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add record for movie: {movie.Title}. Error: {ex.Message}";
                return false;
            }
        }

        public async Task<bool> DeleteMovie(int movieId)
        {
            try
            {
                var movie = await this.connection.Table<Movie>().Where(m => m.Id == movieId).FirstOrDefaultAsync();
                if (movie != null)
                {
                    var result = await this.connection.DeleteAsync(movie);
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete record for movie id: {movieId}. Error: {ex.Message}";
                return false;
            }
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            try
            {
                return await this.connection.Table<Movie>().Where(m => m.Id == movieId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to get record for movie id: {movieId}. Error: {ex.Message}";
            }
            return null;
        }

        public Task<List<Movie>> GetAllMovies()
        {
            try
            {
                return this.connection.Table<Movie>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to get all movie records. Error: {ex.Message}";
            }
            return null;
        }
    }
}
