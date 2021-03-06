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
using UnityEngine;


namespace Articy.The_Key_Of_The_Nile.GlobalVariables
{
    
    
    [Serializable()]
    [CreateAssetMenu(fileName="ArticyGlobalVariables", menuName="Create GlobalVariables", order=620)]
    public class ArticyGlobalVariables : BaseGlobalVariables
    {
        
        [SerializeField()]
        [HideInInspector()]
        private glyph_states mGlyph_states = new glyph_states();
        
        [SerializeField()]
        [HideInInspector()]
        private puzzle_states mPuzzle_states = new puzzle_states();
        
        [SerializeField()]
        [HideInInspector()]
        private temple_object_states mTemple_object_states = new temple_object_states();
        
        [SerializeField()]
        [HideInInspector()]
        private powers mPowers = new powers();
        
        #region Initialize static VariableName set
        static ArticyGlobalVariables()
        {
            variableNames.Add("glyph_states.air_glyph_activated");
            variableNames.Add("glyph_states.fire_glyph_activated");
            variableNames.Add("glyph_states.water_glyph_activated");
            variableNames.Add("glyph_states.earth_glyph_activated");
            variableNames.Add("glyph_states.all_glyphs_activated");
            variableNames.Add("puzzle_states.air_puzzle_solved");
            variableNames.Add("puzzle_states.water_puzzle_solved");
            variableNames.Add("puzzle_states.earth_puzzle_solved");
            variableNames.Add("puzzle_states.fire_puzzle_solved");
            variableNames.Add("temple_object_states.ankh_door_open");
            variableNames.Add("temple_object_states.air_door_open");
            variableNames.Add("temple_object_states.water_door_open");
            variableNames.Add("temple_object_states.earth_door_open");
            variableNames.Add("temple_object_states.fire_door_open");
            variableNames.Add("temple_object_states.antechamber_door_open");
            variableNames.Add("temple_object_states.ankh_recovered");
            variableNames.Add("temple_object_states.ankh_destroyed");
            variableNames.Add("powers.air_power");
            variableNames.Add("powers.water_power");
            variableNames.Add("powers.fire_power");
            variableNames.Add("powers.earth_power");
        }
        #endregion
        
        public glyph_states glyph_states
        {
            get
            {
                return mGlyph_states;
            }
        }
        
        public puzzle_states puzzle_states
        {
            get
            {
                return mPuzzle_states;
            }
        }
        
        public temple_object_states temple_object_states
        {
            get
            {
                return mTemple_object_states;
            }
        }
        
        public powers powers
        {
            get
            {
                return mPowers;
            }
        }
        
        public static ArticyGlobalVariables Default
        {
            get
            {
                return ((ArticyGlobalVariables)(Articy.Unity.ArticyDatabase.DefaultGlobalVariables));
            }
        }
        
        public override void Init()
        {
            glyph_states.RegisterVariables(this);
            puzzle_states.RegisterVariables(this);
            temple_object_states.RegisterVariables(this);
            powers.RegisterVariables(this);
        }
        
        public static ArticyGlobalVariables CreateGlobalVariablesClone()
        {
            return Articy.Unity.BaseGlobalVariables.CreateGlobalVariablesClone<ArticyGlobalVariables>();
        }
    }
}
