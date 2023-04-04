using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCreator : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    public void CreateCoins(int coinsCount, int width, int height, object[] envaironmentParentPunObj)
    {
        List<Vector2> usedPositions = new List<Vector2>();
        for (int i = 0; i < coinsCount; i++)
        {
            Vector2 pos = GenerateCoinPosition(width, height, usedPositions);
            usedPositions.Add(pos);
            PhotonNetwork.Instantiate(coinPrefab.name, pos, Quaternion.identity, 0, envaironmentParentPunObj);
        }
    }

    private Vector2 GenerateCoinPosition(int width, int height, List<Vector2> usedPositions)
    {
        bool canUsePosition = true;
        int iterations = 0;
        while (true)
        {
            if (iterations >= 100) break;
            Vector2 pos = new Vector2(Random.Range(1, width - 1), Random.Range(1, height - 1));
            for (int i = 0; i < usedPositions.Count; i++)
            {
                if (Equals(pos, usedPositions[i])) { canUsePosition = false; iterations++; break; }
                if (i == usedPositions.Count - 1) canUsePosition = true;
            }
            if (canUsePosition) return pos;
        }
        throw new System.NotImplementedException();
    }
}