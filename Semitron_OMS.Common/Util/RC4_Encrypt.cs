using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semitron_OMS.Common
{
    public class RC4_Encrypt
    {
        //string g_szKey = "TCD12345678";
        //RC4KEY rc4key = new RC4KEY();
        List<int> state = new List<int>();

        /// <summary>
        /// 交换数据
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public  void SwapByte(int a, int i, int b, int index)
        {
            int swapByte = a;
            a = b;
            b = swapByte;

            state[i] = a;
            state[index] = b;
        }

        /// <summary>
        /// 准备密钥
        /// </summary>
        public void PrepareKey(string pKeyData, int iKeyDataLen, RC4KEY rc4key)
        {
            //state = pKey.State;

            for (int i = 0; i < 256; i++)
            //for (int i = 0; i < 10; i++)
            {
                int ii = i & 0xFF;
                state.Add(i & 0xFF);
            }
            rc4key.X = 0;
            rc4key.Y = 0;
            int index1 = 0;
            int index2 = 0;

            for (int i = 0; i < 256; i++)
            //for (int i = 0; i < 10; i++)
            {
                int intKeyChar = 0;
                char keyChar = '0';
                if (index1 == pKeyData.Length)
                {
                    intKeyChar = 0;
                }
                else
                {
                    keyChar = pKeyData[index1];
                    intKeyChar = (int)keyChar;
                }
                //int test = (int)keyChar;
                index2 = (intKeyChar + state[i] + index2) % 256;
                SwapByte(state[i], i, state[index2], index2);

                index1 = (index1 + 1) % iKeyDataLen;
            }
            rc4key.State = state;
        }
        /// <summary>
        /// 加密操作
        /// </summary>
        /// <param name="pBuffer"></param>
        /// <param name="iBufferLen"></param>
        /// <param name="pKey"></param>
        public char[] RC4_Code(byte[] pBuffer, int iBufferLen, RC4KEY rc4key)
        //public string RC4_Code(byte[] pBuffer, int iBufferLen, RC4KEY rc4key)
        {
            #region 
            //int x = rc4key.X;
            //int y = rc4key.Y;

            //List<int> state = rc4key.State;
            //char[] tempchar = new char[iBufferLen];
            //for (int i = 0; i < iBufferLen; i++)
            //{
            //    x = (x + 1) % 256;
            //    y = (state[x] + y) % 256;

            //    SwapByte(state[x], x, state[y], y);

            //    int index = (state[x] + state[y]) % 256;
            //    byte t = pBuffer[i];
            //    char char_str = (char)((Convert.ToInt32(pBuffer[i], 2) ^ state[index]));

            //    tempchar[i] = char_str;

            //}

            //rc4key.X = x;
            //rc4key.Y = y;
            //return tempchar;

            //int x = rc4key.X;
            //int y = rc4key.Y;

            //List<int> state = rc4key.State;

            //char[] charArryBuffer = new char[iBufferLen]; ;
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < iBufferLen; i++)
            //{
            //    x = (x + 1) % 256;
            //    y = (state[x] + y) % 256;

            //    SwapByte(state[x], x, state[y], y);
            //    //int x1 = state[x];
            //    //int y1 = state[y];
            //    //int tt = state[x] + state[y];
            //    int index = (state[x] + state[y]) % 256;

            //    //string keyChar = pBuffer[i].ToString();
            //    //int tttt = Convert.ToInt32(pBuffer[i]);
            //    //int ttt = Convert.ToInt32(pBuffer[i]) ^ state[index];
            //    char test = (char)((Convert.ToInt32(pBuffer[i]) ^ state[index]));
            //    //string s = pBuffer[i].ToString();
            //    //s = s.Replace(s, test.ToString());
            //    //pBuffer[i] = Convert.ToByte(test) ;
            //    //sb.Append(test);
            //    charArryBuffer[i] = test; //Convert.ToChar(Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(keyChar)));
            //}
            //rc4key.X = x;
            //rc4key.Y = y;
            //return charArryBuffer;
            //return sb.ToString();

            #endregion

            int x = rc4key.X;
            int y = rc4key.Y;

            List<int> state = rc4key.State;

            char[] charArryBuffer = new char[iBufferLen];
            
            for (int i = 0; i < iBufferLen; i++)
            {
                x = (x + 1) % 256;
                y = (state[x] + y) % 256;

                SwapByte(state[x], x, state[y], y);
                int index = (state[x] + state[y]) % 256;

                char test = (char)((Convert.ToInt32(pBuffer[i]) ^ state[index]));
                charArryBuffer[i] = test; 
            }
            rc4key.X = x;
            rc4key.Y = y;
            return charArryBuffer;
        }
    }
    /// <summary>
    /// rc4加密算法密钥
    /// </summary>
    public partial class RC4KEY
    {
        List<int> state;

        public List<int> State
        {
            get { return state; }
            set { state = value; }
        }
        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
