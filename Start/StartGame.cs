using ShiningLoreLauncher.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

/*
게임 시작 버튼 입력하였을 때 동작 처리 클래스
cmd를 이용하여 게임을 실행한다.
*/

namespace ShiningLoreLauncher
{
    class StartGame
    {
        public static object MessgeBox { get; private set; }

        //해상도 값에 따른 세팅하며 게임 실행 함수 호출
        public static void resolution()
        {
            System.IO.StreamReader objReadFile;
            objReadFile = new System.IO.StreamReader(Application.StartupPath + @"\LauncherSet.txt");
            ChangeDnsToIp cdt = new ChangeDnsToIp();

            //런처셋txt 값 읽어와서 전체화면/창모드 실행
            switch (int.Parse((objReadFile.ReadLine().ToString())))
            {
                case 0:
                    fullMode(cdt.ipAddressGet());
                    break;
                case 1:
                    winMode(cdt.ipAddressGet());
                    break;
            }
        }

        //전체화면으로 게임 실행
        public static void fullMode(string ipAddressName)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();

            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.CreateNoWindow = true;
            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;
            cmd.RedirectStandardInput = true;
            cmd.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();

            process.StandardInput.Write(@"start SlOnline.exe /updatecomplete: /gwip:" + ipAddressName + Environment.NewLine);

            process.WaitForExit(500);
            /*
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            
            System.Diagnostics.Process.Start("cmd.exe", "/C SlOnline.exe" + " /updatecomplete: /gwip:" + ipAddressName);

            
            System.Threading.Thread.Sleep(500);
            Process[] p = Process.GetProcessesByName("cmd");
            if (p.GetLength(0) > 0)
            {
                p[0].Kill();
            }
            */
        }

        //창모드로 게임 실행
        public static void winMode(string ipAddressName)
        {
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();

            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.CreateNoWindow = true;
            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;
            cmd.RedirectStandardInput = true;
            cmd.RedirectStandardError = true;

            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();

            process.StandardInput.Write(@"start SlOnline.exe /updatecomplete: /win/gwip:" + ipAddressName + Environment.NewLine);

            process.WaitForExit(500);

            /*
            ProcessStartInfo cmd = new ProcessStartInfo();
            Process process = new Process();

            cmd.FileName = @"SlOnline.exe /updatecomplete: /win/gwip:" + ipAddressName;
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.CreateNoWindow = true;
            //cmd.UseShellExecute = false;

            //process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            //process.StandardInput.Close();
            */
            /*
            System.Diagnostics.Process.Start("cmd.exe", "/C SlOnline.exe" + " /updatecomplete: /win/gwip:" + ipAddressName);

            System.Threading.Thread.Sleep(500);

            Process[] p = Process.GetProcessesByName("cmd");
            if (p.GetLength(0) > 0)
            {
                p[0].Kill();
            }
            */
        }
    }
}
