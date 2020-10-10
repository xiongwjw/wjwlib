using System;
using Microsoft.Win32;

namespace wjw.helper.Registry
{

    public class Register
    {
        #region �ֶζ���
        /// <summary>
        /// ע���������
        /// </summary>
        private string _subkey;
        /// <summary>
        /// ע��������
        /// </summary>
        private RegDomain _domain;
        /// <summary>
        /// ע����ֵ
        /// </summary>
        private string _regeditkey;
        #endregion

        #region ����
        /// <summary>
        /// ����ע���������
        /// </summary>
        public string SubKey
        {
            //get { return _subkey; }
            set { _subkey = value; }
        }

        /// <summary>
        /// ע��������
        /// </summary>
        public RegDomain Domain
        {
            ///get { return _domain; }
            set { _domain = value; }
        }

        /// <summary>
        /// ע����ֵ
        /// </summary>
        public string RegeditKey
        {
            ///get{return _regeditkey;}
            set { _regeditkey = value; }
        }
        #endregion

        #region ���캯��
        public Register()
        {
            ///Ĭ��ע���������
            _subkey = "software\\";
            ///Ĭ��ע��������
            _domain = RegDomain.LocalMachine;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        public Register(string subKey, RegDomain regDomain)
        {
            ///����ע���������
            _subkey = subKey;
            ///����ע��������
            _domain = regDomain;
        }
        #endregion

        #region ���з���
        #region ����ע�����
        /// <summary>
        /// ����ע����Ĭ�ϴ�����ע������ HKEY_LOCAL_MACHINE���棨��������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// </summary>
        public virtual void CreateSubKey()
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (_subkey == string.Empty || _subkey == null)
            {
                return;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ������ע�����Ľڵ�
            RegistryKey sKey;
            if (!IsSubKeyExist())
            {
                sKey = key.CreateSubKey(_subkey);
            }
            //sKey.Close();
            ///�رն�ע�����ĸ���
            key.Close();
        }

        /// <summary>
        /// ����ע����Ĭ�ϴ�����ע������ HKEY_LOCAL_MACHINE����
        /// �鷽��������ɽ�����д
        /// ���ӣ���subkey��software\\higame\\���򽫴���HKEY_LOCAL_MACHINE\\software\\higame\\ע�����
        /// </summary>
        /// <param name="subKey">ע���������</param>
        public virtual void CreateSubKey(string subKey)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ������ע�����Ľڵ�
            RegistryKey sKey;
            if (!IsSubKeyExist(subKey))
            {
                sKey = key.CreateSubKey(subKey);
            }
            //sKey.Close();
            ///�رն�ע�����ĸ���
            key.Close();
        }

        /// <summary>
        /// ����ע����Ĭ�ϴ�����ע������ HKEY_LOCAL_MACHINE����
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="regDomain">ע��������</param>
        public virtual void CreateSubKey(RegDomain regDomain)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (_subkey == string.Empty || _subkey == null)
            {
                return;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(regDomain);

            ///Ҫ������ע�����Ľڵ�
            RegistryKey sKey;
            if (!IsSubKeyExist(regDomain))
            {
                sKey = key.CreateSubKey(_subkey);
            }
            //sKey.Close();
            ///�رն�ע�����ĸ���
            key.Close();
        }

        /// <summary>
        /// ����ע������������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// ���ӣ���regDomain��HKEY_LOCAL_MACHINE��subkey��software\\higame\\���򽫴���HKEY_LOCAL_MACHINE\\software\\higame\\ע�����
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        public virtual void CreateSubKey(string subKey, RegDomain regDomain)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(regDomain);

            ///Ҫ������ע�����Ľڵ�
            RegistryKey sKey;
            if (!IsSubKeyExist(subKey, regDomain))
            {
                sKey = key.CreateSubKey(subKey);
            }
            //sKey.Close();
            ///�رն�ע�����ĸ���
            key.Close();
        }
        #endregion

        #region �ж�ע������Ƿ����
        /// <summary>
        /// �ж�ע������Ƿ���ڣ�Ĭ������ע������HKEY_LOCAL_MACHINE���жϣ���������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// ���ӣ����������Domain��SubKey���ԣ����ж�Domain\\SubKey������Ĭ���ж�HKEY_LOCAL_MACHINE\\software\\
        /// </summary>
        /// <returns>����ע������Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsSubKeyExist()
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (_subkey == string.Empty || _subkey == null)
            {
                return false;
            }

