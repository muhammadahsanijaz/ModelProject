using System.Collections.Generic;
using MoonKart.UI;
using UnityEngine;

namespace MoonKart
{
    public enum GameMode
    {
        Single,
        Multiplayer,
        PresetMaking
    }

    public enum CardCallFrom
    {
        MyCard,
        Presets,
        Vault,
        CardMerge
        
    }

    public class Menu : Game
    {

        // PRIVATE MEMBERS
        [Header("Menu")] 
        [SerializeField] private Previews _preview;



        protected override GameContext PrepareContext()
        {
            var context = base.PrepareContext();

            context.Previews = _preview;
            

            return context;
        }

        protected override void AddServices(List<GameService> services)
        {
            base.AddServices(services);
        }
        
        protected override void OnActivate()
        {
            base.OnActivate();

            _context.Audio.PlayMusic();
        }
    }
}