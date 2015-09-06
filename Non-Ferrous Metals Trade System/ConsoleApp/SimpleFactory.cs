using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //简单工厂
    public class ComputerFactory : DigitFactory
    {
        public static Computer Create(int i)
        {
            Computer computer = null;

            switch (i)
            {
                case 1:
                    computer = new LenovoComputer();
                    break;
                case 2:
                    computer = new DIYComputer();
                    break;
                default:
                    computer = new DIYComputer();
                    break;
            }

            return computer;
        }

        public override IDigit CreateDigit(int i)
        {
            return ComputerFactory.Create(i);
        }
    }

    public class PhoneFactory : DigitFactory
    {
        public static Phone Create(int i)
        {
            Phone phone = null;

            switch (i)
            {
                case 11:
                    phone = new Mi();
                    break;
                case 12:
                    phone = new Samsung();
                    break;
                default:
                    phone = new Mi();
                    break;
            }

            return phone;
        }

        public override IDigit CreateDigit(int i)
        {
            return PhoneFactory.Create(i);
        }
    }
}
