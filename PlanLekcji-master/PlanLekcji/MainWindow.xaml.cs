using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlanLekcji
{
 
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Subjects { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddSubject(SubjectTextBox.Text);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsListBox.SelectedItem is string selected)
            {
                var result = MessageBox.Show($"Usunąć przedmiot \"{selected}\"?", "Potwierdź usunięcie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Subjects.Remove(selected);
                }
            }
        }

        private void SubjectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = SubjectsListBox.SelectedItem != null;
        }

        private void SubjectTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddSubject(SubjectTextBox.Text);
                e.Handled = true;
            }
        }

        private void AddSubject(string text)
        {
            var s = (text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Podaj nazwę przedmiotu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Subjects.Contains(s))
            {
                MessageBox.Show("Taki przedmiot już istnieje.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Subjects.Add(s);
            SubjectTextBox.Clear();
            SubjectTextBox.Focus();
            SubjectsListBox.SelectedItem = s;
        }
    }
}