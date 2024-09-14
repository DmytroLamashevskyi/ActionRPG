using UnityEngine;
using UnityEditor;

namespace EditorHelper
{
    public class PatruleEditorScript : MonoBehaviour
    {
        public float sphereRadius = 0.5f;  // Радиус сферы для отрисовки

        // Метод для отрисовки сфер на патрульных точках
        private void OnDrawGizmos()
        {
            // Ищем все объекты с тэгом "PatrulеPoint"
            GameObject[] patrolPoints = GameObject.FindGameObjectsWithTag("PatrulеPoint");

            if(patrolPoints.Length == 0)
                return;

            // Отрисовка сфер на каждой патрульной точке
            foreach(GameObject patrolPoint in patrolPoints)
            {
                if(patrolPoint != null)
                {
                    // Проверяем, выделен ли объект в инспекторе
                    if(Selection.activeGameObject == patrolPoint)
                    {
                        Gizmos.color = Color.blue;  // Если объект выбран, рисуем синюю сферу
                    }
                    else
                    {
                        Gizmos.color = Color.green;  // Иначе рисуем зелёную сферу
                    }
                    Gizmos.DrawSphere(patrolPoint.transform.position, sphereRadius);  // Рисуем сферу
                }
            }
        }
    }
}
