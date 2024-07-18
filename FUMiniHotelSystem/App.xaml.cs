using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using DataAccessObjects.DataAccessObjects;
using Microsoft.Extensions.Configuration;

namespace FUMiniHotelSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDAO _authenticationDAO;
        public App()
        {
            // Tạo và cấu hình IConfiguration
            //var solutionDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var solutionDir = @"C:\Users\ADMIN\Documents\Summer2024\PRN212\PRN212\2_PRN212\Assignment2";
            var configFilePath = Path.Combine(solutionDir, "FUMiniHotelSystem", "appsettings.json");
            var builder = new ConfigurationBuilder()
                .SetBasePath(solutionDir)
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            // Tiêm AuthenticationDAO với IConfiguration
            _authenticationDAO = new AuthenticationDAO();
        }
    }

}
