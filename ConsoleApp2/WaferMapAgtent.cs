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
    public class WaferMapAgtent
    {
        public MapData mapData = new MapData();

        public int edge_thickness = 2;
        public uint edge_rollback = 1;
        public WaferMapAgtent()
        {

        }
        /// <summary>
        /// read tsk wafer format binary file(*.dat)
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadFile(string filePath)
        {
            using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open))
            {
                using (var stream = mmf.CreateViewStream())
                {
                    // 
                    if (stream.Position < stream.Length)
                    {

                        byte[] buffer = new byte[Marshal.SizeOf(typeof(Header_Information))];
                        stream.Read(buffer, 0, buffer.Length);


                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        try
                        {
                            mapData.header_Information = (Header_Information)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Header_Information));
                            //mapData.header_Information.printf();

                        }
                        finally
                        {
                            handle.Free();
                        }

                        int NumXs = mapData.header_Information.MapDataAreaRowSize;  //number of x 117
                        int NumYs = mapData.header_Information.MapDataAreaLineSize;//number of y  90
                        int totalDie = NumXs * NumYs;
                        int RangeSize = totalDie * 6;
                        byte[] bufferTestedDie = new byte[RangeSize];


                        stream.Read(bufferTestedDie, 0, bufferTestedDie.Length);

                        Console.WriteLine($"totalTestedDie={totalDie}");
                        Console.WriteLine($"RangeSize= 6 X totalTestedDie={RangeSize}");


                        GCHandle handleDice = GCHandle.Alloc(bufferTestedDie, GCHandleType.Pinned);


                        try
                        {
                            IntPtr bufferPtr = handleDice.AddrOfPinnedObject();
                            mapData.test_Result_Per_Dice = ReadStructureArray<Test_Result_per_Die>(bufferPtr, totalDie);

                        }
                        finally
                        {
                            handleDice.Free();
                        }
                        //
                    }
                }

            }
        }

 


        /// <summary>
        /// Modify MAP Data, need to call this API to save map data
        /// </summary>
        /// <param name="mapData"></param>
        //internal void SetMapData(MapData mapData)
        //{
        //    this.mapData = mapData;
        //}
        public static int[,] RotateMatrix90Degrees(int[,] asciimap)
        {
            int NumXs = asciimap.GetLength(0);
            int NumYs = asciimap.GetLength(1);
            //int[,] rotated = new int[NumYs, NumXs];
            int[,] rotated = new int[NumYs, NumXs];


            for (int x = 0; x < NumXs; x++)
            {
                for (int y = 0; y < NumYs; y++)
                {
                    rotated[y, NumXs - 1 - x] = asciimap[x, y];// 順時針90
                    // rotated[NumYs - 1 - y, x] = asciimap[x, y]; //逆時針90

                }
            }

            return rotated;
        }
        internal int[,] getAsdciiMap()
        {
            int NumXs = mapData.header_Information.MapDataAreaRowSize;  //number of x 117
            int NumYs = mapData.header_Information.MapDataAreaLineSize;//number of y  90
            int totalDie = NumXs * NumYs;

            int[,] asciimap = new int[NumXs, NumYs];

            var minXaxisDie = mapData.test_Result_Per_Dice.ToList().OrderBy(die => die.Xaxis).First(); //get Xmin

            var minYaxisDie = mapData.test_Result_Per_Dice.ToList().OrderBy(die => die.Yaxis).First();//get Ymin

            for (int i = 0; i < mapData.test_Result_Per_Dice.Length; i++)
            {
                int x = mapData.test_Result_Per_Dice[i].Xaxis - minXaxisDie.Xaxis;
                int y = mapData.test_Result_Per_Dice[i].Yaxis - minYaxisDie.Yaxis;

                //test
                //mapData.test_Result_Per_Dice[i].Bin = 1;
                //asciimap[x, y] = (int)mapData.test_Result_Per_Dice[i].Bin ;
                //test

                if (mapData.test_Result_Per_Dice[i].IsEdge)
                {
                    asciimap[x, y] = 0xED;
                }
                else if (IsWaferCenter(mapData.test_Result_Per_Dice[i]))
                {

                    asciimap[x, y] = 0xCC;
                }
                else
                {
                    asciimap[x, y] = (int)mapData.test_Result_Per_Dice[i].Bin;
                }

            }


            return asciimap;
        }


        public void WriteFile_ascii_map(string filePath, int[,] asciimap)
        {
            int NumXs = asciimap.GetLength(0);  //number of x 117
            int NumYs = asciimap.GetLength(1);//number of y  90
            

            StringBuilder sb = new StringBuilder();
            for (int y = NumYs-1; y >= 0; y--)
            {
                for (int x = NumXs-1; x >=0; x--)
                {
                    if (asciimap[x, y] == 0)
                    {
                        sb.Append("..");
                    }
                    else if (asciimap[x, y] == 0xED)
                    {
                        sb.Append("ED");
                    }
                    else if (asciimap[x, y] == 0xCC)
                    {
                        sb.Append("CC");
                    }
                    else
                    {
                        sb.Append(asciimap[x, y].ToString().PadLeft(2, ' '));
                    }
                    
                }
                sb.AppendLine("");
                //Console.WriteLine();
            }

            File.WriteAllText(filePath, sb.ToString());
          
        }
       
        private bool IsWaferCenter(Test_Result_per_Die test_Result_per_Die)
        {
            int xcenter = mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorXOfWaferCenterDie;
            int ycenter = mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorYOfWaferCenterDie;
            return test_Result_per_Die.Xaxis == xcenter && test_Result_per_Die.Yaxis == ycenter;
        }

        /// <summary>
        /// write test per die information
        /// </summary>
        /// <param name="FilePath"></param>
        public void WriteFile_test_die(string FilePath)
        {


            var minXaxisDie = mapData.test_Result_Per_Dice.ToList().OrderBy(die => die.Xaxis).First();

            var minYaxisDie = mapData.test_Result_Per_Dice.ToList().OrderBy(die => die.Yaxis).First();


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mapData.test_Result_Per_Dice.Length; i++)
            {
                
                int x = mapData.test_Result_Per_Dice[i].Xaxis - minXaxisDie.Xaxis;
                int y = mapData.test_Result_Per_Dice[i].Yaxis - minYaxisDie.Yaxis;
                string msg = $"{mapData.test_Result_Per_Dice[i].ToString()},abs X={x}, abs Y={y}";
                sb.AppendLine(msg);
            }                     

            File.WriteAllText(FilePath, sb.ToString());

        }
        
        /// <summary>
        /// write wafer map header information and bin categroy
        /// </summary>
        /// <param name="filePath"></param>
        public void WriteFile_ascii_information(string filePath)
        {
            try
            {

                StringBuilder sb = new StringBuilder();

                List<Test_Result_per_Die> listdata = mapData.test_Result_Per_Dice.ToList();
                List<Test_Result_per_Die> bin0 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 0; });
                List<Test_Result_per_Die> bin1 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 1; });
                List<Test_Result_per_Die> bin4 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 4; });
                List<Test_Result_per_Die> bin3 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 3; });
                List<Test_Result_per_Die> bin16 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 16; });
                List<Test_Result_per_Die> bin06 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 6; });
                List<Test_Result_per_Die> bin17 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 17; });
                List<Test_Result_per_Die> bin08 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 8; });
                List<Test_Result_per_Die> bin5 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 5; });
                List<Test_Result_per_Die> bin14 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 14; });
                List<Test_Result_per_Die> bin18 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 18; });
                List<Test_Result_per_Die> bin11 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 11; });
                List<Test_Result_per_Die> bin13 = listdata.FindAll((item) => { return (item.Word3 & 0x3F) == 13; });


                List<Test_Result_per_Die> binnull = listdata.FindAll((item) => {
                    return (item.Word1 >> 14 & 0x3) == 0 && (item.Word2 >> 14 & 0x3) == 0;
                });


                List<Test_Result_per_Die> binnuED = listdata.FindAll((item) => {
                    return item.IsEdge;
                });


                sb.AppendLine($"bin0  count={bin0.Count}");
                sb.AppendLine($"binnuED  count={binnuED.Count}");
                sb.AppendLine($"binnull  count={binnull.Count}");
                sb.AppendLine($"bin1  count={bin1.Count}");
                sb.AppendLine($"bin4  count={bin4.Count}");
                sb.AppendLine($"bin3  count={bin3.Count}");
                sb.AppendLine($"bin16 count={bin16.Count}");
                sb.AppendLine($"bin06 count={bin06.Count}");
                sb.AppendLine($"bin17 count={bin17.Count}");
                sb.AppendLine($"bin08 count={bin08.Count}");
                sb.AppendLine($"bin5  count={bin5.Count}");
                sb.AppendLine($"bin14 count={bin14.Count}");
                sb.AppendLine($"bin18 count={bin18.Count}");
                sb.AppendLine($"bin11 count={bin11.Count}");
                sb.AppendLine($"bin13 count={bin13.Count}");

                sb.AppendLine("Header Information--------------");
                sb.AppendLine(mapData.header_Information.ToString());
                File.WriteAllText(filePath,sb.ToString());


            }
            catch(Exception ex)
            {
                Console.WriteLine($"WriteAsciiFile failed={ex}");
            }
        }
        public void WriteBinary(string filePath)
        {
            File.WriteAllBytes(filePath, mapData.ToBinary());
        }
        public ref MapData GetMapData()
        {
            return ref mapData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dieWidth">um</param>
        /// <param name="dieHeight">um</param>
        /// <param name="waferDiameter">8 inch:200,  12 inch:300</param>
        /// <param name="left_top_X">left top axis X</param>
        /// <param name="left_top_Y">left top axis Y</param>
        /// <returns></returns>
        public static WaferMapAgtent CreateWaferMap(uint dieWidth, uint dieHeight, uint waferDiameter, int left_top_x, int left_top_y)
        {

            double ddieWidth = (double)dieWidth / 1000.0;
            double ddieHeight = (double)dieHeight / 1000.0;


            int row = Convert.ToInt32(Math.Floor((double)waferDiameter / ddieWidth));

            int col = Convert.ToInt32(Math.Floor((double)waferDiameter / ddieHeight));




            WaferMapAgtent waferMapAgtent = new WaferMapAgtent();

            waferMapAgtent.Init(left_top_x, left_top_y, waferDiameter, row, col, ddieWidth, ddieHeight);

           // waferMapAgtent.ReadFile("./sample/IGT_Sample.DAT");

            //waferMapAgtent.mapData.test_Result_Per_Dice = new Test_Result_per_Die[Row*Line];
            return waferMapAgtent;
        }

        private void Init(int left_top_x, int left_top_y, uint WaferDiameter, int row, int line, double dieWitdh, double dieheight)
        {
            
            mapData.header_Information = new Header_Information();
            mapData.header_Information.init((ushort)row, (ushort)line);



            mapData.test_Result_Per_Dice = new Test_Result_per_Die[row * line];

            //Axis X=35,Axis Y=86,Bin=0,abs X=116, abs Y=89
            //x 117 
            //y 90             

            uint waferDiameter = WaferDiameter- edge_rollback;                 

            int axisX = left_top_x;
            int axisY = left_top_y;

            var waferC =  WaferMapUtility.CalculateWaferCenter(axisX, axisY, row, line);
            Console.WriteLine($"waferCX={waferC.centerX},waferCY={waferC.centerY}");

            mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorXOfWaferCenterDie = waferC.centerX;
            mapData.header_Information.waferProbingCoordinateSystemData.CoordinatorYOfWaferCenterDie = waferC.centerY;


            for (int i = 0; i < mapData.test_Result_Per_Dice.Length; i++)
            {
                mapData.test_Result_Per_Dice[i].init();

                mapData.test_Result_Per_Dice[i].Xaxis = axisX;
                mapData.test_Result_Per_Dice[i].Yaxis = axisY;

                //mapData.test_Result_Per_Dice[i].DieTestResult
                
                var diepos = WaferMapUtility.GetDiePosition(
                    axisX, axisY, waferC.centerX, waferC.centerY, waferDiameter, dieWitdh, dieheight, (int)edge_thickness);


                if (diepos == WaferMapUtility.DiePosition.Edge )
                {
                    mapData.test_Result_Per_Dice[i].DieProperty = 2;                                              
                }
                else if(diepos == WaferMapUtility.DiePosition.InsideEdge)
                {
                    mapData.test_Result_Per_Dice[i].DieProperty = 1; //edge
                }
                else 
                {                    
                    mapData.test_Result_Per_Dice[i].DieProperty = 0; //不會參與PROBING
                    
                }
                //X-------------->
                //35 34 33 ...............0 -------------- -5 -6 
                axisX--;
                if ((i+1) %row == 0)
                {
                    //next row
                    axisX = left_top_x; 
                    axisY--;
                }                               
                
            }



        }

        private static T[] ReadStructureArray<T>(IntPtr ptr, int count) where T : struct
        {
            T[] array = new T[count];
            int size = Marshal.SizeOf(typeof(T));

            for (int i = 0; i < count; i++)
            {
                IntPtr structPtr = IntPtr.Add(ptr, i * size);
                array[i] = (T)Marshal.PtrToStructure(structPtr, typeof(T));
            }

            return array;
        }
    }
}
