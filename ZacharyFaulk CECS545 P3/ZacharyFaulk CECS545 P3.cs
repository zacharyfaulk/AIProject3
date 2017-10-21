using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ZacharyFaulk_CECS545_P3
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Code needed to run the Windows Form Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string fileName = "Random40.TSP";    //File name being tested

            //Variables to keep track of execution time
            Stopwatch stopWatch = new Stopwatch();
            float time1 = 0;
            string time2 = null;
            string timer = null;
            stopWatch.Start();      //Start Stopwatch

            int count = 0;
            float split = 2;        //Starting split point
            float shortSplit = 0;   //Split point resulting in shorest path
            float shortDistance = float.MaxValue;   //Stores shortest distance
            List<int> shortList = new List<int>();  //Stores shorest path
            List<int> lastPath = new List<int>();   //Stores the last tested path

            //Reads file to find # of lines
            //Stores (# of lines  - 7) to get the number of cities in the file
            string[] lines = File.ReadAllLines(fileName);
            int newLength = lines.Length - 7;

            //Array of newCity objects with size = # of cities
            newCity[] cityArray = new newCity[newLength];   

            //Stores city data
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                //Ignore first 7 lines of the file
                //8th line is where the city data begins
                if (count >= 7)
                {
                    string[] data = line.Split(' ');    //Split and store the city data in data string array

                    //Create newCity object using data pulled from data array (ID, x Coordinate, y Coordinate)
                    //Store each newCity object in newCity array
                    cityArray[count - 7] = new newCity(Int32.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
                }
                count++;
            }
            //Close File
            file.Close();

            //Split the map of cities at y = split to find
            //the split point that results in the shorest path
            for (split = 2; split < 100; split++)
            {
                //Call the split function to split the map of cities at y = split
                Split.split(cityArray, split, ref shortSplit, ref shortDistance, ref shortList, ref lastPath);
            }

            stopWatch.Stop();   //Stop Stopwatch

            //Print distance/path/split/time data
            Console.WriteLine("File = " + fileName);
            Console.WriteLine("The Greedy Distance is " + shortDistance);
            Console.WriteLine("The Greedy Path is " + string.Join(",", shortList));
            Console.WriteLine("Shortest split at y = " + shortSplit);
            time1 = stopWatch.ElapsedMilliseconds;
            time1 = (float)TimeSpan.FromMilliseconds(time1).TotalSeconds;
            time2 = stopWatch.ElapsedMilliseconds.ToString();
            timer = time1.ToString();
            Console.WriteLine("Execution time took " + time2 + " Milliseconds, or " + timer + " Seconds");

            //Run Windows Form Application to graph the shortest path
            Application.Run(new P3_GUI(cityArray, shortList));

            Console.ReadKey();
        }
    }

    //newCity class that creates newCity objects
    //Each object has an ID, x coordinate, y coordniate
    public class newCity
    {
        public int id;
        public float xCoordinate;
        public float yCoordinate;
            
        public newCity(int id, float x, float y)
        {
            this.id = id;
            this.xCoordinate = x;
            this.yCoordinate = y;
         }
    }

    //newCorner class that creates newCorner objects
    //Each object has an ID, tempID, coordinate float
    //Used to find the left and right most cities on each side of the split map
    public class newCorner
    {
        public int id;
        public int tempID;
        public float coordinate;

        public newCorner(float x)
        {
            this.coordinate = x;
        }
    }
}

