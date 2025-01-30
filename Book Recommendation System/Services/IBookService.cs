using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Book_Recommendation_System.Models;

namespace Book_Recommendation_System.Services
    {
        public interface IBookService
        {
            IEnumerable<Book> GetBooks();
            void AddBooks(IEnumerable<Book> book);
            
        }
    }



