using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public class Opinion
    {
        public static List<Opinion> allOpinions = new List<Opinion>();

        public Frequency freq;
        public int age;
        public List<BeachOpinion> opinions = new List<BeachOpinion>();
    }
}
