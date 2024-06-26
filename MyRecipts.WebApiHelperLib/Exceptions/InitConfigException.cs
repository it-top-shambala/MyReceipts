using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipts.WebApiHelperLib.Exceptions;

public class InitConfigException : Exception
{
    public InitConfigException(string message) : base(message){}
}