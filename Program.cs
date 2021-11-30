using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.MQTT.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //要了解编码格式,首先必须理清两个概念:一个是什么是字符集;另一个是什么是编码格式。
            //字符集:是一个字符和数字(这个数字也可以称之为代码号)的对应表,表示每一个字符都对应着一个数字(其实它就是一张表)
            //编码格式:是指这些字符对应的数字在计算机中如何储存
            //常见的字符集有:Ascii、GB2312(简体,到后来的GB18030)、Big5(繁体)、MBCS、Unicode
            //常见的编码格式有:Ascii、ANSI(MBCS)、Unicode(LittleEndian,小头)、BigEndainUnicode(大头)、UTF7、UTF8、UTF16、UTF32
            //像GB2312、GB18030、Big5也有自己编码格式
            //以下都以"测试ceshi"为例对各种编码格式进行测试

            //1、ASCII编码格式（一个字符一个字节）
            //{Ascii是算最早的一种字符集,一个字符用一个字节来表示,不过它只能表示部分字符,像中文及其他国家的一些字符并不能表示}
            //Ascii字符集下的Ascii编码格式:
            //System.Text.Encoding有这些编码方式Default.ASCII.Unicode.UTF32.UTF7.UTF8
            string msg = "测试ceshi";//7个字符
            string receive = "";
            List<byte> lst = new List<byte>();
            lst.AddRange(System.Text.Encoding.ASCII.GetBytes(msg));
            lst.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(msg));     // ASCIIEncoding : Encoding
            lst.AddRange(System.Text.UTF8Encoding.ASCII.GetBytes(msg));      //UTF8Encoding : Encoding
            lst.AddRange(System.Text.UnicodeEncoding.ASCII.GetBytes(msg));   // UnicodeEncoding : Encoding
            lst.AddRange(System.Text.UTF32Encoding.ASCII.GetBytes(msg));     //UTF32Encoding : Encoding
            lst.AddRange(System.Text.UTF7Encoding.ASCII.GetBytes(msg));      //UTF7Encoding : Encoding
            //GetBytes实际都执行的Encoding类中的方法，
            //对中文的处理结果都是以0x3F（63）返回
            //转化出来是乱码  //??ceshi??ceshi??ceshi??ceshi??ceshi??ceshi
            string str2 = System.Text.Encoding.ASCII.GetString(lst.ToArray());
            //3F-3F-63-65-73-68-69-3F-3F-63-65-73-68-69-3F-3F-63-65-73-68-69-3F-3F-63-65-73-68-69-3F-3F-63-65-73-68-69-3F-3F-63-65-73-68-69  42byte
            receive = BitConverter.ToString(lst.ToArray());

            /* {基于Ascii字符集的缺点,各国为了表示自己国家的字符,都出现了自己的一套编码方案,像我国的GB18030(简体)、
             * Big5(繁体)等;为了兼容各国的编码方案,就出现了ANSI(即MBCS)字符集,MBCS用多字节表示,比如英文用1个字节表示,
             * 汉字用两个字节表示),算是Unicode字符集的前身}*/
            /* {Unicode字符集才真正统一了世界各国的编码方案,对世界上所有的字符进行了重新编码,
             * 一个字符用2个字节表示(即UCS2);UCS4顾名思义就是一个字符用4个字节;
               像Unicode(LittleEndian,小头)、BigEndainUnicode(大头)、
             * UTF7、UTF8、UTF16、UTF32都是以Unicode字符集为基础进行编码格式的}
               Unicode和BigEndianUnicode都是用2个字节对一个字符进行编码格式;只不过它们储存字符的顺序正好相反*/
            //2、Unicode编码格式（一个字符两个字节）:
            List<byte> listmsg = new List<byte>(System.Text.Encoding.Unicode.GetBytes(msg));//小头
            receive = BitConverter.ToString(listmsg.ToArray());
            //（14byte）
            //str=4B-6D-D5-8B-  63-00-65-00-73-00-68-00-69-00 （测=4B 6D，c=63 00 一个字符两个字节）
            List<byte> listmsgB = new List<byte>(System.Text.Encoding.BigEndianUnicode.GetBytes(msg));//大头
            receive = BitConverter.ToString(listmsgB.ToArray());
            //（14byte）
            //str=6D-4B-8B-D5-  00-63-00-65-00-73-00-68-00-69（顺序相反）

            //3、UTF7编码格式（汉字4byte，英文1byte）,目前主要用于邮件方面:
            listmsg = new List<byte>(System.Text.Encoding.UTF7.GetBytes(msg));
            receive = BitConverter.ToString(listmsg.ToArray());
            //（13byte）2B-62-55-75-4C-31-51-2D-  63-65-73-68-69

            //4、UTF8编码格式（汉字3byte，英文1byte）,//UTF8编码格式,目前是最常用的
            //转化为byte
            listmsg = new List<byte>(System.Text.Encoding.UTF8.GetBytes(msg));
            //转化为string
            receive = System.Text.Encoding.UTF8.GetString(listmsg.ToArray());   //转化回来
            receive = BitConverter.ToString(listmsg.ToArray());                 //十六进制字符串
            //E6-B5-8B-E8-AF-95-  63-65-73-68-69
            byte[] Length = BitConverter.GetBytes(0x7D02);

            Console.ReadLine();
        }
    }
}
