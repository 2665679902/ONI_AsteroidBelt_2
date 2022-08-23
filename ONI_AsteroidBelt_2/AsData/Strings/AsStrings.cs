using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.Strings
{
    /// <summary>
    /// 存放的要用的字符串
    /// </summary>
    internal static class AsStrings
    {
        /// <summary>
        /// 用于标记要翻译的对象
        /// </summary>
        /// <param name="inPut">翻译的对象</param>
        /// <returns>其本身</returns>
        private static string _(string inPut) { return inPut; }

        public static class Test
        {
            public static AsString Empty = _("Empty");

            public static AsString TestString = _("Test");

        }

        public static class AsWorldString
        {
            public static class ClusterString
            {
                public static AsString Name = _("AsteroidBelt");

                public static AsString Discribe = _(" broken...... hopeless......");
            }

            public static class SandstoneString
            {
                public static AsString Name = _("Sandstone");

                public static AsString Discribe = _("A small crumbling mound. Remember to bring your ...... hatch?");

                public static AsString SelectDiscribe = _("You find some Sandstone");

            }

            public static class ForestString
            {
                public static AsString Name = _("Forest");

                public static AsString Discribe = _("A little forest");

                public static AsString SelectDiscribe = _("You find a piece of forest, and a pip");

            }

            public static class SwampString
            {
                public static AsString Name = _("Swamp");

                public static AsString Discribe = _("A sticky world");

                public static AsString SelectDiscribe = _("You find a Swamp");

            }

            public static class AquaticString
            {
                public static AsString Name = _("Aquatic");

                public static AsString Discribe = _("A loooooooot of water");

                public static AsString SelectDiscribe = _("You find a Swamp");

            }

            public static class BarrenString
            {
                public static AsString Name = _("Barren");

                public static AsString Discribe = _("Some Rock");

                public static AsString SelectDiscribe = _("You find a rock");
            }

            public static class FrozenString
            {
                public static AsString Name = _("Frozen");

                public static AsString Discribe = _("ice!");

                public static AsString SelectDiscribe = _("You find some ice");
            }

            public static class JungleString
            {
                public static AsString Name = _("Jungle");

                public static AsString Discribe = _("Chlorine and Hydrogen");

                public static AsString SelectDiscribe = _("You find a toxic ecosystem");
            }

            public static class MagmaString
            {
                public static AsString Name = _("Magma");

                public static AsString Discribe = _("Extremely Heat");

                public static AsString SelectDiscribe = _("You find some Magma");
            }

            public static class MarshString
            {
                public static AsString Name = _("Marsh");

                public static AsString Discribe = _("A marshy world");

                public static AsString SelectDiscribe = _("You find some Polluted Dirt");
            }

            public static class MooString
            {
                public static AsString Name = _("Moo");

                public static AsString Discribe = _("Has a Moo");

                public static AsString SelectDiscribe = _("You find a Moo");
            }

            public static class OceanString
            {
                public static AsString Name = _("Ocean");

                public static AsString Discribe = _("There is some salt water");

                public static AsString SelectDiscribe = _("You find some briny Sand");
            }

            public static class OilString
            {
                public static AsString Name = _("Oil");

                public static AsString Discribe = _("Oily");

                public static AsString SelectDiscribe = _("Oil！");
            }

            public static class RadioactiveString
            {
                public static AsString Name = _("Radioactive");

                public static AsString Discribe = _("A Radioactive ice");

                public static AsString SelectDiscribe = _("You find some volatile ice");
            }

            public static class RegolithString
            {
                public static AsString Name = _("Regolith");

                public static AsString Discribe = _("Where is my mouse?");

                public static AsString SelectDiscribe = _("You find a bounty of Regolith");
            }

            public static class RustString
            {
                public static AsString Name = _("Rust");

                public static AsString Discribe = _("A great deal of rust");

                public static AsString SelectDiscribe = _("You find some Rust");
            }

            public static class WastelandString
            {
                public static AsString Name = _("Wasteland");

                public static AsString Discribe = _("A lot of hot sand");

                public static AsString SelectDiscribe = _("You find some hot sand");
            }

        }

        public static class UI
        {
            public static class UGUI
            {
                public static class SelectWorldUI
                {
                    public static AsString Close = _("Close");

                    public static AsString StartNew = _("Start new");

                    public static AsString History = _("History");

                    public static AsString Current = _("Current");

                    public static AsString Load = _("Load");

                    public static AsString Nothing = _("nothing here");

                    public static AsString GetCoinText = _("Are you ready for the new world?\n\nMaybe the journey will not be very smooth, though...\n\nI get some<color=orange> LUCKY COIN </color>for you");

                    public static AsString GetCoinButtonText = _("Take it");

                    public static AsString SelectStartWorldText = _("when you look around......");

                    public static AsString SelectInnerWorldText = _("What's more,  when you look further ......");

                    public static AsString StartWorld = _("Start world");

                    public static AsString InnerWorld = _("Inner world");

                    public static AsString OuterWorlds = _("Outer worlds");

                    public static AsString CoinLast = _("Coin last");

                    public static AsString CoinTip = _("Tip: the coins will be converted into the same amount of gold");
                }

            }
        }

        public static class AsBuilding
        {
            public static class Habitat
            {
                public static AsString Name = _("Starship");

                public static AsString Description = _("cost huge");

                public static AsString Effect = _("huge");

            }

            public static class Nosecone
            {
                public static AsString Name = _("LowTech Nosecone");

                public static AsString Description = _("You need to pay more for early consumption");

                public static AsString Effect = _("It can work as well as the Basic one");

            }
        }
    }
}
