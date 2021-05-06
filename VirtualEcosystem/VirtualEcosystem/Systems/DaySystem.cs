using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace VirtualEcosystem
{
    static class DaySystem
    {
        static public bool Migration = false;
        static string filePath = "Months.txt";

        static public string currentMonth = "Jan";
        static private string[] Month = File.ReadAllLines(filePath);
        static public bool isDay = true;
        static public int totalDay = 0;

        static private Random mRNG = new Random();
        


        static public void ShowDay()
        {
            totalDay++;
        }
        static int i = 1;
        
        static public string ShowMonth()
        {        
            try
            {
                if (totalDay % 3 == 0)
                {
                    currentMonth = Month[i++];
                    MigrateSeasonCheck();
                }
                return currentMonth;
            }
            catch
            {
                return "FINISHED";
            }
            
        }


        static public void MigrateSeasonCheck()
        {
            if (mRNG.Next(1, 3) == 1)
            {
                if (Migration) 
                {
                    Migration = false;
                }
                else
                {
                    Migration = true;
                }             
            }
        }
    }
}
