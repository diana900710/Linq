using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第二題
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random roll = new Random();
            var ans = new int[4];  //正確答案陣列
            var nums = new int[4]; //猜題陣列
            int Asum, Bsum; //總數
            bool end = false;
            do //判斷是否結束遊戲
            {
                ans[0] = roll.Next(0, 10); //先抽出第一個數字
                for (int i = 1; i < 4; i++) //抽剩下的三個數字
                {
                    ans[i] = roll.Next(0, 10); //出數字
                    for (int j = 0; j < i; j++) //0<=j<i，"判斷數字是否相同"的範圍
                    {
                        //判斷是否有數字相同，若有重抽
                        while (ans[i] == ans[j])
                        {
                            ans[i] = roll.Next(0, 10);
                            j = 0;
                        }
                    }
                }
                foreach (int n in ans) { Console.Write("{0}", n); }

                do //判斷猜的數字和位置是否都正確
                {
                    Asum = 0; Bsum = 0; //先設置初始值
                    Console.Write("請輸入4個數字：");
                    string number = Convert.ToString(Console.ReadLine());
                    //將使用者猜的數字分隔到nums陣列裡
                    for (int i = 0; i < 4; i++) { nums[i] = Convert.ToInt32(number.Substring(i, 1)); }

                    //判斷1A2B的雙迴圈
                    for (int i = 0; i < 4; i++)
                    {
                        if (ans[i] == nums[i]) { Asum++; }
                    }
                    Bsum = ans.Intersect(nums).Count() - Asum;
                    
                    Console.WriteLine($"判定結果是{Asum}A{Bsum}B");
                } while (Asum != 4);
                Console.WriteLine("恭喜你！猜對了！");
                Console.Write("你要繼續玩嗎？(y/n)：");
                string yesorno = Console.ReadLine();
                if (yesorno == "n") { end = true; }
            } while (end != true);
        }
    }
}
