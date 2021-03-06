//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Articy.The_Key_Of_The_Nile.GlobalVariables
{
    
    
    [Articy.Unity.ArticyCodeGenerationHashAttribute(637618501735103182)]
    public class ArticyScriptFragments : BaseScriptFragments, ISerializationCallbackReceiver
    {
        
        #region Fields
        private System.Collections.Generic.Dictionary<uint, System.Func<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider, bool>> Conditions = new System.Collections.Generic.Dictionary<uint, System.Func<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider, bool>>();
        
        private System.Collections.Generic.Dictionary<uint, System.Action<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider>> Instructions = new System.Collections.Generic.Dictionary<uint, System.Action<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider>>();
        #endregion
        
        #region Script fragments
        /// <summary>
        /// ObjectID: 0x100000000000155
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928277?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000155Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.temple_object_states.antechamber_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000053D
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929277?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000053DText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if no other glyphs have been activated.
true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000053E
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929278?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x10000000000053EText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Opens the Air Puzzle Room door.
aGlobalVariablesState.glyph_states.air_glyph_activated = true;
aGlobalVariablesState.temple_object_states.air_door_open = true
;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000544
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929284?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000544Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the Air Puzzle Room has been solved.
true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000545
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929285?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000545Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Opens the Water Puzzle Room door and closes the Air Puzzle Room door.
aGlobalVariablesState.glyph_states.water_glyph_activated = true;
aGlobalVariablesState.temple_object_states.water_door_open = true;
aGlobalVariablesState.temple_object_states.air_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000054B
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929291?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000054BText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the Water Puzzle Room has been solved.
true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000054C
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929292?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x10000000000054CText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Opens the Earth Puzzle Room door and closes the Water Puzzle Room door.
aGlobalVariablesState.glyph_states.earth_glyph_activated = true;
aGlobalVariablesState.temple_object_states.earth_door_open = true;
aGlobalVariablesState.temple_object_states.water_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000552
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929298?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000552Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the Earth Puzzle Room has been solved.
true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000553
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929299?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000553Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Opens the Fire Puzzle Room door and closes the Earth Puzzle Room door.
aGlobalVariablesState.glyph_states.fire_glyph_activated = true;
aGlobalVariablesState.temple_object_states.fire_door_open = true;
aGlobalVariablesState.temple_object_states.earth_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000055D
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929309?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000055DText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the Fire Puzzle Room has been solved.
true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000055E
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929310?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x10000000000055EText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Opens the Ankh Room door and closes the Fire Puzzle Room door.
aGlobalVariablesState.glyph_states.all_glyphs_activated = true;
aGlobalVariablesState.temple_object_states.ankh_door_open = true;
aGlobalVariablesState.temple_object_states.fire_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000524
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929252?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000524Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the air glyph is activated. No longer available after the water glyph is activated.

aGlobalVariablesState.temple_object_states.air_door_open == true && aGlobalVariablesState.puzzle_states.air_puzzle_solved == false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000052F
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929263?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000052FText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the water glyph is activated. No longer available after the earth glyph is activated.
aGlobalVariablesState.temple_object_states.water_door_open == true && aGlobalVariablesState.puzzle_states.water_puzzle_solved == false
;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000057D
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929341?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000057DText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the earth glyph is activated. No longer available after the fire glyph is activated.
aGlobalVariablesState.temple_object_states.earth_door_open == true && aGlobalVariablesState.puzzle_states.earth_puzzle_solved == false;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000585
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929349?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000585Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if the fire glyph is activated. No longer available after the Ankh glyph is activated.
aGlobalVariablesState.temple_object_states.fire_door_open == true &&
aGlobalVariablesState.puzzle_states.fire_puzzle_solved == false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000058D
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929357?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000058DText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Only available if all four elemental puzzle rooms have been solved.
aGlobalVariablesState.temple_object_states.ankh_door_open == true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000529
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929257?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000529Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Player cannot enter until the corresponding glyph is active
true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000167
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928295?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000167Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Player cannot return to the nexus until the puzzle is solved.;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000776
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929846?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000776Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.puzzle_states.air_puzzle_solved = true;
aGlobalVariablesState.temple_object_states.air_door_open = true;
        }
        
        /// <summary>
        /// ObjectID: 0x1000000000008D4
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930196?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x1000000000008D4Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.temple_object_states.air_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000016C
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928300?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000016CText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Player cannot enter until the corresponding glyph is active
true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000016D
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928301?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x10000000000016DText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Player cannot return to the nexus until the puzzle is solved.;
        }
        
        /// <summary>
        /// ObjectID: 0x1000000000008EC
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930220?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x1000000000008ECText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.temple_object_states.water_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x1000000000008F4
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930228?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x1000000000008F4Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.puzzle_states.water_puzzle_solved = true;
aGlobalVariablesState.temple_object_states.water_door_open = true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000018E
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928334?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000018EText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Player cannot enter until the corresponding glyph is active
true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000173
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928307?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000173Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Player cannot return to the nexus until the puzzle is solved.;
        }
        
        /// <summary>
        /// ObjectID: 0x1000000000008FD
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930237?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x1000000000008FDText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.temple_object_states.earth_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000904
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930244?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000904Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.puzzle_states.earth_puzzle_solved = true;
aGlobalVariablesState.temple_object_states.earth_door_open = true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000191
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928337?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000191Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Player cannot enter until the corresponding glyph is active
true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000179
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928313?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000179Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //Player cannot return to the nexus until the puzzle is solved.;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000090F
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930255?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x10000000000090FText(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.temple_object_states.fire_door_open = false;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000916
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037930262?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000916Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            aGlobalVariablesState.puzzle_states.fire_puzzle_solved = true;
aGlobalVariablesState.temple_object_states.fire_door_open = true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000193
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928339?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000193Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return // Player cannot enter until activating the pedestal after all four glyphs are activated and all four puzzle rooms have been solved.

true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000250
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037928528?pane=selected&amp;tab=current
        /// </summary>
        public void Script_0x100000000000250Text(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            //The end.;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000073B
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929787?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000073BExpression(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return aGlobalVariablesState.glyph_states.air_glyph_activated == false && aGlobalVariablesState.puzzle_states.air_puzzle_solved == false;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000074B
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929803?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000074BExpression(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return aGlobalVariablesState.glyph_states.water_glyph_activated == false &&
aGlobalVariablesState.puzzle_states.air_puzzle_solved == true ;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000754
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929812?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000754Expression(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return aGlobalVariablesState.glyph_states.earth_glyph_activated == false && aGlobalVariablesState.puzzle_states.water_puzzle_solved == true;
        }
        
        /// <summary>
        /// ObjectID: 0x10000000000075B
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929819?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x10000000000075BExpression(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return aGlobalVariablesState.glyph_states.fire_glyph_activated == false && aGlobalVariablesState.puzzle_states.earth_puzzle_solved == true;
        }
        
        /// <summary>
        /// ObjectID: 0x100000000000762
        /// Articy Object ref: articy://localhost/view/5e6ae696-800c-4397-98bf-318520f0d5b6/72057594037929826?pane=selected&amp;tab=current
        /// </summary>
        public bool Script_0x100000000000762Expression(ArticyGlobalVariables aGlobalVariablesState, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            return aGlobalVariablesState.glyph_states.all_glyphs_activated == false && aGlobalVariablesState.puzzle_states.fire_puzzle_solved == true
;
        }
        #endregion
        
        #region Unity serialization
        public override void OnBeforeSerialize()
        {
        }
        
        public override void OnAfterDeserialize()
        {
            Conditions = new System.Collections.Generic.Dictionary<uint, System.Func<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider, bool>>();
            Instructions = new System.Collections.Generic.Dictionary<uint, System.Action<ArticyGlobalVariables, Articy.Unity.IBaseScriptMethodProvider>>();
            Instructions.Add(1u, this.Script_0x100000000000155Text);
            Conditions.Add(2u, this.Script_0x10000000000053DText);
            Instructions.Add(3u, this.Script_0x10000000000053EText);
            Conditions.Add(4u, this.Script_0x100000000000544Text);
            Instructions.Add(5u, this.Script_0x100000000000545Text);
            Conditions.Add(6u, this.Script_0x10000000000054BText);
            Instructions.Add(7u, this.Script_0x10000000000054CText);
            Conditions.Add(8u, this.Script_0x100000000000552Text);
            Instructions.Add(9u, this.Script_0x100000000000553Text);
            Conditions.Add(10u, this.Script_0x10000000000055DText);
            Instructions.Add(11u, this.Script_0x10000000000055EText);
            Conditions.Add(12u, this.Script_0x100000000000524Text);
            Conditions.Add(13u, this.Script_0x10000000000052FText);
            Conditions.Add(14u, this.Script_0x10000000000057DText);
            Conditions.Add(15u, this.Script_0x100000000000585Text);
            Conditions.Add(16u, this.Script_0x10000000000058DText);
            Conditions.Add(17u, this.Script_0x100000000000529Text);
            Instructions.Add(18u, this.Script_0x100000000000167Text);
            Instructions.Add(19u, this.Script_0x100000000000776Text);
            Instructions.Add(20u, this.Script_0x1000000000008D4Text);
            Conditions.Add(21u, this.Script_0x10000000000016CText);
            Instructions.Add(22u, this.Script_0x10000000000016DText);
            Instructions.Add(23u, this.Script_0x1000000000008ECText);
            Instructions.Add(24u, this.Script_0x1000000000008F4Text);
            Conditions.Add(25u, this.Script_0x10000000000018EText);
            Instructions.Add(26u, this.Script_0x100000000000173Text);
            Instructions.Add(27u, this.Script_0x1000000000008FDText);
            Instructions.Add(28u, this.Script_0x100000000000904Text);
            Conditions.Add(29u, this.Script_0x100000000000191Text);
            Instructions.Add(30u, this.Script_0x100000000000179Text);
            Instructions.Add(31u, this.Script_0x10000000000090FText);
            Instructions.Add(32u, this.Script_0x100000000000916Text);
            Conditions.Add(33u, this.Script_0x100000000000193Text);
            Instructions.Add(34u, this.Script_0x100000000000250Text);
            Conditions.Add(35u, this.Script_0x10000000000073BExpression);
            Conditions.Add(36u, this.Script_0x10000000000074BExpression);
            Conditions.Add(37u, this.Script_0x100000000000754Expression);
            Conditions.Add(38u, this.Script_0x10000000000075BExpression);
            Conditions.Add(39u, this.Script_0x100000000000762Expression);
        }
        #endregion
        
        #region Script execution
        public override void CallInstruction(Articy.Unity.Interfaces.IGlobalVariables aGlobalVariables, uint aHandlerId, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            if ((Instructions.ContainsKey(aHandlerId) == false))
            {
                return;
            }
            if ((aMethodProvider != null))
            {
                aMethodProvider.IsCalledInForecast = aGlobalVariables.IsInShadowState;
            }
            Instructions[aHandlerId].Invoke(((ArticyGlobalVariables)(aGlobalVariables)), aMethodProvider);
        }
        
        public override bool CallCondition(Articy.Unity.Interfaces.IGlobalVariables aGlobalVariables, uint aHandlerId, Articy.Unity.IBaseScriptMethodProvider aMethodProvider)
        {
            if ((Conditions.ContainsKey(aHandlerId) == false))
            {
                return true;
            }
            if ((aMethodProvider != null))
            {
                aMethodProvider.IsCalledInForecast = aGlobalVariables.IsInShadowState;
            }
            return Conditions[aHandlerId].Invoke(((ArticyGlobalVariables)(aGlobalVariables)), aMethodProvider);
        }
        #endregion
    }
}
