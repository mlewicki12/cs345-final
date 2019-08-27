
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private EntityInfo _entity;
    private Text _name;
    private Slider _healthSlider;

    public Canvas DrawCanvas;
    public GameObject HealthPrefab;

    public float HealthPanelOffset = 0.35f;
    public GameObject HealthPanel;

    // Start is called before the first frame update
    void Start()
    {
        _entity = GetComponent<EntityInfo>();
        HealthPanel = Instantiate(HealthPrefab, DrawCanvas.transform, false);
        HealthPanel.transform.Rotate(new Vector3(0, -180, 0));

        _name = HealthPanel.GetComponentInChildren<Text>();
        _name.text = _entity.Name;

        _healthSlider = HealthPanel.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthSlider.value = Mathf.Max(_entity.GetHealth(), 5);
        
        Vector3 worldPos = transform.position;
        worldPos.y += HealthPanelOffset;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        HealthPanel.transform.position = screenPos;
    }

    public void Hide()
    {
        HealthPanel.SetActive(false);
    }

    public void Display()
    {
        HealthPanel.SetActive(true);
    }
}
