using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployManager.Exceptions;
public class DeployException : Exception
{
    public DeployException(string message) : base(message) { }
}
