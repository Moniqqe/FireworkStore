using CsvHelper;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Interview.FireworkStore.Core.Infrastructure.DataSourceProvider
{
    public class DataReader : IDataReader
    {
        private readonly ILogger<DataReader> _logger;
        private readonly IConfiguration _configuration;
        const string directory = @"Files\";
        

        public DataReader(ILogger<DataReader> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IEnumerable<Firework> LoadFireworks()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fireworksFile = _configuration["FireworksFile"];
            return LoadFile<Firework>(Path.Combine(executableLocation, directory, fireworksFile));
        }

        public IEnumerable<Order> LoadOrders()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var ordersFile = _configuration["OrdersFile"];
            return LoadFile<Order>(Path.Combine(executableLocation, directory, ordersFile));
        }

        private IEnumerable<T> LoadFile<T>(string path)
        {
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<T>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't load file"); 
            }

            return Enumerable.Empty<T>();
        }
    }
}