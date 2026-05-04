using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authService;
    private readonly INavigationService _navigationService;
    private readonly IItemService _itemService;

    [ObservableProperty]
    private User? currentUser;

    [ObservableProperty]
    private string welcomeMessage = string.Empty;

    [ObservableProperty]
    private bool isAdmin;

    [ObservableProperty]
    private ObservableCollection<Item> items = new();

    [ObservableProperty]
    private bool isAddingItem;

    [ObservableProperty]
    private string newItemName = string.Empty;

    [ObservableProperty]
    private string newItemDescription = string.Empty;

    [ObservableProperty]
    private string newItemLocation = string.Empty;

    [ObservableProperty]
    private string newItemDailyRate = string.Empty;

    [ObservableProperty]
    private string newItemRating = string.Empty;

    [ObservableProperty]
    private List<string> categories = Enum.GetNames(typeof(ItemCategory)).ToList();

    [ObservableProperty]
    private string? selectedCategory;

    public MainViewModel()
    {
        Title = "Dashboard";
    }

    public MainViewModel(IAuthenticationService authService, INavigationService navigationService, IItemService itemService)
    {
        _authService = authService;
        _navigationService = navigationService;
        _itemService = itemService;

        Title = "Dashboard";

        LoadUserData();
        _ = LoadItemsAsync();
    }

    private void LoadUserData()
    {
        CurrentUser = _authService.CurrentUser;
        IsAdmin = _authService.HasRole("Admin");

        if (CurrentUser != null)
        {
            WelcomeMessage = $"Welcome, {CurrentUser.FullName}!";
        }
    }

    private async Task LoadItemsAsync()
    {
        try
        {
            var itemsFromDb = await _itemService.GetAllItemsAsync();

            Items.Clear();

            foreach (var item in itemsFromDb)
                Items.Add(item);
        }
        catch (Exception ex)
        {
            SetError($"Failed to load items: {ex.Message}");
        }
    }

    [RelayCommand]
    private void ToggleAddForm()
    {
        IsAddingItem = !IsAddingItem;

        if (!IsAddingItem)
            ClearForm();
    }

    [RelayCommand]
    private async Task SaveItemAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NewItemName) ||
                string.IsNullOrWhiteSpace(NewItemDescription) ||
                string.IsNullOrWhiteSpace(NewItemLocation) ||
                string.IsNullOrWhiteSpace(NewItemDailyRate) ||
                string.IsNullOrWhiteSpace(NewItemRating) ||
                string.IsNullOrWhiteSpace(SelectedCategory))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!decimal.TryParse(NewItemDailyRate, out var dailyRate) || dailyRate <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid daily rate", "OK");
                return;
            }

            if (!int.TryParse(NewItemRating, out var ratingValue) ||
                ratingValue < 1 || ratingValue > 5)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Rating must be between 1 and 5", "OK");
                return;
            }

            var item = new Item
            {
                Name = NewItemName,
                Description = NewItemDescription,
                Location = NewItemLocation,
                Category = Enum.Parse<ItemCategory>(SelectedCategory),
                DailyRate = dailyRate,
                Rating = (Rating)ratingValue,
                OwnerId = CurrentUser!.Id
            };

            await _itemService.AddItemAsync(item);

            await LoadItemsAsync();

            ClearForm();
            IsAddingItem = false;
        }
        catch (Exception ex)
        {
            SetError($"Failed to add item: {ex.Message}");
        }
    }

    [RelayCommand]
    private void CancelAddItem()
    {
        ClearForm();
        IsAddingItem = false;
    }

    private void ClearForm()
    {
        NewItemName = string.Empty;
        NewItemDescription = string.Empty;
        NewItemLocation = string.Empty;
        NewItemDailyRate = string.Empty;
        NewItemRating = string.Empty;
        SelectedCategory = null;
    }

    [RelayCommand]
    private async Task ViewItemAsync(Item item)
    {
        if (item == null)
            return;

        await _navigationService.NavigateToAsync($"ItemDetailsPage?id={item.Id}");
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        var result = await Application.Current.MainPage.DisplayAlert(
            "Logout",
            "Are you sure you want to logout?",
            "Yes",
            "No");

        if (result)
        {
            await _authService.LogoutAsync();
            await _navigationService.NavigateToAsync("LoginPage");
        }
    }

    [RelayCommand]
    private async Task NavigateToProfileAsync()
    {
        await _navigationService.NavigateToAsync("TempPage");
    }

    [RelayCommand]
    private async Task NavigateToSettingsAsync()
    {
        await _navigationService.NavigateToAsync("TempPage");
    }

    [RelayCommand]
    private async Task NavigateToUserListAsync()
    {
        if (!IsAdmin)
        {
            await Application.Current.MainPage.DisplayAlert("Access Denied", "You don't have permission to access admin features.", "OK");
            return;
        }

        await _navigationService.NavigateToAsync("UserListPage");
    }

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            IsBusy = true;

            LoadUserData();
            await LoadItemsAsync();
        }
        catch (Exception ex)
        {
            SetError($"Failed to refresh data: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}