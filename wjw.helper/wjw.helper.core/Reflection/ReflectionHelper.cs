using System;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Resources;
using System.ComponentModel;
using System.Text;
using System.IO;

namespace wjw.helper.Reflection
{
    /// <summary>
    /// ���丨����
    /// </summary>
    public static class ReflectHelper
    {
        #region ��Ա��д
        /// <summary>
        /// ͨ�����������ʵ������
        /// </summary>
        /// <param name="model">ʵ�����</param>
        /// <param name="dRow">������</param>
        public static void FillInstanceValue(object model, DataRow dRow)
        {
            Type type = model.GetType();
            for (int i = 0; i < dRow.Table.Columns.Count; i++)
            {
                PropertyInfo property = type.GetProperty(dRow.Table.Columns[i].ColumnName);
                if (property != null)
                {
                    property.SetValue(model, dRow[i], null);
                }
            }
        }

        /// <summary>
        /// ͨ������ֻ�������ʵ������
        /// </summary>
        /// <param name="model">ʵ�����</param>
        /// <param name="dr">����ֻ����</param>
        public static void FillInstanceValue(object model, IDataReader dr)
        {
            Type type = model.GetType();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                PropertyInfo property = type.GetProperty(dr.GetName(i));
                if (property != null)
                {
                    property.SetValue(model, dr[i], null);
                }
            }
        }

