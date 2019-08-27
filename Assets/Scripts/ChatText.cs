
using UnityEngine;
using UnityEngine.UI;

public class ChatText : MonoBehaviour
{
    private EntityInfo _entity;
    private Text _text;
    private GameObject _uiText;
    private HealthBar _healthBar;
    private bool _conv;
    private bool _health;

    public Canvas DrawCanvas;
    public GameObject TextPrefab;
    public string DefaultText;
    public Conversation Conversation;

    public Vector3 ChatPanelOffset = new Vector3(0.35f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        _entity = GetComponent<EntityInfo>();
        _uiText = Instantiate(TextPrefab, DrawCanvas.transform, false);

        _healthBar = GetComponent<HealthBar>();
        _health = (_healthBar != null);

        _text = _uiText.GetComponentInChildren<Text>();
        _text.text = DefaultText == "--" ? _entity.Name : DefaultText;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = transform.position;
        worldPos += ChatPanelOffset;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        _uiText.transform.position = screenPos;
    }

    public void SetConversation(Conversation conv)
    {
        _conv = true;
        Conversation = conv;

        if (_health)
        {
            _healthBar.Hide();
        }
    }

    public void ClearConversation()
    {
        _conv = false;

        if (_health)
        {
            _healthBar.Display();
        }
        else _text.text = DefaultText == "--" ? _entity.Name : DefaultText;
    }

    public bool InConversation()
    {
        return _conv;
    }

    public void Display(string text)
    {
        _text.text = text;
    }

    public void Clear()
    {
        _text.text = "";
    }
}

