using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class WaferMapUtility
    {
        public enum DiePosition
        {
            Edge,       // 在邊緣
            InsideEdge, // 在邊緣內
            OutsideEdge // 在邊緣外
        }

        public static T ConvertToBigEndian<T>(byte[] data) where T : struct
        {
            // 创建指定类型的实例
            T result;

            // 将字节数组反转为大端格式
            Array.Reverse(data);

            // 分配内存并复制字节到新结构
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        //public static byte[] ConvertToBigEndian(byte[] data)
        //{
        //    // 
        //    if (BitConverter.IsLittleEndian)
        //    {
        //        Array.Reverse(data);
        //    }
        //    return data;
        //}

        public static byte[] ReverseArray(byte[] original)
        {
            byte[] reversed = new byte[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                reversed[i] = original[original.Length - 1 - i];
            }
            return reversed;
        }

        public static uint GetBit(uint number, int position)
        {
            // 移位並使用位元 AND 運算取得指定位元的值
            return (uint)(number >> position) & 1;
        }

        public static uint GetBits(uint number, int start, int end)
        {
            int bandwidth = end - start + 1;

            uint mask = (uint)(1 << bandwidth) - 1;

            return (number >> start) & mask;
                                    
        }

  

        public static uint SetBit(uint number, int position, uint value)
        {
            // 清除指定位元的值
            number &=(uint)~(1 << position);

            // 設定新的值
            number |= (value & 1) << position;

            return number;
        }
        public static uint SetBits(uint number, int start, int end, uint value)
        {
            int bandwidth = end - start + 1;

            uint mask = (uint)(1 << bandwidth) - 1;

            //clear number value to zero
            number &= ~(mask << start);

            number |= (value & mask) << start;

            return number;
        }

        public static uint SetBits(uint number, uint value, uint mask=0xFFFFFFFF)
        {                                
            number &= ~mask;

            number |= (value & mask);

            return number;
        }

        public static (int centerX, int centerY) CalculateWaferCenter(double topLeftX, double topLeftY, int rows, int columns, double dieWidth, double dieHeight)
        {
            // 计算晶圆的实际总宽度和总高度
            double totalWidth = columns * dieWidth;
            double totalHeight = rows * dieHeight;

            // 计算圆心坐标
            double centerX = topLeftX - (totalWidth / 2);
            double centerY = topLeftY - (totalHeight / 2);

            return ((int)centerX, (int)centerY);
        }



        public static (int centerX, int centerY) CalculateWaferCenter(double topLeftX, double topLeftY, int rows, int columns)
        {
            // 计算晶圆的总宽度和总高度
            double totalWidth = rows;
            double totalHeight = columns;

            // 计算圆心坐标
            double centerX = topLeftX - (totalWidth / 2);
            double centerY = topLeftY - (totalHeight / 2);

            return ((int)centerX, (int)centerY);
        }

        //public static bool IsEdgeDie(int dieX, int dieY, int centerX, int centerY, double waferDiameter, double dieWidth, double dieHeight)
        //{
            
        //    // 计算晶圆半径
        //    double waferRadius = waferDiameter / 2.0;

        //    // 设定边缘判断的阈值，例如取 die 宽高中的较小值作为近似边缘厚度
        //    double edgeThreshold = Math.Min(dieWidth, dieHeight) / 2.0;

        //    // 计算 Die 中心的 X 和 Y 坐标
        //    double dieCenterX = dieX + dieWidth / 2.0;
        //    double dieCenterY = dieY + dieHeight / 2.0;

        //    // 计算 Die 中心到圆心的距离
        //    double distanceToCenter = Math.Sqrt(Math.Pow(dieCenterX - centerX, 2) + Math.Pow(dieCenterY - centerY, 2));

        //    // 判断是否为边缘 Die
        //    //Console.WriteLine($"distanceToCenter={distanceToCenter},edgeThreshold range={waferRadius - edgeThreshold}~{waferRadius + edgeThreshold}");
        //    return distanceToCenter >= (waferRadius - edgeThreshold) && distanceToCenter <= (waferRadius + edgeThreshold);
        //}
        public static bool IsEdgeDie(int row, int col, int centerX, int centerY, double waferDiameter, double dieWidth, double dieHeight, uint edgeThreshold)
        {
            // 计算晶圆半径
            double waferRadius = waferDiameter / 2.0;
                        

            // 将行列坐标转换为实际的物理 X, Y 坐标（Die 中心坐标）
            //double dieCenterX = topLeftX - row * dieWidth - dieWidth / 2.0;
            //double dieCenterY = topLeftY - col * dieHeight - dieHeight / 2.0;

            double dieCenterX = ( row) * dieWidth;
            double dieCenterY = ( col) * dieHeight;

            // 计算 Die 中心到晶圆圆心的距离
            double distanceToCenter = Math.Sqrt(Math.Pow(dieCenterX - centerX* dieWidth, 2) + Math.Pow(dieCenterY - centerY* dieHeight, 2));

            // 判断是否为边缘 Die
            return distanceToCenter >= (waferRadius - edgeThreshold) && distanceToCenter <= (waferRadius + edgeThreshold);
        }
        public static DiePosition GetDiePosition(
         int row,
         int col,
         int centerX,
         int centerY,
         double waferDiameter,
         double dieWidth,
         double dieHeight,
         int edgeThreshold)
        {
            // 計算晶圓半徑
            double waferRadius = waferDiameter / 2.0;

            // 將行列坐標轉換為實際的物理 X, Y 坐標（Die 中心坐標）
            double dieCenterX = row * dieWidth;
            double dieCenterY = col * dieHeight;

            // 計算 Die 中心到晶圓圓心的距離
            double distanceToCenter = Math.Sqrt(
                Math.Pow(dieCenterX - centerX * dieWidth, 2) +
                Math.Pow(dieCenterY - centerY * dieHeight, 2)
            );

            // 判斷 Die 的位置狀態
            if (distanceToCenter >= (waferRadius - edgeThreshold) && distanceToCenter <= (waferRadius + edgeThreshold))
            {
                return DiePosition.Edge; // 在邊緣
            }
            else if (distanceToCenter < (waferRadius - edgeThreshold))
            {

                return DiePosition.InsideEdge; // 在邊緣內
            }
            else
            {
                return DiePosition.OutsideEdge; // 在邊緣外
            }
        }
        public static List<MapDie> FindEdgeDies(int waferCenterX, int waferCenterY, int waferRadius, int dieWidth, int dieHeight, int edgeThickness)
        {
            List<MapDie> edgeDies = new List<MapDie>();

            int dieRadius = Math.Min(dieWidth, dieHeight) / 2;

            // 遍历晶圆区域的 Die（从圆心向四周铺设 Die）
            for (int x = -waferRadius; x <= waferRadius; x += dieWidth)
            {
                for (int y = -waferRadius; y <= waferRadius; y += dieHeight)
                {
                    // 计算 Die 的整数中心位置
                    int dieCenterX = waferCenterX + x;
                    int dieCenterY = waferCenterY + y;

                    // 计算 Die 中心到晶圆中心的距离
                    double distanceToCenter = Math.Sqrt(Math.Pow(dieCenterX - waferCenterX, 2) + Math.Pow(dieCenterY - waferCenterY, 2));

                    // 判断 Die 是否在晶圆范围内
                    if (distanceToCenter <= waferRadius)
                    {
                        // 判断是否为 Edge Die，使用扩展的边缘厚度
                        bool isEdge = (distanceToCenter + dieRadius > waferRadius - edgeThickness);

                        // 将 Die 添加到列表中
                        edgeDies.Add(new MapDie(dieCenterX, dieCenterY, isEdge));
                    }
                }
            }

            return edgeDies;
        }
    }
}
