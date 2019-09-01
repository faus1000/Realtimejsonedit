using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Realtimejsonedit
{ 
    public sealed partial class MainPage : Page
    {
       // Notes notes = new Notes();
     
        //, INotifyPropertyChanged
         public string editpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Notas.json" );
        public ObservableCollection<Notes> Mynotes; 
       
        
    

        public MainPage()
        {
            this.InitializeComponent();
         

            // Load data of Notas.json to Listview
            LoadUpdate();

        }

        private void LoadUpdate()
        {
            using (StreamReader file = File.OpenText(editpath))
            {
                var json = file.ReadToEnd();
                baseNotes mainnotes = JsonConvert.DeserializeObject<baseNotes>(json);
                Mynotes = new ObservableCollection<Notes>();

                foreach (var item in mainnotes.notes)
                {
                    Mynotes.Add(new Notes { title = item.title });
                } 
                listNotas.ItemsSource = null;
                listNotas.ItemsSource = Mynotes;
                listNotas.SelectedIndex = 0;
            }
        }
 
        private void ListNotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             string json = File.ReadAllText(editpath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            titulo.Text = jsonObj["notes"][listNotas.SelectedIndex]["title"];

        }

        private void Titulo_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            #region
            string json = File.ReadAllText(editpath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            int indice = listNotas.SelectedIndex;
            jsonObj["notes"][indice]["title"] = titulo.Text;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);
            File.WriteAllText(editpath, output);
            // Show json file text in RicheditBox
            editor.TextDocument.SetText(Windows.UI.Text.TextSetOptions.None, output);            

            
            //Problem
            Binding myBinding = new Binding();
            myBinding.Source = Mynotes[indice];
            myBinding.Path = new PropertyPath("title");
            myBinding.Mode = BindingMode.TwoWay;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(titulo, TextBox.TextProperty, myBinding);
            #endregion
            //ListViewItem text = listNotas.SelectedItem as ListViewItem;
            //text.Content = "dsdg";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewValue));
        }
    }
}
