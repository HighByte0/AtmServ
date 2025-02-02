using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ComponentState")]
    public class ComponentState
    {
        public ComponentState()
        {
        }

        public ComponentState(int component_Id, string description, int stateComponent_Id)
        {
            Component_Id = component_Id;
            Description = description;
            StateComponent_Id = stateComponent_Id;
        }

        [Key, Column(Order = 0)]
        public int State_Id { get; set; }
        [ForeignKey("State_Id")]
        public virtual State State { get; set; }

        [Key, Column(Order = 1)]
        public int Component_Id { get; set; }
        [ForeignKey("Component_Id")]
        public virtual Component Component { get; set; }


        //StateComponent_Id =1 OK , =2 KO, =3 Warning
        public int StateComponent_Id { get; set; }

        public string Description { get; set; }

        public DateTime? LastDate { get; set; }

        public virtual ICollection<StateFieldInt> IntFields { get; set; }
        public virtual ICollection<StateFieldStr> StrFields { get; set; }

    }
    [Table("StateFieldInt")]
    public class StateFieldInt
    {
        [Key]
        public int ComponentState_Id { get; set; }
        public virtual ComponentState ComponentState { get; set; }

        public string Name { get; set; }
        public int Value { get; set; }
        public StateFieldInt(string _name, int _value) { Name = _name; Value = _value; }
        public StateFieldInt() { }

    }
    [Table("StateFieldStr")]
    public class StateFieldStr
    {
        [Key]
        public int ComponentState_Id { get; set; }
        public virtual ComponentState ComponentState { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public StateFieldStr(string _name, string _value) { Name = _name; Value = _value; }
        public StateFieldStr() { }

    }

}
