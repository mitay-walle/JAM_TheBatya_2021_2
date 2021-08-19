using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.Switchable
{
    public static class Helper
    {
    #if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Init()
        {
            Selection.selectionChanged -= selectionChanged;
            Selection.selectionChanged += selectionChanged;
        }

        private static void selectionChanged()
        {
            if (Application.isPlaying) return;
            if (!Selection.activeGameObject) return;
			
            //Debug.Log("selections changed");
        
            var go = Selection.activeGameObject;
			
            var foundScreenOne = go.GetComponent<ISwitchVisible>() ;


        
            var foundScreen = go.GetComponentsInParent<ISwitchVisible>(true);
            //Debug.Log(foundScreen);


            if (foundScreen == null && foundScreenOne == null)
            {
                //Debug.Log("not found");
                return;
            }

            if (foundScreen == null)
            {
                foundScreen = new ISwitchVisible[0]; 
            }
        
        
            if (foundScreenOne != null)
            {
                var list = foundScreen.ToList();
                list.Add(foundScreenOne);

                foundScreen = list.ToArray();
            }


            if (foundScreen.Length == 0)
            {
                //Debug.Log("not found");
                return;
            }
            


            for (var i = 0; i < foundScreen.Length; i++)
            {
                if (!(foundScreen[i] is ISwitchVisible screen))
                {
                    //Debug.Log("wrong type");
                    continue;
                }
                var handler = screen.GetHandler();

                if (handler == null)
                {
                    //Debug.Log("no handler");
                    continue;
                }
                //Debug.Log($"show{screen.myGo}",screen.myGo);
                handler.Show(screen.GetIndex());
            }
        }
#endif
    }
}