using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sysn
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Number.TTSN(Convert.ToString(textBox1.Text), Convert.ToInt32(comboBox2.SelectedItem), Convert.ToInt32(comboBox1.SelectedItem));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    public static class Number
    {
        private static String NUM;
        private static String INUM; 
        private static String FNUM; 
        private static int B1; 
        private static int B2; 
        private static int CharToInt(char s)
        {

            if (s >= '0' && s <= '9') return s - '0';
            else
            {
                if (s >= 'A' && s <= 'Z') return s - 'A' + 10;
                else return 100000;
            }
        }
        private static char IntToChar(int s)
        {
            if (s <= 9) return (char)(s + '0');
            else return (char)(s + 55);
        }
        private static bool NumCrct()
        {
            int l = NUM.Length; 
            int c = 0; 

            if (NUM[0] == '.' || NUM[l - 1] == '.') return false;

            for (int i = 0; i < l; i++)
            {
                if (c > 1) return false;
                if (NUM[i] == '.')
                {
                    c++;
                    continue;
                }
                if (CharToInt(NUM[i]) >= B1) return false;
            }

            return true;
        }
        private static int IntToDec()
        {
            int dN = 0, 
                ix = 1; 
            int c = INUM.Length; 
            for (int i = c - 1; i >= 0; i--)
            {
                int b = CharToInt(INUM[i]);
                dN += (b * ix);
                ix *= B1;
            }
            return dN;
        } 
        private static double FloatToDec()
        {
            double fN = 0.0; 
            double ix = -1; 
            int c = FNUM.Length;
            double b = B1; 
            for (int i = 0; i < c; i++)
            {
                double b2 = CharToInt(FNUM[i]);
                fN += (b2 * Math.Pow(b, ix));
                ix--;
            }
            return fN;
        }
        private static char[] FracPart()
        {
            int d, 
                ix = 0,
                sz = 50;
            char[] arr = new char[sz]; 
            double fN = FloatToDec();   
            double b = fN;
            while (b % 10 != 0)
            {
                b *= 10;
            }
            while (ix < 10)
            {
                fN *= B2;
                d = (int)fN;
                arr[ix] = IntToChar(d);
                fN -= d;
                ix++;
            }
            arr[ix] = '\0';
            return arr;
        }
        public static string TTSN(string sN, int fB, int sB)
        {
            NUM = sN;
            B1 = fB;
            B2 = sB;
            INUM = "";
            FNUM = "";
            if (NumCrct() == false)
            { 
                return "Incorrect Number";
            }
            else
            {
                int j = 0;
                bool c2 = false;
                while (j != sN.Length)
                {

                    if (sN[j] == '.')
                    {
                        c2 = true;
                        j++;
                        continue;
                    }
                    else
                    {
                        if (c2 == false)
                            INUM += sN[j];
                        else
                        {
                            FNUM += sN[j];
                        }

                        j++;
                    }
                }
                int c3 = 0; 
                int dN = IntToDec();
                int b = dN; 
                while (b > 0)
                {
                    b /= B2;
                    c3++;
                }
                char[] arr = new char[c3]; 
                char[] r = new char[100]; 
                int ix = 0;
                while (dN > 0)
                {
                    arr[ix] = IntToChar(dN % sB);
                    dN /= B2;
                    ix++;
                }
                ix = 0;
                for (int i = 0; i < c3; i++)
                {
                    r[i] = arr[c3 - 1 - i];
                    ix++;
                }
                if (c2 == true)
                {
                    char[] fN; 
                    int szfN = 0; 
                    fN = FracPart();
                    r[ix] = '.';
                    while (fN[szfN] != '\0') szfN++;
                    int ind = 0;
                    for (int i = ix + 1; i < szfN + ix + 1; i++)
                    {
                        r[i] = fN[ind];
                        ind++;
                    }
                }string gg = "";
                foreach (char c in r)
                {
                    gg += c;
                }
                return gg;
            }
        }
    }
  
}
