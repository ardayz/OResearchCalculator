using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OResearchCalculator
{
    public partial class QueCalculator : Form
    {
        public class CalculatorBaseClass 
        {
           
            public float lambda { get; set; }
            public float Mu { get; set; }
            public float Ls { get; set; }
            public float Lq { get; set; }
            public float Ws { get; set; }
            public float Wq { get; set; }
            public float CCount { get; set; }
            public float NCount { get; set; }
           

            public float Cb { get; set; }
            public float lambdaeff { get; set; }
            public float lambdalost { get; set; }
            public class p 
            {
                
                public int index { get; set; }
                public float value { get; set; }


            }
            public void createpo()
            {
                p newp = new p();
                newp.index = 0;
                Plist.Add(newp);
            }
            public  float Pt { get; set; }
            public  List<p> Plist = new List<p>();
            public  void Pcal(int index, float lambda,float ccount, float Mu,int type)
            {
                if (type == 0)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)(Math.Pow(lambda, index) / Math.Pow(Mu, index) * ((Mu - lambda) / Mu));
                }
                else if (type == 1)
                {
                    p newp = new p();
                    newp.index = index;
                    
                    Plist.Add(newp);
                    Plist[index].value = (float)(Math.Pow((lambda/Mu), index) * Plist[0].value);
                }
                else if (type == 2)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)((1.0 - (lambda / Mu)) / (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) * (Math.Pow(lambda, index) / Math.Pow(Mu, index)));
                }
                else if (type == 3)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)((1.0 - (lambda / Mu)) / (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) * (Math.Pow(lambda, index) / Math.Pow(Mu, index)));
                }
                else if (type == 4)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)((1.0 - (lambda / Mu)) / (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) * (Math.Pow(lambda, index) / Math.Pow(Mu, index)));
                }
                else if (type == 5)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)((1.0 - (lambda / Mu)) / (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) * (Math.Pow(lambda, index) / Math.Pow(Mu, index)));
                }
                else if (type == 6)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)((1.0 - (lambda / Mu)) / (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) * (Math.Pow(lambda, index) / Math.Pow(Mu, index)));
                }

            }
            public  float TotalP() 
            {
                float rv = 0;
                foreach (var item in Plist)
                {
                    rv += item.value;  
                }
                return rv;
            }
            public  void SystemCalculationsMM1(float lambda, float Mu)
            {
                Ws = (1.0f / (Mu - lambda));
                Wq = (lambda) / (Mu * (Mu - lambda));
                Ls = (lambda / (Mu - lambda));
                Lq = ((float)(Math.Pow(lambda, 2.0f) / (Mu * (Mu - lambda))));
                Cb = lambda / Mu;
            }
            public void SystemCalculationsMM1N()
            {

                lambdalost = lambda * Plist[19].value;
                lambdaeff = lambda - lambdalost;

                Ls = (lambda/Mu)*((1f - (NCount +1f) * (float)(Math.Pow(lambda, NCount) / Math.Pow(Mu, NCount)))+
                    (NCount * (float)(Math.Pow(lambda, NCount + 1f) / Math.Pow(Mu, NCount + 1f))))/(1f - lambda/Mu)*
                    (1- (float)(Math.Pow(lambda, NCount + 1f) / Math.Pow(Mu, NCount + 1f)));
               
                Ws = Ls / lambdaeff;
                Wq = Ws - (1 / Mu);

                Lq = (lambdaeff*Wq);
                Cb = Ls-Lq;
            }
            public void SystemCalculationsMMCN(float lambda, float Mu, float ccount)
            {
                CCount = 1;
                lambdalost = lambda * Plist[19].value;
                lambdaeff = lambda - lambdalost;

                Ls = (float)((lambda / Mu) * (1.0 - (CCount + 1.0)) * (Math.Pow(lambda, ccount) / Math.Pow(Mu, ccount)) + (ccount * (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))) / (1.0 - (lambda / Mu)) * (1.0 - (Math.Pow(lambda, ccount + 1.0) / Math.Pow(Mu, ccount + 1.0))));
                Ws = Ls / lambdaeff;
                Wq = Ws - (1 / Mu);

                Lq = (lambdaeff * Wq);
                Cb = Ls - Lq;
            }

        }


        public CalculatorBaseClass MM1Cal = new CalculatorBaseClass();
        public CalculatorBaseClass MM1NCal = new CalculatorBaseClass();

        
        public QueCalculator()
        {
            InitializeComponent();
            ModelSelection.SelectedIndex = 0;
        }


        private void LamdaInput_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(LamdaInput.Text, out int n))
            {
                MessageBox.Show("Please enter a number!");
                LamdaInput.Text = "";
            }
            else
            {
                if (ModelSelection.SelectedIndex == 0)
                {
                    MM1Cal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
                if (ModelSelection.SelectedIndex == 1)
                {
                    MM1NCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }

                
            }
        }
        private void MuInput_TextChanged_1(object sender, EventArgs e)
        {
            if (ModelSelection.SelectedIndex == 0)
            {
                MM1Cal.Mu = ((float)Convert.ToDouble(MuInput.Text));
            }
            if (ModelSelection.SelectedIndex == 1)
            {
                MM1NCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
            }
           
        }
        


        private void Calculate_Click(object sender, EventArgs e)
        {
            void Rsp(CalculatorBaseClass cc)
            {
                WsOut.Text = Convert.ToString(cc.Ws) ;
                WqOut.Text = Convert.ToString(cc.Wq);
                LsOut.Text = Convert.ToString(cc.Ls);
                LqOut.Text = Convert.ToString(cc.Lq);
                CbOut.Text = Convert.ToString(cc.Cb);
                LambdaEffOut.Text = Convert.ToString(cc.lambdaeff);
                LambdaLost.Text = Convert.ToString(cc.lambdalost);
            }
            if (ModelSelection.SelectedIndex == 0)
            {
                if (MM1Cal.lambda >= MM1Cal.Mu)
                {
                    MessageBox.Show("You are idiot lambda have to bigger than Mu!");
                }
                else
                {


                    for (int i = 0; i <= 19; i++)
                    {
                        MM1Cal.Pcal(i, MM1Cal.lambda,1, MM1Cal.Mu,0);

                    }
                    MM1Cal.SystemCalculationsMM1(MM1Cal.lambda, MM1Cal.Mu);
                    Rsp(MM1Cal);
                    MessageBox.Show(MM1Cal.TotalP().ToString());
                    if (MM1Cal.TotalP() > 0.99f)
                    {
                        MessageBox.Show("Pt = 1 Calculation is succcesful!");
                    }
                }
            }
            if (ModelSelection.SelectedIndex == 1)
            {
                
                    float po = 1;
                    MM1NCal.createpo();
                    for (int i = 1; i <= 19; i++)
                    {
                        
                        po += (float)(Math.Pow((MM1NCal.lambda / MM1NCal.Mu), i));
                        
                        MM1NCal.Plist[0].value = 1f/po;
                    }

                    for (int i = 1; i <= 20; i++)
                    {
                        MM1NCal.Pcal(i, MM1NCal.lambda, 1,MM1NCal.Mu, 1);

                    }
                    MM1NCal.SystemCalculationsMM1N();
                    Rsp(MM1NCal);
                    MessageBox.Show(MM1NCal.TotalP().ToString());
                    if (MM1NCal.TotalP() > 0.99f)
                    {
                        MessageBox.Show("Pt = 1 Calculation is succcesful!");
                    }
                
            }
            if (ModelSelection.SelectedIndex == 2)
            {

                float po = 1;
                MM1NCal.createpo();
                for (int i = 1; i <= 19; i++)
                {

                    po += (float)(Math.Pow((MM1NCal.lambda / MM1NCal.Mu), i));

                    MM1NCal.Plist[0].value = 1f / po;
                }

                for (int i = 1; i <= 20; i++)
                {
                    MM1NCal.Pcal(i, MM1NCal.lambda, 1, MM1NCal.Mu, 1);

                }
                MM1NCal.SystemCalculationsMM1N();
                Rsp(MM1NCal);
                MessageBox.Show(MM1NCal.TotalP().ToString());
                if (MM1NCal.TotalP() > 0.99f)
                {
                    MessageBox.Show("Pt = 1 Calculation is succcesful!");
                }

            }

        }

        private void ModelSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LambdaEffOut.Show();
            LambdaLost.Show();
            CInput.Show();
            KInput.Show();
            RInput.Show();
            NInput.Show();
            LambdaEffOut.Show();
            LambdaLost.Show();
            EtInput.Show();
            VartInput.Show();
            if (ModelSelection.SelectedIndex == 0)
            {
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                CInput.Hide();
                KInput.Hide();
                RInput.Hide();
                NInput.Hide();
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 1)
            {
               
                
                KInput.Hide();
                RInput.Hide();
                CInput.Hide();

                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 2)
            {
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                
                KInput.Hide();
                RInput.Hide();
                NInput.Hide();
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 3)
            {
                
                CInput.Hide();
                KInput.Hide();
                RInput.Hide();
                
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 4)
            {
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                CInput.Hide();
                KInput.Hide();
                RInput.Hide();
                NInput.Hide();
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 5)
            {
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                CInput.Hide();
                NInput.Hide();
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                EtInput.Hide();
                VartInput.Hide();
            }
            else if (ModelSelection.SelectedIndex == 6)
            {
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                CInput.Hide();
                KInput.Hide();
                RInput.Hide();
                NInput.Hide();
                LambdaEffOut.Hide();
                LambdaLost.Hide();
                
            }
        }

        private void Pindex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModelSelection.SelectedIndex == 0)
            {
                Pout.Text = MM1Cal.Plist[Pindex.SelectedIndex].value.ToString();
            }
            if (ModelSelection.SelectedIndex == 1)
            {
                Pout.Text = MM1NCal.Plist[Pindex.SelectedIndex].value.ToString();
            }


        }

        private void CInput_TextChanged(object sender, EventArgs e)
        {
            if (ModelSelection.SelectedIndex == 1)
            {
                MM1NCal.CCount = ((float)Convert.ToDouble(CInput.Text));
            }
           
        }

        private void LsOut_TextChanged(object sender, EventArgs e)
        {
            if (ModelSelection.SelectedIndex == 0)
            {
                MM1Cal.Ls = ((float)Convert.ToDouble(LsOut.Text));
            }
            if (ModelSelection.SelectedIndex == 1)
            {
                MM1NCal.Ls = ((float)Convert.ToDouble(LsOut.Text));
            }
        }

        private void NInput_TextChanged(object sender, EventArgs e)
        {
           
            if (ModelSelection.SelectedIndex == 1)
            {
                MM1NCal.NCount = ((float)Convert.ToDouble(NInput.Text));
            }
            
        }
    }
}
