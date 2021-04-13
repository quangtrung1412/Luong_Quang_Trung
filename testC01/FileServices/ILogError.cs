using System;
using System.Collections.Generic;
using System.Text;

namespace testC01.FileServices
{
    public interface ILogError
    {
        void LogErrorException<T>(Exception e, string param, T value);
    }
}
