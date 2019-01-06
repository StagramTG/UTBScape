using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Gameplay
{
    public class TeamDescriptor
    {
        CharacterSpecies species;
        Team.Type type;
    }

    public static class GameInitData
    {
        public static List<TeamDescriptor> teamsDescriptions;

        // Reset data before configure another game
        public static void Reset()
        {
            teamsDescriptions = new List<TeamDescriptor>();
        }
    }
}
