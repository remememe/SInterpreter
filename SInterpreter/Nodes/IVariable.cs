using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SInterpreter.Nodes
{
    public interface IVariable<T>
    {
        void AssignValue(T value);
        string GetName();
    }
}
