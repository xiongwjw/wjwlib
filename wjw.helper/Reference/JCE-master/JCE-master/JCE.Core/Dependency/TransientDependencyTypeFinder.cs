﻿/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：TransientDependencyTypeFinder
 * 版本号：v1.0.0.0
 * 唯一标识：505e2619-7fa3-493e-a239-f3aaa34d52ce
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:38:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:38:38
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Reflection;
using JCE.Utils.Extensions;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// <see cref="ITransientDependency"/>依赖注入——即时模式接口实现类查找器
    /// </summary>
    public class TransientDependencyTypeFinder:ITypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="TransientDependencyTypeFinder"/>类型的新实例
        /// </summary>
        public TransientDependencyTypeFinder()
        {           
            AssemblyFinder=new DirectoryAssemblyFinder();
        }

        /// <summary>
        /// 获取或设置 程序集查找器
        /// </summary>
        public IAllAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            try
            {
                Assembly[] assemblies = AssemblyFinder.FindAll();
                return
                    assemblies.SelectMany(
                        assembly =>
                            assembly.GetTypes()
                                .Where(type => typeof (ITransientDependency).IsAssignableFrom(type) && !type.IsAbstract))
                        .Distinct()
                        .ToArray();
            }
            catch (ReflectionTypeLoadException ex)
            {
                string msg = ex.Message;
                Exception[] exs = ex.LoaderExceptions;
                msg = msg + "\r\n详情：" + exs.Select(m => m.Message).ExpandAndToString("---");
                throw new Exception(msg, ex);
            }
        }
    }
}
