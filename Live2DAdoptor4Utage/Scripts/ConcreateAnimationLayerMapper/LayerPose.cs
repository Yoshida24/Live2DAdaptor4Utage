using UnityEngine;

namespace Utage {
    public class LayerPose : MonoBehaviour, IAnimationTreeLayer
    {
        /// <summary>
        /// コマンド：ポーズ変更
        /// </summary>
        public void dispatch(string name){
            Debug.Log(name);
        }
    }
}