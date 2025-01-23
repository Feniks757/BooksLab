using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using Microsoft.EntityFrameworkCore;

namespace BookApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly BookContext _context;
        private readonly BookSearch _bookSearch;
        private string _userId;
        private string _title;
        private string _author;
        private string _genres;
        private string _publicationYear;
        private string _annotation;
        private string _isbn;
        private string _searchQuery;
        private int _selectedSearchType;
        private ObservableCollection<Book> _books;
        private string _noResultsMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            _context = new BookContext();
            _bookSearch = new BookSearch();
            Books = new ObservableCollection<Book>();
            AddBookCommand = new RelayCommand(async () => await AddBook());
            SearchCommand = new RelayCommand(async () => await SearchBooks());
            FetchBooksCommand = new RelayCommand(async () => await FetchBooks());
        }

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public string Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged();
            }
        }

        public string PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged();
            }
        }

        public string Annotation
        {
            get => _annotation;
            set
            {
                _annotation = value;
                OnPropertyChanged();
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public int SelectedSearchType
        {
            get => _selectedSearchType;
            set
            {
                _selectedSearchType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public string NoResultsMessage
        {
            get => _noResultsMessage;
            set
            {
                _noResultsMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddBookCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand FetchBooksCommand { get; }

        private async Task AddBook()
        {
            try
            {
                int userId = int.Parse(UserId);
                string title = Title;
                string author = Author;
                List<string> genres = Genres.Split(',').Select(g => g.Trim()).ToList();
                int publicationYear = int.Parse(PublicationYear);
                string annotation = Annotation;
                string isbn = ISBN;

                Book newBook = new Book(title, author, genres, publicationYear, annotation, isbn, userId);

                await _context.AddBookAsync(newBook);

                // Очистка полей после добавления книги
                UserId = string.Empty;
                Title = string.Empty;
                Author = string.Empty;
                Genres = string.Empty;
                PublicationYear = string.Empty;
                Annotation = string.Empty;
                ISBN = string.Empty;
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                NoResultsMessage = $"Ошибка при добавлении книги: {ex.Message}";
            }
        }

        private async Task SearchBooks()
        {
            List<Book> results = new List<Book>();

            switch (SelectedSearchType)
            {
                case 0: // Название
                    results = await _bookSearch.SearchByTitleAsync(_context, SearchQuery);
                    break;
                case 1: // Автор
                    results = await _bookSearch.SearchByAuthorAsync(_context, SearchQuery);
                    break;
                case 2: // ISBN
                    results = await _bookSearch.SearchByISBNAsync(_context, SearchQuery);
                    break;
                case 3: // Ключевые слова
                    results = await _bookSearch.SearchByKeywords(_context, SearchQuery);
                    break;
                default:
                    NoResultsMessage = "Неверный тип поиска";
                    return;
            }

            Books.Clear();
            foreach (var book in results)
            {
                Books.Add(book);
            }

            NoResultsMessage = results.Any() ? string.Empty : "Ничего не найдено";
        }

        private async Task FetchBooks()
        {
            var books = await _context.GetAllBooksAsync();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }

            NoResultsMessage = books.Any() ? string.Empty : "Ничего не найдено";
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
