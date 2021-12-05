using UnityEngine;

interface IEnemy
{
    void Update(DataPlayer dataPlayer, DataType dataType);
}

class Enemy : IEnemy
{
    private const int KCoins = 5;
    private const float KForce = 1.5f;
    private const int MaxHealthPlayer = 20;
    
    private string _name;
    private int _moneyPlayer;
    private int _healthPlayer;
    private int _forcePlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _moneyPlayer = dataPlayer.Money;
                break;
            
            case DataType.Health:
                _healthPlayer = dataPlayer.Health;
                break;
            
            case DataType.Power:
                _forcePlayer = dataPlayer.Power;
                break;
        }
        
        Debug.Log($"Notified {_name} change to {dataPlayer}");
    }

    public int Power
    {
        get
        {
            var kHealth = _healthPlayer > MaxHealthPlayer ? 100 : 5;
            var force = (int) (_moneyPlayer / KCoins + kHealth + _forcePlayer / KForce);
            
            return force;
        }
    }
}
