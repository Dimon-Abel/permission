using SeedDataInitialize.Logisc;
using System;

namespace SeedDataInitialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("初始化种子数据 - 开始");
            InitializeSystem();
            Console.WriteLine("初始化种子数据 - 结束");
            Console.ReadKey();
        }

        #region 初始化系统种子数据

        /// <summary>
        /// 初始化系统种子数据
        /// </summary>
        /// <param name="services"></param>
        static void InitializeSystem()
        {
            SyncLogisc logisc = new SyncLogisc();
            logisc.Write();
        }

        #endregion
    }
}
