﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using ELS.Sirens;
using ELS.Sirens.Tones;

namespace ELS.Sirens
{
    public delegate void StateChangedHandler(Tone tone);

    /// <summary>
    /// Has diffrent tones
    /// </summary>
    public class Siren
    {
        public readonly Vehicle _vehicle;
        public event StateChangedHandler Statechanged;

        struct tones
        {
            public Tone horn;
            public Tone tone1;
            public Tone tone2;
            public Tone tone3;
            public Tone tone4;
        }
        tones _tones;
        public Siren(Vehicle vehicle)
        {
            _vehicle = vehicle;
            _tones = new tones();
            _tones.horn = new Tone("SIRENS_AIRHORN", _vehicle);
            _tones.tone1 = new Tone("", _vehicle);
            _tones.tone2 = new Tone("", _vehicle);
            _tones.tone3 = new Tone("", _vehicle);
            _tones.tone4 = new Tone("", _vehicle);
        }
        public void ticker()
        {
            if (Game.IsControlJustPressed(0, Control.MpTextChatTeam)&&Game.CurrentInputMode==InputMode.MouseAndKeyboard)
            {
                Game.DisableControlThisFrame(0, Control.MpTextChatTeam);
                Game.DisableControlThisFrame(2, Control.ScriptPadDown);
                _tones.horn.SetState(true);
                Statechanged(_tones.horn);
            }
            if ((Game.IsControlJustReleased(0, Control.MpTextChatTeam) && Game.CurrentInputMode == InputMode.MouseAndKeyboard) 
                ||  (Game.IsControlJustReleased(2, Control.ScriptPadDown) && Game.CurrentInputMode == InputMode.GamePad ))
            {
                Game.DisableControlThisFrame(0, Control.MpTextChatTeam);
                Game.DisableControlThisFrame(2, Control.ScriptPadDown);
                _tones.horn.SetState(false);
                Statechanged(_tones.horn);
            }
        }
        
    }
}