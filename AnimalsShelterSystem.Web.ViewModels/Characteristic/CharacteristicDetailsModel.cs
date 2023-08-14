using AnimalsShelterSystem.Web.ViewModels.Characteristic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterSystem.Web.ViewModels.Characteristic
{
    public class CharacteristicDetailsModel : ICharacteristicDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
