using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;


namespace Processor
{
    public class ExcelWriter
    {
        private static String f(string a, int i)
        {
            return a + i.ToString();
        }

        public static void WriteBeaches()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Плажове");

            for(int i = 0; i < Beach.beaches.Count; i++)
            {
                Beach current = Beach.beaches[i];
                int rowIdx = i + 2;
                worksheet.Cell(f("A", rowIdx)).Value = current.name;
                worksheet.Cell(f("B", rowIdx)).Value = current.location;
                worksheet.Cell(f("C", rowIdx)).Value = current.nCar;
                worksheet.Cell(f("D", rowIdx)).Value = current.nCity;
                worksheet.Cell(f("E", rowIdx)).Value = current.nFoot;

                worksheet.Cell(f("F", rowIdx)).Value = current.facilties[0];
                worksheet.Cell(f("G", rowIdx)).Value = current.facilties[1];
                worksheet.Cell(f("H", rowIdx)).Value = current.facilties[2];
                worksheet.Cell(f("I", rowIdx)).Value = current.facilties[3];

                worksheet.Cell(f("J", rowIdx)).Value = current.Density;
                worksheet.Cell(f("K", rowIdx)).Value = current.Polution;
                worksheet.Cell(f("L", rowIdx)).Value = current.Grade;
                worksheet.Cell(f("M", rowIdx)).Value = current.People;
            }

            workbook.SaveAs("ало.xlsx");
        }

        private static String[] lookup = { "седмица", "месец", "година" };

        public static void WritePeople()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Хора");

            for (int i = 0; i < Opinion.allOpinions.Count; i++)
            {
                Opinion o = Opinion.allOpinions[i];
                int rowIdx = i + 2;
                worksheet.Cell(f("A", rowIdx)).Value = o.age;
                worksheet.Cell(f("B", rowIdx)).Value = o.freq.freq;
                worksheet.Cell(f("C", rowIdx)).Value = lookup[(int)o.freq.type];
            }

            workbook.SaveAs("ало-ало.xlsx");
        }
    }
}
