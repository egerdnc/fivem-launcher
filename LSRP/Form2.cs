using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Encodings.Web;
using static System.Text.Encodings.Web.HtmlEncoder;

namespace LSRP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string version = "1.0.0";
        int Move;
        int Mouse_X;
        int Mouse_Y;

        private void Form2_Load(object sender, EventArgs e)
        {
            label5.Text = version;
            label1.Parent = pictureBox1;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.Parent = pictureBox1;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.Parent = pictureBox1;
            label3.BackColor = System.Drawing.Color.Transparent;
            label4.Parent = pictureBox1;
            label4.BackColor = System.Drawing.Color.Transparent;
            label5.Parent = pictureBox1;
            label5.BackColor = System.Drawing.Color.Transparent;
            label6.Parent = pictureBox1;
            label6.BackColor = System.Drawing.Color.Transparent;
            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = System.Drawing.Color.Transparent;
            pictureBox3.Parent = pictureBox1;
            pictureBox3.BackColor = System.Drawing.Color.Transparent;
            pictureBox4.Parent = pictureBox1;
            pictureBox4.BackColor = System.Drawing.Color.Transparent;
            getUpdates();
            getText();
            regQueries();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void getUpdates()
        {
            string link = "https://www.lossantos-rp.com/launcher/update.html";

            Uri url = new Uri(link);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            label4.Text = html;
        }

        private void getText()
        {
            string url = "https://www.lossantos-rp.com/launcher/text.html";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            label4.Text = sr.ReadToEnd();
            sr.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://forum.lossantos-rp.com");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/HpxnGnpVW7");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NP'den çalma ege ya eğvle eğlve ğelve.", "Sikik", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void regQueries()
        {
            RegistryKey uuid2 = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam\", false);
            string nick = uuid2.GetValue("AutoLoginUser").ToString();
            label6.Text = nick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("fivem://connect/localhost");
        }
    }
}
