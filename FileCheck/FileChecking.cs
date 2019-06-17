using ShiningLoreLauncher.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

/*
sha1 을 이용하여 서버와 클라이언트간의 위변조 검출 클래스
*/

namespace ShiningLoreLauncher
{
    class FileChecking
    {
        //해시코드 생성하여 리턴
        public static string GetChecksum(string sPathFile)
        {
            if (!File.Exists(sPathFile))
            {
                return null;
            }

            using (FileStream stream = File.OpenRead(sPathFile))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] byteChecksum = md5.ComputeHash(stream);
                return BitConverter.ToString(byteChecksum).Replace("-", String.Empty);
            }
        }

        internal static string GetChecksum(object p)
        {
            throw new NotImplementedException();
        }

        //클라이언트와 서버간의 해시코드 대조
        public static bool CheckSumFIle()
        {
            //파일 체크 변수
            string celFileCheck; //플레이 컴퓨터파일 해시코드
            string serFileCheck; // 서버 컴퓨터파일 해시코드\

            bool fileCheckBool = false;
            //서버 클라이언트 해시코드
            string[] serfileName = {"Budt.txt", "Dhs.txt", "Dkjsin.txt", "Ehdt.txt", "Ikdmn.txt", "Lkezmd.txt",
                "Osa.txt" , "Qods.txt" , "SlOnline.txt"};
            //클라이언트 해시코드
            string[] celfileName = { @"\Data\Budt.bin", @"\Data\Dhs.bin", @"\Data\Dkjsin.bin", @"\Data\Ehdt.bin", @"\Data\Ikdmn.bin", @"\Data\Lkezmd.bin",
                @"\Data\Osa.bin" , @"\Data\Qods.bin" , @"\SlOnline.exe"};

            //서버 해시코드 대조하기 위해 해시코드 호출
            WebClient webFileRead = new WebClient();

            for (int i = 0; i < serfileName.Length; i++)
            {
                //서버에 있는 txt 파일을 읽어서 해시코드 호출한다.
                Stream stream = webFileRead.OpenRead(new Uri(@"http://youid.iptime.org:9999/updateFile/" + serfileName[i]));
                StreamReader reader = new StreamReader(stream);
                serFileCheck = reader.ReadLine();
                webFileRead.Dispose();

                //클라이언트에 있는 파일 해시코드를 생성하고 변수에 저장
                celFileCheck = FileChecking.GetChecksum(Application.StartupPath + celfileName[i]);

                if (celFileCheck != serFileCheck)
                {
                    MessageBox.Show("클라이언트 개조가 발견되었습니다. 프로그램을 종료합니다.");
                    return fileCheckBool = true;
                }
                else
                {
                    //MessageBox.Show("깨끗한 클라이언트입니다.");
                }
            }

            return fileCheckBool;
        }
    }
}