            ///����ע�������
            ///���sKeyΪnull,˵��û�и�ע�������ڣ��������
            RegistryKey sKey = OpenSubKey(_subkey, _domain);
            if (sKey == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �ж�ע������Ƿ���ڣ�Ĭ������ע������HKEY_LOCAL_MACHINE���ж�
        /// �鷽��������ɽ�����д
        /// ���ӣ���subkey��software\\higame\\�����ж�HKEY_LOCAL_MACHINE\\software\\higame\\ע������Ƿ����
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <returns>����ע������Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsSubKeyExist(string subKey)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///����ע�������
            ///���sKeyΪnull,˵��û�и�ע�������ڣ��������
            RegistryKey sKey = OpenSubKey(subKey);
            if (sKey == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �ж�ע������Ƿ����
        /// �鷽��������ɽ�����д
        /// ���ӣ���regDomain��HKEY_CLASSES_ROOT�����ж�HKEY_CLASSES_ROOT\\SubKeyע������Ƿ����
        /// </summary>
        /// <param name="regDomain">ע��������</param>
        /// <returns>����ע������Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsSubKeyExist(RegDomain regDomain)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (_subkey == string.Empty || _subkey == null)
            {
                return false;
            }

            ///����ע�������
            ///���sKeyΪnull,˵��û�и�ע�������ڣ��������
            RegistryKey sKey = OpenSubKey(_subkey, regDomain);
            if (sKey == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �ж�ע������Ƿ���ڣ���������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// ���ӣ���regDomain��HKEY_CLASSES_ROOT��subkey��software\\higame\\�����ж�HKEY_CLASSES_ROOT\\software\\higame\\ע������Ƿ����
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>����ע������Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsSubKeyExist(string subKey, RegDomain regDomain)
        {
            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///����ע�������
            ///���sKeyΪnull,˵��û�и�ע�������ڣ��������
            RegistryKey sKey = OpenSubKey(subKey, regDomain);
            if (sKey == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region ɾ��ע�����
        /// <summary>
        /// ɾ��ע������������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <returns>���ɾ���ɹ����򷵻�true������Ϊfalse</returns>
        public virtual bool DeleteSubKey()
        {
            ///����ɾ���Ƿ�ɹ�
            bool result = false;

            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (_subkey == string.Empty || _subkey == null)
            {
                return false;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            if (IsSubKeyExist())
            {
                try
                {
                    ///ɾ��ע�����
                    key.DeleteSubKey(_subkey);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            ///�رն�ע�����ĸ���
            key.Close();
            return result;
        }

        /// <summary>
        /// ɾ��ע������������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <returns>���ɾ���ɹ����򷵻�true������Ϊfalse</returns>
        public virtual bool DeleteSubKey(string subKey)
        {
            ///����ɾ���Ƿ�ɹ�
            bool result = false;

            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            if (IsSubKeyExist())
            {
                try
                {
                    ///ɾ��ע�����
                    key.DeleteSubKey(subKey);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            ///�رն�ע�����ĸ���
            key.Close();
            return result;
        }

        /// <summary>
        /// ɾ��ע�����
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>���ɾ���ɹ����򷵻�true������Ϊfalse</returns>
        public virtual bool DeleteSubKey(string subKey, RegDomain regDomain)
        {
            ///����ɾ���Ƿ�ɹ�
            bool result = false;

            ///�ж�ע����������Ƿ�Ϊ�գ����Ϊ�գ�����false
            if (subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(regDomain);

            if (IsSubKeyExist(subKey, regDomain))
            {
                try
                {
                    ///ɾ��ע�����
                    key.DeleteSubKey(subKey);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            ///�رն�ע�����ĸ���
            key.Close();
            return result;
        }
        #endregion

        #region �жϼ�ֵ�Ƿ����
        /// <summary>
        /// �жϼ�ֵ�Ƿ���ڣ���������SubKey��RegeditKey���ԣ�
        /// �鷽��������ɽ�����д
        /// 1.���RegeditKeyΪ�ա�null���򷵻�false
        /// 2.���SubKeyΪ�ա�null����SubKeyָ����ע�������ڣ�����false
        /// </summary>
        /// <returns>���ؼ�ֵ�Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsRegeditKeyExist()
        {
            ///���ؽ��
            bool result = false;

            ///�ж��Ƿ����ü�ֵ����
            if (_regeditkey == string.Empty || _regeditkey == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ����
            if (IsSubKeyExist())
            {
                ///��ע�����
                RegistryKey key = OpenSubKey();
                ///��ֵ����
                string[] regeditKeyNames;
                ///��ȡ��ֵ����
                regeditKeyNames = key.GetValueNames();
                ///������ֵ���ϣ�������ڼ�ֵ�����˳�����
                foreach (string regeditKey in regeditKeyNames)
                {
                    if (string.Compare(regeditKey, _regeditkey, true) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }

        /// <summary>
        /// �жϼ�ֵ�Ƿ���ڣ���������SubKey���ԣ�
        /// �鷽��������ɽ�����д
        /// ���SubKeyΪ�ա�null����SubKeyָ����ע�������ڣ�����false
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <returns>���ؼ�ֵ�Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsRegeditKeyExist(string name)
        {
            ///���ؽ��
            bool result = false;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ����
            if (IsSubKeyExist())
            {
                ///��ע�����
                RegistryKey key = OpenSubKey();
                ///��ֵ����
                string[] regeditKeyNames;
                ///��ȡ��ֵ����
                regeditKeyNames = key.GetValueNames();
                ///������ֵ���ϣ�������ڼ�ֵ�����˳�����
                foreach (string regeditKey in regeditKeyNames)
                {
                    if (string.Compare(regeditKey, name, true) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }

        /// <summary>
        /// �жϼ�ֵ�Ƿ����
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <returns>���ؼ�ֵ�Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsRegeditKeyExist(string name, string subKey)
        {
            ///���ؽ��
            bool result = false;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ����
            if (IsSubKeyExist())
            {
                ///��ע�����
                RegistryKey key = OpenSubKey(subKey);
                ///��ֵ����
                string[] regeditKeyNames;
                ///��ȡ��ֵ����
                regeditKeyNames = key.GetValueNames();
                ///������ֵ���ϣ�������ڼ�ֵ�����˳�����
                foreach (string regeditKey in regeditKeyNames)
                {
                    if (string.Compare(regeditKey, name, true) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }

        /// <summary>
        /// �жϼ�ֵ�Ƿ����
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>���ؼ�ֵ�Ƿ���ڣ����ڷ���true�����򷵻�false</returns>
        public virtual bool IsRegeditKeyExist(string name, string subKey, RegDomain regDomain)
        {
            ///���ؽ��
            bool result = false;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ����
            if (IsSubKeyExist())
            {
                ///��ע�����
                RegistryKey key = OpenSubKey(subKey, regDomain);
                ///��ֵ����
                string[] regeditKeyNames;
                ///��ȡ��ֵ����
                regeditKeyNames = key.GetValueNames();
                ///������ֵ���ϣ�������ڼ�ֵ�����˳�����
                foreach (string regeditKey in regeditKeyNames)
                {
                    if (string.Compare(regeditKey, name, true) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }
        #endregion

        #region ���ü�ֵ����
        /// <summary>
        /// ����ָ���ļ�ֵ���ݣ���ָ�������������ͣ���������RegeditKey��SubKey���ԣ�
        /// ���ڸļ�ֵ���޸ļ�ֵ���ݣ������ڼ�ֵ���ȴ�����ֵ�������ü�ֵ����
        /// </summary>
        /// <param name="content">��ֵ����</param>
        /// <returns>��ֵ�������óɹ����򷵻�true�����򷵻�false</returns>
        public virtual bool WriteRegeditKey(object content)
        {
            ///���ؽ��
            bool result = false;

            ///�ж��Ƿ����ü�ֵ����
            if (_regeditkey == string.Empty || _regeditkey == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ���ڣ���������ڣ���ֱ�Ӵ���
            if (!IsSubKeyExist(_subkey))
            {
                CreateSubKey(_subkey);
            }

            ///�Կ�д��ʽ��ע�����
            RegistryKey key = OpenSubKey(true);

            ///���ע������ʧ�ܣ��򷵻�false
            if (key == null)
            {
                return false;
            }

            try
            {
                key.SetValue(_regeditkey, content);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }

        /// <summary>
        /// ����ָ���ļ�ֵ���ݣ���ָ�������������ͣ���������SubKey���ԣ�
        /// ���ڸļ�ֵ���޸ļ�ֵ���ݣ������ڼ�ֵ���ȴ�����ֵ�������ü�ֵ����
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="content">��ֵ����</param>
        /// <returns>��ֵ�������óɹ����򷵻�true�����򷵻�false</returns>
        public virtual bool WriteRegeditKey(string name, object content)
        {
            ///���ؽ��
            bool result = false;

            ///�жϼ�ֵ�Ƿ����
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ���ڣ���������ڣ���ֱ�Ӵ���
            if (!IsSubKeyExist(_subkey))
            {
                CreateSubKey(_subkey);
            }

            ///�Կ�д��ʽ��ע�����
            RegistryKey key = OpenSubKey(true);

            ///���ע������ʧ�ܣ��򷵻�false
            if (key == null)
            {
                return false;
            }

            try
            {
                key.SetValue(name, content);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }

        /// <summary>
        /// ����ָ���ļ�ֵ���ݣ�ָ�������������ͣ���������SubKey���ԣ�
        /// ���ڸļ�ֵ���޸ļ�ֵ���ݣ������ڼ�ֵ���ȴ�����ֵ�������ü�ֵ����
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="content">��ֵ����</param>
        /// <returns>��ֵ�������óɹ����򷵻�true�����򷵻�false</returns>
        public virtual bool WriteRegeditKey(string name, object content, RegValueKind regValueKind)
        {
            ///���ؽ��
            bool result = false;

            ///�жϼ�ֵ�Ƿ����
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�ж�ע������Ƿ���ڣ���������ڣ���ֱ�Ӵ���
            if (!IsSubKeyExist(_subkey))
            {
                CreateSubKey(_subkey);
            }

            ///�Կ�д��ʽ��ע�����
            RegistryKey key = OpenSubKey(true);

            ///���ע������ʧ�ܣ��򷵻�false
            if (key == null)
            {
                return false;
            }

            try
            {
                key.SetValue(name, content, GetRegValueKind(regValueKind));
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return result;
        }
        #endregion

        #region ��ȡ��ֵ����
        /// <summary>
        /// ��ȡ��ֵ���ݣ���������RegeditKey��SubKey���ԣ�
        /// 1.���RegeditKeyΪ�ա�null����RegeditKeyָʾ�ļ�ֵ�����ڣ�����null
        /// 2.���SubKeyΪ�ա�null����SubKeyָʾ��ע�������ڣ�����null
        /// 3.��֮���򷵻ؼ�ֵ����
        /// </summary>
        /// <returns>���ؼ�ֵ����</returns>
        public virtual object ReadRegeditKey()
        {
            ///��ֵ���ݽ��
            object obj = null;

            ///�ж��Ƿ����ü�ֵ����
            if (_regeditkey == string.Empty || _regeditkey == null)
            {
                return null;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(_regeditkey))
            {
                ///��ע�����
                RegistryKey key = OpenSubKey();
                if (key != null)
                {
                    obj = key.GetValue(_regeditkey);
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return obj;
        }

        /// <summary>
        /// ��ȡ��ֵ���ݣ���������SubKey���ԣ�
        /// 1.���SubKeyΪ�ա�null����SubKeyָʾ��ע�������ڣ�����null
        /// 2.��֮���򷵻ؼ�ֵ����
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <returns>���ؼ�ֵ����</returns>
        public virtual object ReadRegeditKey(string name)
        {
            ///��ֵ���ݽ��
            object obj = null;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return null;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///��ע�����
                RegistryKey key = OpenSubKey();
                if (key != null)
                {
                    obj = key.GetValue(name);
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return obj;
        }

        /// <summary>
        /// ��ȡ��ֵ����
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <returns>���ؼ�ֵ����</returns>
        public virtual object ReadRegeditKey(string name, string subKey)
        {
            ///��ֵ���ݽ��
            object obj = null;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return null;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///��ע�����
                RegistryKey key = OpenSubKey(subKey);
                if (key != null)
                {
                    obj = key.GetValue(name);
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return obj;
        }

        /// <summary>
        /// ��ȡ��ֵ����
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>���ؼ�ֵ����</returns>
        public virtual object ReadRegeditKey(string name, string subKey, RegDomain regDomain)
        {
            ///��ֵ���ݽ��
            object obj = null;

            ///�ж��Ƿ����ü�ֵ����
            if (name == string.Empty || name == null)
            {
                return null;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///��ע�����
                RegistryKey key = OpenSubKey(subKey, regDomain);
                if (key != null)
                {
                    obj = key.GetValue(name);
                }
                ///�رն�ע�����ĸ���
                key.Close();
            }
            return obj;
        }
        #endregion

        #region ɾ����ֵ
        /// <summary>
        /// ɾ����ֵ����������RegeditKey��SubKey���ԣ�
        /// 1.���RegeditKeyΪ�ա�null����RegeditKeyָʾ�ļ�ֵ�����ڣ�����false
        /// 2.���SubKeyΪ�ա�null����SubKeyָʾ��ע�������ڣ�����false
        /// </summary>
        /// <returns>���ɾ���ɹ�������true�����򷵻�false</returns>
        public virtual bool DeleteRegeditKey()
        {
            ///ɾ�����
            bool result = false;

            ///�ж��Ƿ����ü�ֵ���ԣ����û�����ã��򷵻�false
            if (_regeditkey == string.Empty || _regeditkey == null)
            {
                return false;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(_regeditkey))
            {
                ///�Կ�д��ʽ��ע�����
                RegistryKey key = OpenSubKey(true);
                if (key != null)
                {
                    try
                    {
                        ///ɾ����ֵ
                        key.DeleteValue(_regeditkey);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        ///�رն�ע�����ĸ���
                        key.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// ɾ����ֵ����������SubKey���ԣ�
        /// ���SubKeyΪ�ա�null����SubKeyָʾ��ע�������ڣ�����false
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <returns>���ɾ���ɹ�������true�����򷵻�false</returns>
        public virtual bool DeleteRegeditKey(string name)
        {
            ///ɾ�����
            bool result = false;

            ///�жϼ�ֵ�����Ƿ�Ϊ�գ����Ϊ�գ��򷵻�false
            if (name == string.Empty || name == null)
            {
                return false;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///�Կ�д��ʽ��ע�����
                RegistryKey key = OpenSubKey(true);
                if (key != null)
                {
                    try
                    {
                        ///ɾ����ֵ
                        key.DeleteValue(name);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        ///�رն�ע�����ĸ���
                        key.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// ɾ����ֵ
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <returns>���ɾ���ɹ�������true�����򷵻�false</returns>
        public virtual bool DeleteRegeditKey(string name, string subKey)
        {
            ///ɾ�����
            bool result = false;

            ///�жϼ�ֵ���ƺ�ע����������Ƿ�Ϊ�գ����Ϊ�գ��򷵻�false
            if (name == string.Empty || name == null || subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///�Կ�д��ʽ��ע�����
                RegistryKey key = OpenSubKey(subKey, true);
                if (key != null)
                {
                    try
                    {
                        ///ɾ����ֵ
                        key.DeleteValue(name);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        ///�رն�ע�����ĸ���
                        key.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// ɾ����ֵ
        /// </summary>
        /// <param name="name">��ֵ����</param>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>���ɾ���ɹ�������true�����򷵻�false</returns>
        public virtual bool DeleteRegeditKey(string name, string subKey, RegDomain regDomain)
        {
            ///ɾ�����
            bool result = false;

            ///�жϼ�ֵ���ƺ�ע����������Ƿ�Ϊ�գ����Ϊ�գ��򷵻�false
            if (name == string.Empty || name == null || subKey == string.Empty || subKey == null)
            {
                return false;
            }

            ///�жϼ�ֵ�Ƿ����
            if (IsRegeditKeyExist(name))
            {
                ///�Կ�д��ʽ��ע�����
                RegistryKey key = OpenSubKey(subKey, regDomain, true);
                if (key != null)
                {
                    try
                    {
                        ///ɾ����ֵ
                        key.DeleteValue(name);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                    finally
                    {
                        ///�رն�ע�����ĸ���
                        key.Close();
                    }
                }
            }

            return result;
        }
        #endregion
        #endregion

        #region �ܱ�������
        /// <summary>
        /// ��ȡע���������Ӧ�����ڵ�
        /// ���ӣ���regDomain��ClassesRoot���򷵻�Registry.ClassesRoot
        /// </summary>
        /// <param name="regDomain">ע��������</param>
        /// <returns>ע���������Ӧ�����ڵ�</returns>
        protected RegistryKey GetRegDomain(RegDomain regDomain)
        {
            ///��������ע������Ľڵ�
            RegistryKey key;

            

            #region �ж�ע��������
            switch (regDomain)
            {
                case RegDomain.ClassesRoot:
                    key = Microsoft.Win32.Registry.ClassesRoot; break;
                case RegDomain.CurrentUser:
                    key = Microsoft.Win32.Registry.CurrentUser; break;
                case RegDomain.LocalMachine:
                    key = Microsoft.Win32.Registry.LocalMachine; break;
                case RegDomain.User:
                    key = Microsoft.Win32.Registry.Users; break;
                case RegDomain.CurrentConfig:
                    key = Microsoft.Win32.Registry.CurrentConfig; break;
                case RegDomain.DynDa:
                    key = Microsoft.Win32.Registry.DynData; break;
                case RegDomain.PerformanceData:
                    key = Microsoft.Win32.Registry.PerformanceData; break;
                default:
                    key = Microsoft.Win32.Registry.LocalMachine; break;
            }
            #endregion

            return key;
        }

        /// <summary>
        /// ��ȡ��ע����ж�Ӧ��ֵ��������
        /// ���ӣ���regValueKind��DWord���򷵻�RegistryValueKind.DWord
        /// </summary>
        /// <param name="regValueKind">ע�����������</param>
        /// <returns>ע����ж�Ӧ����������</returns>
        protected RegistryValueKind GetRegValueKind(RegValueKind regValueKind)
        {
            RegistryValueKind regValueK;

            #region �ж�ע�����������
            switch (regValueKind)
            {
                case RegValueKind.Unknown:
                    regValueK = RegistryValueKind.Unknown; break;
                case RegValueKind.String:
                    regValueK = RegistryValueKind.String; break;
                case RegValueKind.ExpandString:
                    regValueK = RegistryValueKind.ExpandString; break;
                case RegValueKind.Binary:
                    regValueK = RegistryValueKind.Binary; break;
                case RegValueKind.DWord:
                    regValueK = RegistryValueKind.DWord; break;
                case RegValueKind.MultiString:
                    regValueK = RegistryValueKind.MultiString; break;
                case RegValueKind.QWord:
                    regValueK = RegistryValueKind.QWord; break;
                default:
                    regValueK = RegistryValueKind.String; break;
            }
            #endregion
            return regValueK;
        }

        #region ��ע�����
        /// <summary>
        /// ��ע�����ڵ㣬��ֻ����ʽ��������
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey()
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (_subkey == string.Empty || _subkey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(_subkey);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }

        /// <summary>
        /// ��ע�����ڵ�
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="writable">�����Ҫ���д����Ȩ�ޣ�������Ϊ true</param>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey(bool writable)
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (_subkey == string.Empty || _subkey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(_subkey, writable);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }

        /// <summary>
        /// ��ע�����ڵ㣬��ֻ����ʽ��������
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey(string subKey)
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(subKey);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }

        /// <summary>
        /// ��ע�����ڵ㣬��ֻ����ʽ��������
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="writable">�����Ҫ���д����Ȩ�ޣ�������Ϊ true</param>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey(string subKey, bool writable)
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(_domain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(subKey, writable);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }

        /// <summary>
        /// ��ע�����ڵ㣬��ֻ����ʽ��������
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey(string subKey, RegDomain regDomain)
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(regDomain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(subKey);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }

        /// <summary>
        /// ��ע�����ڵ�
        /// �鷽��������ɽ�����д
        /// </summary>
        /// <param name="subKey">ע���������</param>
        /// <param name="regDomain">ע��������</param>
        /// <param name="writable">�����Ҫ���д����Ȩ�ޣ�������Ϊ true</param>
        /// <returns>���SubKeyΪ�ա�null����SubKeyָʾע�������ڣ��򷵻�null�����򷵻�ע���ڵ�</returns>
        protected virtual RegistryKey OpenSubKey(string subKey, RegDomain regDomain, bool writable)
        {
            ///�ж�ע����������Ƿ�Ϊ��
            if (subKey == string.Empty || subKey == null)
            {
                return null;
            }

            ///��������ע������Ľڵ�
            RegistryKey key = GetRegDomain(regDomain);

            ///Ҫ�򿪵�ע�����Ľڵ�
            RegistryKey sKey = null;
            ///��ע�����
            sKey = key.OpenSubKey(subKey, writable);
            ///�رն�ע�����ĸ���
            key.Close();
            ///����ע���ڵ�
            return sKey;
        }
        #endregion
        #endregion
    }
}
