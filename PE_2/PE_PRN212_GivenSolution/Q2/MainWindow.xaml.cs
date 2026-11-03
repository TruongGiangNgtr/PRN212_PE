using Q2.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadGenres();
            LoadStudios();
            LoadMovieList();
            LoadGenres_AddMovie();
            LoadStudios_AddMovie();
        }

        private void LoadStudios_AddMovie()
        {
            using var db = new Prn2123Context();
            var studios = db.Studios.ToList();
            cmbStudio_AddMovie.ItemsSource = studios;
            cmbStudio_AddMovie.DisplayMemberPath = "StudioName";
            cmbStudio_AddMovie.SelectedValuePath = "StudioId";
            cmbStudio_AddMovie.SelectedIndex = 0;
        }

        private void LoadGenres_AddMovie()
        {
            using var db = new Prn2123Context();
            var genres = db.Genres.ToList();
            cmbGenre_AddMovie.ItemsSource = genres;
            cmbGenre_AddMovie.DisplayMemberPath = "GenreName";
            cmbGenre_AddMovie.SelectedValuePath = "GenreId";
            cmbGenre_AddMovie.SelectedIndex = 0;
        }

        private void LoadGenres()
        {
            using var db = new Prn2123Context();
            var gernes = db.Genres.ToList();

            gernes.Insert(0, new Genre
            {
                GenreId = 0,
                GenreName = "All"
            });

            cmbGenre.ItemsSource = gernes;
            cmbGenre.DisplayMemberPath = "GenreName";
            cmbGenre.SelectedValuePath = "GenreId";
            cmbGenre.SelectedIndex = 0;
        }

        private void LoadStudios()
        {
            using var db = new Prn2123Context();
            var studios = db.Studios.ToList();

            studios.Insert(0, new Studio
            {
                StudioId = 0,
                StudioName = "All"
            });
            cmbStudio.ItemsSource = studios;
            cmbStudio.DisplayMemberPath = "StudioName";
            cmbStudio.SelectedValuePath = "StudioId";
            cmbStudio.SelectedIndex = 0;
        }

        private void LoadMovieList()
        {
            using var db = new Prn2123Context();
            var movies = db.Movies.Select(m => new
            {
                m.MovieId,
                m.Title,
                m.RatingCode,
                m.ReleaseYear,
                m.DurationMinutes,
                m.Genre.GenreName,
                m.Studio.StudioName
            }).ToList();

            dgMovieList.ItemsSource = movies;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Prn2123Context();
            var genreId = (int)cmbGenre.SelectedValue;
            var studioId = (int)cmbStudio.SelectedValue;

            var movies = db.Movies.Where(m => genreId == 0 || m.GenreId == genreId)
                                   .Where(m => studioId == 0 || m.StudioId == studioId)
                                   .Select(m => new
                                   {
                                       m.MovieId,
                                       m.Title,
                                       m.RatingCode,
                                       m.ReleaseYear,
                                       m.DurationMinutes,
                                       m.Genre.GenreName,
                                       m.Studio.StudioName
                                   })
                                   .ToList();

            dgMovieList.ItemsSource = movies;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbGenre.SelectedIndex = 0;
            cmbStudio.SelectedIndex = 0;
            LoadMovieList();
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Prn2123Context();
            var title = txtTitle.Text;
            var ratingCode = txtRatingCode.Text;
            var releaseYear = txtReleaseYear.Text;
            var durationMinutes = txtDurationMinutes.Text;
            var genreId = (int)cmbGenre_AddMovie.SelectedValue;
            var studioId = (int)cmbStudio_AddMovie.SelectedValue;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a movie title.");
                return;
            }

            if (string.IsNullOrEmpty(ratingCode))
            {
                MessageBox.Show("Please enter a rating code.");
                return;
            }

            if (!int.TryParse(releaseYear, out int intReleaseYear))
            {
                MessageBox.Show("Please enter a valid release year.");
                return;
            }

            if (!int.TryParse(durationMinutes, out int intDurationMinutes))
            {
                MessageBox.Show("Please enter a valid duration.");
                return;
            }

            var newMovie = new Movie
            {
                Title = title,
                RatingCode = ratingCode,
                ReleaseYear = intReleaseYear,
                DurationMinutes = intDurationMinutes,
                GenreId = genreId,
                StudioId = studioId
            };

            db.Movies.Add(newMovie);
            db.SaveChanges();
            LoadMovieList();
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Clear();
            txtDurationMinutes.Clear();
            txtRatingCode.Clear();
            txtReleaseYear.Clear();
            cmbGenre_AddMovie.SelectedIndex = 0;
            cmbStudio_AddMovie.SelectedIndex = 0;
        }
    }
}