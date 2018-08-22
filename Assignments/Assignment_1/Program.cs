using System;

namespace Assignment_1
{
    class Program
    {
        string _bikeStationName;

        static void Main(string[] args)
        {
           // Console.WriteLine("Hello World!");
            ICityBikeDataFetcher dataFetcher = new ICityBikeDataFetcher();
            Console.WriteLine("Enter station's name: ");
            _bikeStationName = Console.ReadLine();
            dataFetcher.GetBikeCountInStation("string");
        }
    }

    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }

    class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {

    }
}
