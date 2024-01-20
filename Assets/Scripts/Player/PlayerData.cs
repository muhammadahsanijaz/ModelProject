using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MoonKart
{
    public class AutoVaultState
    {
        public int slotNumber;
        public string cardName;
        public string timeUTC;

        public AutoVaultState(int slotNumber, string cardName, string timeUTC)
        {
            this.slotNumber = slotNumber;
            this.cardName = cardName;
            this.timeUTC = timeUTC;
        }
    }

    /// <summary>
    /// PlayerData Class that have Player data , it is inherit for Player interface 
    /// </summary>
    [Serializable]
    public class PlayerData : IPlayer
    {
        // PUBLIC MEMBERS

        public string UserID => _userID;

        public string Nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
                IsDirty = true;
            }
        }

        public string CarPresetIndex
        {
            get { return _carPresetIndex; }
            set
            {
                _carPresetIndex = value;
                IsDirty = true;
            }
        }



        public int Map
        {
            get { return _map; }
            set
            {
                _map = value;
                IsDirty = true;
            }
        }

        public bool isReady
        {
            get { return _isReady; }
            set
            {
                _isReady = value;
                IsDirty = true;
            }
        }


        public bool IsDirty { get; private set; } // Check => is Player properties Update or Not

        // PRIVATE MEMBERS

        [SerializeField] private string _userID;
        [SerializeField] private string _nickname;
        [SerializeField] private string _carPresetIndex;
        [SerializeField] private bool _isReady;
        [SerializeField] private int _map;
        private List<AutoVaultState> _autoVaultState ;

        // CONSTRUCTORS

        public PlayerData(string userID)
        {
            _userID = userID;
            
        }

        // PUBLIC METHODS

        public void ClearDirty()
        {
            IsDirty = false;
        }
    }
}