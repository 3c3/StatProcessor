using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public enum FType { Weekly, Monthly, Yearly}

    public class Frequency
    {
        public FType type;
        public int freq;
    }
}
