using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public UnityEvent enemiesDefeated;

    private int EnemyCount { get; set; }

    public void SetPause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }

    public void InitLevel()
    {
        var enemyControllers = FindObjectsOfType<EnemyBase>();
        EnemyCount = enemyControllers.Length;
        foreach (var enemyController in enemyControllers)
            enemyController.Died += EnemyController_Died;
    }

    private void EnemyController_Died(object sender, System.EventArgs e)
    {
        if (sender is EnemyBase enemyController)
            enemyController.Died -= EnemyController_Died;
        EnemyCount--;
        if (EnemyCount == 0)
            enemiesDefeated.Invoke();
    }
}
