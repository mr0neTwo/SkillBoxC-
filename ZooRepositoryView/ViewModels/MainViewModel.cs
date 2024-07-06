using System.Collections.ObjectModel;
using System.Windows.Input;
using Models.DataExporters;
using Models.Models;
using ZooRepositoryView.Views;
using Animal = Models.Animals.Animal;

namespace ZooRepositoryView.ViewModels;

public sealed class MainViewModel : ViewModel
{
	public ObservableCollection<Animal> Animals { get; }

	public ICommand AddAnimalCommand => new DelegateCommand(AddAnimal);
	public ICommand SelectAnimalCommand => new DelegateCommand(SelectAnimal);
	public ICommand EditAnimalCommand => new DelegateCommand(EditAnimal, IsAnimalSelected);
	public ICommand RemoveAnimalCommand => new DelegateCommand(RemoveAnimal, IsAnimalSelected);
	public ICommand ExportCommand => new DelegateCommand(Export);
	
	private Animal? SelectedAnimal { get; set; }

	private readonly IAnimalDataProvider _dataProvider;
	private readonly IAnimalExporter _animalExporter;

	public MainViewModel()
	{
		_dataProvider = new ZooRepository();
		_animalExporter = new JsonAnimalExporter();
		Animals = new ObservableCollection<Animal>(_dataProvider.GetAllAnimal());
	}

	private void AddAnimal(object obj)
	{
		AnimalWindow animalWindow = new();
		bool? showDialog = animalWindow.ShowDialog();

		if (showDialog == null || (bool)!showDialog)
		{
			return;
		}

		if (animalWindow.DataContext is not AnimalViewModel animalViewModel)
		{
			return;
		}

		Animal animal = animalViewModel.Animal;
		_dataProvider.AddAnimal(animal);
		
		UpdateAnimals();
	}

	private void SelectAnimal(object obj)
	{
		if (obj is Animal animal)
		{
			SelectedAnimal = animal;
		}
	}

	private void EditAnimal(object obj)
	{
		AnimalWindow animalWindow = new();
		
		if (animalWindow.DataContext is not AnimalViewModel animalViewModel)
		{
			return;
		}

		if (SelectedAnimal == null)
		{
			return;
		}
		
		animalViewModel.Animal = SelectedAnimal;

		bool? showDialog = animalWindow.ShowDialog();

		if (showDialog == null || (bool)!showDialog)
		{
			return;
		}

		Animal animal = animalViewModel.Animal;
		_dataProvider.UpdateAnimal(animal);

		UpdateAnimals();
	}

	private void RemoveAnimal(object obj)
	{
		if (SelectedAnimal == null)
		{
			return;
		}
		
		_dataProvider.DeleteAnimal(SelectedAnimal);
		UpdateAnimals();
	}
	
	private void Export(object obj)
	{
		_animalExporter.Export(Animals.ToArray());
	}

	private void UpdateAnimals()
	{
		Animals.Clear();
		Animal[] animals = _dataProvider.GetAllAnimal();

		foreach (Animal animal in animals)
		{
			Animals.Add(animal);
		}
	}

	private bool IsAnimalSelected(object obj)
	{
		return SelectedAnimal != null;
	}
}
