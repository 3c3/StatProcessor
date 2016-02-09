using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Processor
{
    public class IOHandler
    {
        private string rawDataFile = "raw.txt";
        private string beachFile = "beach.txt";
        private string personFile = "person.txt";

        public void WriteRawData()
        {
            using (StreamWriter writer = File.AppendText(rawDataFile))
            {
                foreach(Opinion o in Opinion.allOpinions)
                {
                    WritePerson(o, writer);
                    o.opinions.ForEach(b => WriteBeachOpinion(b, writer));
                }
            }
        }

        public void WriteBeachData()
        {
            using(StreamWriter writer = File.AppendText(beachFile))
            {
                Beach.beaches.ForEach(b => WriteBeach(b, writer));
            }
        }

        public void WritePersonData()
        {
            using(StreamWriter writer = File.AppendText(personFile))
            {
                Opinion.allOpinions.ForEach(o => WritePerson(o, writer));
            }
        }

        public void WriteOpinion(Opinion op, StreamWriter writer)
        {
            WritePerson(op, writer);
            op.opinions.ForEach(b => WriteBeachOpinion(b, writer));
        }

        /* Формат за мнение за плаж:
         * [Име]
         * [Място]
         * [Кола] [Градски транспорт] [Пеша]
         * Заведения: [код]
         * [гъстота] [замърсеност] [оценка]
         */

        private void WriteBeachOpinion(BeachOpinion b, StreamWriter writer)
        {
            String data = String.Format("{0}\n{1}\n{2} {3} {4}\n{5}\n{6} {7} {8}\n",
                b.name,
                b.location,
                b.car, b.city, b.foot,
                b.facilities,
                b.density, b.polution, b.grade);

            writer.Write(data);
        }


        /* Формат за плаж:
         * [Име]
         * [Място]
         * [Кола] [Градски транспорт] [Пеша]
         * Заведения: [няма] [до 2] [3-5] [над 5]
         * [гъстота] [замърсеност] [оценка] [бр хора]
         */
        private void WriteBeach(Beach b, StreamWriter writer)
        {
            String data = String.Format("{0}\n{1}\n{2} {3} {4}\n{5} {6} {7} {8}\n{9} {10} {11} {12}\n",
                b.name,
                b.location,
                b.nCar, b.nCity, b.nFoot,
                b.facilties[0], b.facilties[1], b.facilties[2], b.facilties[3],
                b.Density, b.Polution, b.Grade, b.People);

            writer.Write(data);
        }

        /* Формат:
         * [възраст] [честота] [честота - код: 0 - седмица, 1 - месец, 2 - година]
         */
        private void WritePerson(Opinion po, StreamWriter writer)
        {
            String data = String.Format("{0} {1} {2}\n", po.age, po.freq.freq, po.freq.type);
            writer.Write(data);
        }
    }
}
