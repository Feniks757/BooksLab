using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BooksLab.Books;
using BooksLab.ConsoleCommands;
using BooksLab.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookApp
{
    public class MainWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly BookContext _context;
        private readonly BookSearch _bookSearch;
        private string _userId = "";
        private string _title  = "";
        private string _author  = "";
        private string _genres  = "";
        private string _publicationYear  = "";
        private string _annotation  = "";
        private string _isbn  = "";
        private string _searchQuery  = "";
        private int _selectedSearchType;
        private ObservableCollection<Book> _books;
        private string _noResultsMessage;
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public MainWindowViewModel()
        {
            _context = new BookContext();
            _bookSearch = new BookSearch();
            Books = new ObservableCollection<Book>();
            //привязка команд к исполняемым функциям
            AddBookCommand = new RelayCommand( () => AddBook(), CanAddBook);
            SearchCommand = new RelayCommand( () => SearchBooks());
            FetchBooksCommand = new RelayCommand( () => FetchBooks());
        }

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
                ValidateUserId();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                ValidateTitle();
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
                ValidateAuthor();
            }
        }

        public string Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged();
                ValidateGenres();
            }
        }

        public string PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged();
                ValidatePublicationYear();
            }
        }

        public string Annotation
        {
            get => _annotation;
            set
            {
                _annotation = value;
                OnPropertyChanged();
                ValidateAnnotation();
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropertyChanged();
                ValidateISBN();
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

        public bool HasErrors => _errors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            (AddBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void ValidateUserId()
        {
            if (!Validator.ValidateUserId(UserId, out _))
                AddError(nameof(UserId), " Invalid User ID");
            else
                ClearErrors(nameof(UserId));
        }

        private void ValidateTitle()
        {
            if (!Validator.ValidateBookTitle(Title))
                AddError(nameof(Title), " Title is required");
            else
                ClearErrors(nameof(Title));
        }

        private void ValidateAuthor()
        {
            if (!Validator.ValidateAuthorName(Author))
                AddError(nameof(Author), " Author is required");
            else
                ClearErrors(nameof(Author));
        }

        private void ValidateGenres()
        {
            if (!Validator.ValidateGenres(Genres, out _))
                AddError(nameof(Genres), " Genres are required");
            else
                ClearErrors(nameof(Genres));
        }

        private void ValidatePublicationYear()
        {
            if (!int.TryParse(PublicationYear, out _))
                AddError(nameof(PublicationYear), " Publication Year is required and must be a number");
            else
                ClearErrors(nameof(PublicationYear));
        }

        private void ValidateAnnotation()
        {
            if (!Validator.ValidateAnnotation(Annotation))
                AddError(nameof(Annotation), " Annotation is required");
            else
                ClearErrors(nameof(Annotation));
        }

        private void ValidateISBN()
        {
            if (!Validator.ValidateISBN(ISBN))
                AddError(nameof(ISBN), " Invalid ISBN");
            else
                ClearErrors(nameof(ISBN));
        }

        //вывести сообщение об ошибке рядом с неверным полем
        private void AddError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
            _errors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        
        //отчистить сообщение об ошибке рядом с неверным полем
        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private bool CanAddBook()
        {
            return !HasErrors;
        }

        private async void AddBook()
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

        private async void SearchBooks()
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
                    NoResultsMessage = " Неверный тип поиска";
                    return;
            }

            Books.Clear();
            foreach (var book in results)
            {
                Books.Add(book);
            }

            NoResultsMessage = results.Any() ? string.Empty : " Ничего не найдено";
        }

        
        private async void FetchBooks()
        {
            var books = await _context.GetAllBooksAsync();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }

            NoResultsMessage = books.Any() ? string.Empty : " Ничего не найдено";
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

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
