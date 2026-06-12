using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.AI
{
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyController> Enemies { get; private set; }
        public int NumberOfEnemiesTotal { get; private set; }
        public int NumberOfEnemiesRemaining => Enemies.Count;
        public LevelCompleteUI uiManager;

        public UnityEvent OnAllEnemiesDead;

        private List<EnemyController> targetEnemies = new List<EnemyController>();

        void Awake()
        {
            Enemies = new List<EnemyController>();
            targetEnemies = new List<EnemyController>();
            Debug.Log("EnemyManager iniciado");
        }

        public void RegisterEnemy(EnemyController enemy)
        {
            Enemies.Add(enemy);
            NumberOfEnemiesTotal++;
            targetEnemies.Add(enemy);
        }

        public void UnregisterEnemy(EnemyController enemyKilled)
        {
            int enemiesRemainingNotification = NumberOfEnemiesRemaining - 1;
            EnemyKillEvent evt = Events.EnemyKillEvent;
            evt.Enemy = enemyKilled.gameObject;
            evt.RemainingEnemyCount = enemiesRemainingNotification;
            EventManager.Broadcast(evt);

            Enemies.Remove(enemyKilled);
            targetEnemies.Remove(enemyKilled);

            Debug.Log("Eliminando enemigo: " + enemyKilled.name);
            Debug.Log("Quedan: " + targetEnemies.Count);

            if (targetEnemies.Count == 0)
            {
                Debug.Log("Nivel completado!");
                if (uiManager != null)
                {
                    uiManager.ShowMessage();
                }
                else
                {
                    Debug.LogError("uiManager NO asignado");
                }
            }
        }
    }
}