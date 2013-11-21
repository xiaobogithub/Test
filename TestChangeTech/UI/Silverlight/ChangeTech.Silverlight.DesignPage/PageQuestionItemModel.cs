using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChangeTech.Silverlight.DesignPage
{
    public class PageQuestionModel : INotifyPropertyChanged
    {
        private string _name;
        private string _caption;
        private bool _isRequired;
        private string _item;
        private string _feedback;
        private int _score;
        private ObservableCollection<PageQuestionItemModel> _subItems;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; NotifyPropertyChanged("Caption"); }
        }

        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; NotifyPropertyChanged("IsRequired"); }
        }

        public string Item
        {
            get { return _item; }
            set { _item = value; NotifyPropertyChanged("Item"); }
        }

        public string Feedback
        {
            get { return _feedback; }
            set { _feedback = value; NotifyPropertyChanged("Feedback"); }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; NotifyPropertyChanged("Score"); }
        }

        public ObservableCollection<PageQuestionItemModel> SubItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class PageQuestionItemModel : INotifyPropertyChanged
    {
        private string _item;
        private string _feedback;
        private int _score;

        public string Item
        { 
            get { return _item; } 
            set { _item = value; NotifyPropertyChanged("Item"); }
        }

        public string Feedback
        {
            get { return _feedback; }
            set { _feedback = value; NotifyPropertyChanged("Feedback"); }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; NotifyPropertyChanged("Score"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    //public class PageQuestionItemModels : List<PageQuestionItemModel>
    //{ }

    //public class PageQuestionModels : List<PageQuestionModel>
    //{

    //}
}
