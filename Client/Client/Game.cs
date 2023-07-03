using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Game : Form
    {
        static string name;
        static TcpClient c;
        BinaryFormatter obj;
        string player;
        List<nearbutton> lnb = new List<nearbutton>();
        public Game()
        {
            InitializeComponent();
        }
        internal void getnameandclient(string n, TcpClient cl)
        {
            c = cl;
            name = n;
            obj = new BinaryFormatter();
        }
        private void Game_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            lblp.Text = "Waiting for another Player";
            buttonlist();
            btndisable();
            Thread wait = new Thread(waiting);
            wait.Start();
        }
        public void waiting()
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] datafromserver = new byte[10];
            NetworkStream ns = c.GetStream();
            ns.Read(datafromserver, 0, datafromserver.Length);
            string msg = Encoding.ASCII.GetString(datafromserver);
            player = msg.Replace("\0", string.Empty);
            lblp.Text = "Player " + player;
            lblp.Font = new Font("Jokerman", 18);
            lblp.ForeColor = Color.Blue;
            ns.Flush();
            //o = (bf.Deserialize(c.GetStream()));
            if (player.Equals("1"))
                enabled();
            Thread t = new Thread(read);
            t.Start();
        }
        public void buttonlist()
        {

            nearbutton nb1 = new nearbutton();
            List<Button> lb1 = new List<Button>();
            lb1.Add(btn2); lb1.Add(btn3); lb1.Add(btn4);
            lb1.Add(btn7); lb1.Add(btn5); lb1.Add(btn9);
            nb1.b = btn1;
            nb1.but = lb1;
            lnb.Add(nb1);
            nearbutton nb2 = new nearbutton();
            List<Button> lb2 = new List<Button>();
            lb2.Add(btn1); lb2.Add(btn3);
            lb2.Add(btn5); lb2.Add(btn8);
            nb2.b = btn2;
            nb2.but = lb2;
            lnb.Add(nb2);
            nearbutton nb3 = new nearbutton();
            List<Button> lb3 = new List<Button>();
            lb3.Add(btn1); lb3.Add(btn2); lb3.Add(btn5);
            lb3.Add(btn7); lb3.Add(btn6); lb3.Add(btn9);
            nb3.b = btn3;
            nb3.but = lb3;
            lnb.Add(nb3);
            nearbutton nb4 = new nearbutton();
            List<Button> lb4 = new List<Button>();
            lb4.Add(btn1); lb4.Add(btn7);
            lb4.Add(btn5); lb4.Add(btn6);
            nb4.b = btn4;
            nb4.but = lb4;
            lnb.Add(nb4);
            nearbutton nb5 = new nearbutton();
            List<Button> lb5 = new List<Button>();
            lb5.Add(btn1); lb5.Add(btn9); lb5.Add(btn2);
            lb5.Add(btn8); lb5.Add(btn3); lb5.Add(btn7);
            lb5.Add(btn4); lb5.Add(btn6);
            nb5.b = btn5;
            nb5.but = lb5;
            lnb.Add(nb5);
            nearbutton nb6 = new nearbutton();
            List<Button> lb6 = new List<Button>();
            lb6.Add(btn3); lb6.Add(btn9);
            lb6.Add(btn4); lb6.Add(btn5);
            nb6.b = btn6;
            nb6.but = lb6;
            lnb.Add(nb6);
            nearbutton nb7 = new nearbutton();
            List<Button> lb7 = new List<Button>();
            lb7.Add(btn1); lb7.Add(btn4); lb7.Add(btn3);
            lb7.Add(btn5); lb7.Add(btn8); lb7.Add(btn9);
            nb7.b = btn7;
            nb7.but = lb7;
            lnb.Add(nb7);
            nearbutton nb8 = new nearbutton();
            List<Button> lb8 = new List<Button>();
            lb8.Add(btn2); lb8.Add(btn5);
            lb8.Add(btn7); lb8.Add(btn9);
            nb8.b = btn8;
            nb8.but = lb8;
            lnb.Add(nb8);
            nearbutton nb9 = new nearbutton();
            List<Button> lb9 = new List<Button>();
            lb9.Add(btn7); lb9.Add(btn8); lb9.Add(btn1);
            lb9.Add(btn5); lb9.Add(btn3); lb9.Add(btn6);
            nb9.b = btn9;
            nb9.but = lb9;
            lnb.Add(nb9);
        }
        public void read()
        {
            while (true)
            {
                byte[] datafromserver = new byte[20];
                NetworkStream ns = c.GetStream();
                ns.Read(datafromserver, 0, datafromserver.Length);
                ns.Flush();
                string msg = Encoding.ASCII.GetString(datafromserver);
                msg = msg.Replace("\0", string.Empty);
                string p = msg.Split(';')[0];
                string btn = msg.Split(';')[2];
                if (btn.Equals("btn1"))
                    btn1.Text = p;
                else if (btn.Equals("btn2"))
                    btn2.Text = p;
                else if (btn.Equals("btn3"))
                    btn3.Text = p;
                else if (btn.Equals("btn4"))
                    btn4.Text = p;
                else if (btn.Equals("btn5"))
                    btn5.Text = p;
                else if (btn.Equals("btn6"))
                    btn6.Text = p;
                else if (btn.Equals("btn7"))
                    btn7.Text = p;
                else if (btn.Equals("btn8"))
                    btn8.Text = p;
                else if (btn.Equals("btn9"))
                    btn9.Text = p;
                if (msg.Split(';')[1].Equals("win"))
                {
                    lblp.Text = "You Loss";
                    lblp.ForeColor = Color.Red;
                }
                else if (msg.Split(';')[1].Equals("draw"))
                {
                    lblp.Text = "Match Draw";
                }
                else
                {
                    enabled();
                }
            }
        }
        public void enabled()
        {
            btn1.Enabled = btn2.Enabled = btn3.Enabled = true;
            btn4.Enabled = btn5.Enabled = btn6.Enabled = true;
            btn7.Enabled = btn8.Enabled = btn9.Enabled = true;
        }
        public void btndisable()
        {
            btn1.Enabled = btn2.Enabled = btn3.Enabled = false;
            btn4.Enabled = btn5.Enabled = btn6.Enabled = false;
            btn7.Enabled = btn8.Enabled = btn9.Enabled = false;
        }
       
        public string getplayer()
        {
            return (int.Parse(player) - 1).ToString();
        }
        public void check(Button but, string bname)
        {
            btndisable();
            bool c = true;
            foreach (nearbutton nb in lnb)
            {
                int count = 0;
                if (nb.b.Equals(but))
                {

                    int i = 0;
                    foreach (Button b in nb.but)
                    {
                        if (b.Text.Equals((int.Parse(player) - 1).ToString()))
                            count++;
                        i++;
                        if (i % 2 == 0 && count != 2)
                            count = 0;
                        if (count == 2)
                            break;
                    }
                }
                if (count == 2)
                {
                    lblp.Text = "You Win";
                    lblp.ForeColor = Color.Green;
                    send("win;" + bname);
                    c = false;
                    break;
                }
            }
            if (c && (!btn1.Text.Equals("") && !btn2.Text.Equals("") && !btn3.Text.Equals("") && !btn4.Text.Equals("") && !btn5.Text.Equals("") && !btn6.Text.Equals("") && !btn7.Text.Equals("") && !btn8.Text.Equals("") && !btn9.Text.Equals("")))
            {
                lblp.Text = "Match draw";
                send("draw;" + bname);
                c = false;
            }
            if (c)
            {
                send("continue;" + bname);
            }
        }
        public void send(string result)
        {
            result = (int.Parse(player) - 1) + ";" + result;
            obj.Serialize(c.GetStream(), result);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn1_Click_1(object sender, EventArgs e)
        {
            if (btn1.Text.Equals(""))
            {
                btn1.Text = getplayer();
                check(btn1, "btn1");
            }
        }

        private void btn2_Click_1(object sender, EventArgs e)
        {
            if (btn2.Text.Equals(""))
            {
                btn2.Text = getplayer();
                check(btn2, "btn2");
            }
        }

        private void btn3_Click_1(object sender, EventArgs e)
        {
            if (btn3.Text.Equals(""))
            {
                btn3.Text = getplayer();
                check(btn3, "btn3");
            }
        }

        private void btn4_Click_1(object sender, EventArgs e)
        {
            if (btn4.Text.Equals(""))
            {
                btn4.Text = getplayer();
                check(btn4, "btn4");
            }
        }

        private void btn5_Click_1(object sender, EventArgs e)
        {
            if (btn5.Text.Equals(""))
            {
                btn5.Text = getplayer();
                check(btn5, "btn5");
            }
        }

        private void btn6_Click_1(object sender, EventArgs e)
        {
            if (btn6.Text.Equals(""))
            {
                btn6.Text = getplayer();
                check(btn6, "btn6");
            }
        }

        private void btn7_Click_1(object sender, EventArgs e)
        {
            if (btn7.Text.Equals(""))
            {
                btn7.Text = getplayer();
                check(btn7, "btn7");
            }
        }

        private void btn8_Click_1(object sender, EventArgs e)
        {
            if (btn8.Text.Equals(""))
            {
                btn8.Text = getplayer();
                check(btn8, "btn8");
            }
        }

        private void btn9_Click_1(object sender, EventArgs e)
        {
            if (btn9.Text.Equals(""))
            {
                btn9.Text = getplayer();
                check(btn9, "btn9");
            }
        }
    }
    class nearbutton
    {
        public Button b;
        public List<Button> but = new List<Button>();
    }
}

