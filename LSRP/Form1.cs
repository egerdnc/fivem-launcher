using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSRP
{
    public partial class Form1 : Form
    {
        int Move;
        int Mouse_X;
        int Mouse_Y;
        public Form1()
        {
            InitializeComponent();
            Init_Data();
            TarihveSaat();
        }
        ToolTip toolTip1 = new ToolTip();
        string version = "1.0.0";
        bool banned = false;

        private void getUpdate()
        {
            Application.Run(new Form3());
        }
        private Boolean checkUpdate()
        {
            Boolean versionStatu;
            try
            {
                WebClient client = new WebClient();
                Stream str = client.OpenRead("https://www.lossantos-rp.com/launcher/update.php?update=" + version);
                StreamReader reader = new StreamReader(str);
                String content = reader.ReadToEnd();
                versionStatu = (content == "updateat") ? versionStatu = true : versionStatu = false;
            }
            catch { versionStatu = false; }
            return versionStatu;
        }
        private void askUpdate()
        {

            if (checkUpdate())
            {
                DialogResult dr = MessageBox.Show("New update is available. \n\r Would you like to install it now?", "Update found", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(getUpdate));
                    thread.Start();
                    this.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            askUpdate();
            textBox2.PasswordChar = '*';
            label1.Parent = pictureBox1;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.Parent = pictureBox1;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.Parent = pictureBox1;
            label3.BackColor = System.Drawing.Color.Transparent;
            label5.Parent = pictureBox1;
            label5.BackColor = System.Drawing.Color.Transparent;
            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = System.Drawing.Color.Transparent;
            pictureBox3.Parent = pictureBox1;
            pictureBox3.BackColor = System.Drawing.Color.Transparent;
            label3.Text = version;
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ban();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://forum.lossantos-rp.com/index.php?lost-password/");
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void steam()
        {
            if (Process.GetProcessesByName("steam").Length > 0)
            {

            }
            else
            {
                MessageBox.Show("Launcher'in Çalışabilmesi İçin" + Environment.NewLine + "Steam aktif olmalıdır.", "LSRP - Launcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void Init_Data()
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                if (Properties.Settings.Default.Remember == true)
                {
                    textBox1.Text = Properties.Settings.Default.Username;
                    checkBox1.Checked = true;
                }
                else
                {
                    textBox1.Text = Properties.Settings.Default.Username;
                }
            }
        }

        private void Save_Data()
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.Username = textBox1.Text.Trim();
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = "hover true";
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "hover false";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            steam();
            TarihveSaat();
        }

        private void TarihveSaat()
        {
            label5.Text = DateTime.Now.ToString(); // tarih ve saat
        }

        public void kapatFivem()
        {

            Process[] localByName = Process.GetProcessesByName("FiveM_GTAProcess");
            foreach (Process p in localByName)
            {
                p.Kill();
            }

        }

        private void ban()
        {
            if (banned == true)
            {
                MessageBox.Show("Bu sunucudan yasaklandınız" + Environment.NewLine + "Bağlantı İptal Ediliyor.", "LSRP - Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                kapatFivem();
                this.Close();
            }
            else
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Kullanıcı adı veya şifre bölümü boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Kullanıcı adı veya şifre bölümü boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Save_Data();
                    this.Hide();

                    Form2 frm2 = new Form2();

                    frm2.Show();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Launchersiz Giriş Engelleme Snippeti
            //  string URL = "https://download.flundar.com/sydreslauncher/delete.php";
            //WebClient webClient = new WebClient();
            //NameValueCollection veri = new NameValueCollection();
            //veri["ip"] = label3.Text;
            //byte[] gonder = webClient.UploadValues(URL, "POST", veri);
            //string islem = Encoding.UTF8.GetString(gonder);
            //lblUyari.Text = islem;
            //webClient.Dispose();
            kapatFivem();
        }

        private void cpuid()
        {
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                string cpuInfo = managObj.Properties["processorID"].Value.ToString();
               // cpu.Text = cpuInfo.ToString();
                break;
            }
        }

        private void mac()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.
            GetAllNetworkInterfaces())
            {
                var macAddress = networkInterface.GetPhysicalAddress().ToString();
                if (macAddress != string.Empty)
                {
                    //label6.Text = macAddress;
                }
            }
        }


        private void hwid()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            mbsList = mos.Get();
            string processorId = string.Empty;
            foreach (ManagementBaseObject mo in mbsList)
            {
                processorId = mo["ProcessorID"] as string;
            }

            mos = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");
            mbsList = mos.Get();
            string systemId = string.Empty;
            foreach (ManagementBaseObject mo in mbsList)
            {
                systemId = mo["UUID"] as string;
            }

            var compIdStr = $"{processorId}{systemId}";
            //label5.Text = "sysid :" + systemId;
        }

        public string toHex(long bit)
        {
            return "steam:" + bit.ToString("X").ToLower();
        }

        public bool steam2 = false;

        public string hex;

        public void steambid()
        {
            try
            {
                long num = long.Parse(Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Valve").OpenSubKey("Steam").OpenSubKey("ActiveProcess").GetValue("ActiveUser").ToString());
                num += 76561197960265728L;
                bool flag = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Valve").OpenSubKey("Steam").OpenSubKey("ActiveProcess").GetValue("ActiveUser").ToString() == "0";
                if (flag)
                {
                    this.steam2 = false;
                }
                else
                {
                    this.hex = this.toHex(num).ToString();
                    this.steam2 = true;
                   // label10.Text = "Steam Dec:" + num.ToString();
                }
            }
            catch
            {
                this.steam2 = false;
            }
        }

        private void AntiSuspend_DoWork()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.Responding)
                {

                }
                else
                {
                    this.Close();
                }
            }

        }
    }
}
// By Sydres#9887 made with ❤️ at 🌍
