
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Maths
{
    /// <summary>
    /// 单位转换
    /// </summary>
    public class UnitConv
    {
        /// <summary>
        /// 摄氏度转换为华氏度
        /// </summary>
        /// <param name="value">摄氏度</param>
        /// <returns></returns>
        public static decimal DegreesCelsiusToFahrenheit(decimal value)
        {
            return (decimal)1.8*value + 32;
        }

        /// <summary>
        /// 摄氏度转换为开氏度(热力学温度)
        /// </summary>
        /// <param name="value">摄氏度</param>
        /// <returns></returns>
        public static decimal DegreesCelsiusToThermodynamicTemperature(decimal value)
        {
            return value + (decimal) 273.16;
        }

        /// <summary>
        /// 华氏度转换为摄氏度
        /// </summary>
        /// <param name="value">华氏度</param>
        /// <returns></returns>
        public static decimal FahrenheitToDegreesCelsius(decimal value)
        {
            return (value - 32)/(decimal)1.8;
        }

        /// <summary>
        /// 华氏度转换为开氏度
        /// </summary>
        /// <param name="value">华氏度</param>
        /// <returns></returns>
        public static decimal FahrenheitToThermodynamicTemperature(decimal value)
        {
            return (value - 32)/ (decimal)1.8 + (decimal)273.16;
        }

        /// <summary>
        /// 开氏度转换为摄氏度
        /// </summary>
        /// <param name="value">开氏度</param>
        /// <returns></returns>
        public static decimal ThermodynamicTemperatureToDegreesCelsius(decimal value)
        {
            return value - (decimal) 273.16;
        }

        /// <summary>
        /// 开氏度转换为华氏度
        /// </summary>
        /// <param name="value">开氏度</param>
        /// <returns></returns>
        public static decimal ThermodynamicTemperatureToFahrenheit(decimal value)
        {
            return (value - (decimal)273.16)* (decimal)1.8 + 32;
        }
    }
}
