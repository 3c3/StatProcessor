using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Processor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Beach.onBeachAdded += Beach_onBeachAdded;
        }

        void Beach_onBeachAdded(string name)
        {
            cbBeachName.Items.Add(name);
        }

        BeachOpinion activeBO = new BeachOpinion();
        Opinion activeOp = new Opinion();

        private void ClearBeach()
        {
            cbBeachName.Text = "<няма>";
            tbLocation.Text = "<няма>";
            cbCar.Checked = false;
            cbBus.Checked = false;
            cbFoot.Checked = false;
        }

        private void ReadBeachOpinion()
        {
            activeBO.name = cbBeachName.Text;
            activeBO.location = tbLocation.Text;
            activeBO.car = cbCar.Checked;
            activeBO.city = cbBus.Checked;
            activeBO.foot = cbFoot.Checked;

            if (rbLittle.Checked) activeBO.facilities = 1;
            else if (rbMed.Checked) activeBO.facilities = 2;
            else if (rbMany.Checked) activeBO.facilities = 3;

            try
            {
                activeBO.density = int.Parse(cbDensity.Text);
                activeBO.polution = int.Parse(cbPolution.Text);
                activeBO.grade = int.Parse(cbGrade.Text);

                activeBO.Register();
                activeOp.opinions.Add(activeBO);
                ClearBeach();
                activeBO = new BeachOpinion();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, e.GetType().ToString());
            }
            
        }

        private void ClearPerson()
        {
            tbAge.Text = "0";
            tbFreq.Text = "0";
        }

        private void ReadPerson()
        {
            try
            {
                activeOp.age = int.Parse(tbAge.Text);

                activeOp.freq = new Frequency();

                activeOp.freq.freq = int.Parse(tbFreq.Text);
                if (rbWeekly.Checked) activeOp.freq.type = FType.Weekly;
                else if (rbMonthly.Checked) activeOp.freq.type = FType.Monthly;
                else if (rbYearly.Checked) activeOp.freq.type = FType.Yearly;

                Opinion.allOpinions.Add(activeOp);
                activeOp = new Opinion();
                ClearPerson();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.GetType().ToString());
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void beachBtn_Click(object sender, EventArgs e)
        {
            ReadBeachOpinion();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            IOHandler.DeleteFiles();
            IOHandler.WriteBeachData();
            IOHandler.WritePersonData();
            IOHandler.WriteRawData();
        }

        private void personBtn_Click(object sender, EventArgs e)
        {
            ReadPerson();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IOHandler.ReadAllBeaches();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelWriter.WriteBeaches();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IOHandler.ReadAllPeople();
            ExcelWriter.WritePeople();
        }
    }
}
