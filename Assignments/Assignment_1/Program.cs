using System;
using System.Collections.Generic;
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
            int bikesAvailable;
            //var test = await httpClient.GetAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            var toast = await httpClient.GetByteArrayAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            string test = System.Text.Encoding.UTF8.GetString(toast);
            //toast.ToString();
            
            //Console.Write(test);
            //Console.WriteLine("station: "  + station + ", test = " + test);
           // test.Split("id");


          // Console.Write(test);

           // string[] stations;

            BikeRentalStationList stationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BikeRentalStationList>(test);
            //stations = test.Split(',');

           // stationInfo.ToString();
           foreach(object bikestation in stationInfo)
           {
               
           }
            Console.WriteLine(stationInfo.stations[5]);

          /*  for(int i = 0; i < stationInfo.stations.Length; ++i)
            {
                if(stationInfo.stations[i].)
            }
*/

            return 1;
        }

    }

    public class BikeRentalStationList
    {
        public object[] stations;
      /*  public string name;
        public string state;
        public int id;
        public int bikesAvailable;
        public int spacesAvailable;*/
    }
}
