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
            public float Et { get; set; }
            public float K { get; set; }
            public float R { get; set; }


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
            public float factorial(float num)
            {
                float n = 0f;
                while (num > 0)
                {
                    n = num;
                    for (float i = n - 1f; i > 0; i--)
                    {
                        n *= i;
                    }
                    Console.WriteLine("Factorial of {0}! = {1}\n", num, n);
                    num--;
                    return n;
                }
                return n;
            }
            public float perm(float n, float r)
            {
                return factorial(n) / factorial(n - r);
            }
            public float Pt { get; set; }
            public List<p> Plist = new List<p>();
            public void Pcal(int index, float lambda, float ccount, float Mu, int type, float et = 0)
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
                    Plist[index].value = (float)(Math.Pow((lambda / Mu), index) * Plist[0].value);
                }
                else if (type == 2)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    Plist[index].value = (float)(Math.Pow(lambda, index) / factorial(index) * (Math.Pow(Mu, index)) * Plist[0].value);
                }
                else if (type == 3)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    if (ccount >= index)
                    {
                        Plist[index].value = (float)(Math.Pow(lambda, index) /
                            ((float)(Math.Pow(ccount, index - CCount)) * factorial(ccount) * (float)(Math.Pow(Mu, ccount)))) * Plist[0].value;
                    }
                    else
                    {
                        Plist[index].value = (float)(Math.Pow(lambda, index) / (factorial(index) * (float)(Math.Pow(Mu, index)))) * Plist[0].value;
                    }

                }
                else if (type == 4)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    if (index == 0)
                    {
                        Plist[index].value = (float)Math.Pow(Math.E, -lambda / Mu);
                    }
                    else
                    {
                        Plist[index].value = (float)(Math.Pow(lambda, index) / (factorial(index) * Math.Pow(Mu, index)) * Math.Pow(Math.E, -lambda / Mu));
                    }
                   
                }
                else if (type == 5)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    if (R < index )
                    {
                        Plist[index].value = (float)(perm(K, index) * ((factorial(index) * Math.Pow(lambda / Mu, index))
                            /factorial(R)*Math.Pow(R,index-R)) * Plist[0].value);
                    }
                    else
                    {
                        Plist[index].value = (float)(perm(K,index) * Math.Pow(lambda/Mu,index) * Plist[0].value);
                    }
                    
                   
                }
                else if (type == 6)
                {
                    p newp = new p();
                    newp.index = index;
                    Plist.Add(newp);
                    if (index == 0)
                    {
                        Plist[index].value = 1f - (lambda * Et);
                    }
                    else
                    {
                        Plist[index].value = (float)(Math.Pow((lambda*et),index) * (1f - (lambda * et)));
                    }
                }

            }
            public float TotalP()
            {
                float rv = 0;
                foreach (var item in Plist)
                {
                    rv += item.value;
                }
                return rv;
            }
            public void SystemCalculationsMM1(float lambda, float Mu)
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

                Ls = (lambda / Mu) * ((1f - (NCount + 1f) * (float)(Math.Pow(lambda, NCount) / Math.Pow(Mu, NCount))) +
                    (NCount * (float)(Math.Pow(lambda, NCount + 1f) / Math.Pow(Mu, NCount + 1f)))) / (1f - lambda / Mu) *
                    (1 - (float)(Math.Pow(lambda, NCount + 1f) / Math.Pow(Mu, NCount + 1f)));

                Ws = Ls / lambdaeff;
                Wq = Ws - (1 / Mu);

                Lq = (lambdaeff * Wq);
                Cb = Ls - Lq;
            }
            public void SystemCalculationsMMC()
            {


                Lq = (float)(Math.Pow(lambda, CCount + 1) / (Math.Pow((CCount - (lambda / Mu)), 2) * factorial(CCount - 1) * Math.Pow(Mu, CCount + 1))) * Plist[0].value;
                Ls = Lq + (lambda / Mu);
                Ws = Ls / lambda;
                Wq = Lq / lambda;


                Cb = Ls - Lq;
            }
            public void SystemCalculationsMMCN()
            {


                Lq = (float)((Math.Pow(lambda, CCount + 1f) / (Math.Pow((CCount - (lambda / Mu)), 2) * factorial(CCount - 1) * Math.Pow(Mu, CCount + 1)))
                        * ((1f - Math.Pow(lambda / (CCount * Mu), NCount - CCount + 1f)) - ((NCount - CCount + 1f) * (1f - (lambda / (CCount * NCount))) * Math.Pow(lambda / (CCount * NCount), NCount - CCount)))) * Plist[0].value;

                lambdalost = (float)(lambda * Plist[Convert.ToInt32(NCount)].value);
                lambdaeff = lambda - lambdalost;

                Ls = Lq + (lambdaeff / Mu);
                Ws = Ls / lambdaeff;
                Wq = Lq / lambdaeff;


                Cb = Ls - Lq;
            }
            public void SystemCalculationsMMGD()
            {
                Ls = lambda / Mu;
                Ws = 1f / Mu;
                Cb = Ls;
                Wq = 0f;
                Lq = Wq;
            }
            public void SystemCalculationsMMRKK()
            {
                for (int i = 0; i <= K; i++)
                {
                    Ls += i * Plist[i].value;
                }
                for (int i = Convert.ToInt32(R); i <= K; i++)
                {
                    Lq += (i - R) * Plist[i].value;
                }
                Ws = Ls / lambda;
                
                Wq = Ws-(1f/Mu);
                Lq = Wq;
            }
            public void SystemCalculationsPK()
            {
                for (int i = 1; i < 21; i++)
                {
                    Ls += i * Plist[i].value;
                }
                
                Ws = Ls / lambda;
                Cb = lambda* Et;
                Wq = (Ls/lambda) - Et;
                Lq = lambda*Wq;
            }
        }


        public CalculatorBaseClass MM1Cal = new CalculatorBaseClass();
        public CalculatorBaseClass MM1NCal = new CalculatorBaseClass();
        public CalculatorBaseClass MMCCal = new CalculatorBaseClass();
        public CalculatorBaseClass MMCNCal = new CalculatorBaseClass();
        public CalculatorBaseClass MMGDCal = new CalculatorBaseClass();
        public CalculatorBaseClass MMRKKCal = new CalculatorBaseClass();
        public CalculatorBaseClass PKCal = new CalculatorBaseClass();

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
                if (ModelSelection.SelectedIndex == 2)
                {
                    MMCCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
                if (ModelSelection.SelectedIndex == 3)
                {
                    MMCNCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
                if (ModelSelection.SelectedIndex == 4)
                {
                    MMGDCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
                if (ModelSelection.SelectedIndex == 5)
                {
                    MMRKKCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
                if (ModelSelection.SelectedIndex == 6)
                {
                    PKCal.lambda = ((float)Convert.ToDouble(LamdaInput.Text));
                }
            }
        }
        private void MuInput_TextChanged_1(object sender, EventArgs e)
        {
            if (!int.TryParse(MuInput.Text, out int n))
            {

                MessageBox.Show("Please enter a number!");
                MuInput.Text = "";


            }
            else if (MuInput.Text == string.Empty)
            {

            }
            else
            {

                if (ModelSelection.SelectedIndex == 0)
                {
                    MM1Cal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
                if (ModelSelection.SelectedIndex == 1)
                {
                    MM1NCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
                if (ModelSelection.SelectedIndex == 2)
                {
                    MMCCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
                if (ModelSelection.SelectedIndex == 3)
                {
                    MMCNCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
                if (ModelSelection.SelectedIndex == 4)
                {
                    MMGDCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
                if (ModelSelection.SelectedIndex == 5)
                {
                    MMRKKCal.Mu = ((float)Convert.ToDouble(MuInput.Text));
                }
            }
        }



        private void Calculate_Click(object sender, EventArgs e)
        {
            void Rsp(CalculatorBaseClass cc)
            {
                WsOut.Text = Convert.ToString(cc.Ws);
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


                    for (int i = 0; i <= 20; i++)
                    {
                        MM1Cal.Pcal(i, MM1Cal.lambda, 1, MM1Cal.Mu, 0);

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
                for (int i = 1; i <= 20; i++)
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
            if (ModelSelection.SelectedIndex == 2)
            {

                float po = 1;
                MMCCal.createpo();
                for (int i = 1; i <= 20; i++)
                {

                    po += (float)(Math.Pow(MMCCal.lambda, i) / MMCCal.factorial(i) * Math.Pow(MMCCal.Mu, i));

                    MMCCal.Plist[0].value = 1f / po;
                }

                for (int i = 1; i <= 20; i++)
                {
                    MMCCal.Pcal(i, MMCCal.lambda, MMCCal.CCount, MMCCal.Mu, 2);

                }
                MMCCal.SystemCalculationsMMC();
                Rsp(MMCCal);
                MessageBox.Show(MMCCal.TotalP().ToString());
                if (MMCCal.TotalP() > 0.99f)
                {
                    MessageBox.Show("Pt = 1 Calculation is succcesful!");
                }

            }
            if (ModelSelection.SelectedIndex == 3)
            {
                float po = 1;
                MMCNCal.createpo();
                for (int i = 1; i <= 20; i++)
                {
                    if (MMCNCal.CCount >= i)
                    {
                        po += (float)(Math.Pow(MMCNCal.lambda, i) /
                            ((float)(Math.Pow(MMCNCal.CCount, i - MMCNCal.CCount)) * MMCNCal.factorial(MMCNCal.CCount) * (float)(Math.Pow(MMCNCal.Mu, MMCNCal.CCount))));


                    }
                    else
                    {
                        po += (float)(Math.Pow(MMCNCal.lambda, i) / (MMCNCal.factorial(i) * (float)(Math.Pow(MMCNCal.Mu, i))));
                    }

                }
                MMCNCal.Plist[0].value = 1f / po;
                for (int i = 1; i <= 20; i++)
                {
                    MMCNCal.Pcal(i, MMCNCal.lambda, MMCNCal.CCount, MMCNCal.Mu, 3);

                }
                MMCNCal.SystemCalculationsMMCN();
                Rsp(MMCNCal);
                MessageBox.Show(MMCNCal.TotalP().ToString());
                if (MMCNCal.TotalP() > 0.99f)
                {
                    MessageBox.Show("Pt = 1 Calculation is succcesful!");
                }
            }
            if (ModelSelection.SelectedIndex == 4)
            {

                for (int i = 0; i <= 20; i++)
                {
                    MMGDCal.Pcal(i, MMGDCal.lambda, 1, MMGDCal.Mu, 4);

                }
                MMGDCal.SystemCalculationsMMGD();
                Rsp(MMGDCal);
                MessageBox.Show(MMGDCal.TotalP().ToString());
                if (MMGDCal.TotalP() > 0.99f)
                {
                    MessageBox.Show("Pt = 1 Calculation is succcesful!");
                }
            
            }
            if (ModelSelection.SelectedIndex == 5)
            {
                
                MMRKKCal.createpo();
                //double po = 1;
                //for (int i = 1; i <= 20; i++)
                //{
                //    if (MMRKKCal.R < i)
                //    {
                //        po += (float)(MMRKKCal.perm(MMRKKCal.K, i) * ((MMRKKCal.factorial(i) * Math.Pow(MMRKKCal.lambda / MMRKKCal.Mu, i))
                //            / MMRKKCal.factorial(MMRKKCal.R) * Math.Pow(MMRKKCal.R, i - MMRKKCal.R)));
                //    }
                //    else
                //    {
                //        po += (float)(MMRKKCal.perm(MMRKKCal.K, i) * Math.Pow(MMRKKCal.lambda / MMRKKCal.Mu, i));
                //    }

                //    MMRKKCal.Plist[0].value = (float)(1f / po);
                //}
                double part1 = 0;
                double part2 = 0;

                for (int i = 0; i <= MMRKKCal.R; i++)
                {
                   part1 = (float)MMRKKCal.perm(MMRKKCal.K,i) * Math.Pow((MMRKKCal.lambda/MMRKKCal.Mu),i);
                }
                for (int i = (int)MMRKKCal.R; i < MMRKKCal.K; i++)
                {
                    part2 = MMRKKCal.perm(MMRKKCal.K, MMRKKCal.R) * ((MMRKKCal.factorial(i) * Math.Pow(MMRKKCal.lambda / MMRKKCal.Mu, i))
                        / (MMRKKCal.factorial(MMRKKCal.R) * Math.Pow(MMRKKCal.R, i - MMRKKCal.R)));
                }
                MMRKKCal.Plist[0].value = (float)Math.Pow(part1+part2,-1f);
                for (int i = 1; i <= 20; i++)
                {
                    MMRKKCal.Pcal(i, MMRKKCal.lambda, 1, MMRKKCal.Mu, 5);

                }
                MMRKKCal.SystemCalculationsMMRKK();
                Rsp(MMRKKCal);
                MessageBox.Show(MMRKKCal.TotalP().ToString());
                if (MMRKKCal.TotalP() > 0.99f)
                {
                    MessageBox.Show("Pt = 1 Calculation is succcesful!");
                }

            }
            if (ModelSelection.SelectedIndex == 6)
            {

                for (int i = 0; i <= 20; i++)
                {
                    PKCal.Pcal(i, PKCal.lambda, 1, PKCal.Mu, 6,PKCal.Et);

                }
                PKCal.SystemCalculationsPK();
                Rsp(PKCal);
                MessageBox.Show(PKCal.TotalP().ToString());
                if (PKCal.TotalP() > 0.99f)
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


            KInput.Hide();
            RInput.Hide();


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
            MuInput.Hide();
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
        if (ModelSelection.SelectedIndex == 2)
        {
            Pout.Text = MMCCal.Plist[Pindex.SelectedIndex].value.ToString();
        }
        if (ModelSelection.SelectedIndex == 3)
        {
            Pout.Text = MMCNCal.Plist[Pindex.SelectedIndex].value.ToString();
        }
        if (ModelSelection.SelectedIndex == 4)
        {
            Pout.Text = MMGDCal.Plist[Pindex.SelectedIndex].value.ToString();
        }
        if (ModelSelection.SelectedIndex == 5)
        {
            Pout.Text = MMRKKCal.Plist[Pindex.SelectedIndex].value.ToString();
        }
        if (ModelSelection.SelectedIndex == 6)
        {
            Pout.Text = PKCal.Plist[Pindex.SelectedIndex].value.ToString();
        }


        }

    private void CInput_TextChanged(object sender, EventArgs e)
    {
            if (!int.TryParse(CInput.Text, out int n))
            {

                MessageBox.Show("Please enter a number!");
                CInput.Text = "";


            }
            else if (CInput.Text == string.Empty)
            {

            }
            else
            {
                if (ModelSelection.SelectedIndex == 1)
                {
                    MM1NCal.CCount = ((float)Convert.ToDouble(CInput.Text));
                }
                if (ModelSelection.SelectedIndex == 2)
                {
                    MMCCal.CCount = ((float)Convert.ToDouble(CInput.Text));
                }
                if (ModelSelection.SelectedIndex == 3)
                {
                    MMCNCal.CCount = ((float)Convert.ToDouble(CInput.Text));
                }
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
            if (!int.TryParse(NInput.Text, out int n))
            {

                MessageBox.Show("Please enter a number!");
                NInput.Text = "";


            }
            else if (NInput.Text == string.Empty)
            {

            }
            else
            {
                if (ModelSelection.SelectedIndex == 1)
                {
                    MM1NCal.NCount = ((float)Convert.ToDouble(NInput.Text));
                }
                if (ModelSelection.SelectedIndex == 3)
                {
                    MMCNCal.NCount = ((float)Convert.ToDouble(NInput.Text));
                }
            }

         

    }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void EtInput_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(EtInput.Text, out int n))
            {

                MessageBox.Show("Please enter a number!");
                EtInput.Text = "";


            }
            else if (EtInput.Text == string.Empty)
            {

            }
            else
            {
                if (ModelSelection.SelectedIndex == 6)
                {
                    PKCal.Et = (float)Convert.ToDouble(EtInput.Text);
                }
            }
            
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            CInput.Text = string.Empty;
            VartInput.Text = string.Empty;
            MuInput.Text = string.Empty;
            LamdaInput.Text = string.Empty;
            RInput.Text = string.Empty;
            KInput.Text = string.Empty;
            EtInput.Text = string.Empty;
            VartInput.Text = string.Empty;
            NInput.Text = string.Empty;

        }

        private void RInput_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(RInput.Text, out int n))
            {

                MessageBox.Show("Please enter a number!");
                RInput.Text = "";


            }
            else if (RInput.Text == string.Empty)
            {

            }
            else
            {
                if (ModelSelection.SelectedIndex == 6)
                {
                    MMRKKCal.R = (float)Convert.ToDouble(RInput.Text);
                }
            }
            
        }

        private void KInput_TextChanged(object sender, EventArgs e)
        {
            if ((float)Convert.ToDouble(KInput.Text)<=20)
            {
                MMRKKCal.K = (float)Convert.ToDouble(KInput.Text);
            }
            else if (KInput.Text == string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Please enter lower K Input < 20");
            }
        }
    }
    
}
