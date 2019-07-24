using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Google.OrTools.ConstraintSolver;
using System.Linq;
using Newtonsoft.Json;
using Google.Protobuf.WellKnownTypes;//Duration
using System.Diagnostics;
using VrpTest.Struct;
using System.Net;
using System.Data.SqlClient;

namespace VrpTest.Struct
{
    public static class LocationDB
    {
        public static List<Location> Locations { get; set; }

        static LocationDB()
        {
            Locations = new List<Location>();

            Locations = GetLocations();
            //GetCustomLocations();
            
        }

   
        public static List<Location> GetLocations()
        {
            //string result = "";
            List<Location> locationList = new List<Location>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                //string sql = "SELECT X, Y FROM LG_XT058005_814" +
                //             "\nWHERE RUT_ICI = @RUT_ICI AND SESSIONID = @SESSIONID";

                string sql = "SELECT SLSCL.CLIENTREF, MIN(CL.DEFINITION_), MIN(SA.X), MIN(SA.Y)," +
                    " MIN(SLSCL.VISITDAY), MIN(SLSCL.VISITPERIOD)" +
                    " FROM LG_814_SLSCLREL AS SLSCL" +
                                "\nINNER JOIN LG_SLSMAN AS SLS ON SLS.LOGICALREF = SLSCL.SALESMANREF" +
                                "\nINNER JOIN LG_814_CLCARD AS CL ON CL.LOGICALREF = SLSCL.CLIENTREF" +
                                "\nINNER JOIN LG_814_SHIPINFO AS SHP ON SHP.CLIENTREF = CL.LOGICALREF" +
                                "\nINNER JOIN LG_XT001_814 AS SA ON SA.PARLOGREF = SHP.LOGICALREF" +
                                "\nWHERE  SLS.ACTIVE = 0" +
                                "\nAND SLS.FIRMNR = 814" +
                                "\nAND SA.Y >= 26" +
                                "\nAND SA.Y <= 30" +
                                "\nAND SA.X >= 37" +
                                "\nAND SA.X <= 41" +
                                "\nAND ISNULL(SA.X, '') != ''" +
                                "\nAND ISNULL(SA.Y, '') != ''" +
                                "\nGROUP BY SLSCL.CLIENTREF";

                SqlCommand command = new SqlCommand(sql, conn);
                //command.Parameters.Add(new SqlParameter("RUT_ICI", 1));
                //command.Parameters.Add(new SqlParameter("SESSIONID", 10530));

                int cntt = 0;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[0] != DBNull.Value && reader[1] != DBNull.Value)
                        {
                            //cntt++;
                            //if (cntt < 108)
                            //    continue;
                            string coordinates = reader[2].ToString() + reader[3].ToString();
                            Console.WriteLine(coordinates);
                            Console.WriteLine(new Position(Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3])).strPos_);

                            Location location = new Location(
                                Convert.ToInt32(reader[0]), reader[1].ToString(),
                                new Position(Convert.ToDouble(reader[2]), 
                                Convert.ToDouble(reader[3])),
                                Convert.ToInt32(reader[4]),
                                Convert.ToInt32(reader[5]),
                                0,600,false
                                );

