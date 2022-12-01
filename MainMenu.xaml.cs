using MAUIStorage;
using TwoPlayerGames.Games;

namespace TwoPlayerGames;

public partial class MainMenu : ContentPage
{
    bool _hasSave;

    private MainStorage _storage = new();

    private VerticalStackLayout _newGameMenu;
    private bool isMenuOpened;

    public MainMenu()
    {
        InitializeComponent();
        NavigatedFrom += OnNavigatedFrom;

        _hasSave = _storage.PrevGame != 0;

        _newGameMenu = new VerticalStackLayout() { Margin = new(20, 10) };
        var pushGameBtn = new Button() { Text = "Push" };
        pushGameBtn.Clicked += OnPushGameBtn;
        _newGameMenu.Add(pushGameBtn);

        if (_hasSave)
            MainLayout.Add(new Button() { Text = "Contiue" });

        var newGameBtn = new Button() { Text = "New Game" };
        newGameBtn.Clicked += OnNewGameButton;
        MainLayout.Add(newGameBtn);
        var statisticsBtn = new Button() { Text = "Statistics" };
        statisticsBtn.Clicked += OnStatisticsBtn;
        MainLayout.Add(statisticsBtn);
    }

    private void OnNavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        Update();
    }

    private void Update()
    {
        _hasSave = _storage.PrevGame != 0;

        if (MainLayout.Count > 3 && _hasSave)
        {
            var continueBtn = new Button() { Text = "Continue" };
            continueBtn.Clicked += OnContinueBtn;
            MainLayout.Insert(1, continueBtn);
        }
    }

    private void OnContinueBtn(object sender, EventArgs e)
    {
        throw new NotImplementedException();

        switch (_storage.PrevGame)
        {

        }
    }

    public void OnNewGameButton(object sender, EventArgs e)
    {
        if (!isMenuOpened)
            AllocateNewGameMenu();
        else
            RemoveNewGameMenu();
    }

    private void AllocateNewGameMenu()
    {
        MainLayout.Insert(_hasSave ? 3 : 2, _newGameMenu);
        isMenuOpened = true;
    }

    private void RemoveNewGameMenu()
    {
        MainLayout.RemoveAt(_hasSave ? 3 : 2);
        isMenuOpened = false;
    }

    private void OnPushGameBtn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PushGame());
        RemoveNewGameMenu();
    }

    private void OnStatisticsBtn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Statistics());
    }
}