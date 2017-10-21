using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZacharyFaulk_CECS545_P3
{
    class Split
    {
        public static void split(newCity[] cityArray, float split, ref float shortSplit, ref float shortDistance, ref List<int> shortList, ref List<int> lastPath)
        {
            //Distance variables
            float distance = 0;
            float totalDistance = 0;

            //newCorner objects to find the 4 corners
            newCorner topLeft = new newCorner(float.MaxValue);
            newCorner topRight = new newCorner(0);
            newCorner bottomLeft = new newCorner(float.MaxValue);
            newCorner bottomRight = new newCorner(0);

            //Lists to store the cities above and below the split point
            List<int> topList = new List<int>();
            List<int> topFinal = new List<int>();
            List<int> bottomList = new List<int>();
            List<int> bottomFinal = new List<int>();

            //Check each city in the cityArray to see if it's
            //above or below the split point
            for (int c = 1; c < cityArray.Length + 1; c++)
            {
                //Above the split point
                if (cityArray[c - 1].yCoordinate > split)
                {
                    topList.Add(c);     //Add to the topList

                    //Check if the city is the left or right most city
                    if (cityArray[c - 1].xCoordinate < topLeft.coordinate)
                    {
                        topLeft.id = c;
                        topLeft.coordinate = cityArray[c - 1].xCoordinate;
                    }
                    if (cityArray[c - 1].xCoordinate > topRight.coordinate)
                    {
                        topRight.id = c;
                        topRight.coordinate = cityArray[c - 1].xCoordinate;
                    }
                }

                //At or below the split point
                else
                {
                    bottomList.Add(c);  //Add to the bottom list

                    //Check if the city is the left or right most city
                    if (cityArray[c - 1].xCoordinate < bottomLeft.coordinate)
                    {
                        bottomLeft.id = c;
                        bottomLeft.coordinate = cityArray[c - 1].xCoordinate;
                    }
                    if (cityArray[c - 1].xCoordinate > bottomRight.coordinate)
                    {
                        bottomRight.id = c;
                        bottomRight.coordinate = cityArray[c - 1].xCoordinate;
                    }
                }
            }

            //Adding the corner cities to the top/bottomFinal lists
            //Removing the corner cities from the top/bottom lists
            topFinal.Add(topLeft.id);
            topFinal.Add(topRight.id);
            bottomFinal.Add(bottomRight.id);
            bottomFinal.Add(bottomLeft.id);
            topList.Remove(topLeft.id);
            topList.Remove(topRight.id);
            bottomList.Remove(bottomLeft.id);
            bottomList.Remove(bottomRight.id);

            //Call insertNode function to generate the new greedy paths
            InsertNode.insertNode(cityArray, topList, ref topFinal);
            InsertNode.insertNode(cityArray, bottomList, ref bottomFinal);

            //Combine the top and bottom paths to complete the tour
            topFinal.AddRange(bottomFinal);
            topFinal.Add(topFinal[0]);

            //If the path's distance wasn't the most recent path to be computed
            //find the distance of the path
            if (topFinal.SequenceEqual(lastPath) == false)
            {
                for (int d = 1; d < topFinal.Count; d++)
                {
                    int xy1 = topFinal[d - 1] - 1; //Location of city A in city List
                    int xy2 = topFinal[d] - 1;     //Location of city B in city List

                    //Find x and y coordinates of city A and B
                    float x1 = cityArray[xy1].xCoordinate;
                    float x2 = cityArray[xy2].xCoordinate;
                    float y1 = cityArray[xy1].yCoordinate;
                    float y2 = cityArray[xy2].yCoordinate;

                    //Use distance equation to find the distance between city A and B
                    //Add distance to totalDistance
                    distance = (float)Math.Sqrt((((x2 - x1)) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
                    totalDistance = totalDistance + distance;
                }

                //If the new path distance is smaller than the
                //current smallest distance then replace
                //the smallest distance with the new distance
                if (totalDistance < shortDistance)
                {
                    shortDistance = totalDistance;  //Store the new shorter distance
                    shortList = topFinal;           //Store the corresponding path
                    shortSplit = split;             //Store the current split value
                }

                //Store the most recent path to avoid
                //computing the same path multiple times
                lastPath = topFinal;
            }

        }
    }
}
