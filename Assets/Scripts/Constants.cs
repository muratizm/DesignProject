using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

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
        public const int WAIT_BETWEEN_LETTERS_MS = 45;
        public const float THOUGHT_BUBBLE_DURATION = 2.5f;
        public const float DIALOGUE_WAIT_FOR_INPUT = 5f;
        public const int AI_DIALOGUE_WAIT_MS = 5000;
        public const int FADEIN_DURATION_MS = 2000;
        public const int FADEOUT_DURATION_MS = 1500;
        public const float TIME_TO_FINISH_PASSWORDQUIZ = 5f;
        public const float TIME_TO_FINISH_CLICKRUSH = 3f;
        public const float TIME_TO_FINISH_CLICKTHESPOTS = 10f;
    }


    public static class Tags {
        public const string PLAYER_TAG = "Player";

        public const string SPEAKER_TAG = "speaker";
        public const string LAYOUT_TAG = "layout";

        public const string CLOSEABLE_BY_ESC_TAG = "CloseableByESC";

        public static class Items{
            public const string TASK = "Task";
        }

        public static class Grounds{
            public const string DIRT = "Dirt";
            public const string STONE = "Stone";
            public const string WATER = "Water";
            public const string WOOD = "Wood";
        }

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
                public const string TREE_FALLING = "Sounds/SFX/tree_falling";
                public const string PAGE_FLIP = "Sounds/SFX/page_flip";
                public const string COUGH = "Sounds/SFX/cough";
            }
            public static class MUSIC {
                public const string HOME = "Sounds/Music/homescene";
                public const string STORY1 = "Sounds/Music/entrance";
            }
        }
    }

    public static class Labels {

        public const string PASSWORDQUIZ_MINIGAME = "minigame_passwordquiz";
        public const string SPOTS_MINIGAME = "minigame_clickthespots";
        public const string CLICKRUSH_MINIGAME = "minigame_clickrush";
        public const string PAINTER_MINIGAME = "minigame_painter";

        
        public const string MEMORY_MINIGAME = "Memory";
        public const string MATH_MINIGAME = "Math";
    }

}
