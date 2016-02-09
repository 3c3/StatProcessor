using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public class Beach
    {
        public static List<Beach> beaches = new List<Beach>();

        public string name;
        public string location;

        public int nCar, nCity, nFoot;
        public int[] facilties = new int[4];
        List<int> density = new List<int>();
        List<int> polution = new List<int>();
        List<int> grade = new List<int>();

        public Beach(BeachOpinion bo)
        {
            name = bo.name;
            location = bo.location;
            AddOpinion(bo);
        }

        public void AddOpinion(BeachOpinion bo)
        {
            if (bo.car) nCar++;
            if (bo.city) nCity++;
            if (bo.foot) nFoot++;
            facilties[bo.facilities]++;
            density.Add(bo.density);
            polution.Add(bo.polution);
            grade.Add(bo.grade);
        }
    }
}
