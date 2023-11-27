using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject GridOverlay;
    private GameObject PreviewTower;
    private GridManager gridManager;
    public Button placeTowerButton;
    public Image buttonImage;
    int TowerCost;
    public GameObject NoMoneyUI;
    public GameObject CancelOverlay;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        GridOverlay.SetActive(false);
        //UpdateButtonInteractability();
        placeTowerButton.interactable = true;
        CancelOverlay.SetActive(false);

    }

    void Update()
    {

        if (PreviewTower != null)
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 GridPos = gridManager.GetNearestGridPoint(MousePos);
            PreviewTower.transform.position = GridPos;

            if (Input.GetMouseButtonDown(0) && gridManager.IsValidPlacement(GridPos))
            {
                PlaceTower(GridPos);
            }
            else if (Input.GetMouseButtonDown(1)) // Right-click to cancel
            {
                Destroy(PreviewTower);
                GridOverlay.SetActive(false);
                CancelOverlay.SetActive(false);
            }
        }
    }

    public void StartTowerPlacement()
    {
        TowerCost = 200;

        if (EcoManager.Instance.Currency >= TowerCost)
        {
            if (PreviewTower == null)
            {
                GridOverlay.SetActive(true);
                CancelOverlay.SetActive(true);
                PreviewTower = Instantiate(TowerPrefab);
                PreviewTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            StartCoroutine(ShowNoMoney());

        }
    }

    private void PlaceTower(Vector2 position)
    {
        TowerCost = 200;

        if (EcoManager.Instance.Currency >= TowerCost)
        {
            Instantiate(TowerPrefab, position, Quaternion.identity);

            Cell cell = gridManager.GetCellAtPosition(position);
            if (cell != null)
            {
                cell.hasTower = true;
            }

            EcoManager.Instance.AddCurrency(-TowerCost);

            Destroy(PreviewTower);
            PreviewTower = null;
            CancelOverlay.SetActive(false);
            GridOverlay.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowNoMoney());
        }
    }

    public void UpdateButtonInteractability()
    {
        TowerCost = 200;
        bool canAffordTower = EcoManager.Instance.Currency >= TowerCost;

        placeTowerButton.interactable = canAffordTower;

        if (canAffordTower)
        {
            buttonImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            buttonImage.color = new Color(1, 1, 1, 0.5f);
        }
    }

    IEnumerator ShowNoMoney()
    {
        NoMoneyUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        NoMoneyUI.SetActive(false);
    }

}
