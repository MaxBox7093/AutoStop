using AutoStop.APIServices;
using AutoStop.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoStop
{
    public partial class ViewAutoPage : ContentPage
    {
        public ObservableCollection<Car> Cars { get; set; }
        private readonly GetCarAPI _getCarAPI;
        private readonly DeleteCarAPI _deleteCarAPI;

        public ICommand EditCarCommand { get; }
        public ICommand DeleteCarCommand { get; }

        public ViewAutoPage()
        {
            InitializeComponent();

            Cars = new ObservableCollection<Car>();
            _getCarAPI = new GetCarAPI();
            _deleteCarAPI = new DeleteCarAPI();
            EditCarCommand = new Command<Car>(OnEditCar);
            DeleteCarCommand = new Command<Car>(OnDeleteCar);

            BindingContext = this;

            LoadCars();
        }

        private async void LoadCars()
        {
            var cars = await _getCarAPI.GetCar();
            if (cars != null)
            {
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось загрузить автомобили", "OK");
            }
        }

        private async void OnEditCar(Car car)
        {
            //Add methods for editing cars in BD
        }

        private void OnDeleteCar(Car car)
        {
            Cars.Remove(car);
            DeleteCarAsync(car);
            LoadCars();
        }

        private async void OnAddCarButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarAddPage());
        }

        private async void DeleteCarAsync(Car car)
        {
            bool success = await _deleteCarAPI.DeleteCar(car);
            if (success)
            {
                await DisplayAlert("Успех", "Автомобиль успешно удалён", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Не удалось удалить автомобиль из вашего списка автомобилей", "OK");
            }
        }
    }
}
