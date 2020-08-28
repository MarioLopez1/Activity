using System;

namespace Text
{
    class Program
    {
        static void Main(string[] args)
        {
            string direccion = "";
            string[] dnames = new string[255];
            int[] velocity = new int[255];
            int[] distance = new int[255];
            int[,] trips = new int[255, 3];
            int dnameIndex = 0;
            int tripsIndex = 0;
            for (int i = 0; i < 255; i++)
            {
                dnames[i] = " ";
                velocity[i] = 0;
                distance[i] = 0;
            }

            Console.WriteLine("Type the directory of the input file: ");
            direccion = Console.ReadLine();

            string[] lines = System.IO.File.ReadAllLines(direccion); //"C:\Users\.net\Desktop\test\hola.txt");
            bool exist = false;
            foreach (string line in lines)
            {
                int found = line.IndexOf(" ");
                string sub = Convert.ToString(line.Substring(0, found));
                if (sub == "Driver")
                {
                    string name = line.Substring(found+1, line.Length-found-1);
                    for (int h = 0; h < 255; h++)
                    {
                        if (name == dnames[h])
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (exist == false)
                    {
                        dnames[dnameIndex] = name;
                        dnameIndex++;
                    }

                }
                else if (sub == "Trip")
                {
                    string sub1 = line.Substring(found+1, line.Length - found-1);
                    int spacebeforehour1 = sub1.IndexOf(" ");
                    string name1 = sub1.Substring(0, spacebeforehour1);
                    string sub2 = sub1.Substring(spacebeforehour1 + 1, sub1.Length - spacebeforehour1 - 1);
                    string hour1 = sub2.Substring(0, 2);
                    int hour_1 = Convert.ToInt32(hour1);
                    int dot1 = sub2.IndexOf(":");
                    string sub3 = sub2.Substring(dot1 + 1, sub2.Length - dot1 - 1);
                    string min1 = sub3.Substring(0, 2);
                    int min_1 = Convert.ToInt32(min1);
                    int spacebeforehour2 = sub3.IndexOf(" ");
                    string sub4 = sub3.Substring(spacebeforehour2 + 1, sub3.Length - spacebeforehour2 - 1);
                    string hour2 = sub4.Substring(0, 2);
                    int hour_2 = Convert.ToInt32(hour2);
                    int dot2 = sub4.IndexOf(":");
                    string sub5 = sub4.Substring(dot2 + 1, sub4.Length - dot2 - 1);
                    string min2 = sub5.Substring(0, 2);
                    int min_2 = Convert.ToInt32(min2);
                    int spacebeforedist = sub5.IndexOf(" ");
                    string sub6 = sub5.Substring(spacebeforedist + 1, sub5.Length - spacebeforedist - 1);
                    string distInt = sub6.Substring(0, 2);
                    double distInt_ = Convert.ToDouble(distInt);
                    int dotI = sub6.IndexOf(".");
                    string sub7 = sub6.Substring(dotI + 1, sub6.Length - dotI - 1);
                    double ditM = Convert.ToDouble(sub7);

                    TimeSpan Date1 = new TimeSpan(hour_1, min_1, 0);
                    TimeSpan Date2 = new TimeSpan(hour_2, min_2, 0);
                    double Distance = distInt_ + ditM / 10;
                    double seconds = (Date2 - Date1).TotalSeconds;

                    int IndexOf = 0;

                    for(int k=0; k< dnameIndex;k++)
                    {
                        if(name1 == dnames[k])
                        {
                            IndexOf = k;
                            break;
                        }
                    }

                    trips[tripsIndex, 0] = IndexOf;
                    trips[tripsIndex, 1] = Convert.ToInt32(seconds);
                    trips[tripsIndex, 2] = Convert.ToInt32(Math.Round(Distance));
                    tripsIndex++;
                }
            }

            Console.WriteLine("Result: ");

            for(int p = 0; p < dnameIndex; p++)
            {
                double TotalTrips = 0;
                double TotalSeconds = 0;
                double TotalDistance = 0;
                string Name = dnames[p];
                for(int u = 0; u<tripsIndex;u++)
                {
                    if(p == trips[u,0])
                    {
                        TotalSeconds += trips[u, 1];
                        TotalDistance += trips[u, 2];
                        TotalTrips++;
                    }
                }

                double speed = 0;

                if (TotalSeconds != 0)
                {
                    speed = TotalDistance / TotalSeconds * 60 * 60;
                }

                Console.WriteLine($"{Name} : {Math.Round(TotalDistance)} miles at {Math.Round(speed)} mph");
                
            }
            

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
