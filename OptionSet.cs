using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShiningLoreLauncher.Class
{
    partial class OptionSet
    {

        //창모드 전체화면 설정값
        public static int resolutionNum = 1;
        //옵션 체크용
        public static bool optionCheck = false;
        
        //옵션 메소드
        public static void optionSet()
        {
            Form form2 = new Form();
            CheckBox cb1 = new CheckBox();
            RadioButton rb1 = new RadioButton();
            RadioButton rb2 = new RadioButton();
            RadioButton rb3 = new RadioButton();
            Button okBt = new Button();
            PictureBox pk1 = new PictureBox();

            Image imageSet = ShiningLoreLauncher.Properties.Resources.dsa;


            cb1.Text = "창모드";
            rb1.Text = "800 x 600";
            rb2.Text = "1024 x 768";
            rb3.Text = "1152 x 864";
            okBt.Text = "설정완료";

            //폼 사이즈 고정
            form2.FormBorderStyle = FormBorderStyle.FixedSingle;
            form2.MaximizeBox = false;

            form2.Size = new Size(250, 200);
            cb1.Size = new Size(100, 30);
            rb1.Size = new Size(100, 30);
            rb2.Size = new Size(100, 30);
            rb3.Size = new Size(100, 30);
            okBt.Size = new Size(100, 30);
            pk1.Size = new Size(147, 192);



            cb1.Location = new Point(0, 0);
            rb1.Location = new Point(0, 30);
            rb2.Location = new Point(0, 60);
            rb3.Location = new Point(0, 90);
            okBt.Location = new Point(0, 120);
            pk1.Location = new Point(100, 0);



            pk1.Image = imageSet;


            form2.Controls.Add(cb1);
            form2.Controls.Add(rb1);
            form2.Controls.Add(rb2);
            form2.Controls.Add(rb3);
            form2.Controls.Add(okBt);
            form2.Controls.Add(pk1);

            rb1.Checked = true;
            cb1.Checked = true;

            okBt.Click += delegate (object sender, EventArgs args)
            {
                //해상도 설정

                RegistryKey reg = Registry.CurrentUser;
                reg = reg.OpenSubKey(@"Software\Phantagram\Shining Lore Online", true);

                if (rb1.Checked == true)
                {
                    if (reg == null)
                    {
                        MessageBox.Show("error : 해상도를 설정할 수 없습니다.");
                        return;
                    }
                    else
                    {
                        reg.SetValue("Resolution", 800);
                    }
                }
                else if (rb2.Checked == true)
                {
                    if (reg == null)
                    {
                        MessageBox.Show("error : 해상도를 설정할 수 없습니다.");
                        return;
                    }
                    else
                    {
                        reg.SetValue("Resolution", 1024);
                    }
                }
                else if (rb3.Checked == true)
                {
                    if (reg == null)
                    {
                        MessageBox.Show("error : 해상도를 설정할 수 없습니다.");
                        return;
                    }
                    else
                    {
                        reg.SetValue("Resolution", 1152);
                    }
                }

                //창모드 전체화면 설정
                if (cb1.Checked == true)
                {
                    resolutionNum = 1;
                }
                else
                {
                    resolutionNum = 0;
                }

                //해상도 모드를 설정하면 설정값을 저장하도록 런처셋 파일을 생성한다.
                System.IO.StreamWriter objSaveFile = new System.IO.StreamWriter(Application.StartupPath + @"\LauncherSet.txt");
                objSaveFile.WriteLine(resolutionNum);
                objSaveFile.Close();

                optionCheck = false;
                form2.Close();
            };

            form2.Show();

            optionCheck = true;
        }

    }
}
