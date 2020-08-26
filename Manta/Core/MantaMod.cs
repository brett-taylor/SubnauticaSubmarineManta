using System.Collections.Generic;
using SMLHelper.V2.Assets;
using UnityEngine;
using Manta.Core.Factory;
using SMLHelper.V2.Crafting;

namespace Manta.Core
{
    /**
     * The Mantaaaaaaaaa
     */
    public class MantaMod : Craftable
    {
        public static readonly string UNIQUE_ID = "SubmarineManta";
        public static readonly string NAME = "Manta";
        public static readonly string DESCRIPTION = "A high-speed, average-armour industrial resource collecting submarine.";

        public static readonly TechType MANTA_TECH_TYPE = new MantaMod().TechType;
        public override string AssetsFolder => System.IO.Path.Combine(EntryPoint.MOD_FOLDER_LOCATION, "Assets");
        public override string IconFileName => "MantaIcon.png";

        public MantaMod() : base(UNIQUE_ID, NAME, DESCRIPTION)
        {
        }

        public override GameObject GetGameObject()
        {
            return MantaFactory.CreateManta();
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Gold, 1)    
                }
            };
        }
    }
}
