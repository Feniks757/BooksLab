using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.EntityFrameworkCore;

namespace BookApp
{
    public partial class MainWindow : Window
    {
        private readonly BookContext _context;
        private readonly BookSearch _bookSearch;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            //_context = new BookContext();
            //_bookSearch = new BookSearch();
        }
        /*
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchType = SearchTypeComboBox.SelectedIndex;
            string query = SearchQueryTextBox.Text;

            List<Book> results = new List<Book>();

            switch (searchType)
            {
                case 0: // Название
                    results = await _bookSearch.SearchByTitleAsync(_context, query);
                    break;
                case 1: // Автор
                    results = await _bookSearch.SearchByAuthorAsync(_context, query);
                    break;
                case 2: // ISBN
                    results = await _bookSearch.SearchByISBNAsync(_context, query);
                    break;
                case 3: // Ключевые слова
                    results = await _bookSearch.SearchByKeywords(_context, query);
                    break;
                default:
                    MessageBox.Show("Неверный тип поиска", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            BookTable.ItemsSource = results;
            NoResultsMessage.Visibility = results.Any() ? Visibility.Hidden : Visibility.Visible;
        }

        private async void FetchBooksButton_Click(object sender, RoutedEventArgs e)
        {
            var books = await _context.GetAllBooksAsync();
            BookTable.ItemsSource = books;
            NoResultsMessage.Visibility = books.Any() ? Visibility.Hidden : Visibility.Visible;
        }

        private async void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userId = int.Parse(UserIdTextBox.Text);
                string title = TitleTextBox.Text;
                string author = AuthorTextBox.Text;
                List<string> genres = GenresTextBox.Text.Split(',').Select(g => g.Trim()).ToList();
                int publicationYear = int.Parse(PublicationYearTextBox.Text);
                string annotation = AnnotationTextBox.Text;
                string isbn = IsbnTextBox.Text;

                Book newBook = new Book(title, author, genres, publicationYear, annotation, isbn, userId);

                await _context.AddBookAsync(newBook);

                MessageBox.Show("Книга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очистка полей после добавления книги
                UserIdTextBox.Clear();
                TitleTextBox.Clear();
                AuthorTextBox.Clear();
                GenresTextBox.Clear();
                PublicationYearTextBox.Clear();
                AnnotationTextBox.Clear();
                IsbnTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
    }
}
