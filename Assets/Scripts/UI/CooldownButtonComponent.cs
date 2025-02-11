using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [ExecuteAlways]
    public class CooldownButtonComponent : MonoBehaviour
    {
        [SerializeField]
        public bool Interactable = false;

        [Range(0f, 1f)]
        public float coverPercent = 1;

        [SerializeField]
        private Image cover;
        private Button btn;

        private TMP_Text text;
        private Color textIColor;
        

        private void Awake()
        {
            btn = GetComponent<Button>();
            text =GetComponent<TMP_Text>();
            if (text != null)
            {
                textIColor = text.color;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            btn.interactable = Interactable;
            if (Interactable)
            {
                cover.gameObject.SetActive(false);
            }
            else
            {
                cover.gameObject.SetActive(true);
                cover.fillAmount = coverPercent;
            }
            if(text != null)
            {
                text.color = Interactable? textIColor : textIColor/5f;
            }
        }
    }
}
