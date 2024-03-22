using UnityEngine;

namespace HoangAnh { 
    public class FPSController : Singleton<FPSController>{
        [Header("Text poperties")]
        [Tooltip("Mã màu RGB. VD: #000000")]
        [SerializeField] private string colorRGB;
        [Tooltip("Kích cỡ chữ hiển thị")]
        [Range(10, 300)][SerializeField] private int sizeText;
        [Space(10)][Header("Rect")]
        [Tooltip("So với trục x tính từ giữa màn hình")]
        [Range(0, 5)][SerializeField] private float xShow = 0;
        [Tooltip("So với trục y tính từ giữa màn hình")]
        [Range(0, 5)][SerializeField] private float yShow = 0.5f;
        private float deltaTime;

        private void Update(){
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI(){
            var msec = deltaTime * 1000.0f;
            var fps = 1.0f / deltaTime;
            var text = string.Format("<color='" + colorRGB + "'>{0:0.0} ms ({1:0.} fps)</color>", msec, fps);
            int w = Screen.width, h = Screen.height;
            var style = new GUIStyle();
            var rect = new Rect(xShow * w, yShow * h, w, 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = sizeText;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = new Color(1, 239.0f / 255.0f, 0, 1);
            GUI.Label(rect, text, style);
        }

        //Phỏng đoán giá trị (giá trị hiện tại cần phỏng đoán, giá trị nhỏ nhất, giá trị lớn nhất, giá trị cần phỏng đoán nhỏ nhất, giá trị cần phỏng đoán lớn nhất)
        public static float NormalizeValue(float val, float oldMin, float oldMax, float newMin, float newMax){
            val = Mathf.Clamp(val, oldMin, oldMax);
            return (val - oldMin) / (oldMax - oldMin) * (newMax - newMin) + newMin;

        }
    }
}