using Game;
using Profile;
using Ui;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, DailyRewardView dailyRewardView, CurrencyView currencyView, FightWindowView fightWindowView, StartFightView startFightView)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _currencyView = currencyView;
        _fightWindowView = fightWindowView;
        _startFightView = startFightView;
        _dailyRewardView = dailyRewardView;


        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private DailyRewardController _dailyRewardController;
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private StartFightController _startFightController;
    private FightWindowController _fightWindowController;
   

    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly DailyRewardView _dailyRewardView;
    private readonly CurrencyView _currencyView;
    private readonly FightWindowView _fightWindowView;
    private readonly StartFightView _startFightView;

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);

                _gameController?.Dispose();
                _dailyRewardController?.Dispose();
                break;

            case GameState.DailyReward:
                _dailyRewardController = new DailyRewardController(_placeForUi, _profilePlayer, _dailyRewardView, _currencyView);
                _dailyRewardController.RefreshView();

                _mainMenuController?.Dispose();
                break;

            case GameState.Fight:
                _fightWindowController = new FightWindowController(_placeForUi, _profilePlayer, _fightWindowView);
                _fightWindowController.RefreshView();

                _startFightController?.Dispose();
                _gameController?.Dispose();
                _mainMenuController?.Dispose();
                break;

            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);

                _startFightController = new StartFightController(_placeForUi, _profilePlayer, _startFightView);
                _startFightController.RefreshView();

                _mainMenuController?.Dispose();
                _fightWindowController?.Dispose();
                break;

            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                
                break;
        }
    }
}
