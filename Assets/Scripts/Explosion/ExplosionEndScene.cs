using UnityEngine;

public class ExplosionEndScene : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public void EndLevelAfterExplosion()
    {
        levelManager.EndLevel();
    }
}
