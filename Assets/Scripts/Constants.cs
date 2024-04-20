using System.Collections;
using System.Collections.Generic;

public static class Constants
{


    //Scene names
    public const string SCENE_HOME = "HomeScene";




    public static class Times {
        public const float WAIT_BETWEEN_LETTERS = .025f; 
        public const float THOUGHT_BUBBLE_DURATION = 2.5f;
        public const int FADEIN_DURATION_MS = 2000;
        public const int FADEOUT_DURATION_MS = 1500;
    }


    public static class Tags {
        public const string SPEAKER_TAG = "speaker";
        public const string LAYOUT_TAG = "layout";

    }

    public static class Paths {

        public const string DIALOGUE_HISTORY_TEXT = "Assets/Ink/Story/debug.txt";
        public const string RESOURCES_SCRIPTIBLEOBJECTS_ITEMS_FOLDER = "ScriptibleObjects/Items/";
        public const string HIERARCHY_MINIGAME_PANEL = "Canvas/MinigamePanel";
    }

    public static class Labels {
        public const string SPOTS_MINIGAME = "minigame_clickthespots";
        public const string CLICKRUSH_MINIGAME = "minigame_clickrush";

        
        public const string MEMORY_MINIGAME = "Memory";
        public const string MATH_MINIGAME = "Math";
    }

}
