using EntityComponent;
using JumpKing;
using JumpKing.Level;
using JumpKing.Mods;
using JumpKing.Player;
using JumpKing.SaveThread;
using MomentumStopBlock.Behaviours;
using MomentumStopBlock.Blocks;
using MomentumStopBlock.Factories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace MomentumStopBlock
{
    [JumpKingMod("Zebra.MomentumStopBlock")]
    public static class ModEntry
    {
        private static string xmlFile;
        private static Dictionary<ulong, int> levelsScreens;

        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            //Debugger.Launch();

            LevelManager.RegisterBlockFactory(new FactoryMomentumStop());

            xmlFile = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}levels.xml";

            levelsScreens = new Dictionary<ulong, int>();
            if (File.Exists(xmlFile))
            {
                XmlReader reader = null;
                try
                {
                    reader = XmlReader.Create(xmlFile);
                    XElement root = XElement.Load(reader);
                    foreach (XElement element in root.Elements())
                    {
                        levelsScreens.Add(
                            ulong.Parse(new string(element.Name.LocalName.Skip(2).ToArray())),
                            int.Parse(element.Value));
                    }
                }
                finally
                {
                    reader?.Close();
                    reader?.Dispose();
                }
            }
        }

        [OnLevelStart]
        public static void OnLevelStart()
        {
            JKContentManager contentManager = Game1.instance.contentManager;
            if (contentManager.level == null)
            {
                return;
            }

            EntityManager entityManager = EntityManager.instance;
            PlayerEntity player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            BehaviourMomentumStop behaviourMomentumStop = new BehaviourMomentumStop();
            player.m_body.RegisterBlockBehaviour(typeof(BlockMomentumStop), behaviourMomentumStop);
            player.m_body.RegisterBlockBehaviour(typeof(BlockMomentumStopSolid), behaviourMomentumStop);

            BehaviourMomentumStopScreen behaviourMomentumStopScreen = new BehaviourMomentumStopScreen();
            player.m_body.RegisterBlockBehaviour(typeof(BlockMomentumStopScreen), behaviourMomentumStopScreen);
            player.m_body.RegisterBlockBehaviour(typeof(BlockMomentumStopScreenSolid), behaviourMomentumStopScreen);

            BehaviourMomentumStopScreen.LastStoppedScreen = -1;
            if (!SaveManager.instance.IsNewGame && levelsScreens.ContainsKey(contentManager.level.ID))
            {
                BehaviourMomentumStopScreen.LastStoppedScreen = levelsScreens[contentManager.level.ID];
            }
        }

        [OnLevelEnd]
        public static void OnLevelEnd()
        {
            JKContentManager contentManager = Game1.instance.contentManager;
            if (contentManager.level == null)
            {
                return;
            }
            levelsScreens[contentManager.level.ID] = BehaviourMomentumStopScreen.LastStoppedScreen;

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(xmlFile, settings);
                XElement element = new XElement("levels",
                        levelsScreens.Select(kv => new XElement($"id{kv.Key}", kv.Value)));
                element.Save(writer);
            }
            finally
            {
                writer?.Flush();
                writer?.Close();
                writer?.Dispose();
            }
        }
    }
}
