using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Common
{
    public class SysLog
    {
        private static object m_syn = new object();
        /// <summary>
        /// Byte 数组转十六进制字符串
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] Bytes)
        {
            string str = string.Empty;
            foreach (byte Byte in Bytes)
            {
                str += string.Format("{0:X2}", Byte) + " ";
            }
            return str.Trim();
        }

        public static void setSystemLog(string czlr, string pathname = "log", string filename = "SystemLog")
        {
            StreamWriter m_streamWriter = null;
            FileStream fs = null;
            try
            {
                Monitor.Enter(m_syn);
                string pathphe = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + pathname;
                if (!System.IO.Directory.Exists(pathphe))
                {
                    System.IO.Directory.CreateDirectory(pathphe);
                }
                String FileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + pathname + "\\" + System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString().PadLeft(2, '0') + System.DateTime.Today.Day.ToString().PadLeft(2, '0') + filename + ".txt";
                fs = new FileStream(@FileName, FileMode.OpenOrCreate, FileAccess.Write);
                m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                m_streamWriter.WriteLine(DateTime.Now.ToString() + "内容：" + czlr);
                m_streamWriter.Flush();

            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (m_streamWriter != null)
                    {
                        m_streamWriter.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    m_streamWriter = null;
                    fs = null;
                }
                catch { }
                Monitor.Exit(m_syn);
            }
        }
        public static void setUploadLTMLog(string czlr)
        {
            setSystemLog(czlr, "logUpload", "AboutLTMlog");
        }
        public static void setUploadQASLog(string czlr)
        {
            setSystemLog(czlr, "logUpload", "AboutQASlog");
        }
        public static void setUploadSAPLog(string czlr) 
        {
            setSystemLog(czlr, "logUpload", "AboutSAPlog");
        }

        public static void setUploadNCPLog(string czlr)
        {
            setSystemLog(czlr, "logUpload", "AboutNCPlog");
        }
        /// <summary>
        /// 向文件中写入要发送到LED的内容 （For 东莞）
        /// </summary>
        /// <param name="czlr"></param>
        public static void seErrorInfo(string czlr)
        {
            StreamWriter m_streamWriter = null;
            FileStream fs = null;
            try
            {
                Monitor.Enter(m_syn);
                string pathphe = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "error";
                if (!System.IO.Directory.Exists(pathphe))
                {
                    System.IO.Directory.CreateDirectory(pathphe);
                }
                String FileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "error\\" + System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString().PadLeft(2, '0') + System.DateTime.Today.Day.ToString().PadLeft(2, '0') + "SystemLog.txt";
                fs = new FileStream(@FileName, FileMode.OpenOrCreate, FileAccess.Write);
                m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                m_streamWriter.WriteLine(DateTime.Now.ToString() + "内容：" + czlr);
                m_streamWriter.Flush();

            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (m_streamWriter != null)
                    {
                        m_streamWriter.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    m_streamWriter = null;
                    fs = null;
                }
                catch { }
                Monitor.Exit(m_syn);
            }
        }

    }
}
