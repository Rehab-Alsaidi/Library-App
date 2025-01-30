using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Book_Recommendation_System.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Book_Recommendation_System.Data;
using System.Globalization;

namespace Book_Recommendation_System.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Book.ToList();
        }

        public void AddBooks(IEnumerable<Book> books)
        {
            _dbContext.Book.AddRange(books);
            _dbContext.SaveChanges();
        }


    }
}

