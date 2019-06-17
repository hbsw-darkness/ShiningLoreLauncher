using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
프로세스 리스트를 컨트롤 하는 클래스로
다중 클라이언트 제어를 위한 기능 추가 예정
1인 1클라이언트 혹은 1인 2클라이언트까지만 제어할 예정

게임 시작 버튼 입력 시 클라이언트 개수를 체크하여 실행 제한을 둔다.
    
혹시 편법으로 클라이언트를 일정 개수 이상 실행할 경우도 염두하여
프로세스 리스트를 5분 단위로 체크하여 일정 개수 이상의 클라이언트가
실행 될 경우 모든 게임 종료.
*/

namespace ShiningLoreLauncher
{
    class ProcessControl
    {
        int count = 0;

        //모든 프로세스 리스트 검색
        public void ProcessCheckM(int numCheck)
        {
            Process[] pl = Process.GetProcesses();
            if (numCheck == 0)
            {
                foreach (Process p in pl)
                {
                    WriteProcessInfo(p);
                }
            }
            else if(numCheck == 1)
            {
                foreach (Process p in pl)
                {
                    AllClose(p);
                }
            }
        }

        //프로세스 리스트중에서 샤이닝로어 클라이언트 검출
        private void WriteProcessInfo(Process processInfo)
        {
            string processname = processInfo.ProcessName.ToString();
            if (processname == "SlOnline")
            {
                count++;
            }
            //만약에 샤로가 2개 넘어서 켜질 경우
            if (count > 2 && processname == "SlOnline")
            {
                processInfo.Kill();
                MessageBox.Show("클라이언트가 세개 이상입니다. 샤이닝로어를 강제 종료합니다.");
            }
        }

        public void AllClose(Process processInfo)
        {
            string processname = processInfo.ProcessName.ToString();
            if (processname == "SlOnline")
            {
                processInfo.Kill();
                MessageBox.Show("런처가 종료되었습니다. 모든 샤이닝로어를 종료합니다.");
            }
        }
    }
}
