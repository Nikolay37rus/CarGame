using Profile;
using UnityEngine;

public class FightWindowController : BaseController
{
    private FightWindowView _fightWindowView;
    private ProfilePlayer _profilePlayer;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;

    private Money _money;
    private Health _heath;
    private Power _power;

    private Enemy _enemy;


    public FightWindowController(Transform placeForUi, ProfilePlayer profilePlayer, FightWindowView fightWindowView)
    {
        _profilePlayer = profilePlayer;

        _fightWindowView = Object.Instantiate(fightWindowView, placeForUi);
        AddGameObjects(_fightWindowView.gameObject);
    }

    public void RefreshView() 
    {
        _enemy = new Enemy("Enemy Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _heath = new Health(nameof(Health));
        _heath.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        SubbcribeButtons();
    }

    private void SubbcribeButtons()
    {
        _fightWindowView.AddCoinsButton.onClick.AddListener(() => ChangeMoney(true));
        _fightWindowView.MinusCoinsButton.onClick.AddListener(() => ChangeMoney(false));

        _fightWindowView.AddHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _fightWindowView.MinusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _fightWindowView.AddPowerButton.onClick.AddListener(() => ChangePower(true));
        _fightWindowView.AddPowerButton.onClick.AddListener(() => ChangePower(false));

        _fightWindowView.FightButton.onClick.AddListener(Fight);
        _fightWindowView.LeaveFightButton.onClick.AddListener(CloseWindow);
    }


    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void Fight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.Power
        ? "<color=#07FF00>Win!!!</color>"
        : "<color=#FF0000>Lose!!!</color>");
    }

    private void CloseWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _fightWindowView.CountMoneyText.text = $"Player Money {countChangeData}";
                _money.Money = countChangeData;
                break;

            case DataType.Health:
                _fightWindowView. CountHealthText.text = $"Player Health {countChangeData}";
                _heath.Health = countChangeData;
                break;

            case DataType.Power:
                _fightWindowView. CountPowerText.text = $"Player Power {countChangeData}";
                _power.Power = countChangeData;
                break;
        }

        _fightWindowView.CountPowerEnemyText.text = $"Enemy Power {_enemy.Power}";
    }

    protected override void OnDispose()
    {
        _fightWindowView.AddCoinsButton.onClick.RemoveAllListeners();
        _fightWindowView.MinusCoinsButton.onClick.RemoveAllListeners();

        _fightWindowView.AddHealthButton.onClick.RemoveAllListeners();
        _fightWindowView.MinusHealthButton.onClick.RemoveAllListeners();

        _fightWindowView.AddPowerButton.onClick.RemoveAllListeners();
        _fightWindowView.AddPowerButton.onClick.RemoveAllListeners();

        _fightWindowView.FightButton.onClick.RemoveAllListeners();
        _fightWindowView.LeaveFightButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _heath.Detach(_enemy);
        _power.Detach(_enemy);


        base.OnDispose();
    }

    
}
