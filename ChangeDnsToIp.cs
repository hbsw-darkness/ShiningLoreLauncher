using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

/*
게임 접속시 DNS를 IP로 변경하여 게임 서버 접속요청 클래스
*/
namespace ShiningLoreLauncher
{
    class ChangeDnsToIp
    {
        public string ipAddressGet()
        {
            //DNS 이름 저장
            string HostName = "";

            //ip 주소 저장
            string ipAddressName = "";

            HostName = "youid.iptime.org";
            //실제 name 을 가져오기 위해 IPHostEntry을 이용

            IPHostEntry hostEntry = Dns.GetHostEntry(HostName);

            //IP 주소를 찾는다.
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                ipAddressName = ip.ToString();
            }
            return ipAddressName;
        }
    }
}
