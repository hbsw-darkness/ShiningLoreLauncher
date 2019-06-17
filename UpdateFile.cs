using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ShiningLoreLauncher.Class
{
    class UpdateFile
    {
        //메인 폼에 있는 텍스트라벨을 갱신하기 위해 델리게이트 사용
        public delegate void uptext(string upLabelText);
        public event uptext ReturnToText;

        //메인 폼에 있는 버튼 제어
        public delegate void startBtnSet(bool set);
        public event startBtnSet startBtnSetM;

        public delegate void optionBtnSet(bool set);
        public event optionBtnSet optionBtnSetM;

        //파일 개수 확인
        int fileCount = 0;

        //파일 다운로드 메소드
        private void updateSet()
        {

            MessageBox.Show("업데이트가 있습니다. 서버 상황에 따라 약 1~5분이 소요됩니다.\n확인버튼을 누르시면 업데이트를 시작합니다.");

            string[] sefileName = {"Budt.bin", "Dhs.bin", "Dkjsin.bin", "Ehdt.bin", "Ikdmn.bin", "Lkezmd.bin",
                "Osa.bin" , "Qods.bin" , "SlOnline.exe", "ClientVersion.txt"};

            string[] cefileName = { @"\Data\Budt.bin", @"\Data\Dhs.bin", @"\Data\Dkjsin.bin", @"\Data\Ehdt.bin", @"\Data\Ikdmn.bin", @"\Data\Lkezmd.bin",
                @"\Data\Osa.bin" , @"\Data\Qods.bin" , @"\SlOnline.exe", @"\Data\ClientVersion.txt"};

            for (int i = 0; i < sefileName.Length; i++)
            {
                //자동 업데이트 기능. 동적할당
                WebClient webFileDown = new WebClient();
                //서버에 있는 txt 파일을 읽어서 해시코드 호출한다.

                webFileDown.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

                webFileDown.DownloadFileAsync(new Uri(@"http://youid.iptime.org:9999/updateFile/" + sefileName[i]), Application.StartupPath + cefileName[i]);

                webFileDown.Dispose();

                //ReturnToText("파일 다운로드중.. (" + (fileCount / 2) + "/" + "9)");
            }
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            string fileName = (string)e.UserState;
            int Percentage = e.ProgressPercentage;                   // 비동기 작업의 진행을 나타내는 백분율 값입니다.
            long TotalBytesToReceive = e.TotalBytesToReceive;  // 다운받아야 할 데이터 길이입니다.
            long BytesReceived = e.BytesReceived;// 현재까지 다운 받은 데이터 길이입니다.

            startBtnSetM(false);
            optionBtnSetM(false);

            if (TotalBytesToReceive == BytesReceived)
            {
                fileCount += 1;
                ReturnToText("파일 다운로드중.. (" + (fileCount / 2) + "/" + "10) ");
            }

            if ((fileCount / 2) >= 10)
            {
                startBtnSetM(true);
                optionBtnSetM(true);
                //StartBtn.Enabled = true;
                //Option.Enabled = true;
                ReturnToText("업데이트 완료!");
            }
        }


        //버전 확인 메소드
        private void versionTextCheck()
        {
            String mainStr;
            String bufStr;

            int mainInt;
            int subInt;
            //서버에 있는 버전 파일 내용을 읽어온다.
            WebClient webFileRead = new WebClient();

            //읽어온 파일의 내용을 잘라서 버퍼에 저장한다.
            mainStr = webFileRead.DownloadString(new Uri(@"http://youid.iptime.org:9999/updateFile/ClientVersion.txt")).Substring(16);

            webFileRead.Dispose();

            //파일 사이즈 검사 가끔 업데이트하다 종료할 경우 버전 파일이 다운안되서 사이즈가 0 일때를 대비함.
            FileInfo fInfo = new FileInfo(Application.StartupPath + @"\Data\ClientVersion.txt");
            //파일 사이즈가 0일때 파일 생성하고 업데이트 체크 함수 호출
            if (fInfo.Length == 0)
            {
                System.IO.StreamWriter objSaveFile;
                objSaveFile = new System.IO.StreamWriter(Application.StartupPath + @"\Data\ClientVersion.txt");
                objSaveFile.WriteLine("Client Vercion = 20150829");
                objSaveFile.Close();
                updateCheck();
            }
            //버전 텍스트 파일 사이즈가 0이 아닐떄
            else
            {
                System.IO.StreamReader objReadFile;
                //현재 폴더에 있는 버전 파일 읽어온다.
                objReadFile = new System.IO.StreamReader(Application.StartupPath + @"\Data\ClientVersion.txt");

                //읽어온 파일의 내용을 잘라서 버퍼에 저장한다.
                bufStr = objReadFile.ReadLine().Substring(16);

                //자른 문자를 int형으로 형변환 한다.
                mainInt = int.Parse(mainStr);
                subInt = int.Parse(bufStr);
                objReadFile.Close();

                //비교 버전이 같지않으면 업데이트 실행
                if (mainInt != subInt)
                {
                    updateSet();
                }
                else
                {
                    //ReturnToText("최신버전입니다.");
                }
            }
        }

        //버전 확인 파일 유무 체크 및 파일 버전 체크
        public void updateCheck()
        {
            //버전 텍스트 파일이 있는지 확인하고 없으면 생성 있으면 바로 버전체크
            FileInfo fIlecheck = new FileInfo(Application.StartupPath + @"\Data\ClientVersion.txt");

            string sDirPath;
            sDirPath = Application.StartupPath + "\\Data";
            DirectoryInfo di = new DirectoryInfo(sDirPath);

            if (di.Exists == false)
            {
                di.Create();
            }
            //파일이 있다면
            if (fIlecheck.Exists == true)
            {
                versionTextCheck();

            }
            //파일이 없다면
            else
            {
                System.IO.StreamWriter objSaveFile;
                objSaveFile = new System.IO.StreamWriter(Application.StartupPath + @"\Data\ClientVersion.txt");
                objSaveFile.WriteLine("Client Vercion = 20150829");
                objSaveFile.Close();
                updateCheck();
            }
            //System.IO.StreamWriter objSaveFile = new System.IO.StreamWriter(@"version.txt");
        }
    }
}
