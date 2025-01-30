using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TeamGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Teams")]
    private static void GenerateTeams()
    {
        // Teams grouped by region
        Dictionary<string, string[]> regionTeams = new Dictionary<string, string[]>
        {
            { "north", new string[] { "Sharks", "Gold", "Jets", "Knights", "Lions", "Saints", "Lynx", "Redbacks", "Seahawks", "Hawks", "Phoenix", "Seaeagles", "Eagles", "Cougars", "Lightning", "Renegades", "Goblins", "Blaze", "Roos", "Redhawks", "Wildcats" } },
            { "south", new string[] { "Cats", "Mustangs", "Raiders", "Brumbies", "Stingrays", "Crows", "Bandicoots", "Rockets", "Demons", "Dragons", "Phantoms", "Icebirds", "Angels", "Pegasi", "Paladins", "Cerberuses", "Medusas", "Nomads", "Unicorns", "Gargoyles", "Warriors", "Bombers", "Rams" } },
            { "west", new string[] { "Hydras", "Manticores", "Magpies", "Bluebirds", "Champions", "Druids", "Titans", "Tigers", "Sapphire", "Snow Leopards", "Power", "Dolphins", "Genies", "Ghosts", "Griffins", "Hydras", "Wyverns", "Dokers", "Cardinals", "Wizards" } },
            { "east", new string[] { "Sultans", "Giants", "Panthers", "Royals", "Cygnets", "Centaurs", "Cyclops", "Devils", "Bears", "Kings", "Suns", "Wolves", "Bulldogs", "Cyborgs", "Mutants", "Elves", "Hunters", "Treants", "Golems", "Rangers", "Thunderbirds" } }
        };
















































        Dictionary<string, string> teamFullNames = new Dictionary<string, string>
        {
            // East
            { "Bears", "BAYSWATER BEARS" },
            { "Devils", "ARMADALE DEVILS" },
            { "Thunderbirds", "KALAMUNDA THUNDERBIRDS" },
            { "Centaurs", "MIDLAND CENTAURS" },
            { "Cyclops", "GUILDFORD CYCLOPS" },
            { "Giants", "KELMSCOTT GIANTS" },
            { "Rangers", "HILLS RANGERS" },
            { "Kings", "BURSWOOD KINGS" },
            { "Suns", "CAVERSHAM SUNS" },
            { "Royals", "BALLAJURA ROYALS" },
            { "Bulldogs", "MORLEY BULLDOGS" },
            { "Sultans", "CANNINGTON SULTANS" },
            { "Cygnets", "UPPER SWAN CYGNETS" },
            { "Golems", "BALCATTA GOLEMS" },
            { "Panthers", "MOUNT LAWLEY PANTHERS" },
            { "Wolves", "THORNLIE WOLVES" },
            { "Hunters", "DARLINGTON HUNTERS" },
            { "Cyborgs", "GOSNELLS CYBORGS" },
            { "Mutants", "MIRRABOOKA MUTANTS" },
            { "Elves", "EAST PERTH ELVES" },
            { "Treants", "VICTORIA PARK TREANTS" },

            // North
            { "Cougars", "CARINE COUGARS" },
            { "Jets", "JOONDALUP KINROSS JETS" },
            { "Wildcats", "WHITFORDS WILDCATS" },
            { "Lynx", "KINGSLEY LYNX" },
            { "Redhawks", "YANCHEP REDHAWKS" },
            { "Phoenix", "CURRAMBINE PHOENIX" },
            { "Roos", "WANNEROO ROOS" },
            { "Knights", "WARWICK GREENWOOD KNIGHTS" },
            { "Hawks", "SORRENTO DUNCRAIG HAWKS" },
            { "Eagles", "EDGEWATER WOODVALE EAGLES" },
            { "Saints", "KARRINYUP SAINTS" },
            { "Sharks", "OCEAN REEF SHARKS" },
            { "Seahawks", "BRIGHTON SEAHAWKS" },
            { "Lightning", "QUINNS LIGHTNING" },
            { "Gold", "GWELUP GOLD" },
            { "Lions", "NORTH BEACH LIONS" },
            { "Redbacks", "OSBORNE PARK REDBACKS" },
            { "Seaeagles", "SCARBOROUGH SEAEAGLES" },
            { "Renegades", "NORTH PERTH RENEGADES" },
            { "Goblins", "ALKIMOS GOBLINS" },
            { "Blaze", "LANDSDALE BLAZE" },

            // South
            { "Bombers", "ATTADALE BOMBERS" },
            { "Angels", "GOLDEN BAY ANGELS" },
            { "Mustangs", "MANDURAH MUSTANGS" },
            { "Paladins", "KWINANA PALADINS" },
            { "Demons", "PALMYRA DEMONS" },
            { "Bandicoots", "BIBRA LAKE BANDICOOTS" },
            { "Cats", "PORT KENNEDY CATS" },
            { "Stingrays", "SOUTH PERTH STINGRAYS" },
            { "Rams", "ROCKINGHAM RAMS" },
            { "Brumbies", "BALDIVIS BRUMBIES" },
            { "Pegasi", "MEADOW SPRINGS PEGASI" },
            { "Phantoms", "MURDOCH PHANTOMS" },
            { "Raiders", "ROSSMOYNE RAIDERS" },
            { "Warriors", "COCKBURN WARRIORS" },
            { "Icebirds", "SAFETY BAY ICEBIRDS" },
            { "Dragons", "APPLECROSS DRAGONS" },
            { "Cerberuses", "CANNING VALE CERBERUSES" },
            { "Unicorns", "COOGEE UNICORNS" },
            { "Medusas", "LAKELANDS MEDUSAS" },
            { "Nomads", "WILLETTON NOMADS" },
            { "Rockets", "JANDAKOT ROCKETS" },
            { "Crows", "HARRISDALE CROWS" },
            { "Gargoyles", "WARNBRO GARGOYLES" },

            // West
            { "Tigers", "SWANBOURNE TIGERS" },
            { "Magpies", "COTTESLOE MAGPIES" },
            { "Dokers", "FREMANTLE DOKERS" },
            { "Power", "PEPPERMINT GROVE POWER" },
            { "Magma", "MOSMAN PARK MAGMA" },
            { "Titans", "CLAREMONT TITANS" },
            { "Hydras", "WOODLANDS HYDRAS" },
            { "Sapphire", "DALKEITH NEDLANDS SAPPHIRE" },
            { "Manticores", "SUBIACO MANTICORES" },
            { "Wizards", "FLOREAT WIZARDS" },
            { "Champions", "WEMBLEY DOWNS CHAMPIONS" },
            { "Griffins", "DOUBLEVIEW GRIFFINS" },
            { "Dolphins", "LEEDERVILLE DOLPHINS" },
            { "Cardinals", "WEST PERTH CARDINALS" },
            { "Bluebirds", "SHENTON PARK BLUEBIRDS" },
            { "Snow Leopards", "MOUNT CLAREMONT SNOW LEOPARDS" },
            { "Wyverns", "CHURCHLANDS WYVERNS" },
            { "Druids", "MOUNT HAWTHORN DRUIDS" },
            { "Genies", "EAST FREMANTLE GENIES" },
            { "Ghosts", "KARRAKATTA GHOSTS" }
        };













































        string skinsFolderPath = "Assets/Sources/Skins and badges";
        string abilitiesFolderPath = "Assets/Abilities";
        string outputFolderPath = "Assets/Teams";

        BaseAbility[] abilities = LoadAssets<BaseAbility>(abilitiesFolderPath);
        if (abilities.Length < 3)
        {
            Debug.LogError("Not enough abilities found in Assets/Abilities. At least 3 abilities are required.");
            return;
        }

        if (!Directory.Exists(outputFolderPath))
        {
            Directory.CreateDirectory(outputFolderPath);
            AssetDatabase.Refresh();
        }

        foreach (var region in regionTeams)
        {
            string regionName = region.Key;
            string[] teams = region.Value;

            Dictionary<string, List<Sprite>> organizedSprites = LoadAndSortSprites(skinsFolderPath, regionName);
            List<Sprite> orderedSprites = OrderSpritesCorrectly(organizedSprites);

            int teamsPerImage = 3;
            int spritesPerTeam = 4;
            int totalTeams = teams.Length;
            int fullImageCount = totalTeams / teamsPerImage;
            int remainingTeams = totalTeams % teamsPerImage;

            int spriteIndex = 0;
            for (int i = 0; i < totalTeams; i++)
            {
                Team team = ScriptableObject.CreateInstance<Team>();

                // Assign the full name if it exists in the dictionary
                if (teamFullNames.TryGetValue(teams[i], out string fullName))
                {
                    team.name = fullName;
                }
                else
                {
                    team.name = teams[i]; // Fallback to short name if no full name exists
                }

                team.badge = null;
                team.speed = 5;
                team.strength = 5;
                team.attack = 5;
                team.defense = 5;
                team.durability = 5;

                team.skins = new Sprite[4];
                for (int j = 0; j < spritesPerTeam; j++)
                {
                    if (spriteIndex < orderedSprites.Count)
                    {
                        team.skins[j] = orderedSprites[spriteIndex];
                        spriteIndex++;
                    }
                    else
                    {
                        Debug.LogError($"Not enough sprites for team {teams[i]}. Check your slicing setup.");
                        return;
                    }
                }

                team.reusableAbility = abilities[Random.Range(0, abilities.Length)];
                team.oneShotAbilities = new List<BaseAbility>
                {
                    abilities[Random.Range(0, abilities.Length)],
                    abilities[Random.Range(0, abilities.Length)],
                    abilities[Random.Range(0, abilities.Length)]
                };

                string teamAssetPath = Path.Combine(outputFolderPath, $"{teams[i]}.asset");
                AssetDatabase.CreateAsset(team, teamAssetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("All teams generated successfully!");
    }

    private static Dictionary<string, List<Sprite>> LoadAndSortSprites(string folderPath, string region)
    {
        string[] guids = AssetDatabase.FindAssets($"t:Texture2D {region} skins", new[] { folderPath });
        Dictionary<string, List<Sprite>> spriteDictionary = new Dictionary<string, List<Sprite>>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);

            List<Sprite> sprites = new List<Sprite>();
            foreach (Object asset in assets)
            {
                if (asset is Sprite sprite)
                {
                    sprites.Add(sprite);
                }
            }

            sprites.Sort((a, b) => ExtractSpriteIndex(a.name).CompareTo(ExtractSpriteIndex(b.name)));
            spriteDictionary[path] = sprites;
        }

        return spriteDictionary;
    }

    private static List<Sprite> OrderSpritesCorrectly(Dictionary<string, List<Sprite>> spriteDictionary)
    {
        List<Sprite> orderedSprites = new List<Sprite>();

        List<string> sortedKeys = new List<string>(spriteDictionary.Keys);
        sortedKeys.Sort((a, b) => ExtractImageIndex(a).CompareTo(ExtractImageIndex(b)));

        foreach (string key in sortedKeys)
        {
            if (spriteDictionary.TryGetValue(key, out List<Sprite> sprites))
            {
                orderedSprites.AddRange(sprites);
            }
        }

        return orderedSprites;
    }

    private static T[] LoadAssets<T>(string folderPath) where T : Object
    {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
        T[] assets = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
        return assets;
    }
    private static int ExtractSpriteIndex(string spriteName)
    {
        int underscoreIndex = spriteName.LastIndexOf('_');
        if (underscoreIndex != -1 && int.TryParse(spriteName.Substring(underscoreIndex + 1), out int index))
        {
            return index;
        }
        return int.MaxValue;
    }

    private static int ExtractImageIndex(string imagePath)
    {
        string filename = Path.GetFileNameWithoutExtension(imagePath);
        int lastDotIndex = filename.LastIndexOf('.');
        if (lastDotIndex != -1 && int.TryParse(filename.Substring(lastDotIndex + 1), out int index))
        {
            return index;
        }
        return int.MaxValue;
    }
}
