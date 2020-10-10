using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Utils
{
    /// <summary>
    /// 货币操作工具类
    /// </summary>
    public class MoneyUtil
    {
        #region ToUppercaseAmount(转换人民币大写形式金额)
        /// <summary>
        /// 转换人民币大写形式金额
        /// </summary>
        /// <param name="number">金额</param>
        /// <returns></returns>
        public static string ToUppercaseAmount(decimal number)
        {
            string uppercaseNumber = Const.ChineseNumbers;//0-9所对应的汉字
            string moneyUtil = Const.MonetaryUnit;//数字位所对应的汉字
            string result = "";//人民币大写金额形式
            string numberStr = "";//数字的字符串形式

            int i, j;//循环变量
            string char2 = "";//数字位的汉语读法
            int nzero = 0;//用来计算连续的零值是几个

            number = Math.Round(Math.Abs(number), 2);//四舍五入
            numberStr = ((long)(number * 100)).ToString();//将number*100并转为字符串形式
            j = numberStr.Length;//找出最高位
            if (j > 15)
            {
                return "溢出";
            }
            moneyUtil = moneyUtil.Substring(15 - j);//取出对应位数的数字位的值

            for (i = 0; i < j; i++)
            {
                var sourceValue = numberStr.Substring(i, 1);//从number值中取出的值
                var temp = Convert.ToInt32(sourceValue);//临时值
                var char1 = "";//数字的汉语读法
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))//当所取位数不为元、万、亿、万亿上的数字时 
                {
                    if (sourceValue == "0")
                    {
                        char1 = "";
                        char2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (sourceValue != "0" && nzero != 0)
                        {
                            char1 = "零" + uppercaseNumber.Substring(temp * 1, 1);
                            char2 = moneyUtil.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            char1 = uppercaseNumber.Substring(temp * 1, 1);
                            char2 = moneyUtil.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else//该位是万亿，亿，万，元位等关键位
                {
                    if (numberStr != "0" && nzero != 0)
                    {
                        char1 = "零" + uppercaseNumber.Substring(temp * 1, 1);
                        char2 = moneyUtil.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (numberStr != "0" && nzero == 0)
                        {
                            char1 = uppercaseNumber.Substring(temp * 1, 1);
                            char2 = moneyUtil.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (numberStr == "0" && nzero >= 3)
                            {
                                char1 = "";
                                char2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    char1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {

                                    char1 = "";
                                    char2 = moneyUtil.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    char2 = moneyUtil.Substring(i, 1);
                }
                result = result + char1 + char2;

                if (i == j - 1 && numberStr == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    result = result + "整";
                }
            }
            if (number == 0)
            {
                result = "零元整";
            }
            return result;
        }
        /// <summary>
        /// 重载，转换人民币大写形式金额
        /// </summary>
        /// <param name="number">金额</param>
        /// <returns></returns>
        public static string ToUppercaseAmount(string number)
        {
            try
            {
                decimal num = Convert.ToDecimal(number);
                return ToUppercaseAmount(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        #endregion
    }
}
