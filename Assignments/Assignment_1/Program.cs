using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment_1
{
    class Program
    {
        bool running = true;
       // bool offline = false;
        public ICityBikeDataFetcher dataFetcher;

        string[] invalidCharacters = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
        static void Main(string[] args)
        {
            Program p = new Program();
            string _bikeStationName;

            Console.WriteLine("Do you want to use the application (On)line or (Of)fline?");
            var answer = Console.ReadLine();
            if(answer.ToLower().Contains("on")) {
                Console.Write("Using online mode, fetcing data in real time..\n");
                p.dataFetcher = new RealTimeCityBikeDataFetcher();
            }
            else if(answer.ToLower().Contains("of")) {
                Console.Write("Using offline mode, loading data from a pre-loaded file.\n");
                p.dataFetcher = new OfflineCityBikeDataFetcher();
            }
            else {
                Console.Write("Defaulting online mode, fetcing data in real time.. \n");
                p.dataFetcher = new RealTimeCityBikeDataFetcher();
            }

           // ICityBikeDataFetcher dataFetcher = new RealTimeCityBikeDataFetcher();
            
            while(p.running)
            {
                Console.WriteLine("Enter station's name or type 'exit' to exit: ");
                _bikeStationName = Console.ReadLine();
                if(_bikeStationName.ToLower().Contains("exit")) 
                {
                    p.running = false;
                    break;
                }

            try {
                for(int i = 0; i < p.invalidCharacters.Length; ++i) 
                {
                    if(_bikeStationName.ToLower().Contains(p.invalidCharacters[i])) 
                    {
                        throw new System.ArgumentException("Invalid character");
                    }
                }
                p.dataFetcher.GetBikeCountInStation(_bikeStationName).Wait();
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }
    }

    public interface ICityBikeDataFetcher
    {
       Task<int> GetBikeCountInStation(string stationName);
    }

    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {           
        
        public int _bikesAvailable;
        public async Task<int> GetBikeCountInStation(string station)
        {
           // int amount;
            var httpClient = new System.Net.Http.HttpClient();

            var toast = await httpClient.GetByteArrayAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            string utfString = System.Text.Encoding.UTF8.GetString(toast);
            BikeRentalStationList stationInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BikeRentalStationList>(utfString);
            
            if(stationInfo.stations.Length != 0) 
            {
                for(int i = 0; i < stationInfo.stations.Length; ++i) 
                {
                    if(station.ToLower() == stationInfo.stations[i].name.ToLower()) 
                    {
                        _bikesAvailable = stationInfo.stations[i].bikesAvailable;
                        Console.WriteLine(stationInfo.stations[i].name + " has " + _bikesAvailable + " bike(s) available.\n");
                        return _bikesAvailable;
                    }
                }
            }
            Console.WriteLine("Station with that name was not found");
            return 0;
        }

    }

    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
         
        public int _bikesAvailable;
        public async Task<int> GetBikeCountInStation(string station)
        {

            using (StreamReader streamReader = new StreamReader(@"./bikedata.txt")) 
            {

                while(!streamReader.EndOfStream) 
                {  
                    var line = await streamReader.ReadLineAsync();
                    if(line.ToLower().Contains(station.ToLower())) 
                    {

                        string[] result = Regex.Split(line, @"\D+"); 
                        foreach(string temp in result) 
                        {
                            if(string.IsNullOrEmpty(temp) == false)  //if regex doesn't find a number
                            {
                                int _bikesAvailable = int.Parse(temp);
                                Console.WriteLine(station + " has "+ _bikesAvailable + " bikes available.\n");
                                return _bikesAvailable;
                            }
                        }

                    }
                }
                Console.WriteLine("Station with that name was not found.\n");
            }

            return 0;
        }

    }

    public class BikeRentalStationList
    {
        public BikeStation[] stations;
    }


    public class BikeStation 
    {
        public string name;
        public string state;
        public int id;
        public int bikesAvailable;
        public int spacesAvailable;

    }

}
