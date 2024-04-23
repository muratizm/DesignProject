using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    public const int ASKAI_CHOICE_INDEX = 3; // 4th choice in the list


    public static class Positions {
        public const float PLAYER_START_DEFAULT_X = -20f;
        public const float PLAYER_START_DEFAULT_Y = 40f;
        public const float PLAYER_START_DEFAULT_Z = -80f;
    }


    public static class Scenes {
        public const string SCENE_HOME = "HomeScene";
        public const string SCENE_PLAY = "Play";
    }




    public static class Durations {
        public const float WAIT_BETWEEN_LETTERS = .025f; 
        public const float THOUGHT_BUBBLE_DURATION = 2.5f;
        public const float DIALOGUE_WAIT_FOR_INPUT = 5f;
        public const int AI_DIALOGUE_WAIT_MS = 3000;
        public const int FADEIN_DURATION_MS = 2000;
        public const int FADEOUT_DURATION_MS = 1500;
    }


    public static class Tags {
        public const string SPEAKER_TAG = "speaker";
        public const string LAYOUT_TAG = "layout";
        public const string CLOSEABLE_BY_ESC_TAG = "CloseableByESC";

    }

    public static class Paths {

        public const string DIALOGUE_HISTORY_TEXT = "Assets/Ink/Story/debug.txt";
        public const string RESOURCES_SCRIPTIBLEOBJECTS_ITEMS_FOLDER = "ScriptibleObjects/Items/";
        public const string HIERARCHY_MINIGAME_PANEL = "Canvas/MinigamePanel";

        public static class Sounds {
            public static class SFX {
                public const string BUTTON_CLICK = "Sounds/SFX/button_click";
                public const string CHEST_OPEN = "Sounds/SFX/chest_open";
                public const string CHEST_CLOSE = "Sounds/SFX/chest_close";
                public const string PAGE_FLIP = "Sounds/SFX/page_flip";
            }
            public static class MUSIC {
                public const string HOME = "Sounds/Music/homescene";
                public const string STORY1 = "Sounds/Music/entrance";
            }
        }
    }

    public static class Labels {
        public const string SPOTS_MINIGAME = "minigame_clickthespots";
        public const string CLICKRUSH_MINIGAME = "minigame_clickrush";

        
        public const string MEMORY_MINIGAME = "Memory";
        public const string MATH_MINIGAME = "Math";
    }

}
