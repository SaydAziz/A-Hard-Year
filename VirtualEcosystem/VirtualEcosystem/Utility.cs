using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    static class Utility
    {
        public delegate string DisplayLogInfo(List<string> consoleLog);
        public static DisplayLogInfo ShowLog = PrintLog;


        public static string PrintLog(List<string> consoleLog)
        {
            string[] LogList = new string[consoleLog.Count];
            int i = 0;
            foreach (string item in consoleLog)
            {
                LogList[i++] = item;
            }


            string LogListString = string.Join("\n", LogList);

            return LogListString;
        }
    }
}
