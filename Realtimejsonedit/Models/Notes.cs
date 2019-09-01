using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Realtimejsonedit
{
    
    public class Notes : INotifyPropertyChanged
    {
        public int created { get; set; }


       //public string title { get; set; }

        private string Title;

        public string title
        {
            get { return Title; }
            set {
                Title = value;
                NotifyPropertyChanged("title");
                
            }
        }
        public string text { get; set; }
        public int id { get; set; }
        public int updated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
               
            }
        }
       
      
    }

    public class baseNotes
    {
        public List<Notes> notes { get; set; }
         
    } 
}