using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeamNameAssigner : MonoBehaviour
{
    [MenuItem("Tools/Assign Team Names")]
    private static void AssignTeamNames()
    {
        string outputFolderPath = "Assets/Teams";

        // Dictionary mapping the short names to full names (slogan + name)
        Dictionary<string, string> teamNames = new Dictionary<string, string>
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

        string[] teamAssets = AssetDatabase.FindAssets("t:Team", new[] { outputFolderPath });

        foreach (string guid in teamAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Team team = AssetDatabase.LoadAssetAtPath<Team>(path);

            if (team != null && teamNames.TryGetValue(team.name, out string fullName))
            {
                team.name = fullName;
                EditorUtility.SetDirty(team);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Team names assigned successfully!");
    }
}
