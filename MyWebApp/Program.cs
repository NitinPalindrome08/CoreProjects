/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MyWebApp.Controller
{
        public class CsvData
        {
            public string country { get; set; }
            public string lineOfBusiness { get; set; }

            public string Y2008 { get; set; }
            public string Y2009 { get; set; }
            public string Y2010 { get; set; }

            public string Y2011 { get; set; }
            public string Y2012 { get; set; }
            public string Y2013 { get; set; }
            public string Y2014 { get; set; }
            public string Y2015 { get; set; }
    }

    public class DataModel
        {

        Dictionary<string, Dictionary<string, Dictionary<string, double>>> csvData = new Dictionary<string, Dictionary<string, Dictionary<string, double>>>();
        string csvPath = "C:/Users/nitinkumar/Documents/gwp.csv";

        public double getLobAvg(string country, string lob)
        {
            double sum = 0; 
            foreach(var grossItem in csvData[country][lob])
            {
                sum += grossItem.Value;
            }

            return sum/8;
        }
        
        public void PopulateCSV()
        {
           
            // Open the CSV file with a StreamReader
            using (var reader = new StreamReader(csvPath))
            {
                // Create a CsvReader to parse the CSV content
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Read the CSV values into a list of objects
                    IEnumerable<CsvData> records = csv.GetRecords<CsvData>();

                    // Iterate over the list of objects and store the data in the dictionary
                    foreach (CsvData record in records)
                    {
                        string countryName = record.country;
                        string lineOfBusiness = record.lineOfBusiness;

                        string Y2008 = record.Y2008;
                        string Y2009 = record.Y2009;
                        string Y2010 = record.Y2010;
                        string Y2011 = record.Y2011;
                        string Y2012 = record.Y2012;
                        string Y2013 = record.Y2013;
                        string Y2014 = record.Y2014;
                        string Y2015 = record.Y2015;


                        if (!csvData.ContainsKey(countryName))
                        {
                            csvData[countryName] = new Dictionary<string, Dictionary<string, double>>();
                        }

                        if (!csvData[countryName].ContainsKey(lineOfBusiness))
                        {
                            csvData[countryName][lineOfBusiness] = new Dictionary<string, double>();
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2008))
                        {
                            csvData[countryName][lineOfBusiness]["Y2008"] = double.Parse(string.IsNullOrEmpty(Y2008) ? "0" : Y2008);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2009))
                        {
                            csvData[countryName][lineOfBusiness]["Y2009"] = double.Parse(string.IsNullOrEmpty(Y2009) ? "0" : Y2009);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2010))
                        {
                            csvData[countryName][lineOfBusiness]["Y2010"] = double.Parse(string.IsNullOrEmpty(Y2010) ? "0" : Y2010);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2011))
                        {
                            csvData[countryName][lineOfBusiness]["Y2011"] = double.Parse(string.IsNullOrEmpty(Y2011) ? "0" : Y2011);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2012))
                        {
                            csvData[countryName][lineOfBusiness]["Y2012"] = double.Parse(string.IsNullOrEmpty(Y2012) ? "0" : Y2012);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2013))
                        {
                            csvData[countryName][lineOfBusiness]["Y2013"] = double.Parse(string.IsNullOrEmpty(Y2013) ? "0" : Y2013);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2014))
                        {
                            csvData[countryName][lineOfBusiness]["Y2014"] = double.Parse(string.IsNullOrEmpty(Y2014) ? "0" : Y2014);
                        }

                        if (!csvData[countryName][lineOfBusiness].ContainsKey(Y2015))
                        {
                            csvData[countryName][lineOfBusiness]["Y2015"] = double.Parse(string.IsNullOrEmpty(Y2015) ? "0" : Y2015);
                        }
                    }
                }
            }

        }



    }
    public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                services.AddRouting();
                //services.AddNewtonsoftJson();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                app.Run(async context =>
                {
                    //Post request
                    if (context.Request.Method == HttpMethods.Post && context.Request.Path == "/api/gwp")
                    {
                        // Handle the POST request here
                        using (StreamReader reader = new StreamReader(context.Request.Body))
                        {
                            string requestBody = await reader.ReadToEndAsync();

                            //var myJson = JsonConvert.DeserializeObject(requestBody);

                            var jsonObject = JObject.Parse(requestBody);

                            var cn = jsonObject["country"];
                            var lobArray = jsonObject["lob"];
                            var lob = lobArray.ToObject<string[]>();


                            string requestResponse = "";
                            DataModel dm = new DataModel();
                            dm.PopulateCSV();
                            foreach (var item in lob)
                            {
                                double result = dm.getLobAvg(Convert.ToString(cn), item);
                                requestResponse += item + " : " + Convert.ToString(result) + "\n"; 
                            }
                            
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(requestResponse);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync("Not found");
                    }
                });
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                //DataModel dm = new DataModel();
                //dm.PopulateCSV();
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls("http://localhost:9091")
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
        }
}