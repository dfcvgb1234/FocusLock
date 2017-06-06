using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace SheetsQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static string programPath = @"C:\Windows\System32\drivers\etc\Programs.begeba";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1decwKsk9kd8FqJP5nAVwAcKoaUYvwe9DFAZNEE5gjPQ";
            String range = "Ark1!A2:C999";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            int i = 0;
            if (File.Exists(programPath))
            {
                File.Delete(programPath);
            }
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                { 
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}, {2}",values[i][0],values[i][1],values[i][2]);

                    File.AppendAllText(programPath, values[i][0] + "," + values[i][1] + "," + values[i][2] + ";");

                    i++;
                }
            }
            else
            {
                Console.WriteLine("Error! CODE 2: Could not find data in Sheet or Sheet does not exists");
                Console.Read();
            }

            using (WebClient webclient = new WebClient())
            {
                webclient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                webclient.UploadFile("ftp://ftp.focuslock.dk/ftp/Programs.begeba", programPath);

                Console.Read();
            }
        }
    }
}