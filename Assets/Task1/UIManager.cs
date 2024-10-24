using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Button[] _items;
    int _activeItem = -1;

    [SerializeField] GameObject[] _windows;

    [SerializeField] GameObject _intro;
    [SerializeField] GameObject _popup;

    private void Start()
    {
        ResetAllWindows();

        _activeItem = 0;
        _items[_activeItem].Select();
        _windows[_activeItem].SetActive(true);
    }

    private void Update()
    {
        if (!_popup.activeSelf && Input.GetButtonDown("Jump"))
        {
            _intro.SetActive(false);
            _popup.SetActive(true);
        }

        _items[_activeItem].Select();
    }

    public void Select(int id)
    {
        _windows[_activeItem].SetActive(false);

        _activeItem = id;
        _windows[_activeItem].SetActive(true);
    }

    void ResetAllWindows()
    {
        foreach (var item in _windows) {
            item.SetActive(false);
        }
    }
}