                            locationList.Add(location);
                        }
                    }
                }
                conn.Close();
            }

            return locationList;
        }

        public static string GetConnectionString()
        {
            return "Data Source=(localdb)" +
                "\\" +
                "MSSQLLocalDB;Initial Catalog=BASAKMANUNISEL;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public static void GetCustomLocations()
        {
            ////Default Locations
            //Location location = new Location(0,"", new Position(35.051031, -89.793120), 0, 200, true);//depot -- 3610+Hacks+Cross+Rd+Memphis+TN
            //Locations.Add(location);

            //Location location1 = new Location(0,"", new Position(35.045108, -89.621660), 40, 56, false);
            //Locations.Add(location1);

            //Location location2 = new Location(0,"", new Position(35.049904, -89.614416), 30, 45, false);
            //Locations.Add(location2);

            //Location location3 = new Location(0,"", new Position(35.045100, -89.610934), 55, 70, false);
            //Locations.Add(location3);

            //Location location4 = new Location(0,"", new Position(35.138350, -90.010171), 65, 88, false);
            //Locations.Add(location4);

            //Location location5 = new Location(0,"",new Position(34.912475, -89.892589), 40, 65, false);
            //Locations.Add(location5);

            //Location location6 = new Location(0,"",new Position(35.122387, -89.941454), 25, 50, false);
            //Locations.Add(location6);

            //Location location7 = new Location(0,"",new Position(34.861102, -89.831365), 0, 35, false);
            //Locations.Add(location7);

            //Location location8 = new Location(0,"",new Position(35.106714, -89.916721), 25, 54, false);
            //Locations.Add(location8);

            //Location location9 = new Location(0,"",new Position(35.116022, -89.962513), 30, 55, false);
            //Locations.Add(location9);

            //Location location10 = new Location(0,"",new Position(35.153155, -89.992550), 100, 120, false);
            //Locations.Add(location10);

            //Location location11 = new Location(0,"",new Position(35.153526, -90.043629), 50, 65, false);
            //Locations.Add(location11);

            //Location location12 = new Location(0,"",new Position(35.148928, -90.053377), 65, 80, false);
            //Locations.Add(location12);

            //Location location13 = new Location(0,"",new Position(35.098665, -89.865847), 15, 35, false);
            //Locations.Add(location13);

            //Location location14 = new Location(0,"",new Position(35.155035, -89.966837), 45, 80, false);
            //Locations.Add(location14);

            //Location location15 = new Location(0,"",new Position(35.159642, -89.959872), 55, 100, false);
            //Locations.Add(location15);


            //    //Istanbul
            //    //Not gunluk 600 dakika calisabilir

            //    Location location16 = new Location("40.797142+29.382287", 0, 200, true);
            //    Locations.Add(location16);//40.797142, 29.382287 Gebze Depo

            //    Location location17 = new Location("40.879389+29.257468", 0, 80, false);
            //    Locations.Add(location17);//Pendik Bakkal1

            //    Location location18 = new Location("40.891902+29.189942", 0, 120, false);
            //    Locations.Add(location18);//Kartal Bakkal1 

            //    Location location19 = new Location("40.892085+29.193903", 30, 120, false);
            //    Locations.Add(location19);//Kartal Bakkal2

            //    Location location20 = new Location("40.930321+29.156061", 0, 180, false);
            //    Locations.Add(location20);//Gulsuyu Bakkal1

            //    Location location21 = new Location("40.925076+29.135498", 60, 210, false);
            //    Locations.Add(location21);//Baglarbasi Bakkal1

            //    Location location22 = new Location("41.039699+29.104331", 360, 480, false);
            //    Locations.Add(location22);//Umraniye Bakkal1 

            //    Location location23 = new Location("41.030835+29.035284", 0, 180, false);
            //    Locations.Add(location23);//Uskudar Bakkal1 

            //    //Gebze tarafi

            //    Location location24 = new Location("40.835618+29.301208", 0, 180, false);
            //    Locations.Add(location24);//Tuzla Bim

            //    Location location25 = new Location("40.824819+29.310304", 0, 180, false);
            //    Locations.Add(location25);//Tuzla Carrefour

            //    Location location26 = new Location("40.825654+29.319878", 0, 180, false);
            //    Locations.Add(location26);//Tuzla Mopas

            //    Location location27 = new Location("40.824750+29.372604", 0, 240, false);
            //    Locations.Add(location27);//Cayirova Mopas

            //    Location location28 = new Location("40.817789+29.371047", 0, 240, false);
            //    Locations.Add(location28);//Cayirova Migros Jet 

            //    Location location29 = new Location("40.813845+29.402778", 0, 240, false);
            //    Locations.Add(location29);//Gevze Bizim Market

            //    Location location30 = new Location("40.810741+29.403533", 0, 240, false);
            //    Locations.Add(location30);//Gebze Akmar

            //    Location location31 = new Location("40.776801+29.389061", 0, 240, false);
            //    Locations.Add(location31);//Darica Akdeniz Market 

            //    Location location32 = new Location("40.776448+29.353651", 0, 240, false);
            //    Locations.Add(location32);//Darica Ozen Market 

            //    Location location33 = new Location("40.759876+29.385730", 0, 240, false);
            //    Locations.Add(location33);//Darica Bim 


            //    //Avrupa yakası
            //    Location location34 = new Location("41.009756+28.967856", 240, 400, false);
            //    Locations.Add(location34);//Şaafi Kebap Salonu Eminönü

            //    Location location35 = new Location("41.016102+28.973159", 240, 600, false);
            //    Locations.Add(location35);//Hafız Mustafa Eminönü

            //    Location location36 = new Location("41.062989+28.990483", 0, 240, false);
            //    Locations.Add(location36);//Günay Rest Şişli

            //    Location location37 = new Location("41.109442+29.018298", 500, 600, false);
            //    Locations.Add(location37);//Plaza Cubes Sarıyer

            //    Location location38 = new Location("41.228735+29.111791", 0, 600, false);
            //    Locations.Add(location38);//Cafe Fener Rumelifeneri

            //    Location location39 = new Location("41.064852+28.841873", 0, 400, false);
            //    Locations.Add(location39);//Batışehir Focaccia Bağcılar/Esenler

            //    Location location40 = new Location("40.992084+28.834974", 0, 200, false);
            //    Locations.Add(location40);//Nef Ataköy 22

            //    Location location41 = new Location("40.971358+28.791196", 200, 360, false);
            //    Locations.Add(location41);//Görkem Kilis Sofrası Bakırköy

            //    Location location42 = new Location("41.022248+28.773106", 350, 500, false);
            //    Locations.Add(location42);//Köfteci onur küçükçekmece atatürk parkı arkası

            //    Location location43 = new Location("41.181198+28.745308", 0, 240, false);
            //    Locations.Add(location43);//Dominos Pizza Arnavutköy

            //    Location location44 = new Location("41.007845+28.716998", 240, 600, false);
            //    Locations.Add(location44);//Safran Elite Mangalbaşı & Cafe Avcılar

            //    Location location45 = new Location("41.103315+28.882457", 180, 480, false);
            //    Locations.Add(location45);//Bülbülen Cagkebabi

        }

    }
}
