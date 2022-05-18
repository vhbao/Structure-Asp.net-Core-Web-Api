using Wolf.Core.Abstracts;
using Wolf.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wolf.Core.Core
{
    public class TreeMultiLevel<T> where T : absTree<T>
    {
        public static int NodeLevel(T nodeChild, List<T> listData)
        {
            int level = -1;
            while (nodeChild != null)
            {
                var nodeParent = listData.FirstOrDefault(o => o.Id == nodeChild.ParentId);
                nodeChild = nodeParent;
                level++;
            }
            return level < 0 ? 0 : level;
        }
        public static List<T> ListToTree(List<T> listData, bool isShowLevelNodes = false, string rootId = "", string symbolLevel = "-", int startLevel = 0)
        {
            List<T> treeData = new List<T>();
            Dictionary<string, int> map = new Dictionary<string, int>();
            int lenthData = listData.Count; 
            for (int i = 0; i < lenthData; i++)
            {
                if (isShowLevelNodes)
                {                    
                    int level = NodeLevel(listData[i], listData);
                    level = level - startLevel;
                    listData[i].NodeLevel = level;
                    listData[i].Name = listData[i].Name.MultiInsert(symbolLevel, level, 0);
                }
                map.Add(listData[i].Id, i);
            }
            for (int i = 0; i < lenthData; i++)
            {
                var node = listData[i];
                if (string.IsNullOrEmpty(node.ParentId) || node.ParentId == Guid.Empty.ToString())
                {
                    treeData.Add(node);
                }
                else
                {
                    if (map.ContainsKey(node.ParentId))
                    {
                        listData[map[node.ParentId]].Children.Add(node);
                    }
                }
            }
            if (!string.IsNullOrEmpty(rootId))
            {
                treeData = new List<T>();
                for (int i = 0; i < lenthData; i++)
                {
                    var node = listData[i];
                    if(node.Id == rootId)
                    {
                        treeData.Add(node);
                        break;
                    }    
                }
            }
            return treeData;
        }
    }
}
