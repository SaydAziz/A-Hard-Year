using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;
using static VirtualEcosystem.DaySystem;

namespace VirtualEcosystem
{
    static class TemperatureSystem
    {
        static Random rng = new System.Random();
        static public int currentTemp;

        public static int CalculateTemp()
        {
                switch (currentMonth)
                {
                    case "Jan":
                        {
                            currentTemp = rng.Next(7, 16);
                            break;
                        }
                    case "Feb":
                        {
                            currentTemp = rng.Next(8, 17);
                            break;
                        }
                    case "Mar":
                        {
                            currentTemp = rng.Next(9, 20);
                            break;
                        }
                    case "Apr":
                        {
                            currentTemp = rng.Next(12, 24);
                            break;
                        }
                    case "May":
                        {
                            currentTemp = rng.Next(17, 29);
                            break;
                        }
                    case "Jun":
                        {
                            currentTemp = rng.Next(22, 35);
                            break;
                        }
                    case "Jul":
                        {
                            currentTemp = rng.Next(26, 38);
                            break;
                        }
                    case "Aug":
                        {
                            currentTemp = rng.Next(24, 36);
                            break;
                        }
                    case "Sep":
                        {
                            currentTemp = rng.Next(21, 33);
                            break;
                        }
                    case "Oct":
                        {
                            currentTemp = rng.Next(16, 27);
                            break;
                        }
                    case "Nov":
                        {
                            currentTemp = rng.Next(10, 20);
                            break;
                        }
                    case "Dec":
                        {
                            currentTemp = rng.Next(7, 16);
                            break;
                        }
                    default:
                        {
                            break;
                        }       
                }
            return currentTemp;
        }
    }
}
