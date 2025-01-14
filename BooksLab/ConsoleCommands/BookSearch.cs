using BooksLab.Books;
using BooksLab.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLab.ConsoleCommands
{
    public class BookSearch : IBookSearch
    {
        private IBookSearch _bookSearchImplementation;

        public async Task<List<Book>> SearchAsync(int userId, string query, Func<Book, string> field)
        {
            await using (BookContext context = new(userId))
            {
                return context.Books.AsEnumerable().Where(book => field(book).ToLower().Contains(query.ToLower())).ToList();
            }
        }

        public async Task<List<Book>> SearchByTitleAsync(int userId, string query)
        {
            return await SearchAsync(userId, query, book => book.Title);
        }

        public async Task<List<Book>> SearchByISBNAsync(int userId, string query)
        {
            return await SearchAsync(userId, query, book => book.ISBN);
        }

        public async Task<List<Book>> SearchByAuthorAsync(int userId, string query)
        {
            return await SearchAsync(userId, query, book => book.Author);
        }

        public async Task<List<Book>> SearchBooksAsync(int userId, string query, string searchType)
        { 
            return searchType.ToLower() switch
            {
                "title" => await SearchByTitleAsync(userId, query),
                "isbn" => await SearchByISBNAsync(userId, query),
                "author" => await SearchByAuthorAsync(userId, query),
                _ => throw new ArgumentException("Invalid search type")
            };
        }
    }
}