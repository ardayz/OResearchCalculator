using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OResearchCalculator
{
    public static class MM1N 
    {
        
    }
    static class MM1
    {
        public static double lambda;
        public static double Mu;
        public static double Ls;
        public static double Lq;
        public static double Ws;
        public static double Wq;
        public static double Cb;
        public class p 
        {
            public int index;
            public double value;
            
            public void Totalcal() 
            {
                Pt += value;
            }
        }
        public static double Pt;
        public static List<p> Plist = new List<p>() ;

       
        public static void Pcal(int index ,double lambda ,double Mu  ) 
        {
            Plist[index].value = (Math.Pow(lambda,index) / Math.Pow(Mu,index)) * ((Mu - lambda)/Mu) ;
            Plist[index].Totalcal();

        }

        public static void SystemCalculations(double lambda, double Mu) 
        {
            Ws = (1 / (Mu - lambda));
            Wq = (lambda / Mu * (Mu - lambda));
            Ls = (lambda / (Mu - lambda));
            Lq = (Math.Pow(lambda, 2) / Mu * (Mu - lambda));
            Cb = lambda / Mu;
        } 

        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QueCalculator());


            
        }
    }
}
