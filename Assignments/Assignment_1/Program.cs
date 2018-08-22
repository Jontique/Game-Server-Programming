using System;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft;

namespace Assignment_1
{
    class Program
    {

        static void Main(string[] args)
        {
            string _bikeStationName;
            ICityBikeDataFetcher dataFetcher = new RealTimeCityBikeDataFetcher();
            Console.WriteLine("Enter station's name: ");
            _bikeStationName = Console.ReadLine();
            dataFetcher.GetBikeCountInStation(_bikeStationName).Wait();
            Console.ReadKey();

        }
    }

    public interface ICityBikeDataFetcher
    {
       Task<int> GetBikeCountInStation(string stationName);
    }

    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string station)
        {
           // int amount;
            var httpClient = new System.Net.Http.HttpClient();
            //var test = await httpClient.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            var toast = await httpClient.GetByteArrayAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            string test = System.Text.Encoding.UTF8.GetString(toast);
            //toast.ToString();
            
            //Console.WriteLine("station: "  + station + ", test = " + test);
           // test.Split("id");

           

           // string[] stations;

            var stationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BikeStation>(test);
            //stations = test.Split(',');

            Console.WriteLine(stationInfo);


            return 1;
        }

    }

    class BikeStation
    {
        string name;
        string state;
        int id;
        int bikesAvailable;
        int spacesAvailable;

    }
}
