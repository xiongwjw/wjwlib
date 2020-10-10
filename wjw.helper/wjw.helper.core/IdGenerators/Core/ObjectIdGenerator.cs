using wjw.helper.IdGenerators.Abstractions;
using wjw.helper.IdGenerators.Ids;

namespace wjw.helper.IdGenerators.Core
{
    /// <summary>
    /// ObjectId 生成器
    /// </summary>
    public class ObjectIdGenerator : IStringGenerator
    {
        /// <summary>
        /// 获取<see cref="ObjectIdGenerator"/>类型的实例
        /// </summary>
        public static ObjectIdGenerator Current { get; } = new ObjectIdGenerator();

        /// <summary>
        /// 创建ID
        /// </summary>
        public string Create() => ObjectId.GenerateNewStringId();
    }
}
