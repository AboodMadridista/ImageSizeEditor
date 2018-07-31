using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizeUI
{
    public class Logger
    {
        /// <summary>
        /// Logger Write Function
        /// Function add hour values to log input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Write(string input)
        {
            File.AppendAllText("ImageResizeUI.log", DateTime.Now + " ### " + input + Environment.NewLine);
            return "### " + DateTime.Now + " ###" + input;
        }
    }
}
