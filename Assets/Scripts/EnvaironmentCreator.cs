using Photon.Pun;
using UnityEngine;

public class EnvaironmentCreator : MonoBehaviour
{
    [SerializeField] private Transform envaironmentParent;

    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private GameObject floorPrefab;

     private CoinsCreator coinsCreator;

    private object[] envaironmentParentPunObj;

    private void Awake()
    {
        coinsCreator = FindObjectOfType<CoinsCreator>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("master");
            envaironmentParentPunObj = new object[] { envaironmentParent.GetComponent<PhotonView>().ViewID };
            var size = GetFieldSize();
            GenerateEnvaironment(size[0], size[1]);

            coinsCreator.CreateCoins(size[0] * size[1] / 4, size[0], size[1], envaironmentParentPunObj);

            CentralizeEnvaironmentParent();
        }

        //SetCameraZoom();
    }

    private float[] GetFieldSizeInFloat()
    {
        var min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        var max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float width = Mathf.Abs(min.x) + Mathf.Abs(max.x);
        float height = Mathf.Abs(min.y) + Mathf.Abs(max.y);
        return new float[] { width, height };
    }

    private int[] GetFieldSize()
    {
        var size = GetFieldSizeInFloat();
        return new int[] { (int) size[0], (int)size[1] };
    }

    private void GenerateEnvaironment(int width, int height)
    {
        GenerateBorders(width, height);
        GenerateFloor(width, height);
    }

    private void GenerateBorders(int width, int height)
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (w > 0 && w < width - 1 && h > 0 && h < height - 1) continue;
                PhotonNetwork.Instantiate(borderPrefab.name, new Vector2(w, h), Quaternion.identity, 0 ,envaironmentParentPunObj);
            }
        }
    }

    private void GenerateFloor(int width, int height)
    {
        for (int w = 1; w < width - 1; w++)
        {
            for (int h = 1; h < height - 1; h++)
            {
                PhotonNetwork.Instantiate(floorPrefab.name, new Vector2(w, h), Quaternion.identity, 0, envaironmentParentPunObj);
            }
        }
    }

    private void SetCameraZoom()
    {
        var parent = PhotonView.Find((int)envaironmentParentPunObj[0]).gameObject;
        var renderers = parent.GetComponentsInChildren<SpriteRenderer>();
        if (renderers == null || renderers.Length == 0) { Debug.Log("there is no envaironment"); return; }
        while (true)
        {
            bool canStop = false;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].isVisible == false) { Camera.main.orthographicSize *= 0.95f; Debug.Log("Cant see"); break; }
                if (i == renderers.Length - 1) canStop = true;
            }
            if (canStop) break;
        }
    }

    private void CentralizeEnvaironmentParent()
    {
        int topX = 0;
        int topY = 0;
        for (int i = 0; i < envaironmentParent.childCount; i++)
        {
            if (envaironmentParent.GetChild(i).transform.localPosition.x > topX) topX = (int)envaironmentParent.GetChild(i).transform.localPosition.x;
            if (envaironmentParent.GetChild(i).transform.localPosition.y > topY) topY = (int)envaironmentParent.GetChild(i).transform.localPosition.y;
        }
        envaironmentParent.transform.position = new Vector2(topX / -2f, topY / -2f); 
    }
}