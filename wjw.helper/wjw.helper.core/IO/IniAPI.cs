using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace wjw.helper.IO
{
    public static class IniAPI
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer,
                                                               uint nSize,
                                                               string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName,
                                                          string lpKeyName,
                                                          string lpDefault,
                                                          StringBuilder lpReturnedString,
                                                          int nSize,
                                                          string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName,
                                                          string lpKeyName,
                                                          string lpDefault,
                                                          [In, Out] char[] lpReturnedString,
                                                          int nSize,
                                                          string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string lpAppName,
                                                         string lpKeyName,
                                                         string lpDefault,
                                                         IntPtr lpReturnedString,
                                                         uint nSize,
                                                         string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileInt(string lpAppName,
                                                      string lpKeyName,
                                                      int lpDefault,
                                                      string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileSection(string lpAppName,
                                                          IntPtr lpReturnedString,
                                                          uint nSize,
                                                          string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WritePrivateProfileString(string lpAppName,
                                                            string lpKeyName,
                                                            string lpString,
                                                            string lpFileName);
    }

    /// <summary> 
    /// Provides methods for reading and writing to an INI file. 
    /// </summary> 
    public class IniHandler
    {
        /// <summary> 
        /// The maximum size of a section in an ini file. 
        /// </summary> 
        /// <remarks> 
        /// This property defines the maximum size of the buffers 
        /// used to retreive data from an ini file.  This value is 
        /// the maximum allowed by the win32 functions 
        /// GetPrivateProfileSectionNames() or 
        /// GetPrivateProfileString(). 
        /// </remarks> 
        public const int MaxSectionSize = 32767; // 32 KB 

        //The path of the file we are operating on. 
        private string m_path;

        /// <summary> 
        /// Initializes a new instance of the <see cref="IniHandler"/> class. 
        /// </summary> 
        /// <param name="argPath">The ini file to read and write from.</param> 
        public IniHandler(string argPath)
        {
            //Convert to the full path.  Because of backward compatibility, 
            // the win32 functions tend to assume the path should be the 
            // root Windows directory if it is not specified.  By calling 
            // GetFullPath, we make sure we are always passing the full path 
            // the win32 functions. 
            m_path = argPath;
        }

        /// <summary> 
        /// Gets the full path of ini file this object instance is operating on. 
        /// </summary> 
        /// <value>A file path.</value> 
        public string Path
        {
            get
            {
                return m_path;
            }
        }

        #region Get Value Methods

        /// <summary> 
        /// Gets the value of a setting in an ini file as a <see cref="T:System.String"/>. 
        /// </summary> 
        /// <param name="argSectionName">The name of the section to read from.</param> 
        /// <param name="argKeyName">The name of the key in section to read.</param> 
        /// <param name="argDefaultValue">The default value to return if the key 
        /// cannot be found.</param> 
        /// <returns>The value of the key, if found.  Otherwise, returns 
        /// <paramref name="argDefaultValue"/></returns> 
        /// <remarks> 
        /// The retreived value must be less than 32KB in length. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="argSectionName"/> or <paramref name="argKeyName"/> are 
        /// a null reference  (Nothing in VB) 
        /// </exception> 
        public string GetString(string argSectionName,
                                string argKeyName,
                                string argDefaultValue = null)
        {
            if (string.IsNullOrEmpty(argSectionName) || string.IsNullOrEmpty(argKeyName))
            {
                return null;
            }

            StringBuilder retval = new StringBuilder(IniHandler.MaxSectionSize);

            IniAPI.GetPrivateProfileString(argSectionName,
                                                  argKeyName,
                                                  argDefaultValue,
                                                  retval,
                                                  IniHandler.MaxSectionSize,
                                                  m_path);

            return retval.ToString();
        }

        #endregion

        #region GetSectionValues Methods

        /// <summary> 
        /// Gets all of the values in a section as a list. 
        /// </summary> 
        /// <param name="sectionName"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <returns> 
        /// A <see cref="List{T}"/> containing <see cref="KeyValuePair{T1, T2}"/> objects 
        /// that describe this section.  Use this verison if a section may contain 
        /// multiple items with the same key value.  If you know that a section 
        /// cannot contain multiple values with the same key name or you don't 
        /// care about the duplicates, use the more convenient 
        /// <see cref="GetSectionValues"/> function. 
        /// </returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="sectionName"/> is a null reference  (Nothing in VB) 
        /// </exception> 
        public List<KeyValuePair<string, string>> GetSectionValuesAsList(string argSectionName)
        {
            List<KeyValuePair<string, string>> retval;
            string[] keyValuePairs;
            string key, value;
            int equalSignPos;

            if (string.IsNullOrEmpty(argSectionName))
            {
                return null;
            }

            //Allocate a buffer for the returned section names. 
            IntPtr ptr = Marshal.AllocCoTaskMem(IniHandler.MaxSectionSize);

            try
            {
                //Get the section key/value pairs into the buffer. 
                int len = IniAPI.GetPrivateProfileSection(argSectionName,
                                                                 ptr,
                                                                 IniHandler.MaxSectionSize,
                                                                 m_path);

                keyValuePairs = ConvertNullSeperatedStringToStringArray(ptr, len);
            }
            finally
            {
                //Free the buffer 
                Marshal.FreeCoTaskMem(ptr);
            }

            //Parse keyValue pairs and add them to the list. 
            retval = new List<KeyValuePair<string, string>>(keyValuePairs.Length);

            for (int i = 0; i < keyValuePairs.Length; ++i)
            {
                //Parse the "key=value" string into its constituent parts 
                equalSignPos = keyValuePairs[i].IndexOf('=');

                key = keyValuePairs[i].Substring(0, equalSignPos);

                value = keyValuePairs[i].Substring(equalSignPos + 1,
                                                   keyValuePairs[i].Length - equalSignPos - 1);

                retval.Add(new KeyValuePair<string, string>(key, value));
            }

            return retval;
        }

        /// <summary> 
        /// Gets all of the values in a section as a dictionary. 
        /// </summary> 
        /// <param name="argSectionName"> 
        /// Name of the section to retrieve values from. 
        /// </param> 
        /// <returns> 
        /// A <see cref="Dictionary{T, T}"/> containing the key/value 
        /// pairs found in this section.   
        /// </returns> 
        /// <remarks> 
        /// If a section contains more than one key with the same name, 
        /// this function only returns the first instance.  If you need to 
        /// get all key/value pairs within a section even when keys have the 
        /// same name, use <see cref="GetSectionValuesAsList"/>. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="argSectionName"/> is a null reference  (Nothing in VB) 
        /// </exception> 
        public Dictionary<string, string> GetSectionValues(string argSectionName)
        {
            List<KeyValuePair<string, string>> keyValuePairs;
            Dictionary<string, string> retval;

            keyValuePairs = GetSectionValuesAsList(argSectionName);

            //Convert list into a dictionary. 
            retval = new Dictionary<string, string>(keyValuePairs.Count);

            foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
            {
                //Skip any key we have already seen. 
                if (!retval.ContainsKey(keyValuePair.Key))
                {
                    retval.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return retval;
        }

        #endregion

        #region Get Key/Section Names

        /// <summary> 
        /// Gets the names of all keys under a specific section in the ini file. 
        /// </summary> 
        /// <param name="sectionName"> 
        /// The name of the section to read key names from. 
        /// </param> 
        /// <returns>An array of key names.</returns> 
        /// <remarks> 
        /// The total length of all key names in the section must be 
        /// less than 32KB in length. 
        /// </remarks> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="sectionName"/> is a null reference  (Nothing in VB) 
        /// </exception> 
        public string[] GetKeyNames(string argSectionName)
        {
            int len;
            string[] retval;

            if (string.IsNullOrEmpty(argSectionName))
            {
                return null;
            }

            //Allocate a buffer for the returned section names. 
            IntPtr ptr = Marshal.AllocCoTaskMem(IniHandler.MaxSectionSize);

            try
            {
                //Get the section names into the buffer. 
                len = IniAPI.GetPrivateProfileString(argSectionName,
                                                            null,
                                                            null,
                                                            ptr,
                                                            IniHandler.MaxSectionSize,
                                                            m_path);

                retval = ConvertNullSeperatedStringToStringArray(ptr, len);
            }
            finally
            {
                //Free the buffer 
                Marshal.FreeCoTaskMem(ptr);
            }

            return retval;
        }

        /// <summary> 
        /// Gets the names of all sections in the ini file. 
        /// </summary> 
        /// <returns>An array of section names.</returns> 
        /// <remarks> 
        /// The total length of all section names in the section must be 
        /// less than 32KB in length. 
        /// </remarks> 
        public string[] GetSectionNames()
        {
            string[] retval;
            int len;

            //Allocate a buffer for the returned section names. 
            IntPtr ptr = Marshal.AllocCoTaskMem(IniHandler.MaxSectionSize);

            try
            {
                //Get the section names into the buffer. 
                len = IniAPI.GetPrivateProfileSectionNames(ptr,
                    IniHandler.MaxSectionSize, m_path);

                retval = ConvertNullSeperatedStringToStringArray(ptr, len);
            }
            finally
            {
                //Free the buffer 
                Marshal.FreeCoTaskMem(ptr);
            }

            return retval;
        }

        /// <summary> 
        /// Converts the null seperated pointer to a string into a string array. 
        /// </summary> 
        /// <param name="ptr">A pointer to string data.</param> 
        /// <param name="valLength"> 
        /// Length of the data pointed to by <paramref name="ptr"/>. 
        /// </param> 
        /// <returns> 
        /// An array of strings; one for each null found in the array of characters pointed 
        /// at by <paramref name="ptr"/>. 
        /// </returns> 
        private static string[] ConvertNullSeperatedStringToStringArray(IntPtr ptr, int valLength)
        {
            string[] retval;

            if (valLength == 0)
            {
                //Return an empty array. 
                retval = new string[0];
            }
            else
            {
                //Convert the buffer into a string.  Decrease the length 
                //by 1 so that we remove the second null off the end. 
                string buff = Marshal.PtrToStringAuto(ptr, valLength - 1);

                //Parse the buffer into an array of strings by searching for nulls. 
                retval = buff.Split('\0');
            }

            return retval;
        }

        #endregion

        #region Write Methods

        /// <summary> 
        /// Writes a <see cref="T:System.String"/> value to the ini file. 
        /// </summary> 
        /// <param name="argSectionName">The name of the section to write to .</param> 
        /// <param name="argKeyName">The name of the key to write to.</param> 
        /// <param name="argValue">The string value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        private bool WriteValueInternal(string argSectionName, string argKeyName, string argValue)
        {
            return IniAPI.WritePrivateProfileString(argSectionName, argKeyName, argValue, m_path);
        }

        /// <summary> 
        /// Writes a <see cref="T:System.String"/> value to the ini file. 
        /// </summary> 
        /// <param name="argSectionName">The name of the section to write to .</param> 
        /// <param name="argKeyName">The name of the key to write to.</param> 
        /// <param name="argValue">The string value to write</param> 
        /// <exception cref="T:System.ComponentModel.Win32Exception"> 
        /// The write failed. 
        /// </exception> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="argSectionName"/> or <paramref name="argKeyName"/> or 
        /// <paramref name="argValue"/>  are a null reference  (Nothing in VB) 
        /// </exception> 
        public bool WriteValue(string argSectionName, string argKeyName, string argValue)
        {
            if (string.IsNullOrEmpty(argSectionName) ||
                string.IsNullOrEmpty(argKeyName) ||
                string.IsNullOrEmpty(argValue))
            {
                return false;
            }

            return WriteValueInternal(argSectionName, argKeyName, argValue);
        }

        #endregion

        #region Delete Methods

        /// <summary> 
        /// Deletes the specified key from the specified section. 
        /// </summary> 
        /// <param name="sectionName"> 
        /// Name of the section to remove the key from. 
        /// </param> 
        /// <param name="argKeyName"> 
        /// Name of the key to remove. 
        /// </param> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="sectionName"/> or <paramref name="argKeyName"/> are 
        /// a null reference  (Nothing in VB) 
        /// </exception> 
        public bool DeleteKey(string argSectionName, string argKeyName)
        {
            if (string.IsNullOrEmpty(argSectionName) || string.IsNullOrEmpty(argKeyName))
            {
                return false;
            }

            return WriteValueInternal(argSectionName, argKeyName, null);
        }

        /// <summary> 
        /// Deletes a section from the ini file. 
        /// </summary> 
        /// <param name="argSectionName"> 
        /// Name of the section to delete. 
        /// </param> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="argSectionName"/> is a null reference (Nothing in VB) 
        /// </exception> 
        public bool DeleteSection(string argSectionName)
        {
            if (string.IsNullOrEmpty(argSectionName))
            {
                return false;
            }

            return WriteValueInternal(argSectionName, null, null);
        }

        #endregion
    }
}


