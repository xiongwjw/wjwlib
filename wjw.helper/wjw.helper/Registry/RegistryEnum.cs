
namespace wjw.helper.Registry
{
    public enum RegDomain
    {
        /// <summary>
        /// 对应于HKEY_CLASSES_ROOT主键
        /// </summary>
        ClassesRoot = 0,
        /// <summary>
        /// 对应于HKEY_CURRENT_USER主键
        /// </summary>
        CurrentUser = 1,
        /// <summary>
        /// 对应于 HKEY_LOCAL_MACHINE主键
        /// </summary>
        LocalMachine = 2,
        /// <summary>
        /// 对应于 HKEY_USER主键
        /// </summary>
        User = 3,
        /// <summary>
        /// 对应于HEKY_CURRENT_CONFIG主键
        /// </summary>
        CurrentConfig = 4,
        /// <summary>
        /// 对应于HKEY_DYN_DATA主键
        /// </summary>
        DynDa = 5,
        /// <summary>
        /// 对应于HKEY_PERFORMANCE_DATA主键
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

   