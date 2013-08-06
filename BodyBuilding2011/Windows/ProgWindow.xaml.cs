using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BodyBuilding2011.Misc;
using BodyBuilding2011.Model;
using Microsoft.Win32;

namespace BodyBuilding2011.Windows
{
    /// <summary>
    /// Interaction logic for ProgWindow.xaml
    /// </summary>
    public partial class ProgWindow : Window
    {
        TrainProg Prog { get; set; }

        public ProgWindow()
        {
            Prog = new TrainProg();
            this.DataContext = Prog;
            InitializeComponent();
            dayMuscleGroups.Items.Add("Трапециевидные");
            dayMuscleGroups.Items.Add("Дельтовидные");
            dayMuscleGroups.Items.Add("Трицепсы");
            dayMuscleGroups.Items.Add("Бицепсы");
            dayMuscleGroups.Items.Add("Предплечья");
            dayMuscleGroups.Items.Add("Грудные");
            dayMuscleGroups.Items.Add("Спины");
            dayMuscleGroups.Items.Add("Живота");
            dayMuscleGroups.Items.Add("Ягодичные");
            dayMuscleGroups.Items.Add("Бедра");
            dayMuscleGroups.Items.Add("Икроножные");

            for (int i = 0; i < 7;i++ )
            {
                Prog.Cycle.Add(new TrainProgDay(Prog));
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Prog.Cycle.Count > 0)
            {
                Prog.Cycle.RemoveAt(Prog.Cycle.Count - 1);
                progDays.SelectedIndex = -1;
            }
                
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Prog.Cycle.Add(new TrainProgDay(Prog));
        }

        private void Debugger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void progDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (progDays.SelectedIndex != -1)
            {
                
                // Загружаем новый день

                dayMuscleGroups.DataContext = progDays.Items[progDays.SelectedIndex];

                var checkboxes = new List<CheckBox>();
                TabNavigationHelper.GetChildOfType(dayMuscleGroups, ref checkboxes);
                //checkboxes.ForEach(c => c.IsChecked = false);

                for (int i = 0; i < dayMuscleGroups.Items.Count; i++ )
                {
                    TrainProgDay day = (TrainProgDay)dayMuscleGroups.DataContext;
                    var item = dayMuscleGroups.Items[i];

                    MuscleGroup group;
                    if (Enum.TryParse(item.ToString(), out group))
                    {
                        if (day.Groups.Contains(group))
                        {
                            checkboxes[i].IsChecked = true;
                            //dayMuscleGroups.Items[]
                        }
                        else
                        {
                            checkboxes[i].IsChecked = false;
                        }
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MuscleGroup grp;
            Enum.TryParse<MuscleGroup>(((CheckBox) sender).Content.ToString(), out grp);

            var day = (TrainProgDay) dayMuscleGroups.DataContext;

            if (!day.Groups.Contains(grp))
                day.Groups.Add(grp);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MuscleGroup grp;
            Enum.TryParse<MuscleGroup>(((CheckBox)sender).Content.ToString(), out grp);

            var day = (TrainProgDay)dayMuscleGroups.DataContext;
            if (day.Groups.Contains(grp))
                day.Groups.Remove(grp);
        }

        private void saveBt_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var dlg = new SaveFileDialog {Filter = "*.bbp|*.bbp"};
                if (dlg.ShowDialog() == true)
                {
                    Prog.SaveToFile(dlg.FileName);
                    Close();
                }
            }
        }

        private void cancelBt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
