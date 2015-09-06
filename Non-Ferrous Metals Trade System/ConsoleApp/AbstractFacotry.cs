using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IDigit
    {
        string ConfigurationWrite();
    }

    public abstract class DigitFactory
    {
        public abstract IDigit CreateDigit(int i);
    }

    public abstract class DigitFactory1
    {
        public abstract IDigit CreateDigit();
    }

    public class LenoveComputerFactory : DigitFactory1
    {
        public override IDigit CreateDigit()
        {
            return new LenovoComputer();
        }
    }

    public class DIYComputerFactory : DigitFactory1
    {
        public override IDigit CreateDigit()
        {
            return new DIYComputer();
        }
    }

    public class MiPhoneFactory : DigitFactory1
    {
        public override IDigit CreateDigit()
        {
            return new Mi();
        }
    }

    public class SamsangPhoneFactory : DigitFactory1
    {
        public override IDigit CreateDigit()
        {
            return new Samsung();
        }
    }

}
