using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using CsvHelper;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Interview.FireworkStore.Core.Infrastructure.DataSourceProvider
{
    public class DataWriter<T> : IDataWriter<T>
    {
        private readonly ILogger<DataWriter<T>> _logger;
        private readonly IConfiguration _configuration;
        const string directory = @"Files\";

        public DataWriter(ILogger<DataWriter<T>> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public bool Create(T obj)
        {
            try
            {
                var ordersFile = _configuration["OrdersFile"];
                string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using var writer = new StreamWriter(Path.Combine(executableLocation, directory, ordersFile), true);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.NextRecord();
                csv.WriteRecord(obj);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Couldn't create order");
            }

            return false;
        }

        public bool Create(IEnumerable<T> objs)
        {
            try
            {
                var ordersFile = _configuration["OrdersFile"];
                string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using var writer = new StreamWriter(Path.Combine(executableLocation, directory, ordersFile), true);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                foreach(var obj in objs)
                {
                    csv.NextRecord();
                    csv.WriteRecord(obj);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't create orders");
            }

            return false;
        }
    }
}
