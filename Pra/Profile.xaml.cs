namespace Pra;

using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;

public partial class Profile : ContentPage
{
    private string currentUserName = "Имя пользователя";
    private string avatarPath = string.Empty;

    public Profile()
    {
        InitializeComponent();
        LoadUserData();
    }

    private void LoadUserData()
    {
        // Загружаем сохраненные данные
        currentUserName = Preferences.Get("UserName", "Имя пользователя");
        UserNameLabel.Text = currentUserName;

        avatarPath = Preferences.Get("AvatarPath", string.Empty);
        if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
        {
            AvatarImage.Source = ImageSource.FromFile(avatarPath);
        }

        
        GoalStepper.Value = Preferences.Get("DailyGoal", 20);
        GoalLabel.Text = $"{GoalStepper.Value} слов";

        NotificationSwitch.IsToggled = Preferences.Get("Notifications", false);
        DarkThemeSwitch.IsToggled = Preferences.Get("DarkTheme", false);

        // Выбираем язык
        string language = Preferences.Get("Language", "Английский");
        int index = LanguagePicker.Items.IndexOf(language);
        if (index >= 0)
            LanguagePicker.SelectedIndex = index;
    }

   
    private async void OnAvatarTapped(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите аватарку",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "image/*" } },
                    { DevicePlatform.iOS, new[] { "public.image" } },
                    { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".bmp" } },
                    { DevicePlatform.macOS, new[] { ".jpg", ".jpeg", ".png", ".bmp" } }
                })
            });

            if (result != null)
            {
                
                avatarPath = result.FullPath;
                AvatarImage.Source = ImageSource.FromFile(avatarPath);
                Preferences.Set("AvatarPath", avatarPath);

                await DisplayAlert("Успех", "Аватарка обновлена!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", "Не удалось выбрать изображение: " + ex.Message, "OK");
        }
    }

    
    private async void OnEditNameClicked(object sender, EventArgs e)
    {
        string newName = await DisplayPromptAsync(
            "Изменить имя",
            "Введите новое имя:",
            initialValue: currentUserName,
            maxLength: 30,
            keyboard: Keyboard.Text);

        if (!string.IsNullOrWhiteSpace(newName))
        {
            currentUserName = newName.Trim();
            UserNameLabel.Text = currentUserName;
            Preferences.Set("UserName", currentUserName);

            await DisplayAlert("Успех", "Имя обновлено!", "OK");
        }
    }

    
    private void OnGoalChanged(object sender, ValueChangedEventArgs e)
    {
        double value = Math.Round(e.NewValue);
        GoalLabel.Text = $"{value} слов";
        Preferences.Set("DailyGoal", (int)value);
    }

   
    private void OnLanguageChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedIndex != -1)
        {
            string language = picker.SelectedItem.ToString();
            Preferences.Set("Language", language);
        }
    }

    
    private void OnNotificationToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("Notifications", e.Value);
        if (e.Value)
        {
            DisplayAlert("Уведомления", "Уведомления включены", "OK");
        }
    }

    
    private async void OnDarkThemeToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("DarkTheme", e.Value);
        await DisplayAlert("Тема", $"Темная тема {(e.Value ? "включена" : "выключена")}", "OK");
        
    }

   
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert(
            "Выход",
            "Вы уверены, что хотите выйти?",
            "Да",
            "Нет");

        if (answer)
        {
            Preferences.Clear();
            
            await Navigation.PopToRootAsync();
        }
    }

    
    private async void OnResetProgressClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert(
            "Сброс прогресса",
            "Вы уверены, что хотите сбросить весь прогресс? Это действие необратимо!",
            "Да, сбросить",
            "Отмена");

        if (answer)
        {
            await DisplayAlert("Прогресс сброшен", "Весь прогресс был успешно сброшен", "OK");
        }
    }

  
    private async void OnUrokTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage1());
    }

    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        // Уже на странице профиля
    }

    private async void OnStaticTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Stats());
    }

    private async void OnSlovarTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Slovar());
    }

    private async void OnDostigTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new NewPage2());
    }
    private async void OnPovtorTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Povtorenie());
    }
}