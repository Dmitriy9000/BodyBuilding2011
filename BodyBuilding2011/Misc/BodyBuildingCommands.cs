using System.Windows.Input;

namespace BodyBuilding2011.Misc
{
    internal class BodyBuildingCommands
    {
        private static readonly RoutedUICommand AddExcerciseCommand;
        private static readonly RoutedUICommand AddSetCommand;
        private static readonly RoutedUICommand ExportResultsCommand;

        static BodyBuildingCommands()
        {
            AddExcerciseCommand = new RoutedUICommand("Добавить упражнение", "AddExcercise",
                                                      typeof (BodyBuildingCommands));
            AddSetCommand = new RoutedUICommand("Добавить подход", "AddSet", typeof (BodyBuildingCommands));
            ExportResultsCommand = new RoutedUICommand("Экспортировать результаты", "ExportResults", typeof(BodyBuildingCommands));
        }

        public static RoutedUICommand AddExcercise
        {
            get { return AddExcerciseCommand; }
        }

        public static RoutedUICommand AddSet
        {
            get { return AddSetCommand; }
        }

        public static RoutedUICommand ExportResults
        {
            get
            {
                return ExportResultsCommand;
            }
        }
    }
}