using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;

namespace Lb4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog1.FileName = "";
            saveFileDialog1.Filter = "(*.txt)|*.txt|All files(*.*)|*.*";
        }

        public bool Ferma(int num)
        {
            Random random = new Random();
            int raz = 100;
            if (num > 1)
            {
                while (raz != 0)
                {
                    int i = random.Next(1, num - 1);
                    var n = BigInteger.ModPow(i, num - 1, num);
                    if (n != 1)
                    {
                        return false;
                    }
                    raz--;
                }
            }else return false;

            return true;
        }

        BigInteger Power(BigInteger x, BigInteger y, BigInteger N)
        {
            if (y == 0) return 1;
            BigInteger z = Power(x, y / 2, N);
            if (y % 2 == 0)
                return (z * z) % N;
            else
                return (x * z * z) % N;
        }


        BigInteger Power2(BigInteger x, BigInteger y)
        {
            if (y == 0) return 1;
            BigInteger z = Power2(x, y / 2);
            if (y % 2 == 0)
                return (z * z);
            else
                return (x * z * z);
        }

        Random random = new Random();
        int p, q;
        public BigInteger g, y, H, r, s, w, u1, u2, v, x,  h, k;

        private void text_txt_Click(object sender, EventArgs e)
        {
            text_txt.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            text_txt.Text = fileText;
        }

        private new void KeyPress(object sender, KeyPressEventArgs e)
        {

            char key = e.KeyChar;

            if (!Char.IsDigit(key) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

        }

        private void generate_btn_Click(object sender, EventArgs e)
        {
            while (true)
            {
                int q1 = random.Next(100, 1000);

                if (Ferma(q1))
                {
                    q = q1;
                    break;
                }
            }
            q_txt.Text = q.ToString();


            int pp = random.Next(100, 1000);
            while (true)
            {

                if ((pp - 1) % q == 0 && Ferma(pp))
                {
                    p = pp;
                    break;
                }
                else pp++;             
            }
            p_txt.Text = p.ToString();

            h = random.Next(1, p - 1);
            h_txt.Text = h.ToString();

            x = random.Next(0, q);
            x_txt.Text = x.ToString();

            k = random.Next(0, q);
            k_txt.Text = k.ToString();
                               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(q_txt.Text) || !String.IsNullOrEmpty(p_txt.Text) || !String.IsNullOrEmpty(h_txt.Text)
                || !String.IsNullOrEmpty(x_txt.Text) || !String.IsNullOrEmpty(k_txt.Text) || !String.IsNullOrEmpty(text_txt.Text))
                {

                    q = Convert.ToInt32(q_txt.Text);
                    p = Convert.ToInt32(p_txt.Text);
                    h = Convert.ToInt32(h_txt.Text);
                    x = Convert.ToInt32(x_txt.Text);
                    k = Convert.ToInt32(k_txt.Text);


                    if (Ferma(q))
                    {
                        if ((p - 1) % q == 0 && Ferma(p))
                        {
                            if (h > 0 && h < p - 1)
                            {
                                if (x >= 0 && x < q)
                                {
                                    if (k >= 0 && k < q)
                                    {
                                        encoding_txt.Clear();

                                byte[] txt = Encoding.GetEncoding(1251).GetBytes(text_txt.Text);

                                for (int i = 0; i < txt.Length; i++)
                                {
                                    encoding_txt.Text += txt[i] + " ";
                                }

                                g = Power(h, (p - 1) / q, p);

                                y = Power(g, x, p);

                                H = Power(100 + txt[0], 2, q);
                                for (int i = 1; i < txt.Length; i++)
                                {
                                    H = Power(H + txt[i], 2, q);
                                }
                                hM_txt.Text = H.ToString();


                                r = Power(g, k, p) % q;


                               BigInteger _k = Power(k, q - 2, q);


                                s = _k * (H + x * r) % q;
                                if (r == 0)
                                {
                                    MessageBox.Show("r = 0");
                                    k_txt.Clear();
                                }
                                else if (s == 0)
                                {
                                    MessageBox.Show("s = 0");
                                    k_txt.Clear();
                                }
                                else
                                {
                                    ecp_txt.Text = r.ToString() + " " + s.ToString();


                                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                        return;
                                    string filename = saveFileDialog1.FileName;

                                    string str = text_txt.Text + "," + r.ToString() + "," + s.ToString();

                                    System.IO.File.WriteAllText(filename, str);
                                    Process.Start(filename);
                                        }
                                    }
                                    else
                                        MessageBox.Show("k - не подходит");
                                }
                                else
                                    MessageBox.Show("x - не подходит");
                            }
                            else
                                MessageBox.Show("h - не подходит");
                        }
                        else
                            MessageBox.Show("p - не подходит");

                    }
                    else MessageBox.Show("q - составное число");

                }
                else MessageBox.Show("Заполните все поля");
                
            }
            catch (Exception)
            {}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            string[] words = fileText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


            byte[] txt = Encoding.GetEncoding(1251).GetBytes(words[0]);

            r = Convert.ToInt32(words[1]);
            s = Convert.ToInt32(words[2]);


            H = Power(100 + txt[0], 2, q);
            for (int i = 1; i < txt.Length; i++)
            {
                H = Power(H + txt[i], 2, q);
            }



            BigInteger _s = Power(s, q - 2, q);
            w = _s % q;
            _w.Text = w.ToString();

            u1 = H * w % q;
            _u1.Text = u1.ToString();
            u2 = r * w % q;
            _u2.Text = u2.ToString();

            int s1 = Convert.ToInt32(u1.ToString());

            int s2 = Convert.ToInt32(u2.ToString());

            v = ((Power2(g, s1) * Power2(y, s2)) % p) % q;
            _v.Text = v.ToString();
            if (r == v)
            {
                MessageBox.Show("ЭЦП верна: " + v + " = " + r);              
            }
            else MessageBox.Show("ЭЦП не верна: " + v + " = " + r);
        }
    }
}
