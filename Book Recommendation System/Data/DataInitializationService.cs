using Book_Recommendation_System.Data;
using Book_Recommendation_System.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Book_Recommendation_System.Data
{
    public class DataInitializationService
    {
        private readonly AppDbContext _dbContext;

        public DataInitializationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            try
            {
                // Specify the path to your CSV file
                var csvFilePath = @"C:\Users\rehab\Desktop\BookRS-Web-ML.net\Book_Recommendation_System.csv";

                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<Book>().ToList();
                    _dbContext.Book.AddRange(records);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., log them or throw them as needed
                // It's important to handle exceptions to prevent the application from crashing
            }
        }

        public void FillSampleData()
        {
            try
            {
                // Define your sample data
                var sampleData = new List<Book>
                {
                    new Book
                    {
                        UserID = 230117,
                        Location = "gujrat, brandenburg, india",
                        Age = 19,
                        ISBN = 0886776198,
                        Title = "The Ruins of Ambrai (Exiles, Vol. 1)",
                        Author = "Melanie Rawn",
                        YearOfPublication = 1994,
                        Publisher = "Daw Books",
                        Rating = 10
                    },
                    // Add more sample data here...
                };

                _dbContext.Book.AddRange(sampleData);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., log them or throw them as needed
                // It's important to handle exceptions to prevent the application from crashing
            }
        }
    }
}
