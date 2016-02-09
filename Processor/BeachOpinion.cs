using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public class BeachOpinion
    {
        public string name, location;
        public bool car, city, foot;
        public int facilities; // 0 - няма; 1 - до 2; 2 - 3-5; 3 - над 5
        public int density, polution, grade;

        public void Register()
        {
            Beach current = null;
            foreach(Beach b in Beach.beaches) if(b.name == name)
                {
                    current = b;
                    break;
                }

            if (current != null) current.AddOpinion(this);
            else Beach.beaches.Add(new Beach(this));
        }
    }
}
