﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SongLoaderPlugin.OverrideClasses
{
    public class CustomBeatmapLevelPackSO : BeatmapLevelPackSO
    {

        public static CustomBeatmapLevelPackSO GetPack(CustomLevelCollectionSO beatmapLevelCollectionSO)
        {

            var newPack = CreateInstance<CustomBeatmapLevelPackSO>();
            newPack.Init(beatmapLevelCollectionSO);
            return newPack;
            //       var packs = Resources.FindObjectsOfTypeAll<BeatmapLevelPackSO>();

            //   return pack;


        }

        public static CustomBeatmapLevelPackSO GetPack(CustomLevelCollectionSO beatmapLevelCollectionSO, bool WipPack)
        {

            var newPack = CreateInstance<CustomBeatmapLevelPackSO>();
            newPack.Init(beatmapLevelCollectionSO, WipPack);
            return newPack;
            //       var packs = Resources.FindObjectsOfTypeAll<BeatmapLevelPackSO>();

            //   return pack;


        }
        private void Init(CustomLevelCollectionSO beatmapLevelCollectionSO, bool WipPack)
        {
            _isPackAlwaysOwned = true;
            if (!WipPack)
            {
                _packID = "CustomMaps";
                _packName = "Custom Maps";
                _coverImage = CustomUI.Utilities.UIUtilities.LoadSpriteFromResources("SongLoaderPlugin.Icons.CustomSongs.png");
            }
            else
            {
                _packID = "WIPMaps";
                _packName = "WIP Maps";
                _coverImage = CustomUI.Utilities.UIUtilities.LoadSpriteFromResources("SongLoaderPlugin.Icons.squek.png");
            }

            _beatmapLevelCollection = beatmapLevelCollectionSO;
        }

        private void Init(CustomLevelCollectionSO beatmapLevelCollectionSO)
        {
            _isPackAlwaysOwned = true;
            _packID = "CustomMaps";
            _packName = "Custom Maps";
            _coverImage = CustomUI.Utilities.UIUtilities.LoadSpriteFromResources("SongLoaderPlugin.Icons.CustomSongs.png");


            _beatmapLevelCollection = beatmapLevelCollectionSO;
        }
        public void AddToPack(CustomLevelCollectionSO beatmapLevelCollectionSO)
        {
            var levelcollection = this.beatmapLevelCollection as CustomLevelCollectionSO;
            foreach (BeatmapLevelSO a in levelcollection._levelList)
            {
                var customlevel = a as CustomLevel;
                if (!beatmapLevelCollectionSO._levelList.Contains(a))
                    beatmapLevelCollectionSO.AddCustomLevel(a as CustomLevel);
            }
            _beatmapLevelCollection = beatmapLevelCollectionSO;

        }
        public void ReplaceLevels(CustomLevelCollectionSO customLevelCollection)
        {
            _beatmapLevelCollection = customLevelCollection;
        }
    }
}
