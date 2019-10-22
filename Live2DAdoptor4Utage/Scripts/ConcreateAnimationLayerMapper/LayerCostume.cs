using UnityEngine;

namespace Utage {
    public class LayerCostume : MonoBehaviour, IAnimationTreeLayer
    {
        /// <summary>
        /// コマンド：コスチューム変更
        /// </summary>
        public void dispatch(string name){
            Debug.Log(name);
        }
    }
}
