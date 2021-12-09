using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.Transversal.Log
{
    public interface ILoggerManager 
    {

        void LogDebug(string message);

        void LogError(string message);

        void LogInfo(string message);

        void LogWarn(string message);
    }

}
