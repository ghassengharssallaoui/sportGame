using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeamsManager))]
public class PopulateTeamsEditor : Editor
{
    private static readonly Dictionary<string, List<string>> teamOrder = new Dictionary<string, List<string>>
    {
        { "north", new List<string> { "Sharks", "Gold", "Jets", "Knights", "Lions", "Saints", "Lynx", "Redbacks", "Seahawks", "Hawks", "Phoenix", "Seaeagles", "Eagles", "Cougars", "Lightning", "Renegades", "Goblins", "Blaze", "Roos", "Redhawks", "Wildcats" } },
        { "south", new List<string> { "Cats", "Mustangs", "Raiders", "Brumbies", "Stingrays", "Crows", "Bandicoots", "Rockets", "Demons", "Dragons", "Phantoms", "Icebirds", "Angels", "Pegasi", "Paladins", "Cerberuses", "Medusas", "Nomads", "Unicorns", "Gargoyles", "Warriors", "Bombers", "Rams" } },
        { "west", new List<string> { "Manticores", "Magpies", "Bluebirds", "Champions", "Druids", "Titans", "Tigers", "Sapphire", "Snow Leopards", "Power", "Dolphins", "Genies", "Ghosts", "Griffins", "Hydras", "Wyverns", "Dokers", "Cardinals", "Wizards" } },
        { "east", new List<string> { "Sultans", "Giants", "Panthers", "Royals", "Cygnets", "Centaurs", "Cyclops", "Devils", "Bears", "Kings", "Suns", "Wolves", "Bulldogs", "Cyborgs", "Mutants", "Elves", "Hunters", "Treants", "Golems", "Rangers", "Thunderbirds" } }
    };

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TeamsManager manager = (TeamsManager)target;

        if (GUILayout.Button("Populate Teams"))
        {
            PopulateTeams(manager);
        }
    }

    private void PopulateTeams(TeamsManager manager)
    {
        string teamsFolderPath = "Assets/Teams";
        string[] teamAssetGuids = AssetDatabase.FindAssets("t:Team", new[] { teamsFolderPath });

        Dictionary<string, Team> loadedTeams = new Dictionary<string, Team>();

        // ✅ Load teams using file names, NOT team.name
        foreach (string guid in teamAssetGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileNameWithoutExtension(assetPath); // ✅ Get file name
            Team team = AssetDatabase.LoadAssetAtPath<Team>(assetPath);
            if (team != null)
            {
                loadedTeams[fileName] = team;
            }
        }

        if (loadedTeams.Count == 0)
        {
            Debug.LogError("No Team assets found in Assets/Teams!");
            return;
        }

        List<Team> orderedTeams = new List<Team>();

        foreach (var region in teamOrder)
        {
            foreach (string teamName in region.Value)
            {
                if (loadedTeams.TryGetValue(teamName, out Team foundTeam))
                {
                    orderedTeams.Add(foundTeam);
                }
                else
                {
                    Debug.LogWarning($"Team asset not found for: {teamName}");
                }
            }
        }

        if (orderedTeams.Count == 0)
        {
            Debug.LogError("No matching teams found to assign!");
            return;
        }

        // ✅ Update TeamsManager
        Undo.RecordObject(manager, "Populate Teams");
        manager.GetType().GetField("teams", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(manager, orderedTeams.ToArray());
        EditorUtility.SetDirty(manager);
        AssetDatabase.SaveAssets();
        Debug.Log($"Teams successfully populated with {orderedTeams.Count} teams.");
    }
}
