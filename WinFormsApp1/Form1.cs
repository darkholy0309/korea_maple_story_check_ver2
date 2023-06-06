using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Size = new Size(650, 180);
            main_();
            label1.Location = new Point(10, 10);
            label2.Location = new Point(10, 40);
            label3.Location = new Point(140, 70);
            label4.Location = new Point(340, 70);
            label1.Font = new Font("dotum", 14);
            label2.Font = new Font("dotum", 14);
            label3.Font = new Font("dotum", 14);
            label4.Font = new Font("dotum", 14);
            button1.Location = new Point(150, 100);
            button2.Location = new Point(250, 100);
            button3.Location = new Point(350, 100);
            button1.Text = "홈페이지";
            button2.Text = "다시체크";
            button3.Text = "사이트";
            /*
            2023.06
            net7 / visual studio 2022 ver 17.6
            http://gamebulletin.nexon.com/maplestory/inspection3.html
            http://gamebulletin.nexon.com/maplestory/game3.html
            hapxpathfinder
            */
        }

        void main_()
        {
            RegistryKey localmachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            var opensubkey = localmachine.OpenSubKey("software\\microsoft\\windows nt\\currentversion");
            int currentbuild = Convert.ToInt32(opensubkey.GetValue("currentbuild"));
            if (currentbuild < 19045)
            {
                MessageBox.Show("windows 10 update 22H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                maple();
                maplet();
                maple_ip_check();
            }
            else
            {
                MessageBox.Show("인터넷확인");
                Environment.Exit(0);
            }
            localmachine.Dispose();
        }

        void maple()
        {
            string text = null;
            HtmlAgilityPack.HtmlDocument htmldocument = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb htmlweb = new HtmlAgilityPack.HtmlWeb();
            htmldocument = htmlweb.Load("https://maplestory.nexon.com/news/notice/inspection");
            var innertext = htmldocument.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div/div[1]/div[2]/ul/li[1]/p/a/span").InnerText.Trim();
            if (innertext == null)
            {
                text = "점검중입니다.";
            }
            else
            {
                if (innertext.Substring(0, 9) == "(연장)[패치중]")
                {
                    text = innertext;
                }
                if (innertext.Substring(0, 6) == "[패치예정]")
                {
                    text = innertext;
                }
                if (innertext.Substring(0, 5) == "[패치중]")
                {
                    text = innertext;
                }
                if (innertext.Substring(0, 9) == "(연장)[점검중]")
                {
                    text = innertext;
                }
                if (innertext.Substring(0, 6) == "[점검예정]")
                {
                    text = innertext;
                }
                if (innertext.Substring(0, 5) == "[점검중]")
                {
                    text = innertext;
                }
                label1.Text = text;
                if (text == null)
                {
                    label1.Text = "공지없음";
                }
            }
        }

        void maplet()
        {
            string text2 = null;
            HtmlAgilityPack.HtmlDocument htmldocument = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlWeb htmlweb = new HtmlAgilityPack.HtmlWeb();
            htmldocument = htmlweb.Load("https://maplestory.nexon.com/testworld/notice");
            var innertext2 = htmldocument.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div/div[2]/div/h1").InnerText.Trim();
            if (innertext2 == null)
            {
                text2 = "점검중입니다.";
            }
            else
            {
                if (innertext2.Substring(0, 14) == "테스트월드 운영기간입니다.")
                {
                    var innertext_121 = htmldocument.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div/div[1]/div[2]/ul/li[1]/p/a/span").InnerText.Trim();
                    if (innertext_121 == null)
                    { }
                    else
                    {
                        if (innertext_121.Substring(0, 4) == "[오픈]")
                        {
                            text2 = innertext_121;
                        }
                    }
                    var innertext_122 = htmldocument.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div/div[1]/div[2]/ul/li[2]/p/a/span").InnerText.Trim();
                    if (innertext_122 == null)
                    { }
                    else
                    {
                        if (innertext_122.Substring(0, 4) == "[오픈]")
                        {
                            text2 = innertext_122;
                        }
                    }
                    var innertext_123 = htmldocument.DocumentNode.SelectSingleNode("//*[@id=\"container\"]/div/div[1]/div[2]/ul/li[3]/p/a/span").InnerText.Trim();
                    if (innertext_123 == null)
                    { }
                    else
                    {
                        if (innertext_123.Substring(0, 4) == "[오픈]")
                        {
                            text2 = innertext_123;
                        }
                    }
                }
            }
            label2.Text = text2;
        }

        void maple_ip_check()
        {
            string[] ip = new string[11];
            ip[0] = "175.207.0.33";
            ip[1] = "175.207.0.34";
            ip[2] = "175.207.0.35";
            ip[3] = "175.207.0.36";
            ip[4] = "175.207.0.37";
            ip[5] = "175.207.0.38";
            ip[6] = "175.207.0.39";
            ip[7] = "175.207.0.40";
            ip[8] = "175.207.0.41";
            ip[9] = "175.207.0.42";
            ip[10] = "175.207.0.43";
            Random random = new Random();
            int random_number = random.Next(0, ip.Length);
            string ip_list = ip[random_number];
            TcpClient tcpclient = new TcpClient();
            int port = 8484;//포트 설정
            int second_want = 1;//초단위 설정
            TimeSpan timespan = TimeSpan.FromSeconds(second_want);
            if (tcpclient.ConnectAsync(ip_list, port).Wait(timespan))
            {
                label3.Text = "서버 정상";
            }
            else
            {
                label3.Text = "서버 연결실패";
            }
            TcpClient tcpclient2 = new TcpClient();
            string test_server_ip = "175.207.2.136";
            if (tcpclient2.ConnectAsync(test_server_ip, port).Wait(timespan))
            {
                label4.Text = "테섭 정상";
            }
            else
            {
                label4.Text = "테섭 연결실패";
            }
            tcpclient.Close();
            tcpclient.Dispose();
            tcpclient2.Close();
            tcpclient2.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            string chrome = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application";
            string msedge = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\microsoft\\edge\\application";
            string url = "http://www.mapleportal.net";
            if (Directory.Exists(chrome))
            {
                process.StartInfo.FileName = chrome + "\\chrome";
                process.StartInfo.Arguments = "/new-window /incognito" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
            else
            {
                process.StartInfo.FileName = msedge + "\\msedge";
                process.StartInfo.Arguments = "/new-window /inprivate" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                button2.Enabled = false;
                label1.Text = null;
                label2.Text = null;
                label3.Text = null;
                label4.Text = null;
                maple();
                maplet();
                maple_ip_check();
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("인터넷확인");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            string chrome = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application";
            string msedge = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\microsoft\\edge\\application";
            string url = "http://maplestory.nexon.com/home/main";
            if (Directory.Exists(chrome))
            {
                process.StartInfo.FileName = chrome + "\\chrome";
                process.StartInfo.Arguments = "/new-window /incognito" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
            else
            {
                process.StartInfo.FileName = msedge + "\\msedge";
                process.StartInfo.Arguments = "/new-window /inprivate" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
        }
    }
}