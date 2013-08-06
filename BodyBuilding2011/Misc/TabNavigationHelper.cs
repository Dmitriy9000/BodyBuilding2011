using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BodyBuilding2011.Misc
{
    internal class TabNavigationHelper
    {
        private readonly List<TextBox> _controls;

        public TabNavigationHelper(DependencyObject owner)
        {
            _controls = new List<TextBox>();
            GetChildOfType(owner, ref _controls);
        }

        public TextBox GoRight(TextBox box)
        {
            int index = _controls.IndexOf(box);
            if (index != -1)
            {
                int next = index + 1;
                if (_controls.Count > next)
                    return _controls[next];
                else
                {
                    return _controls[0];
                }
            }
            return null;
        }

        public TextBox GoLeft(TextBox box)
        {
            int index = _controls.IndexOf(box);
            if (index != -1)
            {
                int next = index - 1;
                if (next >= 0)
                    return _controls[next];
                else
                {
                    return _controls[_controls.Count - 1];
                }
            }
            return null;
        }

        public TextBox GoUp(TextBox box)
        {
            int index = _controls.IndexOf(box);
            if (index != -1)
            {
                int next = index - 2;
                if (next >= 0)
                    return _controls[next];
                else
                {
                    return GoLeft(box);
                }
            }
            return null;
        }

        public TextBox GoDown(TextBox box)
        {
            int index = _controls.IndexOf(box);
            if (index != -1)
            {
                int next = index + 2;
                if (_controls.Count > next)
                    return _controls[next];
                else
                {
                    return GoRight(box);
                }
            }
            return null;
        }

        /// <summary>
        /// Вспомогательная функция для обхода коллекции потомков
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static void GetChildOfType<T>(DependencyObject depObj, ref List<T> list) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                if (child is T)
                    list.Add((T) child);
                else
                    GetChildOfType(child, ref list);
            }
        }
    }
}