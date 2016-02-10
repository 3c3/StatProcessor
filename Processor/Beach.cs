using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{

    public delegate void BeachAdded(String name);

    public class Beach
    {
        public static List<Beach> beaches = new List<Beach>();
        public static event BeachAdded onBeachAdded;

        public string name;
        public string location;

        public int nCar, nCity, nFoot;
        public int[] facilties = new int[4];
        List<int> density = new List<int>();
        List<int> polution = new List<int>();
        List<int> grade = new List<int>();

        public int iDensity;
        public int iCount;
        public int iPolution;
        public int iGrade;

        public int Density
        {
            get 
            {
                int sum = 0;
                density.ForEach(i => sum += i);
                sum += iDensity * iCount;
                return sum / (density.Count + iCount);
            }
        }

        public int Polution
        {
            get
            {
                int sum = 0;
                polution.ForEach(i => sum += i);
                sum += iPolution * iCount;
                return sum / (density.Count + iCount);
            }
        }

        public int Grade
        {
            get
            {
                int sum = 0;
                grade.ForEach(i => sum += i);
                sum += iGrade * iCount;
                return sum / (density.Count + iCount);
            }
        }

        

        public int People
        {
            get { return grade.Count + iCount; }
        }

        public Beach(string name)
        {
            this.name = name;
            if (onBeachAdded != null) onBeachAdded(name);
        }

        public Beach(BeachOpinion bo)
        {
            name = bo.name;
            location = bo.location;
            AddOpinion(bo);
            if (onBeachAdded != null) onBeachAdded(name);
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
