using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BodyBuilding2011.Misc;
using BodyBuilding2011.Model;
using BodyBuilding2011.Properties;
using Microsoft.Win32;
using Modeling;
using System.Xml.Serialization;

namespace BodyBuilding2011.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String ExcercisesFilePath = @"Data\Library\Excercises.xml";
        private const String SuperSetsFilePath = @"Data\Library\SuperSets.xml";
        private const String ResultsFilePath = @"Data\Results.bbr";
        private const String FavoritesFilePath = @"Data\Favorites.xml";
        public static TrainResults Results;
        private TabNavigationHelper _tabHelper;

        private Boolean _NoSave;

        public MainWindow()
        {
            InitializeComponent();
        }

        public static ExcercisesLib ExcercisesLib { get; private set; }
        public static SuperSets SuperSetsLib { get; private set; }
        public static Favorites FavoritesLib { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now > new DateTime(2011, 6, 20))
            {
                MessageBox.Show(
                    "Для продолжения использования программы вы должны выслать фотографии со сканами документов на адрес электронной почты ducebod@gmail.com");
                _NoSave = true;
                Close();
            }
            else
            {
                CommandBindingInit();
                LoadFiles();
                Results.SetCalendar(calendar);
            }
            //calendar.MarkedDays = new ObservableCollection<DateTime>(Results.TrainDays.Select(c => c.Date));
        }

        private void CommandBindingInit()
        {
            var addExcBinding = new CommandBinding();
            addExcBinding.Command = BodyBuildingCommands.AddExcercise;
            addExcBinding.Executed += addExcBinding_Executed;
            CommandBindings.Add(addExcBinding);

            var addSetBinding = new CommandBinding();
            addSetBinding.Command = BodyBuildingCommands.AddSet;
            addSetBinding.Executed += addSetBinding_Executed;
            CommandBindings.Add(addSetBinding);

            var exportResultsBinding = new CommandBinding();
            exportResultsBinding.Command = BodyBuildingCommands.ExportResults;
            exportResultsBinding.Executed += new ExecutedRoutedEventHandler(exportResultsBinding_Executed);
            CommandBindings.Add(exportResultsBinding);
        }

        void exportResultsBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fn = e.Parameter.ToString();
            var srl = new XmlSerializer(typeof (TrainResults));
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            srl.Serialize(fs, MainWindow.Results);
            fs.Close();
        }

        private void addSetBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var exc = (ExcerciseResult) e.Parameter;
            exc.Sets.Add(new Set(exc));
        }

        private void addExcBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                var w = new ExcercisesWindow();
                w.ShowDialog();
                if (w.DialogResult == true)
                {
                    TrainResult train = Results[calendar.SelectedDate.Value];
                    if (train == null)
                    {
                        train = new TrainResult(calendar.SelectedDate.Value, Results);
                        Results.TrainDays.Add(train);
                        resultsSp.DataContext = Results[calendar.SelectedDate.Value];
                    }
                    var res = new ExcerciseResult(w.Selected, train, 4);
                    train.Uprs.Add(res);
                }
            }
        }

        private void LoadFiles()
        {
            if (File.Exists(ResultsFilePath))
            {
                try
                {
                    Results = BinaryFileMemento<TrainResults>.Load(ResultsFilePath);
                    foreach (TrainResult r in Results.TrainDays)
                    {
                        foreach (ExcerciseResult exc in r.Uprs)
                        {
                            exc.DeserializationInit();
                        }
                        r.DeserializationInit();
                    }
                    Results.DeserializationInit();
                }
                catch (SerializationException)
                {
                    MessageBox.Show(
                        "Файл с информацией о результатах тренировок был в старом формате. Создан новый файл.",
                        "Конфликт версий", MessageBoxButton.OK, MessageBoxImage.Information);
                    Results = new TrainResults();
                }
            }
            else
            {
                MessageBox.Show(
                    "Файл с информацией о результатах треировок не найден. Создан новый файл.",
                    "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Information);
                Results = new TrainResults();
            }

            if (File.Exists(ExcercisesFilePath))
            {
                try
                {
                    ExcercisesLib = ExcercisesLib.LoadFromFile(ExcercisesFilePath);
                }
                catch (SerializationException)
                {
                    MessageBox.Show(
                        "Файл с информацией об упражнениях был в старом формате. Библиотека проинициализирована стандартными упражнениями.",
                        "Конфликт версий", MessageBoxButton.OK, MessageBoxImage.Information);
                    ExcercisesLib = new ExcercisesLib(fillLibBaseExcercises: true);
                }
            }
            else
            {
                MessageBox.Show(
                    "Файл с информацией об упражнениях не найден. Библиотека проинициализирована стандартными упражнениями.",
                    "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Information);
                ExcercisesLib = new ExcercisesLib(fillLibBaseExcercises: true);
            }

            if (File.Exists(SuperSetsFilePath))
            {
                try
                {
                    SuperSetsLib = SuperSets.LoadFromFile(SuperSetsFilePath);
                }
                catch (SerializationException)
                {
                    MessageBox.Show(
                        "Файл с информацией о суперсетах был в старом формате. Создан новый файл.",
                        "Конфликт версий", MessageBoxButton.OK, MessageBoxImage.Information);
                    SuperSetsLib = new SuperSets();
                }
            }
            else
            {
                MessageBox.Show(
                    "Файл с информацией о суперсетах не найден. Создан новый файл.",
                    "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Information);
                SuperSetsLib = new SuperSets();
            }

            if (File.Exists(FavoritesFilePath))
            {
                try
                {
                    FavoritesLib = Favorites.LoadFromFile(FavoritesFilePath);
                }
                catch (SerializationException)
                {
                    MessageBox.Show(
                        "Файл с информацией об избранных упражнениях был в старом формате. Создан новый файл.",
                        "Конфликт версий", MessageBoxButton.OK, MessageBoxImage.Information);
                    FavoritesLib = new Favorites();
                }
            }
            else
            {
                MessageBox.Show(
                    "Файл с информацией об избранных упражнениях не найден. Создан новый файл.",
                    "Файл не найден", MessageBoxButton.OK, MessageBoxImage.Information);
                FavoritesLib = new Favorites();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_NoSave)
            {
                Settings.Default.Save();

                var saver = new BinaryFileMemento<TrainResults>(Results);
                saver.Save(@"Data\Results.bbr");
                SuperSetsLib.SaveToFile(SuperSetsFilePath);
                FavoritesLib.SaveToFile(FavoritesFilePath);
            }
        }

        //private void traindaysLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (traindaysLb.SelectedIndex != -1)
        //    {
        //        var res = (TrainResult)traindaysLb.Items[traindaysLb.SelectedIndex];
        //        calendar.SetDate(res.Date);
        //    }
        //}

        private void calendar_SelectedDatesChanged()
        {
            if (calendar.SelectedDate.HasValue)
            {
                resultsSp.DataContext = Results[calendar.SelectedDate.Value];
            }
        }

        private void removeSet_Click(object sender, RoutedEventArgs e)
        {
            var set = ((Button) sender).Tag as Set;
            set.Remove();
        }

        private void removeExcResult_Click(object sender, RoutedEventArgs e)
        {
            var result = ((Button) sender).Tag as ExcerciseResult;
            result.Remove();
        }

        private void OpenLib_Click(object sender, RoutedEventArgs e)
        {
            var w = new ExcercisesWindow();
            w.ShowDialog();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox) sender).SelectAll();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Tab) || (e.Key == Key.Right))
            {
                _tabHelper = new TabNavigationHelper(resultsTv);
                TextBox next = _tabHelper.GoRight((TextBox) sender);
                if (next != null)
                {
                    next.Focus();
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Left)
            {
                _tabHelper = new TabNavigationHelper(resultsTv);
                TextBox next = _tabHelper.GoLeft((TextBox) sender);
                if (next != null)
                {
                    next.Focus();
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Down)
            {
                _tabHelper = new TabNavigationHelper(resultsTv);
                TextBox next = _tabHelper.GoDown((TextBox) sender);
                if (next != null)
                {
                    next.Focus();
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.Up)
            {
                _tabHelper = new TabNavigationHelper(resultsTv);
                TextBox next = _tabHelper.GoUp((TextBox) sender);
                if (next != null)
                {
                    next.Focus();
                    e.Handled = true;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var w = new ProgWindow();
            w.ShowDialog();
        }

        private void exportResults_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "*.xml|*.xml";
            if (dlg.ShowDialog().Value)
            {
                BodyBuildingCommands.ExportResults.Execute(dlg.FileName, this);   
            }
        }
    }
}