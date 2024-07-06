using System.Collections.ObjectModel;
using System.Windows.Input;
using DatabaseEF.Entities;
using Models.Animals;
using Models.Factory;
using ZooRepositoryView.Views;
using Animal = Models.Animals.Animal;

namespace ZooRepositoryView.ViewModels;

public sealed class AnimalViewModel : ViewModel
{
	public ObservableCollection<AnimalType> AnimalTypeOptions { get; } = new(Enum.GetValues<AnimalType>());

	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			OnPropertyChanged();
		}
	}

	public int Age
	{
		get => _age;
		set
		{
			_age = value;
			OnPropertyChanged();
		}
	}

	public float Weight
	{
		get => _weight;
		set
		{
			_weight = value;
			OnPropertyChanged();
		}
	}

	public AnimalType AnimalType
	{
		get => _animalType;
		set
		{
			_animalType = value;
			OnPropertyChanged();
		}
	}

	public Animal Animal
	{
		get => _animal;
		set
		{
			_animal = value;
			Name = _animal.Name;
			Age = _animal.Age;
			Weight = _animal.Weight;

			AnimalType = value switch
			{
				Mammal => AnimalType.Mammal,
				Bird => AnimalType.Bird,
				Amphibian => AnimalType.Amphibian,
				_ => AnimalType.None
			};
		}
	}

	public ICommand SaveCommand => new DelegateCommand(Save, CanSave);
	public ICommand CancelCommand => new DelegateCommand(Cancel);

	private Animal _animal = new UnknownAnimal();

	private string _name = string.Empty;
	private int _age;
	private float _weight;
	private AnimalType _animalType;

	private void Save(object obj)
	{
		if (obj is not AnimalWindow animalWindow)
		{
			return;
		}
		
		Animal = AnimalFactory.Create(Animal.Id, Name, Age, Weight, AnimalType);
		
		animalWindow.DialogResult = true;
		animalWindow.Close();
	}

	private bool CanSave(object arg)
	{
		return !string.IsNullOrEmpty(Name);
	}

	private void Cancel(object obj)
	{
		if (obj is AnimalWindow animalWindow)
		{
			animalWindow.DialogResult = false;
			animalWindow.Close();
		}
	}
}
