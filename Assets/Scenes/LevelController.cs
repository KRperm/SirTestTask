using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
            enemyController.died.AddListener(EnemyController_Died);
    }

    public void RestartLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void EnemyController_Died()
    {
        EnemyCount--;
        if (EnemyCount == 0)
            enemiesDefeated.Invoke();
    }
}
