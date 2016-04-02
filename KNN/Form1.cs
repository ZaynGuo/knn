using KNNLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }    
        
       
        private void btn_start_Click(object sender, EventArgs e)
        {
            
           // this.tbx_class_label.Text = this.calcKNN();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] SIM = new double[1800];
            for (int testobj = 0; testobj < 79; testobj++)
            {


                SIM[testobj] = 100;////豪斯多夫改为0，最近邻改为100
                int trainingobj = 0;
                ArrayList Q = combinelist(testobj);
                Obj[] test_obj = (Obj[])Q.ToArray(typeof(Obj));
                Dictionary<int, double> dic = new Dictionary<int, double>();
                while (trainingobj < 79)
                {
                    if (trainingobj != testobj)
                    {
                        ArrayList P = combinelist(trainingobj);
                        Obj[] training_set = (Obj[])P.ToArray(typeof(Obj));
                        SIM[trainingobj] = new KNNLib.KNN(ref training_set, ref test_obj, 3).KNNCluster();
                        dic.Add(trainingobj, SIM[trainingobj]);
                        // System.Console.WriteLine("No." + trainingobj + " sim " + SIM[trainingobj]);
                    } trainingobj++;
                }
                dic.OrderBy(o => o.Value);
                int[] simobj = dic.Keys.ToArray();
                //for (int i = 0; i < simobj.Length; i++)
                //{
                //    System.Console.WriteLine(simobj[i]);
                //}

                int classnum = simprocess(SIM);

                System.Console.WriteLine(classnum);
                

            }
        }

        public int simprocess(double[] sim){
            int StartIndex=0;
            int EndIndex=9;
            double[] classsim=new double[8];
            for (int i = 0; i < classsim.Length; i++)
            {
                double [] sim2 = SplitArray(sim, StartIndex, EndIndex);
                 sim2 = sim2.OrderBy(x => x).ToArray();
                 classsim[i] = sim2[0];//////豪斯多夫改为sim2[9],最近邻改为sim2[0]
                StartIndex += 10;
                EndIndex += 10;
            }

            double a = classsim.Min();

            int classnum = Array.IndexOf(classsim, a);
            return classnum+1;
        }

        public double[] SplitArray(double[] Source, int StartIndex, int EndIndex)
        {
            try
            {
                double[] result = new double[EndIndex - StartIndex + 1];
                for (int i = 0; i < EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region combinelist()
        public ArrayList combinelist(int combinenum)
        {
            double[] temporary = new double[1000];
            int objnum = combinenum % 10;
            int classnum = combinenum / 10;
          //  System.Console.WriteLine(classnum +"x"+ objnum);
            int picnum = 0;
            ArrayList M = new ArrayList();
            while (picnum < 41)
            {
                temporary = Sqlsource("zernike", picnum, objnum, classnum);
            String cl = "" + objnum;
            M.Add(new Obj { ID = "0", Class_label = cl, Attributes = new Attr_Arr(temporary) });
            picnum++;
          }
                return M;
        } 
        #endregion

        #region sql (string type, int picnum,int objnum,string numtype)
        public double[] Sqlsource(string type, int picnum, int objnum, int classnum)
        {
            String Textfea="" ;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=.;Initial Catalog=Pic;Integrated Security=True";
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "select *  from imagex where classnum = " + classnum + "and objectnum =" + objnum + "and picnum =" + picnum;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string color = reader.GetString(reader.GetOrdinal("color"));
                     //   string sobel = reader.GetString(reader.GetOrdinal("sobel"));
                        string hog = reader.GetString(reader.GetOrdinal("hog"));
                        string zernike = reader.GetString(reader.GetOrdinal("zernike"));
                        if (type == "color")
                        {
                            Textfea = color;
                        }
                        //if (type == "sobel")
                        //{
                        //    Textfea = sobel;
                        //}
                        if (type == "hog") {
                            
                            Textfea  = hog;
                        }
                        if (type == "zernike") {
                            Textfea = zernike;
                        }
                    }
                }
                    string[] T = Textfea.Split(new char[] { 'x', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> listx = new List<string>();
                    foreach (string ax in T)
                    {
                        if (!string.IsNullOrEmpty(ax))
                        {
                            listx.Add(ax);
                        }
                    }
                    string[] sf = listx.ToArray();
                    List<double> douhog = sf.ToList<string>().Select(n => Convert.ToDouble(n)).ToList<double>();
                 double [] eigenvalue=douhog.ToArray(); 
                return eigenvalue;
            }
            #endregion
        }
    }
}
