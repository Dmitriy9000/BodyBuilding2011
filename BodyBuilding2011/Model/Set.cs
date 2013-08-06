using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class Set : INotifyPropertyChanged
    {
        public ExcerciseResult Parent;

        private string _name;

        // Для сериализации
        private Set(){}

        public Set(ExcerciseResult p, string name = null)
        {
            PropertyChanged += Set_PropertyChanged;
            Parent = p;
            Name = name;
        }

        public float Weight { get; set; }
        public int Repeats { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Set_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public void Remove()
        {
            Parent.Sets.Remove(this);
        }
    }

    public class SetValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = value.ToString();
            int repeats = Int32.Parse(s);
            if (repeats < 0)
            {
                return new ValidationResult(false, "Вес не может быть отрицательным");
            }
            else if (repeats >= 10000)
            {
                return new ValidationResult(false, "Слишком много повторов");
            }
            else
            {
                return new ValidationResult(true, "");
            }
        }
    }
}