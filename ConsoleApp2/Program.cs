using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //int waferCenterX = -23, waferCenterY = 41;
            //int waferRadius = 100;
            //int dieWidth = 2, dieHeight = 2;
            //int edgeThickness = 4;


            //List<MapDie> edgeDies = WaferMapUtility.FindEdgeDies(waferCenterX, waferCenterY, waferRadius, dieWidth, dieHeight, edgeThickness);

            ////foreach (var die in edgeDies)
            ////{
            ////    Console.WriteLine($"Die at ({die.X}, {die.Y}) - IsEdge: {die.IsEdge}");
            ////}

            //// 找到晶圆内 Die 的边界，以便确定打印范围
            //double minX = double.MaxValue, maxX = double.MinValue;
            //double minY = double.MaxValue, maxY = double.MinValue;

            //foreach (var die in edgeDies)
            //{
            //    if (die.X < minX) minX = die.X;
            //    if (die.X > maxX) maxX = die.X;
            //    if (die.Y < minY) minY = die.Y;
            //    if (die.Y > maxY) maxY = die.Y;
            //}

            //// 打印 Die 排布
            //for (double y = maxY; y >= minY; y -= dieHeight)
            //{
            //    for (double x = minX; x <= maxX; x += dieWidth)
            //    {
            //        // 找到当前坐标对应的 Die
            //        var die = edgeDies.Find(d => Math.Abs(d.X - x) < 0.01 && Math.Abs(d.Y - y) < 0.01);

            //        if (die != null)
            //        {
            //            Console.Write(die.IsEdge ? "ED" : "..");  // 边缘 Die 显示 "ED"，其他 Die 显示 ".."
            //        }
            //        else
            //        {
            //            Console.Write("XX");  // 如果该位置没有 Die，则留空
            //        }
            //    }
            //    Console.WriteLine();
            //}


            //var center = WaferMapUtility.CalculateWaferCenter(35, 86, 117,90 );
            //Console.WriteLine($"centerX={center.centerX}, centerY={center.centerY}");


            //Console.WriteLine("-------------Wafer struct Start.................");
            //int Wafer_Testing_Size = Marshal.SizeOf(typeof(Wafer_Testing));
            //Console.WriteLine($"Wafer_Testing_Size={Wafer_Testing_Size}");

            //int Test_Result_per_Die_Size = Marshal.SizeOf(typeof(Test_Result_per_Die));
            //Console.WriteLine($"Test_Result_per_Die_Size={Test_Result_per_Die_Size}");

            //int Header_Information_Die_Size = Marshal.SizeOf(typeof(Header_Information));
            //Console.WriteLine($"Header_Information_Die_Size={Header_Information_Die_Size}");

            //int MapData_Size = Marshal.SizeOf(typeof(MapData));
            //Console.WriteLine($"MapData_Size={MapData_Size}");

            //Console.WriteLine("-------------Wafer struct End.................\n");

            //Console.WriteLine("Start-------------Read Wafer map.................");
            //WaferMapAgtent waferMapAgtent = new WaferMapAgtent();

            //waferMapAgtent.ReadFile(@"./Galaxy_MAP_0001.DAT");

            //waferMapAgtent.WriteFile_ascii_information("./wafer_information.txt");
            //waferMapAgtent.WriteFile_test_die("./wafer_test_dices.txt");


            //int[,] asciimap = waferMapAgtent.getAsdciiMap();
            //waferMapAgtent.WriteFile_ascii_map("./wafer_map.txt", asciimap);

            //int[,]  rotatemap = WaferMapAgtent.RotateMatrix90Degrees(asciimap);
            //waferMapAgtent.WriteFile_ascii_map("./wafer_map_rotate90.txt", rotatemap);
            //Console.WriteLine("End-------------Read Wafer map.................");


            //Console.WriteLine("Start-------------Modify Wafer map.................");

            //MapData mapData = waferMapAgtent.GetMapData();
            //mapData.header_Information.wafer_Testing.OperatorName = "John-123456";
            //mapData.header_Information.wafer_Testing.WaferSize = 90;
            //mapData.header_Information.wafer_Testing.MachineNo = 3;
            //mapData.header_Information.wafer_Testing.DeviceName = "Innogrity";
            //mapData.header_Information.wafer_Testing.FinalEditingMachinetype = 1;
            //mapData.header_Information.wafer_Testing.IndexSizeX = 99;
            //mapData.header_Information.wafer_Testing.IndexSizeY = 88;
            //mapData.header_Information.wafer_Testing.StandardOrientationFlatDirection = 270;

            //mapData.header_Information.printf();

            //Console.WriteLine($"Word3={mapData.test_Result_Per_Dice[0].Word3}");
            //Console.WriteLine($"Xaxis={mapData.test_Result_Per_Dice[0].Xaxis}");            
            //mapData.test_Result_Per_Dice[0].Bin = 9;

            //Console.WriteLine($"Xaxis={mapData.test_Result_Per_Dice[0].Xaxis}");
            //Console.WriteLine($"Bin={mapData.test_Result_Per_Dice[0].Bin}");
            //Console.WriteLine($"Word3={mapData.test_Result_Per_Dice[0].Word3}");


            //for (int i = 1; i < mapData.test_Result_Per_Dice.Length; i++)
            //{
            //    mapData.test_Result_Per_Dice[i].Bin = 7;
            //    mapData.test_Result_Per_Dice[i].Marking = 1;
            //}


            //waferMapAgtent.WriteBinary("./IGTBinary.dat");
            //waferMapAgtent.WriteFile_ascii_map("./modify_map.txt");
            //waferMapAgtent.WriteFile_test_die("./modify_wafer_test_dices.txt");

            //Console.WriteLine("End-------------Modify Wafer map.................\n");






            //
            uint waferDiameter = 200;
            int OtopLeftX = 30;
            int OtopLeftY = 30;
            uint dieWitdh = 1780;  //um
            uint dieheight = 2186;//um


            Console.WriteLine("Start-------------New Wafer map.................");
            WaferMapAgtent newwafer = WaferMapAgtent.CreateWaferMap(dieWitdh, dieheight, waferDiameter, OtopLeftX, OtopLeftY);


            newwafer.WriteFile_test_die("./new_wafer_test_dices.txt");
            int[,] asciimapNew = newwafer.getAsdciiMap();
            newwafer.WriteFile_ascii_map("./new_map.txt", asciimapNew);


            int[,]  rotatemap_180 = WaferMapAgtent.RotateMatrix90Degrees(asciimapNew);
            rotatemap_180 = WaferMapAgtent.RotateMatrix90Degrees(rotatemap_180);
            newwafer.WriteFile_ascii_map("./new_map_180.txt", rotatemap_180);

            newwafer.WriteBinary("./new_IGTBinary.dat");

            Console.WriteLine("End-------------New Wafer map.................\n");
        }



    }
}
