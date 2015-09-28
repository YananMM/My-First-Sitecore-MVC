// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-27-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-27-2015
// ***********************************************************************
// <copyright file="ValidateCode.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Landmark.Classes
{
    /// <summary>
    /// Class ValidateCode.
    /// </summary>
    public class ValidateCode
    {
        public ValidateCode()
        {
        }

        /// <summary>
        /// 验证码的最大长度
        /// </summary>
        public int MaxLength
        {
            get { return 10; }
        }

        public int MinLength
        {
            get { return 1; }
        }



        /// <summary>
        /// Creates the validate code.
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeLength">Length of the code.</param>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        /// <param name="FontSize">Size of the font.</param>
        /// <returns>System.Byte[][].</returns>
        public byte[] CreateValidateCode(int CodeLength, int Width, int Height, int FontSize, string sCode, Random oRnd)
        {
            //颜色列表，用于验证码，噪点，噪线
            Color[] oColors =
            {
                System.Drawing.Color.Black,
                //System.Drawing.Color.Gray,
                //System.Drawing.Color.White,
                //System.Drawing.Color.Green,
                //System.Drawing.Color.Orange,
                //System.Drawing.Color.Brown,
                //System.Drawing.Color.Brown,
                //System.Drawing.Color.DarkBlue
            };
            //字体列表，用于验证码，这里只写了一种
            string[] oFontNames = { "Times New Roman" };
            Bitmap oBmp = null;
            Graphics oGraphics = null;
            int N1 = 0;
            Point oPoint1 = default(System.Drawing.Point);
            Point oPoint2 = default(System.Drawing.Point);
            string sFontName = null;
            Font oFont = null;
            Color oColor = default(Color);

            //Random oRnd;
            //var sCode = CreateCode(CodeLength, out oRnd);

            oBmp = new Bitmap(Width, Height);
            oGraphics = Graphics.FromImage(oBmp);
            oGraphics.Clear(System.Drawing.Color.White);
            //for (N1 = 0; N1 <= 4; N1++)
            //{
            //    oPoint1.X = oRnd.Next(Width);
            //    oPoint1.Y = oRnd.Next(Height);
            //    oPoint2.X = oRnd.Next(Width);
            //    oPoint2.Y = oRnd.Next(Height);
            //    oColor = oColors[oRnd.Next(oColors.Length)];
            //    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
            //}

            float spaceWith = 0, dotX = 0, dotY = 0;
            if (CodeLength != 0)
            {
                spaceWith = (Width - FontSize * CodeLength - 10) / CodeLength;
            }

            for (N1 = 0; N1 <= sCode.Length - 1; N1++)
            {
                //画验证码字符串
                sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                oFont = new Font(sFontName, FontSize, FontStyle.Italic);
                oColor = oColors[oRnd.Next(oColors.Length)];

                dotY = (Height - oFont.Height) / 2 + 2; //中心下移2像素
                dotX = Convert.ToSingle(N1) * FontSize + (N1 + 1) * spaceWith;

                oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
            }

            for (int i = 0; i <= 30; i++)
            {
                //画噪点
                int x = oRnd.Next(oBmp.Width);
                int y = oRnd.Next(oBmp.Height);
                Color clr = oColors[oRnd.Next(oColors.Length)];
                oBmp.SetPixel(x, y, clr);
            }

            //保存图片数据
            MemoryStream stream = new MemoryStream();
            oBmp.Save(stream, ImageFormat.Jpeg);
            //输出图片流
            return stream.ToArray();
        }

        public string CreateCode(int CodeLength, out Random oRnd)
        {
            int N1;
            String sCode = String.Empty;

            char[] oCharacter =
            {
                '2', '3', '4', '5', '6', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y'
            };
            oRnd = new Random();


            //生成验证码字符串
            for (N1 = 0; N1 <= CodeLength - 1; N1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }
            return sCode;
        }
    }
}