using DAL.Dal;
using DAL.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Mvc
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> BuildList<T>(this IEnumerable<T> items, Func<T, string> text, Func<T, int> value = null, Func<T, bool> selected = null)
        {
            items = EnSureList(items);
            return items.Select(delegate (T x)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = text(x);
                selectListItem.Value = (value == null) ? text(x) : value(x).ToString(System.Globalization.CultureInfo.InvariantCulture);
                selectListItem.Selected = selected != null && selected(x);
                return selectListItem;
            }
            );
        }
        public static IEnumerable<SelectListItem> BuildList<T>(this IEnumerable<T> items, Func<T, string> text, Func<T, string> value = null, Func<T, bool> selected = null)
        {
            items = EnSureList(items);
            return items.Select(delegate (T x)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = text(x);
                selectListItem.Value = (value == null) ? text(x) : value(x).ToString(System.Globalization.CultureInfo.InvariantCulture);
                selectListItem.Selected = selected != null && selected(x);
                return selectListItem;
            }
            );
        }
        public static IEnumerable<Sort> BuildSortList<T>(this IEnumerable<T> items, Func<T, int> id, Func<T, string> name)
        {
            List<Sort> list = new List<Sort>();
            foreach (T current in items)
            {
                Sort item = new Sort
                {
                    SortId = id(current),
                    Name = name(current),
                    Order = ((IOrderable)current).Order
                };
                list.Add(item);
            }
            return from x in list orderby x.Order select x;
        }
        public static IEnumerable<TreeParentNode> BuildTreeParentList<T>(this IEnumerable<T> items, Func<T, string> name, Func<T, int> treeParentNodeId, Func<T, bool> check = null, string icon = null) where T : NestedTree
        {
            IList<TreeParentNode> list = new List<TreeParentNode>();
            items = EnSureList(items);
            IList<T> list2 = (items as IList<T>) ?? items.ToList();
            using (IEnumerator<T> enumerator = list2.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;
                    T t = list2.FirstOrDefault(delegate (T m)
                    {
                        int arg_23_0 = m.Depth;
                        T item3 = item;
                        int arg_6F_0;
                        if (arg_23_0 == item3.Depth - 1)
                        {
                            int arg_46_0 = m.Left;
                            item3 = item;
                            if (arg_46_0 < item3.Left)
                            {
                                int arg_69_0 = m.Right;
                                item3 = item;
                                arg_6F_0 = ((arg_69_0 > item3.Right) ? 1 : 0);
                                return arg_6F_0 != 0;
                            }
                        }
                        arg_6F_0 = 0;
                        return arg_6F_0 != 0;
                    }
                    );
                    int value = (t != null) ? treeParentNodeId(t) : 0;
                    TreeParentNode treeParentNode = new TreeParentNode();
                    treeParentNode.TreeParentNodeId = treeParentNodeId(item);
                    treeParentNode.ParentId = new int?(value);
                    treeParentNode.Name = name(item);
                    treeParentNode.Checked = (check != null && check(item));
                    TreeParentNode arg_EC_0 = treeParentNode;
                    T item4 = item;
                    arg_EC_0.IsLeaf = item4.IsLeaf;
                    treeParentNode.IconSkin = icon;
                    treeParentNode.Enable = true;
                    TreeParentNode item2 = treeParentNode;
                    list.Add(item2);
                }
            }
            return list;
        }
        public static IEnumerable<TreeSetNode> BuildTreeSetList<T>(this IEnumerable<T> items, Func<T, string> name, Func<T, int> treeSetNodeId, Func<T, bool> check = null, string icon = null) where T : NestedTree
        {
            IList<TreeSetNode> list = new List<TreeSetNode>();
            items = EnSureList(items);
            IList<T> list2 = (items as IList<T>) ?? items.ToList();
            using (IEnumerator<T> enumerator = list2.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;
                    T t = list2.FirstOrDefault(delegate (T m)
                    {
                        int arg_23_0 = m.Depth;
                        T item2 = item;
                        int arg_6F_0;
                        if (arg_23_0 == item2.Depth - 1)
                        {
                            int arg_46_0 = m.Left;
                            item2 = item;
                            if (arg_46_0 < item2.Left)
                            {
                                int arg_69_0 = m.Right;
                                item2 = item;
                                arg_6F_0 = ((arg_69_0 > item2.Right) ? 1 : 0);
                                return arg_6F_0 != 0;
                            }
                        }
                        arg_6F_0 = 0;
                        return arg_6F_0 != 0;
                    }
                    );
                    if (t == null)
                    {
                        T node = item;
                        List<TreeSetNode> children = (
                            from m in list2
                            where m.Left > node.Left && m.Right < node.Right
                            select m).BuildTreeSetList(name, treeSetNodeId, check, null).ToList();
                        TreeSetNode item3 = new TreeSetNode
                        {
                            TreeSetNodeId = treeSetNodeId(item),
                            Name = name(item),
                            Checked = check != null && check(item),
                            Children = children,
                            IconSkin = icon
                        };
                        list.Add(item3);
                    }
                }
            }
            return list;
        }
        public static IEnumerable<TreeSetNode> BuildTreeSetList<T>(this IEnumerable<T> items, Func<T, int> treeSetNodeId, Func<T, string> name, Func<T, IList<TreeSetNode>> children, Func<T, bool> check = null, string icon = null)
        {
            items = SelectListExtensions.EnSureList<T>(items);
            return
                from x in items
                select new TreeSetNode
                {
                    TreeSetNodeId = treeSetNodeId(x),
                    Name = name(x),
                    Checked = check != null && check(x),
                    Children = children(x),
                    IconSkin = icon
                };
        }
        private static IEnumerable<T> EnSureList<T>(IEnumerable<T> list)
        {
            IEnumerable<T> result;
            if (list is IQueryable)
            {
                IQueryable<T> queryable = list as IQueryable<T>;
                if (queryable != null)
                {
                    result = queryable.ToList();
                    return result;
                }
            }
            result = list;
            return result;
        }
    }
}
