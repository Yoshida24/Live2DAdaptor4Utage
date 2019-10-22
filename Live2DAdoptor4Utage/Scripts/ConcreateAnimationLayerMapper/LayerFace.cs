using UnityEngine;

namespace Utage {
    public class LayerFace : MonoBehaviour, IAnimationTreeLayer
    {
        /// <summary>
        /// コマンド：表情変更
        /// </summary>
        public void dispatch(string name){
            Debug.Log(name);
        }
    }
}