using UnityEngine;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
  const string CURRENT_LEVEL = "currentLvl";
  const string LAST_LEVEL = "LastLvl";
    const string MAX_LEVEL = "MaxLvl";
    const string STARS_OF_LEVELS = "stars of levels-";


    public static int GetCurrentLevel() { return PlayerPrefs.GetInt(CURRENT_LEVEL, 1); }
    public static void SetCurrentLevel(int level) { PlayerPrefs.SetInt(CURRENT_LEVEL, level); }

    public static int GetLastLevel() { return PlayerPrefs.GetInt(LAST_LEVEL, 1); }
    public static void SetLastLevel(int level) { PlayerPrefs.SetInt(LAST_LEVEL, level); }

    public static int GetMaxLevel()=> PlayerPrefs.GetInt(MAX_LEVEL, 1); 
    public static void SetMaxLevel(int level) { PlayerPrefs.SetInt(MAX_LEVEL, GetMaxLevel()<level?level:GetMaxLevel()); }

    public static int GetStarsOfLevel(int level) => PlayerPrefs.GetInt(STARS_OF_LEVELS+level, 0);
    public static void SetStarsOfLevel(int level,int stars)
    {
        if (stars > 3) stars = 3;
        if (stars < 0) stars = 0;
        int temp = GetStarsOfLevel(level);
        PlayerPrefs.SetInt(STARS_OF_LEVELS + level, temp < stars ? stars : temp);
     }

}
