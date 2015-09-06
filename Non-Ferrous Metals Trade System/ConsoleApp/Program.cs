using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Hashtable hs = new Hashtable();
            //hs.Add("1", "1");
            //hs.Add("2", "2");

            //foreach (var h in hs)
            //{
            //    Console.WriteLine(h);
            //}

            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("1", "1");
            //dic.Add("2", "2");

            //foreach (var di in dic)
            //{
            //    Console.WriteLine(di);
            //}
            int menuId = 1;

            Console.WriteLine(((NFMT.User.MenuEnum)menuId).ToString());

            Console.ReadLine();

            /***********
            //工厂方法
            Computer computer1 = new LenovoComputer();
            Computer computer2 = new DIYComputer();

            Console.WriteLine(computer1.ConfigurationWrite());
            Console.WriteLine(computer2.ConfigurationWrite());

            Console.ReadLine();


            //简单工厂
            Computer computer3 = ComputerFactory.Create(1);
            Computer computer4 = ComputerFactory.Create(2);

            Console.WriteLine(computer3.ConfigurationWrite());
            Console.WriteLine(computer4.ConfigurationWrite());

            Console.ReadLine();

            Phone phone1 = PhoneFactory.Create(11);
            Phone phone2 = PhoneFactory.Create(12);

            Console.WriteLine(phone1.ConfigurationWrite());
            Console.WriteLine(phone2.ConfigurationWrite());

            Console.ReadLine();

            //抽象工厂1
            DigitFactory digitFactory1 = new ComputerFactory();
            IDigit digit1 = digitFactory1.CreateDigit(1);
            IDigit digit2 = digitFactory1.CreateDigit(2);

            Console.WriteLine(digit1.ConfigurationWrite());
            Console.WriteLine(digit2.ConfigurationWrite());

            DigitFactory digitFactory2 = new PhoneFactory();
            IDigit digit3 = digitFactory2.CreateDigit(11);
            IDigit digit4 = digitFactory2.CreateDigit(12);

            Console.WriteLine(digit3.ConfigurationWrite());
            Console.WriteLine(digit4.ConfigurationWrite());

            //抽象工厂2           
            while (Console.ReadLine() != "exit")
            {
                Random dom = new Random();
                int key = dom.Next(1, 5);

                DigitFactory1 factory1 = null;
                switch (key)
                {
                    case 1:
                        factory1 = new LenoveComputerFactory();
                        break;
                    case 2:
                        factory1 = new DIYComputerFactory();
                        break;
                    case 3:
                        factory1 = new MiPhoneFactory();
                        break;
                    case 4:
                        factory1 = new SamsangPhoneFactory();
                        break;
                    default:
                        factory1 = new LenoveComputerFactory();
                        break;
                }

                IDigit digit5 = factory1.CreateDigit();
                Console.WriteLine(digit5.ConfigurationWrite());
            }

            Console.WriteLine("谢谢聆听！！！");
            Console.ReadLine();
             * 
             * ***********/
        }
    }
}
