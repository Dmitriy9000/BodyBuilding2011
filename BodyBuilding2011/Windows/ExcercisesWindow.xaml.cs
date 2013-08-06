using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BodyBuilding2011.Model;

namespace BodyBuilding2011.Windows
{
    /// <summary>
    /// Interaction logic for ExcercisesWindow.xaml
    /// </summary>
    public partial class ExcercisesWindow : Window
    {
        public Excercise Selected;

        public ExcercisesWindow()
        {
            InitializeComponent();

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(excercisesLb.ItemsSource);
            if (collectionView.GroupDescriptions.Count == 0)
                collectionView.GroupDescriptions.Add(new PropertyGroupDescription("MuscleGroups"));

            ICollectionView supersetsView = CollectionViewSource.GetDefaultView(supersetsLb.ItemsSource);
            if (supersetsView.GroupDescriptions.Count == 0)
                supersetsView.GroupDescriptions.Add(new PropertyGroupDescription("MuscleGroups"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((extypesTab.SelectedIndex == 1) && (excercisesLb.SelectedIndex != -1))
                || ((extypesTab.SelectedIndex == 2) && (supersetsLb.SelectedIndex != -1))
                || ((extypesTab.SelectedIndex == 0) && (favoritesLb.SelectedIndex != -1))
                )
            {
                if (extypesTab.SelectedIndex == 0)
                {
                    string name = favoritesLb.Items[favoritesLb.SelectedIndex].ToString();
                    Selected = MainWindow.ExcercisesLib.Lib.Single(c => c.Name == name);
                    if (Selected == null)
                    {
                        Selected = MainWindow.SuperSetsLib.Lib.Single(c => c.Name == name);
                    }
                }
                else if (extypesTab.SelectedIndex == 1)
                {
                    Selected = (Excercise) excercisesLb.Items[excercisesLb.SelectedIndex];
                }
                else
                {
                    Selected = (Excercise) supersetsLb.Items[supersetsLb.SelectedIndex];
                }

                DialogResult = true;
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void excercisesLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var w = new SupersetWindow();
            w.ShowDialog();
            if (w.DialogResult.Value)
            {
                MainWindow.SuperSetsLib.Lib.Add(w.Superset);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var s = (CheckBox) sender;
            var exc = (Excercise) s.Tag;

            MainWindow.FavoritesLib.Lib.Add(exc.Name);

            //if (exc is Excercise)
            //{
            //    if (!MainWindow.FavoritesLib.ExcLib.Contains(exc))
            //        MainWindow.FavoritesLib.ExcLib.Add((Excercise) exc);
            //}
            //else if (exc is SuperSet)
            //{
            //    if (!MainWindow.FavoritesLib.SetsLib.Contains(exc))
            //        MainWindow.FavoritesLib.SetsLib.Add((SuperSet) exc);
            //}
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var s = (CheckBox) sender;
            var exc = (Excercise) s.Tag;

            MainWindow.FavoritesLib.Lib.Remove(exc.Name);

            //if (exc is Excercise)
            //{
            //    var toRem = MainWindow.FavoritesLib.ExcLib.Where(c => c.Name == exc.Name);
            //    toRem.ToList().ForEach(c => MainWindow.FavoritesLib.ExcLib.Remove(c));
            //}
            //else
            //{
            //    var toRem = MainWindow.FavoritesLib.SetsLib.Where(c => c.Name == exc.Name);
            //    toRem.ToList().ForEach(c => MainWindow.FavoritesLib.SetsLib.Remove(c));
            //}
        }

        private void supersetsLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
        }

        private void favoritesLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, new RoutedEventArgs());
        }

        private void extypesTab_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("tab loaded");
        }

        private void extypesTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            //var exp = BindingOperations.GetBindingExpression(favoritesLb, ListBox.ItemsSourceProperty);
            //exp.UpdateTarget();


            //var fav = MainWindow.FavoritesLib;
            //var list = new List<IExcercise>();
            //list.AddRange(fav.ExcLib);
            //list.AddRange(fav.SetsLib);
            //favoritesLb.ItemsSource = list;
        }
    }
}