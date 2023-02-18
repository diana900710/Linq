using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第一題
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var readConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    HasHeaderRecord = true
            //};
            //using (var reader = new StreamReader("product.csv"))
            //using (var csv = new CsvReader(reader, readConfiguration))
            //{
            //    var records = csv.GetRecords<Product>();
            //    foreach (var employee in records)
            //    {
            //        Console.WriteLine(employee.Name + "," + employee.ID);
            //    }
            //}
            var list = CreateList();
            Console.Write("1.計算所有商品的總價格 : ");
            var PriceSum = list.Sum((x) => x.Price);
            Console.WriteLine(PriceSum);
            Console.WriteLine();
            Console.Write("2. 計算所有商品的平均價格 : ");
            var PriceAve = list.Average(x => x.Price);
            Console.WriteLine(PriceAve);
            Console.WriteLine();
            Console.Write("3. 計算商品的總數量 : ");
            var NumberSum = list.Sum((x) => x.Number);
            Console.WriteLine(NumberSum);
            Console.WriteLine();
            Console.Write("4. 計算商品的平均數量 : ");
            var NumberAve = list.Average(x => x.Number);
            Console.WriteLine(NumberAve);
            Console.WriteLine();
            //需要找到商品名稱
            Console.Write("5. 找出哪一項商品最貴 : ");
            var PriceMax = list.Max((x) => x.Price);
            var PriceMaxName = list.Where((x) => x.Price == PriceMax);
            foreach (var item in PriceMaxName) { Console.WriteLine($"{item.Name} -- {PriceMax}"); }
            Console.WriteLine();
            //需要找到商品名稱
            Console.Write("6. 找出哪一項商品最便宜 : ");
            var PriceMin = list.Min((x) => x.Price);
            var PriceMinName = list.Where((x) => x.Price == PriceMin);
            foreach (var item in PriceMinName) { Console.WriteLine($"{item.Name} -- {PriceMin}"); }
            Console.WriteLine();
            Console.Write("7. 計算產品類別為 3C 的商品總價 : ");
            var PriceSum3C = list.Where((x) => x.Category == "3C").Sum((x) => x.Price);
            Console.WriteLine(PriceSum3C);
            Console.WriteLine();
            Console.WriteLine("8. 計算產品類別為飲料及食品的商品總價 : "); //平均?
            var PriceDrink = list.Where((x) => x.Category == "飲料").Sum((x) => x.Price);
            Console.WriteLine($"飲料       的總價格是 {PriceDrink}");
            var PriceFood = list.Where((x) => x.Category == "食品").Sum(x => x.Price);
            Console.WriteLine($"食品       的總價格是 {PriceFood}");
            var PriceDrinkFood = list.Where((x) => x.Category == "飲料" || x.Category == "食品").Sum((x) => x.Price);
            Console.WriteLine($"飲料和食品 的總價格是 {PriceDrinkFood}");
            Console.WriteLine();
            Console.WriteLine("9. 找出所有商品類別為食品，而且商品數量大於 100 的商品 : ");
            var FoodNumberHundred = list.Where((x) => x.Category == "食品" && x.Number > 100);
            foreach (var item in FoodNumberHundred) { Console.WriteLine($"{item.Name} -- {item.Number}"); }
            Console.WriteLine($"\n");
            Console.WriteLine("10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品 : ");
            var PriceThousand =  list.Where((x) => x.Price > 1000).GroupBy((x) => x.Category);
            foreach(var key in PriceThousand)
            {
                Console.WriteLine(key.Key);
                foreach(var item in key) { Console.WriteLine($"  {item.Name} -- {item.Price}"); }
                Console.WriteLine("=-=-=-=-=-=");
            }
            Console.WriteLine();
            Console.WriteLine("11. 呈上題，請計算該類別底下所有商品的平均價格 : ");
            foreach (var item in PriceThousand)
            {
                Console.WriteLine($"{item.Key} -- {item.Average(x => x.Price)}");
            }
            Console.WriteLine();
            Console.WriteLine("12. 依照商品價格由高到低排序 : ");
            var PriceSort1 = list.OrderByDescending((x) => x.Price);
            foreach (var item in PriceSort1) { Console.WriteLine($"{item.Name} -- {item.Price}"); }
            Console.WriteLine();
            Console.WriteLine("13. 依照商品數量由低到高排序 : ");
            var PriceSort2 = list.OrderBy((x) => x.Number);
            foreach (var item in PriceSort2) { Console.WriteLine($"{item.Name} -- {item.Number}"); }
            Console.WriteLine();
            Console.WriteLine("14. 找出各商品類別底下，最貴的商品 : ");
            var CategoryPriceMax = list.OrderByDescending((x) => x.Price).GroupBy((x) => x.Category);
            foreach (var item in CategoryPriceMax)
            {
                Console.Write($"{item.Key} : ");
                foreach(var item2 in item) { Console.WriteLine($"{item2.Name} -- {item2.Price}"); break; }
            }
            Console.WriteLine();
            Console.WriteLine("15. 找出各商品類別底下，最便宜的商品 : ");
            var CategoryPriceMin = list.OrderBy((x) => x.Price).GroupBy((x) => x.Category);
            foreach (var item in CategoryPriceMin)
            {
                Console.Write($"{item.Key} : ");
                foreach (var item2 in item) { Console.WriteLine($"{item2.Name} -- {item2.Price}"); break; }
            }
            Console.WriteLine();
            Console.WriteLine("16. 找出價格小於等於 10000 的商品 : ");
            var PriceLessThousand = list.Where((x) => x.Price < 1000);
            foreach (var item in PriceLessThousand) { Console.WriteLine($"{item.Name} -- {item.Price}"); }
            Console.WriteLine();
            Console.WriteLine("17. 製作一頁 4 筆總共 5 頁的分頁選擇器 : ");
            bool leave = false;
            do
            {
                Console.Write("請輸入 1-5 的分頁數或 n 結束查看 : ");
                string pag = Console.ReadLine();
                switch (pag)
                {
                    case "1":
                        var result1 = list.Take(4);
                        foreach (var item in result1) 
                        { 
                            Console.WriteLine($"{item.ID} -- {item.Name} -- {item.Number} -- {item.Price} -- {item.Category}"); 
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        var result2 = list.Skip(4).Take(4);
                        foreach (var item in result2) 
                        { 
                            Console.WriteLine($"{item.ID} -- {item.Name} -- {item.Number} -- {item.Price} -- {item.Category}"); 
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        var result3 = list.Skip(8).Take(4);
                        foreach (var item in result3) 
                        { 
                            Console.WriteLine($"{item.ID} -- {item.Name} -- {item.Number} -- {item.Price} -- {item.Category}"); 
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        var result4 = list.Skip(12).Take(4);
                        foreach (var item in result4) 
                        { 
                            Console.WriteLine($"{item.ID} -- {item.Name} -- {item.Number} -- {item.Price} -- {item.Category}"); 
                        }
                        Console.WriteLine();
                        break;
                    case "5":
                        var result5 = list.Skip(16).Take(4);
                        foreach (var item in result5) 
                        { 
                            Console.WriteLine($"{item.ID} -- {item.Name} -- {item.Number} -- {item.Price} -- {item.Category}"); 
                        }
                        Console.WriteLine();
                        break;
                    case "n":
                        Console.WriteLine("已結束觀看分頁!!");
                        leave = true; break;
                    default:
                        Console.WriteLine("錯誤"); break;
                }
            } while (leave!=true);

            Console.ReadKey();
        }
        static List<Product> CreateList()
        {
            return new List<Product>
            {
                new Product {ID ="P001", Name = "Iphone 14", Number = 23, Price = 45000, Category = "3C"},
                new Product {ID ="P002", Name = "SamSung A52", Number = 10, Price = 23000, Category = "3C"},
                new Product {ID ="P003", Name = "SamSung S20", Number = 15, Price = 35200, Category = "3C"},
                new Product {ID ="P004", Name = "青森頻果原汁", Number = 100, Price = 370, Category = "飲料"},
                new Product {ID ="P005", Name = "綠茶(瓶裝)", Number = 1000, Price = 25, Category = "飲料"},
                new Product {ID ="P006", Name = "辛拉麵(袋裝)", Number = 1000, Price = 50, Category = "食品"},
                new Product {ID ="P007", Name = "台酒麻油雞泡麵(碗裝)", Number = 5000, Price = 53, Category = "食品"},
                new Product {ID ="P008", Name = "台酒花雕雞泡麵(碗裝)", Number = 10000, Price = 53, Category = "食品"},
                new Product {ID ="P009", Name = "台酒酸菜牛肉泡麵(碗)", Number = 12000, Price = 53, Category = "食品"},
                new Product {ID ="P010", Name = "滿漢大餐珍味牛肉麵(碗裝)", Number = 25080, Price = 53, Category = "食品"},
                new Product {ID ="P011", Name = "烏龍茶(瓶裝)", Number = 10000, Price = 35, Category = "飲料"},
                new Product {ID ="P012", Name = "錫蘭奶茶(瓶裝)", Number = 5000, Price = 20, Category = "飲料"},
                new Product {ID ="P013", Name = "紅茶(瓶裝)", Number = 1230, Price = 25, Category = "飲料"},
                new Product {ID ="P014", Name = "台酒花雕雞泡麵(碗裝)", Number = 10000, Price = 53, Category = "食品"},
                new Product {ID ="P015", Name = "台酒花雕雞泡麵(碗裝)", Number = 10000, Price = 53, Category = "食品"},
                new Product {ID ="P016", Name = "Ipad Pro", Number = 1000, Price = 53420, Category = "3C"},
                new Product {ID ="P017", Name = "筆記型電腦", Number = 1235, Price = 33023, Category = "3C"},
            };
        }
    }
}
