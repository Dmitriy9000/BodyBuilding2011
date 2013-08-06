using System.Windows;
using System.Windows.Input;
using BodyBuilding2011.Model;

namespace BodyBuilding2011.Windows
{
    /// <summary>
    /// Interaction logic for SupersetWindow.xaml
    /// </summary>
    public partial class SupersetWindow : Window
    {
        public SuperSet Superset;

        public SupersetWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameTb.Text) && (!string.IsNullOrWhiteSpace(infoTb.Text)) &&
                (supersetExcLb.Items.Count > 0))
            {
                Superset = new SuperSet();
                Superset.Name = nameTb.Text;
                Superset.Info = infoTb.Text;

                foreach (object i in supersetExcLb.Items)
                {
                    Superset.ExcercisesList.Add((Excercise) i);
                }

                DialogResult = true;
                Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void listBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void supersetExcLb_Drop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(typeof (Excercise));
            object newItem = ((Excercise) data).Clone();
            supersetExcLb.Items.Add(newItem);
            supersetExcLb.SelectedIndex = -1;
        }

        private void supersetExcLb_DragEnter(object sender, DragEventArgs e)
        {
            bool presenter = e.Data.GetDataPresent(typeof (Excercise));
            if ((!presenter))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        private void excercisesLb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (excercisesLb.SelectedIndex != -1)
            {
                var data = new DataObject(typeof (Excercise), excercisesLb.Items[excercisesLb.SelectedIndex]);
                DragDrop.DoDragDrop(excercisesLb, data, DragDropEffects.Move);
            }
        }

        private void supersetExcLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (supersetExcLb.SelectedIndex != -1)
            {
                supersetExcLb.Items.RemoveAt(supersetExcLb.SelectedIndex);
            }
        }
    }
}