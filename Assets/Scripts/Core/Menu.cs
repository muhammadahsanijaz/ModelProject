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
        [SerializeField] private Garage _garage;



        protected override GameContext PrepareContext()
        {
            var context = base.PrepareContext();

            context.Garage = _garage;
            

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