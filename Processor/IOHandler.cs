using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Processor
{
    public class IOHandler
    {
        private static string rawDataFile = "raw.txt";
        private static string beachFile = "beach.txt";
        private static string personFile = "person.txt";

        public static void DeleteFiles()
        {
            File.Delete(rawDataFile);
            File.Delete(beachFile);
            File.Delete(personFile);
        }

        public static void WriteRawData()
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

        public static void WriteBeachData()
        {
            using(StreamWriter writer = File.AppendText(beachFile))
            {
                Beach.beaches.ForEach(b => WriteBeach(b, writer));
            }
        }

        public static void WritePersonData()
        {
            using(StreamWriter writer = File.AppendText(personFile))
            {
                Opinion.allOpinions.ForEach(o => WritePerson(o, writer));
            }
        }

        public static void WriteOpinion(Opinion op, StreamWriter writer)
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

        private static void WriteBeachOpinion(BeachOpinion b, StreamWriter writer)
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
        private static void WriteBeach(Beach b, StreamWriter writer)
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
        private static void WritePerson(Opinion po, StreamWriter writer)
        {
            String data = String.Format("{0} {1} {2}\n", po.age, po.freq.freq, po.freq.type);
            writer.Write(data);
        }


        /* Формат за плаж:
         * [Име]
         * [Място]
         * [Кола] [Градски транспорт] [Пеша]
         * Заведения: [няма] [до 2] [3-5] [над 5]
         * [гъстота] [замърсеност] [оценка] [бр хора]
         */
        private static void ReadBeach(String[] lines, int offset)
        {
            Beach current = new Beach(lines[offset]);
            current.location = lines[offset + 1];

            // TRANSPORT

            String[] trans = lines[offset + 2].Split(' ');
            try
            {
                current.nCar = int.Parse(trans[0]);
            }
            catch(Exception e)
            {
                MessageBox.Show("IO: " + e.Message, e.GetType().ToString());
                current.nCar = 0;
            }

            try
            {
                current.nCity = int.Parse(trans[1]);
            }
            catch (Exception e)
            {
                MessageBox.Show("IO: " + e.Message, e.GetType().ToString());
                current.nCity = 0;
            }

            try
            {
                current.nFoot = int.Parse(trans[2]);
            }
            catch (Exception e)
            {
                MessageBox.Show("IO: " + e.Message, e.GetType().ToString());
                current.nFoot = 0;
            }



            // FACILITIES

            String[] fac = lines[offset + 3].Split(' ');
            try
            {
                for (int i = 0; i < 4; i++) current.facilties[i] = int.Parse(fac[i]);
            }
            catch (Exception e)
            {
                MessageBox.Show("IO(facilities): " + e.Message, e.GetType().ToString());
            }

            // [гъстота] [замърсеност] [оценка] [бр хора]

            String[] whev = lines[offset + 4].Split(' ');
            try
            {
                current.iDensity = int.Parse(whev[0]);
                current.iPolution = int.Parse(whev[1]);
                current.iGrade = int.Parse(whev[2]);
                current.iCount = int.Parse(whev[3]);
            }
            catch (Exception e)
            {
                MessageBox.Show("IO(last line): " + e.Message, e.GetType().ToString());
            }

            Beach.beaches.Add(current);
        }

        public static void ReadAllBeaches()
        {
            String[] data = File.ReadAllLines(beachFile);

            int idx = 0;
            while(idx + 4 <= data.Length)
            {
                ReadBeach(data, idx);
                idx += 5;
            }
        }

        private static void ReadPerson(String line)
        {
            String[] data = line.Split(' ');
            Opinion o = new Opinion();
            o.age = int.Parse(data[0]);

            o.freq = new Frequency();

            o.freq.freq = int.Parse(data[1]);
            if (data[2][0] == 'W') o.freq.type = FType.Weekly;
            else if (data[2][0] == 'M') o.freq.type = FType.Monthly;
            else if (data[2][0] == 'Y') o.freq.type = FType.Yearly;

            Opinion.allOpinions.Add(o);
        }

        public static void ReadAllPeople()
        {
            String[] data = File.ReadAllLines(personFile);
            foreach (String line in data) ReadPerson(line);
        }
    }
}
