using HC.Resource;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace HC.Game
{
    public class FurnitureManager
    {
        //bg
        static SpriteRenderer floor;
        static SpriteRenderer sky;
        static SpriteRenderer wall;
        static SpriteRenderer wall_Partition;

        //restaurant
        static SpriteRenderer board;
        static SpriteRenderer table1;
        static SpriteRenderer table2;
        static SpriteRenderer table3;
        static SpriteRenderer table4;
        static SpriteRenderer table5;
        static SpriteRenderer table6;
        static SpriteRenderer selfcorner;
        static SpriteRenderer gate;
        static SpriteRenderer drinking;
        static SpriteRenderer counter;

        //kitchen
        static SpriteRenderer carpet;
        static SpriteRenderer window;
        static SpriteRenderer sink;
        static SpriteRenderer shelf;
        static SpriteRenderer refrigerator;
        static SpriteRenderer plants;
        static SpriteRenderer oven_01;
        static SpriteRenderer oven_02;
        static SpriteRenderer oven_03;
        static SpriteRenderer oven_04;
        static SpriteRenderer oven_05;
        static SpriteRenderer oven_06;

        //terrace
        static SpriteRenderer cushion_01;
        static SpriteRenderer cushion_02;
        static SpriteRenderer cushion_03;
        static SpriteRenderer cushion_04;

        public static void FurnitureInit()
        {
            FurnitureObjectSetting();
            FurnitureDataSetting(new TempFurnitureInfo());
        }

        private static void FurnitureObjectSetting()
        {
            Transform BG = GameObject.Find("BG").GetComponent<Transform>();

            floor = BG.Find("floor").GetComponent<SpriteRenderer>();
            sky = BG.Find("sky").GetComponent<SpriteRenderer>();
            wall = BG.Find("wall").GetComponent<SpriteRenderer>();
            wall_Partition = BG.Find("wall_b").GetComponent<SpriteRenderer>();

            Transform restaurnat = BG.Find("BG_Restaurant");

            board = restaurnat.Find("board").GetComponent<SpriteRenderer>();
            table1 = restaurnat.Find("table_01").GetComponent<SpriteRenderer>();
            table2 = restaurnat.Find("table_02").GetComponent<SpriteRenderer>();
            table3 = restaurnat.Find("table_03").GetComponent<SpriteRenderer>();
            table4 = restaurnat.Find("table_04").GetComponent<SpriteRenderer>();
            table5 = restaurnat.Find("table_05").GetComponent<SpriteRenderer>();
            table6 = restaurnat.Find("table_06").GetComponent<SpriteRenderer>();
            selfcorner = restaurnat.Find("selfcorner").GetComponent<SpriteRenderer>();
            gate = restaurnat.Find("gate").GetComponent<SpriteRenderer>();
            drinking = restaurnat.Find("drinking").GetComponent<SpriteRenderer>();
            counter = restaurnat.Find("counter").GetComponent<SpriteRenderer>();

            Transform kitchen = BG.Find("BG_Kitchen");

            carpet = kitchen.Find("carpet").GetComponent<SpriteRenderer>();
            window = kitchen.Find("window").GetComponent<SpriteRenderer>();
            sink = kitchen.Find("sink").GetComponent<SpriteRenderer>();
            shelf = kitchen.Find("shelf").GetComponent<SpriteRenderer>();
            refrigerator = kitchen.Find("refrigerator").GetComponent<SpriteRenderer>();
            plants = kitchen.Find("plants").GetComponent<SpriteRenderer>();
            oven_01 = kitchen.Find("oven_01").GetComponent<SpriteRenderer>();
            oven_02 = kitchen.Find("oven_02").GetComponent<SpriteRenderer>();
            oven_03 = kitchen.Find("oven_03").GetComponent<SpriteRenderer>();
            oven_04 = kitchen.Find("oven_04").GetComponent<SpriteRenderer>();
            oven_05 = kitchen.Find("oven_05").GetComponent<SpriteRenderer>();
            oven_06 = kitchen.Find("oven_06").GetComponent<SpriteRenderer>();

            Transform terrace = BG.Find("BG_Terrace");

            cushion_01 = terrace.Find("cushion_01").GetComponent<SpriteRenderer>();
            cushion_02 = terrace.Find("cushion_02").GetComponent<SpriteRenderer>();
            cushion_03 = terrace.Find("cushion_03").GetComponent<SpriteRenderer>();
            cushion_04 = terrace.Find("cushion_04").GetComponent<SpriteRenderer>();

        }

        private static async void FurnitureDataSetting(TempFurnitureInfo info)
        {
            floor.sprite = await LoadAddressableManager.LoadImage_Furniture(info.floor);
            sky.sprite = await LoadAddressableManager.LoadImage_Furniture(info.sky);
            wall.sprite = await LoadAddressableManager.LoadImage_Furniture(info.wall);
            board.sprite = await LoadAddressableManager.LoadImage_Furniture(info.board);
            table1.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table1);
            table2.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table2);
            table3.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table3);
            table4.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table4);
            table5.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table5);
            table6.sprite = await LoadAddressableManager.LoadImage_Furniture(info.table6);
            selfcorner.sprite = await LoadAddressableManager.LoadImage_Furniture(info.selfcorner);
            gate.sprite = await LoadAddressableManager.LoadImage_Furniture(info.gate);
            drinking.sprite = await LoadAddressableManager.LoadImage_Furniture(info.drinking);
            counter.sprite = await LoadAddressableManager.LoadImage_Furniture(info.counter);
            carpet.sprite = await LoadAddressableManager.LoadImage_Furniture(info.carpet);
            window.sprite = await LoadAddressableManager.LoadImage_Furniture(info.window);
            sink.sprite = await LoadAddressableManager.LoadImage_Furniture(info.sink);
            shelf.sprite = await LoadAddressableManager.LoadImage_Furniture(info.shelf);
            refrigerator.sprite = await LoadAddressableManager.LoadImage_Furniture(info.refrigerator);
            plants.sprite = await LoadAddressableManager.LoadImage_Furniture(info.plants);
            oven_01.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_01);
            oven_02.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_02);
            oven_03.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_03);
            oven_04.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_04);
            oven_05.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_05);
            oven_06.sprite = await LoadAddressableManager.LoadImage_Furniture(info.oven_06);
            cushion_01.sprite = await LoadAddressableManager.LoadImage_Furniture(info.cushion_01);
            cushion_02.sprite = await LoadAddressableManager.LoadImage_Furniture(info.cushion_02);
            cushion_03.sprite = await LoadAddressableManager.LoadImage_Furniture(info.cushion_03);
            cushion_04.sprite = await LoadAddressableManager.LoadImage_Furniture(info.cushion_04);
        }
    }

    class TempFurnitureInfo
    {
        public string floor = "basica_floor_001";
        public string sky = "basica_sky_001";
        public string wall = "basica_walla_001";
        public string wall_b = "basica_wallb_001";
        public string board = "basica_board_001";
        public string table1 = "basica_table_001";
        public string table2 = "basicb_table_002";
        public string table3 = "clover_table_003";
        public string table4 = "";
        public string table5 = "";
        public string table6 = "";
        public string selfcorner = "basica_selfcorner_001";
        public string gate = "basica_gate_001";
        public string drinking = "basica_drinking_001";
        public string counter = "basica_counter_001";
        public string carpet = "basica_carpet_001";
        public string window = "basica_window_001";
        public string sink = "basica_sink_001";
        public string shelf = "basica_shelf_001";
        public string refrigerator = "basica_refrigerator_001";
        public string plants = "basica_plants_001";
        public string oven_01 = "basica_oven_001";
        public string oven_02 = "basicb_oven_002";
        public string oven_03 = "clover_oven_003";
        public string oven_04 = "";
        public string oven_05 = "";
        public string oven_06 = "";
        public string cushion_01 = "basica_cushion_001";
        public string cushion_02 = "basica_cushion_001";
        public string cushion_03 = "basicb_cushion_002";
        public string cushion_04 = "clover_cushion_003";
    }
}

