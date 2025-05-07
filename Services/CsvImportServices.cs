using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace backend.Services
{
    public interface ICsvImportService
    {
        Task<List<T>> ParseCsvAsync<T>(IFormFile file);
    }

    public class CsvImportService : ICsvImportService
    {
        public async Task<List<T>> ParseCsvAsync<T>(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            var records = new List<T>();

            using var stream = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            records = csv.GetRecords<T>().ToList();

            return records;
        }
    }
}

