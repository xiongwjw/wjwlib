
namespace wjw.helper.Registry
{
    public enum RegDomain
    {
        /// <summary>
        /// ��Ӧ��HKEY_CLASSES_ROOT����
        /// </summary>
        ClassesRoot = 0,
        /// <summary>
        /// ��Ӧ��HKEY_CURRENT_USER����
        /// </summary>
        CurrentUser = 1,
        /// <summary>
        /// ��Ӧ�� HKEY_LOCAL_MACHINE����
        /// </summary>
        LocalMachine = 2,
        /// <summary>
        /// ��Ӧ�� HKEY_USER����
        /// </summary>
        User = 3,
        /// <summary>
        /// ��Ӧ��HEKY_CURRENT_CONFIG����
        /// </summary>
        CurrentConfig = 4,
        /// <summary>
        /// ��Ӧ��HKEY_DYN_DATA����
        /// </summary>
        DynDa = 5,
        /// <summary>
        /// ��Ӧ��HKEY_PERFORMANCE_DATA����
        /// </summary>
        PerformanceData = 6,
    }

    public enum RegValueKind
    {
        Unknown,
        String,
        ExpandString,
        Binary,
        DWord,
        MultiString,
        QWord
    }
}

   