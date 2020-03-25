using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using FunctionAppSample.Models;
using System.Linq;

namespace FunctionAppSample
{

    public static class Library
    {
        public static List<Book> books = new List<Book>
        {
            new Book {Id = 1, Name = @"New Qiita", IsRent = false},
            new Book {Id = 2, Name = @"TryAgain—é–Ø‚ÌYouTube", IsRent = false}
        };
    }

    public static class Function1
    {

        [FunctionName("GetBooks")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(Library.books);
        }
    }
    public static class Function2
    {

        [FunctionName("PostBook")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var book = JsonConvert.DeserializeObject<Book>(req.ReadAsStringAsync().Result);

            book.Id = Library.books.Max(i => i.Id) + 1;
            Library.books.Add(book);


            return new CreatedResult(book.Id.ToString(), Library.books.Where(_ => _.Id == book.Id));
        }
    }
}
