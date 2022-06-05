using GameFramework;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 辅助器创建器相关的实用函数。
    /// </summary>
    public static class GameObjectHelper
    {
        /// <summary>
        /// 创建辅助器。
        /// </summary>
        /// <typeparam name="T">要创建的辅助器类型。</typeparam>
        /// <param name="helperTypeName">要创建的辅助器类型名称。</param>
        /// <param name="customHelper">若要创建的辅助器类型为空时，使用的自定义辅助器类型。</param>
        /// <returns>创建的辅助器。</returns>
        public static T CreateHelper<T>(string helperTypeName, T customHelper) where T : MonoBehaviour
        {
            return CreateHelper(helperTypeName, customHelper, 0);
        }

        /// <summary>
        /// 创建辅助器。
        /// </summary>
        /// <typeparam name="T">要创建的辅助器类型。</typeparam>
        /// <param name="helperTypeName">要创建的辅助器类型名称。</param>
        /// <param name="customHelper">若要创建的辅助器类型为空时，使用的自定义辅助器类型。</param>
        /// <param name="index">要创建的辅助器索引。</param>
        /// <returns>创建的辅助器。</returns>
        public static T CreateHelper<T>(string helperTypeName, T customHelper, int index) where T : MonoBehaviour
        {
            T helper = null;
            if (!string.IsNullOrEmpty(helperTypeName))
            {
                System.Type helperType = Utility.Assembly.GetType(helperTypeName);
                if (helperType == null)
                { 
                    Debug.LogWarning($"Can not find helper type '{helperTypeName}'.");
                    return null;
                }

                if (!typeof(T).IsAssignableFrom(helperType))
                {
                    Debug.LogWarning($"Type '{ typeof(T).FullName}' is not assignable from '{helperType.FullName}'.");
                    return null;
                }

                helper = (T)new GameObject().AddComponent(helperType);
            }
            else if (customHelper == null)
            {
                Debug.LogWarning($"You must set custom helper with '{typeof(T).FullName}' type first.");
                return null;
            }
            else if (customHelper.gameObject.scene.name != null)
            {
                helper = index > 0 ? Object.Instantiate(customHelper) : customHelper;
            }
            else
            {
                helper = Object.Instantiate(customHelper);
            }

            return helper;
        }
    }
}
