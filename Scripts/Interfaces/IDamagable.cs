using PadZex;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Scripts.Interfaces
{
    public interface IDamagable
    {
        public void Damage(Entity entity, float damage = 0);
    }
}