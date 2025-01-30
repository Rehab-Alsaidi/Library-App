using Book_Recommendation_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;

namespace Book_Recommendation_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //  private readonly MLContext mlContext;
        //private IDataView dataView;
        //private ITransformer model;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            // mlContext = new MLContext();
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /* public IActionResult LoadAndPreprocessData()
         {
             // Define the data structure (BookRatingData) and load data
             dataView = mlContext.Data.LoadFromTextFile<BookRatingData>("C:\\Users\\rehab\\Desktop\\Book_BRS_L.csv", separatorChar: ',');

             /// Further preprocessing,such as data cleaning, 
             /// can be added here but i already do this before using colab .

             // Define the recommendation model options
             var options = new MatrixFactorizationTrainer.Options
             {
                 MatrixColumnIndexColumnName = "UserID",
                 MatrixRowIndexColumnName = "Title",
                 LabelColumnName = "Rating",
                 NumberOfIterations = 20,  
                 ApproximationRank = 100,  
             };

             // Create the recommendation pipeline
             var pipeline = mlContext.Transforms.Conversion.MapValueToKey("UserID")
                 .Append(mlContext.Transforms.Conversion.MapValueToKey("Title"))
                 .Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

             // Train the model
             model = pipeline.Fit(dataView);

             // Save the trained model for later use
             mlContext.Model.Save(model, dataView.Schema, "BRS-model.zip");

             return View();
         }

         public IActionResult GetRecommendedBooks(int userId)
         {
             // Create a prediction engine
             var predictionEngine = mlContext.Model.CreatePredictionEngine<BookRatingData, BookRatingPrediction>(model);

             var allBookTitles = dataView.GetColumn<string>("Title").ToArray();
             var allUserIds = dataView.GetColumn<float>("UserID").ToArray();

             var userIndex = Array.IndexOf(allUserIds, (float)userId);

             if (userIndex == -1)
             {
                 return View("UserNotFoundView"); // Handle the case where the user ID is not found in the data
             }

             var userScores = model.Transform(dataView);
             var userVector = userScores.Preview().RowView[userIndex].Values;

             var topRecommendations = userScores
                 .GetColumn<float>("Score")
                 .Select((score, index) => new { Index = index, Score = score })
                 .OrderByDescending(item => item.Score)
                 .Take(5);

             var recommendedBookTitles = topRecommendations
                 .Select(item => allBookTitles[item.Index])
                 .ToList();

             // Pass the recommended book titles to the view
             ViewData["RecommendedBooks"] = recommendedBookTitles;

             return View("RecommendedBooksView"); // Create a view to display the recommended books
         }

     } */
    }
}