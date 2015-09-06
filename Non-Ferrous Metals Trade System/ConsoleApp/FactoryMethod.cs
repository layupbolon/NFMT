using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //工厂方法
    //工厂方法模式的实质是“定义一个创建对象的接口，但让实现这个接口的类来决定实例化哪个类。工厂方法让类的实例化推迟到子类中进行。”
    public abstract class Computer : IDigit
    {
        public abstract string ComputerName { get; }

        public abstract string View { get; }

        public abstract string KeyBoard { get; }

        public abstract string Mouse { get; }

        public abstract string Box { get; }

        public string ConfigurationWrite()
        {
            string config = string.Format("我是：{0},显示器：{1},键盘：{2}，鼠标：{3},机箱：{4}", ComputerName, View, KeyBoard, Mouse, Box);
            return config;
        }
    }

    public class LenovoComputer : Computer
    {
        public override string ComputerName
        {
            get { return "联想电脑"; }
        }

        public override string View
        {
            get { return "ThinkVision"; }
        }

        public override string KeyBoard
        {
            get { return "Lenovo"; }
        }

        public override string Mouse
        {
            get { return "Lenovo"; }
        }

        public override string Box
        {
            get { return "Lenovo"; }
        }
    }

    public class DIYComputer : Computer
    {
        public override string ComputerName
        {
            get { return "组装电脑"; }
        }

        public override string View
        {
            get { return "Benq"; }
        }

        public override string KeyBoard
        {
            get { return "Logitech"; }
        }

        public override string Mouse
        {
            get { return "Logitech"; }
        }

        public override string Box
        {
            get { return "DIY"; }
        }
    }


    public abstract class Phone : IDigit
    {
        public abstract string Memory { get; }

        public abstract string Cpu { get; }

        public abstract string Price { get; }

        public abstract string PhoneName { get; }

        public string ConfigurationWrite()
        {
            string config = string.Format("我是：{0},内存：{1},处理器：{2},价格：{3}", PhoneName, Memory, Cpu, Price);
            return config;
        }
    }

    public class Mi : Phone
    {
        public override string Memory
        {
            get { return "64G"; }
        }

        public override string Cpu
        {
            get { return "5G"; }
        }

        public override string Price
        {
            get { return "2000人民币"; }
        }

        public override string PhoneName
        {
            get { return "小米"; }
        }
    }

    public class Samsung : Phone
    {
        public override string Memory
        {
            get { return "32G"; }
        }

        public override string Cpu
        {
            get { return "2.3G"; }
        }

        public override string Price
        {
            get { return "3200人民币"; }
        }

        public override string PhoneName
        {
            get { return "三星"; }
        }
    }
}