        /// <summary>
        /// ��ȡʵ��������Ե�ֵ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetInstanceValue(object obj, string propertyName)
        {
            object objRet = null;
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj).Find(propertyName, true);
                if (descriptor != null)
                {
                    objRet = descriptor.GetValue(obj);
                }
            }
            return objRet;
        } 
        #endregion

        #region ��������
        /// <summary>
        /// ֱ�ӵ����ڲ�����ķ���/�������ȡ����(֧�����ص���)
        /// </summary>
        /// <param name="refType">Ŀ����������</param>
        /// <param name="funName">�������ƣ����ִ�Сд��</param>
        /// <param name="objInitial">����������ԣ���Ϊ��ض���ĳ�ʼ�����ݣ�����ΪNull��</param>
        /// <param name="funParams">����������Ϣ</param>
        /// <returns>���н��</returns>
        public static object InvokeMethodOrGetProperty(Type refType, string funName, object[] objInitial, params object[] funParams)
        {
            MemberInfo[] mis = refType.GetMember(funName);
            if (mis.Length < 1)
            {
                throw new InvalidProgramException(string.Concat("����/���� [", funName, "] ��ָ������(", refType.ToString(), ")�в����ڣ�"));
            }
            else
            {
                MethodInfo targetMethod = null;
                StringBuilder pb = new StringBuilder();
                foreach (MemberInfo mi in mis)
                {
                    if (mi.MemberType != MemberTypes.Method)
                    {
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            #region �������Է���Get
                            targetMethod = ((PropertyInfo)mi).GetGetMethod();
                            break;
                            #endregion
                        }
                        else
                        {
                            throw new InvalidProgramException(string.Concat("[", funName, "] ������Ч�ĺ���/���Է�����"));
                        }
                    }
                    else
                    {
                        #region ��麯���������������� ����ȷ�ĺ�����Ŀ�����
                        bool validParamsLen = false, validParamsType = false;

                        MethodInfo curMethod = (MethodInfo)mi;
                        ParameterInfo[] pis = curMethod.GetParameters();
                        if (pis.Length == funParams.Length)
                        {
                            validParamsLen = true;

                            pb = new StringBuilder();
                            bool paramFlag = true;
                            int paramIdx = 0;

                            #region ����������� ����validParamsType�Ƿ���Ч
                            foreach (ParameterInfo pi in pis)
                            {
                                pb.AppendFormat("Parameter {0}: Type={1}, Name={2}\n", paramIdx, pi.ParameterType, pi.Name);

                                //����Null�ͽ���Object���͵Ĳ������
                                if (funParams[paramIdx] != null && pi.ParameterType != typeof(object) &&
                                     (pi.ParameterType != funParams[paramIdx].GetType()))
                                {
                                    #region ��������Ƿ����
                                    try
                                    {
                                        funParams[paramIdx] = Convert.ChangeType(funParams[paramIdx], pi.ParameterType);
                                    }
                                    catch (Exception)
                                    {
                                        paramFlag = false;
                                    }
                                    #endregion
                                    //break;
                                }
                                ++paramIdx;
                            }
                            #endregion

                            if (paramFlag == true)
                            {
                                validParamsType = true;
                            }
                            else
                            {
                                continue;
                            }

                            if (validParamsLen && validParamsType)
                            {
                                targetMethod = curMethod;
                                break;
                            }
                        }
                        #endregion
                    }
                }

                if (targetMethod != null)
                {
                    object objReturn = null;
                    #region ���Ч�ʺͼ������غ�������
                    try
                    {
                        object objInstance = System.Activator.CreateInstance(refType, objInitial);
                        objReturn = targetMethod.Invoke(objInstance, BindingFlags.InvokeMethod, Type.DefaultBinder, funParams,
                            System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        objReturn = refType.InvokeMember(funName, BindingFlags.InvokeMethod, Type.DefaultBinder, null, funParams);
                    }
                    #endregion
                    return objReturn;
                }
                else
                {
                    throw new InvalidProgramException(string.Concat("����/���� [", refType.ToString(), ".", funName,
                        "(args ...) ] �������Ⱥ��������Ͳ���ȷ��\n ���ò�����Ϣ�ο���\n",
                        pb.ToString()));
                }
            }

        }

        /// <summary>
        /// �������ʵ�����͵ĺ�������
        /// </summary>
        /// <param name="refType">ʵ������</param>
        /// <param name="funName">��������</param>
        /// <param name="funParams">���������б�</param>
        /// <returns>���øú���֮��Ľ��</returns>
        public static object InvokeFunction(Type refType, string funName, params object[] funParams)
        {
            return InvokeMethodOrGetProperty(refType, funName, null, funParams);
        } 
        #endregion

        #region ��Դ��ȡ
        /// <summary>
        /// ��ȡ������Դ��λͼ��Դ
        /// </summary>
        /// <param name="assemblyType">�����е�ĳһ��������</param>
        /// <param name="resourceHolder">��Դ�ĸ����ơ����磬��Ϊ��MyResource.en-US.resources������Դ�ļ��ĸ�����Ϊ��MyResource����</param>
        /// <param name="imageName">��Դ������</param>
        public static Bitmap LoadBitmap(Type assemblyType, string resourceHolder, string imageName)
        {
            Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
            ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
            return (Bitmap)rm.GetObject(imageName);
        }

        /// <summary>
        ///  ��ȡ������Դ���ı���Դ
        /// </summary>
        /// <param name="assemblyType">�����е�ĳһ��������</param>
        /// <param name="resName">��Դ������</param>
        /// <param name="resourceHolder">��Դ�ĸ����ơ����磬��Ϊ��MyResource.en-US.resources������Դ�ļ��ĸ�����Ϊ��MyResource����</param>
        public static string GetStringRes(Type assemblyType, string resName, string resourceHolder)
        {
            Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
            ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
            return rm.GetString(resName);
        }

        /// <summary>
        /// ��ȡ����Ƕ����Դ���ı���ʽ
        /// </summary>
        /// <param name="assemblyType">�����е�ĳһ��������</param>
        /// <param name="charset">�ַ�������</param>
        /// <param name="ResName">Ƕ����Դ���·��</param>
        /// <returns>��û�ҵ�����Դ�򷵻ؿ��ַ�</returns>
        public static string GetManifestString(Type assemblyType, string charset, string ResName)
        {
            Assembly asm = Assembly.GetAssembly(assemblyType);
            Stream st = asm.GetManifestResourceStream(string.Concat(assemblyType.Namespace,
                ".", ResName.Replace("/", ".")));
            if (st == null) { return ""; }
            int iLen = (int)st.Length;
            byte[] bytes = new byte[iLen];
            st.Read(bytes, 0, iLen);
            return (bytes != null) ? Encoding.GetEncoding(charset).GetString(bytes) : "";
        } 
        #endregion

    }
}

